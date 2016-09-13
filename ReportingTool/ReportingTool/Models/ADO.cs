using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ReportingTool.Models
{
    public class ADO
    {
        private string conStr;
        private SqlConnection con;
        public ADO()
        {
            this.conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            con = new SqlConnection(this.conStr);
        }
        // the method to run none query sql
        public bool RunNonQuery(SqlCommand sqlcmd)
        {
            bool State = false;
            try
            {
                con.Open();
                sqlcmd.Connection = con;
                sqlcmd.ExecuteNonQuery();
                con.Close();
                State = true;
            }
            catch (Exception e)
            {
                con.Close();
                State = false;

            }
            return State;
        }

        // the method to run sql that return row
        // the sql command must be select statement
        public DataTable RunQuery(SqlCommand sqlcmd)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd.CommandText, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
            }
            return dt;
        }
        // method to return a single row of data
        public DataRow RunScalarQuery(SqlCommand sqlcmd)
        {

            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd.CommandText, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                }
                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
            }
            return dr;

        }


    }
}