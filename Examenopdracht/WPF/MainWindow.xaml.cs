﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //private readonly IBoekService _boekLogica;
        //private readonly IGenreService _genreLogica;

        private readonly BoekClient _boekClient = new BoekClient();
        private readonly GenreClient _genreClient = new GenreClient();

        public MainWindow()
        {
            InitializeComponent();

            //var genreChannelFactory = new ChannelFactory<IGenreService>(new BasicHttpBinding());
            //_genreLogica = genreChannelFactory.CreateChannel(new EndpointAddress("http://localhost:8054/GenreService.svc"));

            //var boekChannelFactory = new ChannelFactory<IBoekService>(new BasicHttpBinding());
            //_boekLogica = boekChannelFactory.CreateChannel(new EndpointAddress("http://localhost:8054/BoekService.svc"));

            ToonBoeken();
            ToonGenres();
        }




        private async void btnBoekToevoegen_Click(object sender, RoutedEventArgs e)
        {
            await BewaarBoek();
            ToonBoeken();
            MaakVeldenLeeg();
        }

        private async void btnBoekBewerken_Click(object sender, RoutedEventArgs e)
        {
            await EditeerBoek();
            ToonBoeken();
            MaakVeldenLeeg();
        }

        private async void btnBoekVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            await VerwijderBoek();
            ToonBoeken();
            MaakVeldenLeeg();
        }

        private async void lsbBoeken_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            await ToonGeselecteerdBoek();
        }




        public bool IsGeldigBoek()
        {
            if (string.IsNullOrEmpty(txtTitel.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtAuteur.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtAantalPaginas.Text))
            {
                return false;
            }

            try
            {
                Convert.ToInt32(txtAantalPaginas.Text);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task BewaarBoek()
        {
            if (IsGeldigBoek())
            {
                var boek = MaakBoekVanInvoerVelden();
                var geselecteerdeGenreIds = NeemGeselecteerdeGenres().Select(g => g.Id).ToList();
                var nieuwBoek = await _boekClient.BewaarBoek(boek);
                await _genreClient.KoppelGenresVoorBoek(nieuwBoek.Id, geselecteerdeGenreIds);
            }

            else
            {
                MessageBox.Show("Controleer of je alle velden correct hebt ingevuld.", "Fout tijdens bewaren van het boek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Genre> NeemGeselecteerdeGenres()
        {
            return lsbGenre.SelectedItems.Cast<Genre>().ToList();
        }
        public Genre NeemListBoxGenreObjectVanGenre(Genre genre)
        {
            return lsbGenre.Items.Cast<Genre>().Where(g => g.Id == genre.Id).FirstOrDefault();
        }

        public async void ToonBoeken()
        {
            lsbBoeken.Items.Clear();

            var boekenlijst = await _boekClient.NeemAlleBoeken();

            if (boekenlijst != null && boekenlijst.Any())
            {
                foreach (var boek in boekenlijst)
                {
                    lsbBoeken.Items.Add(boek);
                }
            }
            
        }

        public async void ToonGenres()
        {
            var genrelijst = await _genreClient.NeemAlleGenres();
            foreach (var genre in genrelijst)
            {
                lsbGenre.Items.Add(genre);
            }
        }
        public void VisualiseerGeselecteerdeGenres(List<Genre> genresTeSelecteren)
        {
            lsbGenre.SelectedItems.Clear();
            if(genresTeSelecteren == null || genresTeSelecteren.Count == 0)
            {
                return;
            }
            foreach(var genre in genresTeSelecteren)
            {                
                lsbGenre.SelectedItems.Add(NeemListBoxGenreObjectVanGenre(genre));
            }
        }

        public void MaakVeldenLeeg()
        {
            txtTitel.Text = "";
            txtAuteur.Text = "";
            txtAantalPaginas.Text = "";
            lsbGenre.SelectedItems.Clear();
        }

        public async Task VerwijderBoek()
        {
            Boek geselecteerdBoek = (Boek)lsbBoeken.SelectedItem;

            if (geselecteerdBoek != null)
            {
                await _boekClient.VerwijderBoek(geselecteerdBoek.Id);
            }

        }

        public async Task EditeerBoek()
        {
            if (IsGeldigBoek())
            {
                Boek geselecteerdBoek = (Boek)lsbBoeken.SelectedItem;

                if (geselecteerdBoek != null)
                {
                    var gewijzigdBoek = MaakBoekVanInvoerVelden();
                    var geselecteerdeGenreIds = NeemGeselecteerdeGenres().Select(g => g.Id).ToList();
                    gewijzigdBoek.Id = geselecteerdBoek.Id;
                    await _boekClient.WijzigBoek(gewijzigdBoek);
                    await _genreClient.KoppelGenresVoorBoek(gewijzigdBoek.Id, geselecteerdeGenreIds);
                }

                else
                {
                    MessageBox.Show("Het lijkt erop dat een andere collega het boek, dat u wenste te bewerken, heeft verwijderd.", "Fout tijdens bewerken van het boek.", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }


            else
            {
                MessageBox.Show("Controleer of je alle velden correct hebt ingevuld.", "Fout tijdens bewaren van het boek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task ToonGeselecteerdBoek()
        {
            Boek geselecteerdBoek = (Boek)lsbBoeken.SelectedItem;
            if (geselecteerdBoek != null)
            {
                // data ophalen
                geselecteerdBoek = await _boekClient.NeemBoek(geselecteerdBoek.Id);
                geselecteerdBoek.Genres = await _genreClient.GeefGenresVoorBoek(geselecteerdBoek.Id);

                // visualiseren
                txtTitel.Text = geselecteerdBoek.Titel;
                txtAuteur.Text = geselecteerdBoek.Auteur;
                txtAantalPaginas.Text = geselecteerdBoek.AantalPaginas.ToString();
                VisualiseerGeselecteerdeGenres(geselecteerdBoek.Genres?.ToList());
            }
            
        }

        private Boek MaakBoekVanInvoerVelden()
        {
            var boek = new Boek()
            {
                Titel = txtTitel.Text,
                Auteur = txtAuteur.Text,
                AantalPaginas = Convert.ToInt32(txtAantalPaginas.Text),
                Genres = null
            };

            return boek;
        }


    }
}