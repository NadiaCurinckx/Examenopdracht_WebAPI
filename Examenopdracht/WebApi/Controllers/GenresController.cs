using System.Web.Http;
using BL;
using Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    public class GenresController : ApiController
    {
        private readonly GenreLogica _logica = new GenreLogica();

        public Task<List<Genre>> Get()
        {
            return _logica.NeemAlleGenres();
        }
    }
}