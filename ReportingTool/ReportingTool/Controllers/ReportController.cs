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
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // get sale summary report by current date, the default date
        // when the page is in first loading with no filter options
        [HttpGet]
        public ActionResult GetSummaryReport()
        {
            string sql = "select Invoice,concat(convert(date,[Date]),'') as 'Date',CustomerName,StaffName,Total";
            sql += " from v_R_SaleSummary where CONVERT(date,[Date])=Convert(date,getdate())";
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
       
        // get sale by product by current date, the default date
        // when the page is in the first loading with  filter options
        [HttpGet]
        public ActionResult GetSaleByProduct()
        {
            string sql = "select ProductName, Category, Unit, Qty, Total from v_R_ProductTopSold ";
            sql += " where convert(date,[Date])= convert(date, getdate())";
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
        // search sale by product
        [HttpPost]
        public ActionResult SearchSaleByProduct()
        {
            var fdate = Request.Form["fromdate"];
            var tdate = Request.Form["todate"];
            var user = Request.Form["user"];
            string sql = "select ProductName, Category, Unit, Qty, Total from v_R_ProductTopSold ";
            var where = "";
            if (fdate!="" && tdate!="")
            {
                where = " where (convert(date,[Date])>='" + fdate + "' and convert(date,[Date])<='" + tdate + "') ";
                if (user!="")
                {
                    where += " and StaffName like N'%" + user + "%' ";
                }
            }
            else
            {
                if (user!="")
                {
                    where = " where StaffName like N'%" + user + "%' ";
                }
            }
            sql += where;
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
        // search sale sumary by filter options
        [HttpPost]
        public ActionResult Search()
        {
            var fDate = Request.Form["fromdate"];
            var tDate = Request.Form["todate"];
            var customer = Request.Form["customer"];
            var user = Request.Form["user"];
            var sql = "select Invoice,concat(convert(date,[Date]),'') as 'Date',CustomerName,StaffName,Total from v_R_SaleSummary ";
            var where = "";
            if (fDate != "" && tDate != "")
            {
                where = " where (CONVERT(date,[Date])>='" + fDate + "' and CONVERT(date,[Date])<='" + tDate + "') ";
                if (customer != "")
                {
                    where += " and CustomerName like N'%" + customer + "%'";
                }
                if (user != "")
                {
                    where += " and StaffName like N'%" + user + "%'";

                }
            }
            else
            {
                if (customer != "")
                {
                    where = " where CustomerName like N'%" + customer + "%' ";
                    if (user != "")
                    {
                        where += " and StaffName like N'%" + user + "%' ";
                    }
                }
                else
                {
                    if (user != "")
                    {
                        where = " where StaffName like N'%" + user + "%' ";
                    }
                }
            }
            if (where.Contains("like"))
            {
                //where += " COLLATE SQL_Latin1_General_CP1_CI_AS";
            }
            sql += where;
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
        // load sale by product view
        public ActionResult SaleByProduct()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("SaleByProduct");
        }
        // load stock balance view
        [HttpGet]
        public ActionResult StockBalance()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("ProductBalance");
        }
        // get stock balance
        [HttpGet]
        public ActionResult GetStockBalance()
        {
            string sql = "select ProductName, Category, Unit, Qty, AVGCost, Qty*AVGCost as 'Total' from v_R_ProductBalance";
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
        // search stock balance
        [HttpPost]
        public ActionResult SearchStockBalance()
        {
            var product = Request.Form["product"];
            var category = Request.Form["category"];
            var sql = "select ProductName, Category, Unit, Qty, AVGCost, Qty*AVGCost as 'Total' from v_R_ProductBalance ";
            var where = "";
            if (product!="")
            {
                where = " where ProductName like N'%" + product + "%' ";
                if (category!="")
                {
                    where += " and Category like N'%" + category + "%' ";
                }
            }
            else
            {
                if (category!="")
                {
                    where = " where Category like N'%" + category + "%' ";
                }
            }
            if (where.Contains("like"))
            {
               // where += " COLLATE SQL_Latin1_General_CP1_CI_AS";
            }
            sql += where;
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            var dt = new ADO().RunQuery(cmd);
            var str = string.Empty;
            str = JsonConvert.SerializeObject(dt);
            return Content(str);
        }
    }
}