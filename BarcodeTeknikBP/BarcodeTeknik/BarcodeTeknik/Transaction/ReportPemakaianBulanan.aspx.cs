using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace BarcodeTeknik.Transaction
{
    public partial class ReportPemakaianBulanan : System.Web.UI.Page
    {
      
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public string bulan,bulan2,site;
        public string tahun;
        public string export;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bulan = Request.QueryString["bulan"];
                bulan2 = Request.QueryString["bulan2"];
                tahun = Request.QueryString["tahun"];
                site = Request.QueryString["site"];
                export = Request.QueryString["export"];
              
            }
        }

        [WebMethod]
        public static string BinData(string bulan,string bulan2, string tahun,string site)
        {
            DataTable dt = new DataTable();
            string SelectString = "[SP_ReportPemakaianPerbulan]'" + bulan + "','" + bulan2 + "','" + tahun + "','"+ site + "','0'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            cmd.CommandTimeout = 1000000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
          
            da.Fill(dt);
            var a = JsonConvert.SerializeObject(dt);
            //lvBKKDetail.DataSource = dt
            //lvBKKDetail.DataBind();
            con.Close();
          
            return a;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            bulan = Request.QueryString["bulan"];
            bulan2 = Request.QueryString["bulan2"];
            tahun = Request.QueryString["tahun"];
            site = Request.QueryString["site"];
            export = "1";
            string SelectString = "[SP_ReportPemakaianPerbulan]'" + bulan + "','" + bulan2 + "','" + tahun + "','" + site + "','" + export + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            cmd.CommandTimeout = 1000000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt2);
        
            con.Close();

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt2, "PemakaianPerBulan");
            wb.SaveAs(Server.MapPath("~/tmp/PemakaianPerBulan.xlsx"));
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment; filename=PemakaianPerBulan.xlsx");
            // Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(Server.MapPath("~/tmp/PemakaianPerBulan.xlsx"));
            Response.End();

        }
    }
}