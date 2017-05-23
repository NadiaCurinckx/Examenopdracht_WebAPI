﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Model
{
    [ServiceContract]
    public interface IGenreService
    {
        [OperationContract]
        Task<List<Genre>> NeemAlleGenres();

        [OperationContract]
        Task<Genre> GeefGenre(Int32 id);

        [OperationContract]
        Task<List<Genre>> GeefGenresVoorBoek(int id);

        [OperationContract]
        Task<int> KoppelGenresVoorBoek(int boekId, List<int> genreIds);

        /*
     [OperationContract]
     Task GenreOpslaan(Genre genre);

     [OperationContract]
     Task GenreWijzigen(Genre genre);

     [OperationContract]
     Task GenreVerwijderen(Int32 id);
     */
    }
}