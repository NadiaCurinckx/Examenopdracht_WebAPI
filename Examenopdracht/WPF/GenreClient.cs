using Model;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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

        public Task KoppelGenresVoorBoek(int boekId, List<int> geselecteerdeGenreIds)
        {
            var request = new RestRequest($"boeken/{boekId}/genres", Method.POST);
            request.AddJsonBody(geselecteerdeGenreIds);
            return _client.ExecuteTaskAsync(request);
        }

        public Task<List<Genre>> GeefGenresVoorBoek(int boekId)
        {
            var request = new RestRequest($"boeken/{boekId}/genres", Method.GET);
            return _client.GetTaskAsync<List<Genre>>(request);
        }
    }
}
