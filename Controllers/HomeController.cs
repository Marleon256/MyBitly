using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBitly.Models;
using MyBitly.Controllers;

namespace MyBitly.Controllers
{
    public class HomeController : Controller
    {
        private UrlContext db;

        public HomeController(UrlContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.UrlDatas.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UrlData urlData)
        {
            urlData.CreationDate = DateTime.Now;
            urlData.HopCount = 0;
            urlData.ShortUrl = GetShortUrl(urlData.CreationDate.GetHashCode());
            db.UrlDatas.Add(urlData);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public string GetShortUrl(int dateTimeHash)
        {
            var mask = 0xF;
            var shortUrl = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 8; i++)
                shortUrl.Append((char)('a' + ((dateTimeHash >> (i * 4)) & mask) + 10 * random.Next(2)));
            return shortUrl.ToString();
        }

        public async Task<IActionResult> Edit(string shortUrl)
        {
            if (shortUrl != null)
            {
                UrlData urlData = await db.UrlDatas.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
                if (urlData != null)
                    return View(urlData);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UrlData urlData)
        {
            UrlData oldUrlData = await db.UrlDatas.AsNoTracking().FirstOrDefaultAsync(u => u.ShortUrl == urlData.ShortUrl);
            urlData.CreationDate = oldUrlData.CreationDate;
            urlData.HopCount = oldUrlData.HopCount;

            db.UrlDatas.Update(urlData);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string shortUrl)
        {
            if (shortUrl != null)
            {
                UrlData urlData = await db.UrlDatas.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
                if (urlData != null)
                {
                    db.UrlDatas.Remove(urlData);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> ShortUrl(string shortUrl)
        {
            UrlData urlData = await db.UrlDatas.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
            if (urlData != null)
            {
                urlData.HopCount++;
                db.Update(urlData);
                await db.SaveChangesAsync();
                return Redirect(urlData.FullUrl);
            }
            return NotFound();
        }
    }
}
