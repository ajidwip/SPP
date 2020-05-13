using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace BarcodeTeknik.Transaction
{
    public partial class Reporthasilopname : System.Web.UI.Page
    {
        string tanggal = "";
        string site = "";
        DataTable dt = new DataTable();
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                 tanggal = Request.QueryString["tanggal"];
                 site = Request.QueryString["site"];
                BinData(tanggal, site);
                lbltanggal.Text = Request.QueryString["tanggal"];
            }
           
        }
        public void BinData(string tanggal,string site)
        {
            string SelectString = "[SP_HasilOpname]'"+ tanggal + "','"+ site + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            da.Fill(dt);

            lvBKKDetail.DataSource = dt;
            lvBKKDetail.DataBind();
            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string SelectString = "[SP_HasilOpname]'" + lbltanggal.Text + "','"+ site + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            lvBKKDetail.DataSource = dt;
            lvBKKDetail.DataBind();
            con.Close();

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Opaname "+lbltanggal.Text.ToString());
            wb.SaveAs(Server.MapPath("~/tmp/StockOpname_"+lbltanggal.Text+".xlsx"));
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment; filename=StockOpname_" + lbltanggal.Text + ".xlsx");
           // Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(Server.MapPath("~/tmp/StockOpname_" + lbltanggal.Text + ".xlsx"));
            Response.End();
            
        }
    }
}