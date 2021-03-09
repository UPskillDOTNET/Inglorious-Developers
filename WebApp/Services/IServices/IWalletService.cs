using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
namespace WebApp.Services.IServices
{
    public interface IWalletService
    {
        public Task<ActionResult<WalletDTO>> GetUserWalletById(string id);
    }
}
