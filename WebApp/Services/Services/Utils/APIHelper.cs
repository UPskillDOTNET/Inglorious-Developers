﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace WebApp.Services.Services.Utils
{
    public class APIHelper
    {
        private readonly HttpClient _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected HttpContext HttpContext => _httpContextAccessor.HttpContext;
        public APIHelper (HttpClient clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
           
        }
    
        public async Task<HttpResponseMessage> GetClientAsync2(string url)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);

        }
        public async Task<HttpResponseMessage> GetClientAsync(string url)
        {
           
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://centralapi--parkaround.azurewebsites.net/");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.BaseAddress = new Uri("https://centralapi--parkaround.azurewebsites.net/");
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.BaseAddress = new Uri("https://centralapi--parkaround.azurewebsites.net/");
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            throw new HttpRequestException(response.ReasonPhrase);
        }
    }
}
