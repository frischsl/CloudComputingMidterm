using FantasyFootballMidterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyFootball.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            //using (var context = new FantasyFootballEntities())
            //{
            //    ViewBag.AllPlayers = context.usp_Players_ReturnAll().ToList();
            //}
        }

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name";
            ViewBag.Week1SortParm = sortOrder == "w1" ? "w1_desc" : "w1";
            ViewBag.Week2SortParm = sortOrder == "w2" ? "w2_desc" : "w2";
            ViewBag.Week3SortParm = sortOrder == "w3" ? "w3_desc" : "w3";
            ViewBag.Week4SortParm = sortOrder == "w4" ? "w4_desc" : "w4";
            ViewBag.Week5SortParm = sortOrder == "w5" ? "w5_desc" : "w5";
            ViewBag.Week6SortParm = sortOrder == "w6" ? "w6_desc" : "w6";
            ViewBag.Week7SortParm = sortOrder == "w7" ? "w7_desc" : "w7";
            ViewBag.Week8SortParm = sortOrder == "w8" ? "w8_desc" : "w8";
            ViewBag.Week9SortParm = sortOrder == "w9" ? "w9_desc" : "w9";
            ViewBag.Week10SortParm = sortOrder == "w10" ? "w10_desc" : "w10";
            ViewBag.Week11SortParm = sortOrder == "w11" ? "w11_desc" : "w11";
            ViewBag.Week12SortParm = sortOrder == "w12" ? "w12_desc" : "w12";
            ViewBag.Week13SortParm = sortOrder == "w13" ? "w13_desc" : "w13";
            ViewBag.Week14SortParm = sortOrder == "w14" ? "w14_desc" : "w14";
            ViewBag.Week15SortParm = sortOrder == "w15" ? "w15_desc" : "w15";
            ViewBag.Week16SortParm = sortOrder == "w16" ? "w16_desc" : "w16";
            ViewBag.Week17SortParm = sortOrder == "w17" ? "w17_desc" : "w17";
            
            //List<usp_FantasyFootballTeams_GetPlayersByUserID_Real_Result> team;
            List<usp_GamesByWeek_GetFantasyTeamScoreByWeek_Result> teams;
            using (var context = new FantasyFootballEntities())
            {
                ViewBag.TeamName = context.usp_FantasyFootballTeams_GetTeamNameByUserID((int?)Session["UserID"]).FirstOrDefault();
                //team = context.usp_FantasyFootballTeams_GetPlayersByUserID_Real((int?)Session["UserID"]).ToList(); // (int?) Session["UserID"]
                teams = context.usp_GamesByWeek_GetFantasyTeamScoreByWeek().ToList();
            }

            if (string.IsNullOrEmpty(searchString) && (string.IsNullOrEmpty(sortOrder) || (string) Session["PrevSortOrder"] == sortOrder))
            {
                Session["GlobalSearchString"] = string.Empty;
            }

            if (Session["GlobalSearchString"] != null && !string.IsNullOrEmpty((string) Session["GlobalSearchString"]) && string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(sortOrder))
            {
                if (Session["UserID"] != null)
                {
                    teams = teams.Where(t => t.TeamName.ToUpper().Contains(Session["GlobalSearchString"].ToString().ToUpper()) || t.UserID == (int)Session["UserID"]).ToList();
                }
            }
            else if ((Session["GlobalSearchString"] == null || string.IsNullOrEmpty((string) Session["GlobalSearchString"])) && !string.IsNullOrEmpty(searchString))
            {
                Session["GlobalSearchString"] = searchString;
                if (Session["UserID"] != null)
                {
                    teams = teams.Where(t => t.TeamName.ToUpper().Contains(searchString.ToUpper()) || t.UserID == (int)Session["UserID"]).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                if (Session["UserID"] != null)
                {
                    teams = teams.Where(t => t.TeamName.ToUpper().Contains(searchString.ToUpper()) || t.UserID == (int)Session["UserID"]).ToList();
                }
            }

            Session["PrevSortOrder"] = sortOrder;



            switch (sortOrder)
            {
                case "name":
                    teams = teams.OrderBy(t => t.TeamName).ToList();
                    break;
                case "name_desc":
                    teams = teams.OrderByDescending(t => t.TeamName).ToList();
                    break;

                case "w1":
                    teams = teams.OrderBy(t => t.Week1).ToList();
                    break;
                case "w1_desc":
                    teams = teams.OrderByDescending(t => t.Week1).ToList();
                    break;

                case "w2":
                    teams = teams.OrderBy(t => t.Week2).ToList();
                    break;
                case "w2_desc":
                    teams = teams.OrderByDescending(t => t.Week2).ToList();
                    break;

                case "w3":
                    teams = teams.OrderBy(t => t.Week3).ToList();
                    break;
                case "w3_desc":
                    teams = teams.OrderByDescending(t => t.Week3).ToList();
                    break;

                case "w4":
                    teams = teams.OrderBy(t => t.Week4).ToList();
                    break;
                case "w4_desc":
                    teams = teams.OrderByDescending(t => t.Week4).ToList();
                    break;

                case "w5":
                    teams = teams.OrderBy(t => t.Week5).ToList();
                    break;
                case "w5_desc":
                    teams = teams.OrderByDescending(t => t.Week5).ToList();
                    break;

                case "w6":
                    teams = teams.OrderBy(t => t.Week6).ToList();
                    break;
                case "w6_desc":
                    teams = teams.OrderByDescending(t => t.Week6).ToList();
                    break;

                case "w7":
                    teams = teams.OrderBy(t => t.Week7).ToList();
                    break;
                case "w7_desc":
                    teams = teams.OrderByDescending(t => t.Week7).ToList();
                    break;

                case "w8":
                    teams = teams.OrderBy(t => t.Week8).ToList();
                    break;
                case "w8_desc":
                    teams = teams.OrderByDescending(t => t.Week8).ToList();
                    break;

                case "w9":
                    teams = teams.OrderBy(t => t.Week9).ToList();
                    break;
                case "w9_desc":
                    teams = teams.OrderByDescending(t => t.Week9).ToList();
                    break;

                case "w10":
                    teams = teams.OrderBy(t => t.Week10).ToList();
                    break;
                case "w10_desc":
                    teams = teams.OrderByDescending(t => t.Week10).ToList();
                    break;

                case "w11":
                    teams = teams.OrderBy(t => t.Week11).ToList();
                    break;
                case "w11_desc":
                    teams = teams.OrderByDescending(t => t.Week11).ToList();
                    break;

                case "w12":
                    teams = teams.OrderBy(t => t.Week12).ToList();
                    break;
                case "w12_desc":
                    teams = teams.OrderByDescending(t => t.Week12).ToList();
                    break;

                case "w13":
                    teams = teams.OrderBy(t => t.Week13).ToList();
                    break;
                case "w13_desc":
                    teams = teams.OrderByDescending(t => t.Week13).ToList();
                    break;

                case "w14":
                    teams = teams.OrderBy(t => t.Week14).ToList();
                    break;
                case "w14_desc":
                    teams = teams.OrderByDescending(t => t.Week14).ToList();
                    break;

                case "w15":
                    teams = teams.OrderBy(t => t.Week15).ToList();
                    break;
                case "w15_desc":
                    teams = teams.OrderByDescending(t => t.Week15).ToList();
                    break;

                case "w16":
                    teams = teams.OrderBy(t => t.Week16).ToList();
                    break;
                case "w16_desc":
                    teams = teams.OrderByDescending(t => t.Week16).ToList();
                    break;

                case "w17":
                    teams = teams.OrderBy(t => t.Week17).ToList();
                    break;
                case "w17_desc":
                    teams = teams.OrderByDescending(t => t.Week17).ToList();
                    break;

                default:
                    teams = teams.OrderBy(t => t.TeamName).ToList();
                    break;
            }

            return View(teams);
        }

    }
}