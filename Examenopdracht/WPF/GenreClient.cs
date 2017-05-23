using Model;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPF
{
    public  class GenreClient
    {
        private readonly RestClient _client = new RestClient("http://localhost:51672");

        public Task<List<Genre>> NeemAlleGenres()
        {
            var request = new RestRequest("genres", Method.GET);
            return _client.GetTaskAsync<List<Genre>>(request);
        }


    }
}
