using FantasyFootballMidterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyFootball.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index(string pos, List<usp_Players_GetByPosition_Result> players)
        {

            switch (pos)
            {
                case null:
                    ViewBag.posQuantity = "ONE";
                    ViewBag.fullPosName = "Quarterback";
                    ViewBag.Position = "QB";
                    break;

                case "QB":
                    ViewBag.posQuantity = "TWO";
                    ViewBag.fullPosName = "Runningbacks";
                    ViewBag.Position = "RB";
                    break;

                case "RB":
                    ViewBag.posQuantity = "TWO";
                    ViewBag.fullPosName = "Wide Receivers";
                    ViewBag.Position = "WR";
                    break;

                case "WR":
                    ViewBag.posQuantity = "ONE";
                    ViewBag.fullPosName = "Tight End";
                    ViewBag.Position = "TE";
                    break;

            }

            string stringifyPosition = ViewBag.Position;
            List<usp_Players_GetByPosition_Result> playersForPosition;
            using (var context = new FantasyFootballEntities())
            {
                playersForPosition = context.usp_Players_GetByPosition(stringifyPosition)?.ToList();
            }

            return View("Index", playersForPosition);
        }


        List<string> FantasyTeam = new List<string>();
        // POST: Team
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(List<usp_Players_GetByPosition_Result> players)
        {
            try
            {
                List<usp_Players_GetByPosition_Result> selectedPlayers = players.Where(p => p.Selected == true).ToList();

                switch (selectedPlayers.FirstOrDefault().Position)
                {
                    case "QB":
                        Session["QB"] = selectedPlayers.FirstOrDefault().Player;
                        break;

                    case "RB":
                        Session["RB1"] = selectedPlayers[0].Player;
                        Session["RB2"] = selectedPlayers[1].Player;
                        break;

                    case "WR":
                        Session["WR1"] = selectedPlayers[0].Player;
                        Session["WR2"] = selectedPlayers[1].Player;
                        break;

                    case "TE":
                        Session["TE"] = selectedPlayers.FirstOrDefault().Player;
                        break;

                }

                if (selectedPlayers.FirstOrDefault().Position != "TE")
                    return RedirectToAction("Index", "Team", new { pos = players[0].Position });

                return RedirectToAction("Finish");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        public ActionResult Finish()
        {
            using (var context = new FantasyFootballEntities())
            {
                context.usp_FantasyFootballTeams_SaveTeam(Session["TeamName"].ToString(), Session["QB"].ToString(), Session["RB1"].ToString(), Session["RB2"].ToString(), Session["WR1"].ToString(), Session["WR2"].ToString(), Session["TE"].ToString(), (int?)Session["UserID"]);
            }
            Session["Completed"] = true;
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Teamname()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Teamname(string teamName)
        {
            Session["TeamName"] = teamName;
            return RedirectToAction("Index", "Team", new { pos = string.Empty });
        }
    }
}