using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckDigitWeb.Models;

namespace CheckDigitWeb.Controllers
{
    public class CheckDigitResult
    {
        public string Input { get; set; }
        public string[] Lines { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int num2check)
        {
            var n = num2check % 10_000_000;
            var d7 = n / 1_000_000;
            n %= 1_000_000;
            var d6 = n / 100_000;
            n %= 100_000;
            var d5 = n / 10_000;
            n %= 10_000;
            var d4 = n / 1_000;
            n %= 1_000;
            var d3 = n / 100;
            n %= 100;
            var d2 = n / 10;
            n %= 10;
            var d1 = n;

            var d = d7 + d6 * 2 + d5 * 3 + d4 * 4 + d3 * 5 + d2 * 6 + d7 * 7;
            var chk = 9 - (d % 10);
            var r = new CheckDigitResult
            {
                Input = string.Format("{0:0000000}", n),
                Lines = new []
                {
                    string.Format("1 x {0} = {1}", d7, d7),
                    string.Format("2 x {0} = {1}", d6, d6 * 2),
                    string.Format("3 x {0} = {1}", d5, d5 * 3),
                    string.Format("4 x {0} = {1}", d4, d4 * 4),
                    string.Format("5 x {0} = {1}", d3, d3 * 5),
                    string.Format("6 x {0} = {1}", d2, d2 * 6),
                    string.Format("7 x {0} = {1}", d1, d1 * 7),
                    string.Format("Sum : {0}", d),
                    string.Format("CheckDigit: {0} <== 10 complimentary of sum", chk),
                    string.Format("Number with check digit: {0:0000000}{1}", num2check, chk)
                }
            };

            return View(r);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
