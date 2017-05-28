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
        
        private readonly BoekLogica _boekLogica = new BoekLogica();
        private readonly GenreLogica _genreLogica = new GenreLogica();
        
        public async Task<List<Boek>> Get()
        {
            return  await _boekLogica.NeemAlleBoeken();
        }

        public async Task<Boek> Get(Int32 id)
        {
            return await _boekLogica.NeemBoek(id);
        }

        public Task<Boek> Post(Boek boek)
        {
            return _boekLogica.BewaarBoek(boek);
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
            return _boekLogica.WijzigBoek(boek);
        }

        public Task Delete(Int32 id)
        {
            return _boekLogica.VerwijderBoek(id);
        }
    }
}
