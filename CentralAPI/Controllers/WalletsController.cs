using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Services.IServices;

namespace CentralAPI.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : Controller
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // GET: Wallets
        [HttpGet]
        public Task<ActionResult<IEnumerable<Wallet>>> GetAllWallets()
        {
            return _walletService.GetAllWallets();
        }

        //// GET: Wallets/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets
        //        .FirstOrDefaultAsync(m => m.walletID == id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wallet);
        //}

        //// GET: Wallets/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Wallets/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("walletID,totalAmount,currency")] Wallet wallet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(wallet);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(wallet);
        //}

        //// GET: Wallets/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets.FindAsync(id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(wallet);
        //}

        //// POST: Wallets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("walletID,totalAmount,currency")] Wallet wallet)
        //{
        //    if (id != wallet.walletID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(wallet);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!WalletExists(wallet.walletID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(wallet);
        //}


        //private bool WalletExists(string id)
        //{
        //    return _context.Wallets.Any(e => e.walletID == id);
        //}
    }
}

        //// GET: Wallets/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets
        //        .FirstOrDefaultAsync(m => m.walletID == id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wallet);
        //}

        //// POST: Wallets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var wallet = await _context.Wallets.FindAsync(id);
        //    _context.Wallets.Remove(wallet);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}