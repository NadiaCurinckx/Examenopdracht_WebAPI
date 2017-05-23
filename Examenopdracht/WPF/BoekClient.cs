using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace WPF
{
    public class BoekClient
    {

        private readonly RestClient _client = new RestClient("http://localhost:51672/api");

        public Task<List<Boek>> NeemAlleBoeken()
        {
            var request = new RestRequest("boeken", Method.GET);
            return _client.GetTaskAsync<List<Boek>>(request);
        }

        //public Task<Boek> NeemBoek(Int32 code)
        //{

        //}

        ///*public Task BewaarBoek(Int32 code)
        //{

        //    }*/

        public Task BewaarBoek(Boek boek)
        {
            var request = new RestRequest("boeken", Method.POST);
            request.AddJsonBody(boek);
            return _client.ExecuteTaskAsync(request);
        }


        public Task WijzigBoek(Boek boek)
        {
            var request = new RestRequest("boeken/" + boek.Id, Method.PUT);
            request.AddJsonBody(boek);
            return _client.ExecuteTaskAsync(request);
        }

        public Task VerwijderBoek(Int32 id)
        {
            var request = new RestRequest("boeken/" + id, Method.DELETE);
            return _client.ExecuteTaskAsync(request);
        }




    }
}
