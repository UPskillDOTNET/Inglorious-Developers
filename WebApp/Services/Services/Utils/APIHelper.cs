using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Services.Services.Utils
{
    public class APIHelper
    {
        private readonly HttpClient _clientFactory;
        public APIHelper (HttpClient clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> GetClientAsync(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/");
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);

        }

        public async Task<HttpResponseMessage> PostClientAsync(string url, StringContent content)
        {
            var client = new HttpClient();
            //var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44381/");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);
        }

        public async Task<HttpResponseMessage> PutClientAsync(string url, StringContent content)
        {
            var client = new HttpClient();
            //var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44381/");
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);
        }

        ////its gonna be used?
        public async Task<HttpResponseMessage> PayClientAsync(string url, StringContent content)
        {
            var client = new HttpClient();
            //var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44312/");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);
        }
    }
}
