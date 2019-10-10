using MinesweeperWebpage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinesweeperWebpage.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Game()
        {
            MinesweeperModel minesweeper = new MinesweeperModel(90);
            Session["minesweeper"] = minesweeper;
            return View(minesweeper);
        }

        public ActionResult GameMove(int x, int y)
        {
            var minesweeper = (MinesweeperModel)Session["minesweeper"];
            minesweeper.Inspect(minesweeper.GetBoard()[x, y]);
            return View("Game", minesweeper);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}