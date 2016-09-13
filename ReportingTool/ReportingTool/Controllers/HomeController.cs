using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ReportingTool.Models;
namespace ReportingTool.Controllers
{
    public class HomeController : Controller
    {
        // GET: load login view
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
        // signout
        public ActionResult Signout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        // do login
        [HttpPost]
        public ActionResult DoLogin()
        {
            var username = Request.Form["username"];
            var pass = Request.Form["pass"];
            var sm = "";
            var cmd = new SqlCommand();
            cmd.CommandText = "select UserID, UserName, Password, GroupID from sUser where UserName=N'" + username + "'";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new ADO().RunQuery(cmd);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr = dt.Rows[0];
                if (dr[2].ToString() == pass)
                {
                    Session["userid"] = dr[0].ToString();
                    Session["username"] = username;
                    sm = "yes";
                }
                else
                {
                    sm = "no";
                }
            }
            else
            {
                sm = "no";
            }
            return Content(sm);
        }
    }
}