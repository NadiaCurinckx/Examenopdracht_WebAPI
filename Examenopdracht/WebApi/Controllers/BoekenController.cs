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


        public Task<List<Boek>> Get()
        {
            return _logica.NeemAlleBoeken();
        }

        public async Task<Boek> Get(Int32 id)
        {
            var boeken = await _logica.NeemAlleBoeken();
            return boeken.SingleOrDefault(x => x.Id == id);
        }

        public Task Post(Boek boek)
        {
            return _logica.BewaarBoek(boek);
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
