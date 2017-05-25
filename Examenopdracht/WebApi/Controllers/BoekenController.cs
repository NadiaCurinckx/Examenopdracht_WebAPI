using System.Web.Http;
using BL;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace WebApi.Controllers
{
    public class BoekenController : ApiController
    {
        
        private readonly BoekLogica _logica = new BoekLogica();
        private readonly GenreLogica _genreLogica = new GenreLogica();

        public async Task<List<Boek>> Get()
        {
            return  await _logica.NeemAlleBoeken();
        }

        public async Task<Boek> Get(Int32 id)
        {
            var boeken = await _logica.NeemAlleBoeken();
            return boeken.SingleOrDefault(x => x.Id == id);
        }

        public Task<Boek> Post(Boek boek)
        {
            return _logica.BewaarBoek(boek);
        }

        [HttpPost]
        [Route("boeken/{id}/genres")]
        public Task KoppelGenresVoorBoek(int id, List<int> genreIdsVoorBoek)
        {
            return _genreLogica.KoppelGenresVoorBoek(id, genreIdsVoorBoek);
        }

        [HttpGet]
        [Route("boeken/{id}/genres")]
        public Task<List<Genre>> GeefGenresVoorBoek(int id)
        {
            return _genreLogica.GeefGenresVoorBoek(id);
        }

        public Task Put(Int32 id, Boek boek)
        {
            return _logica.WijzigBoek(boek);
        }

        public Task Delete(Int32 id)
        {
            return _logica.VerwijderBoek(id);
        }

        //[HttpGet, Route("api/producten/totaleprijs")]
        //public Task<Decimal> BerekenTotalePrijs()
        //{
        //    return _logica.BerekenTotalePrijs();
        //}

        //[HttpGet, Route("api/producten/totaalaantal")]
        //public Task<Int32> BerekenTotaalAantalProducten()
        //{
        //    return _logica.BerekenTotaalAantalProducten();
        //}








    }
}
