using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace BarcodeTeknik.Transaction
{
    public partial class ReportWoPartnNonPart : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        static string site = "", tanggalreg, tanggalreg2;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            site = Request.QueryString["site"];
            tanggalreg = Convert.ToDateTime(Request.QueryString["tanggalreg"]).ToString("yyyy-MM-dd");
            tanggalreg2 = Convert.ToDateTime(Request.QueryString["tanggalreg2"]).ToString("yyyy-MM-dd");
            BinData(site, tanggalreg, tanggalreg2);
        }

        public void BinData(string sitem,string tanggalreg,string tanggalreg2)
        {
            string SelectString = "[SP_WO_PartnNoPart]'" + site + "','"+ tanggalreg + "','"+ tanggalreg2+ "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            lvBKKDetail.DataSource = dt;
            lvBKKDetail.DataBind();
            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string SelectString = "[SP_WO_PartnNoPart]'" + site + "','" + tanggalreg + "','" + tanggalreg2 + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            lvBKKDetail.DataSource = dt;
            lvBKKDetail.DataBind();
            con.Close();

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "WoPartnNonPart");
            wb.SaveAs(Server.MapPath("~/tmp/WoPartnNonPart.xlsx"));
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment; filename=WoPartnNonPart.xlsx");
            // Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(Server.MapPath("~/tmp/WoPartnNonPart.xlsx"));
            Response.End();

        }
    }
}