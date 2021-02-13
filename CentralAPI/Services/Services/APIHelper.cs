﻿using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Services.IServices;
using CentralAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class ClientHelper
    {
        private readonly IParkingLotService _parkingLotService;

        public ClientHelper(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }


        public async Task<HttpResponseMessage> GetClientAsync(int id, string url)
        {
            var parkingLot = _parkingLotService.GetParkingLot(id).Result.Value; 
            var client = new HttpClient();
            client.BaseAddress = new Uri(parkingLot.myURL);
            var response = await client.GetAsync(url);  
            response.EnsureSuccessStatusCode();
            return response;
        }
        public async Task<HttpResponseMessage> PostClientAsync(int id, string url, StringContent content)
        {
            var parkingLot = _parkingLotService.GetParkingLot(id).Result.Value;
            var client = new HttpClient();
            client.BaseAddress = new Uri(parkingLot.myURL);
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> PutClientAsync(int id, string url, StringContent content)
        {
            var parkingLot = _parkingLotService.GetParkingLot(id).Result.Value;
            var client = new HttpClient();
            client.BaseAddress = new Uri(parkingLot.myURL);
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}