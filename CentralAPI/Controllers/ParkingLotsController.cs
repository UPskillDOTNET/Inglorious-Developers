using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {

        //private readonly string privateApiBaseUrl;
        //private readonly string publicApiBaseUrl;        
        //private readonly IConfiguration _configure;

        //public ParkingLotsController(IConfiguration configuration)
        //{
        //    _configure = configuration;
        //    privateApiBaseUrl = _configure.GetValue<string>("PrivateAPIBaseurl");
        //    publicApiBaseUrl = _configure.GetValue<string>("PublicAPIBaseurl");            
        //}


        //// PRIVATE PARKING LOTS


        ////[HttpGet]
        ////[Route("/centralapi/parkinglots/{route}")]
        ////public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingLotDTO>>> GetAllPrivateParkingLots(string route)
        ////{
        ////    var parkingLotsList = new List<PrivateParkAPI.DTO.ParkingLotDTO>();
        ////    using (var client = new HttpClient())
        ////    {
        ////        string endpoint = baseApiUrl + route + "/api/parkingspots";
        ////        var response = await client.GetAsync(endpoint);
        ////        response.EnsureSuccessStatusCode();
        ////        parkingLotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingLotDTO>>();
        ////    }
        ////    return parkingLotsList;
        ////}

        ////Get All Private Parking Lots
        //[HttpGet]
        //[Route("centralapi/privateparkinglots")]
        //public async Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ParkingLotDTO>>> GetAllPrivateParkingLots()
        //{
        //    var parkingLotsList = new List<PrivateParkAPI.DTO.ParkingLotDTO>();
        //    using (var client = new HttpClient())
        //    {
        //        string endpoint = privateApiBaseUrl + "/parkinglots";
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        parkingLotsList = await response.Content.ReadAsAsync<List<PrivateParkAPI.DTO.ParkingLotDTO>>();
        //    }
        //    return parkingLotsList;
        //}

        ////Get Specific Private Parking Lots
        //[HttpGet]
        //[Route("centralapi/privateparkinglots/{id}")]
        //public async Task<ActionResult<PrivateParkAPI.DTO.ParkingLotDTO>> GetSpecificPrivateParkingLot(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    PrivateParkAPI.DTO.ParkingLotDTO parkingLotDTO;
        //    using (var client = new HttpClient())
        //    {
        //        string endpoint = privateApiBaseUrl + "/parkinglots/" + id;
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        parkingLotDTO = await response.Content.ReadAsAsync<PrivateParkAPI.DTO.ParkingLotDTO>();
        //    }
        //    if (parkingLotDTO == null)
        //    {
        //        return NotFound();
        //    }
        //    return parkingLotDTO;
        //}

        ////Put Private Parking Lot
        //[HttpPost]
        //[Route("centralapi/privateparkinglots/{id}")]
        //public async Task<ActionResult<PrivateParkAPI.DTO.ParkingLotDTO>> PutPrivateParkingLot(int id, PrivateParkAPI.DTO.ParkingLotDTO parkingLotDTO)
        //{
        //    //[Bind("parkingLotID, name, companyOwner, location, capacity, openingTime, closingTime")] 
        //    if (id != parkingLotDTO.parkingLotID)
        //    {
        //        return NotFound();
        //    }
        //    using (var client = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(parkingLotDTO), Encoding.UTF8, "application/json");
        //        string endpoint = privateApiBaseUrl + "/parkinglots/" + id;
        //        var response = await client.PutAsync(endpoint, content);
        //    }
        //    return parkingLotDTO;
        //}

        ////[HttpPost]
        ////[Route("centralapi/privateparkinglots")]
        ////public async Task<ActionResult<PrivateParkAPI.DTO.ParkingLotDTO>> PostPrivateParkingLot([Bind("parkingLotID, name, companyOwner, location, capacity, openingTime, closingTime")] PrivateParkAPI.DTO.ParkingLotDTO parkingLotDTO)
        ////{
        ////    //if (parkingLotDTO.parkingLotID == null)
        ////    //{
        ////    //    return BadRequest();
        ////    //}
        ////    using (var client = new HttpClient())
        ////    {
        ////        StringContent content = new StringContent(JsonConvert.SerializeObject(parkingLotDTO), Encoding.UTF8, "application/json");
        ////        string endpoint = privateApiBaseUrl + "/parkinglots";
        ////        var response = await client.PostAsync(endpoint, content);
        ////    }
        ////    return parkingLotDTO;
        ////}

        ////PUBLIC PARKING LOTS

        ////Get All Public Parking Lots
        //[HttpGet]
        //[Route("centralapi/publicparkinglots")]
        //public async Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ParkingLotDTO>>> GetAllPublicParkingLots()
        //{
        //    var parkingLotsList = new List<PublicParkAPI.DTO.ParkingLotDTO>();
        //    using (var client = new HttpClient())
        //    {
        //        string endpoint = publicApiBaseUrl + "/testesParkingLots";
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        parkingLotsList = await response.Content.ReadAsAsync<List<PublicParkAPI.DTO.ParkingLotDTO>>();
        //    }
        //    return parkingLotsList;
        //}

        ////Get Specific Public Parking Lots
        //[HttpGet]
        //[Route("centralapi/publicparkinglots/{id}")]
        //public async Task<PublicParkAPI.DTO.ParkingLotDTO> GetSpecificPublicParkingLot(int? id)
        //{
        //    PublicParkAPI.DTO.ParkingLotDTO parkingSpotDTO;
        //    using (var client = new HttpClient())
        //    {
        //        string endpoint = publicApiBaseUrl + "/testesParkingLots/" + id;
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        parkingSpotDTO = await response.Content.ReadAsAsync<PublicParkAPI.DTO.ParkingLotDTO>();
        //    }
        //    return parkingSpotDTO;
        //}

        ////PUT Public Parking Lot
        //[HttpPost]
        //[Route("centralapi/publicparkinglots/{id}")]
        //public async Task<ActionResult<PublicParkAPI.DTO.ParkingLotDTO>> PutPublicParkingLot(int id, PublicParkAPI.DTO.ParkingLotDTO parkingLotDTO)
        //{
        //    //[Bind("parkingLotID, name, companyOwner, location, capacity, openingTime, closingTime")] 
        //    if (id != parkingLotDTO.parkingLotID)
        //    {
        //        return NotFound();
        //    }
        //    using (var client = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(parkingLotDTO), Encoding.UTF8, "application/json");
        //        string endpoint = publicApiBaseUrl + "/testesParkingLots/" + id;
        //        var response = await client.PutAsync(endpoint, content);
        //    }
        //    return parkingLotDTO;
        //}
    }
}
