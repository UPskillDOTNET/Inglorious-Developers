using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;
using WebApp.Services.Services.Utils;

namespace WebApp.Services.Services
{
    public class WalletService:IWalletService
    {
        private readonly APIHelper _helper;

        public WalletService(APIHelper helper)
        {
            _helper = helper;
        }
        public async Task<ActionResult<WalletDTO>> GetUserWalletById(string id)
        {
            var response = await _helper.GetClientAsync("central/Wallets/balance/" + id);
            return await response.Content.ReadAsAsync<WalletDTO>();
        }
        public async Task<ActionResult<WalletDTO>> Deposit(string id, decimal value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            var response = await _helper.PutClientAsync("central/Wallets/deposit/" +id + "/" + value, content);
            return await response.Content.ReadAsAsync<WalletDTO>();
        }
    }
}
