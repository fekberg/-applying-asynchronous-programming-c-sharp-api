﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using StockAnalyzer.Core;

namespace StockAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";

            // Let's make sure that we can load the files when you start the project!
            var store = new DataStore(HostingEnvironment.MapPath("~/bin"));

            await store.LoadStocks();

            return View();
        }

        [Route("Stock/{ticker}")]
        public async Task<ActionResult> Stock(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier)) identifier = "MSFT";

            ViewBag.Title = $"Stock Details for {identifier.ToUpperInvariant()}";

            var store = new DataStore(HostingEnvironment.MapPath("~/bin"));

            var data = await store.LoadStocks();

            return View(data[identifier.ToUpperInvariant()]);
        }
    }
}
