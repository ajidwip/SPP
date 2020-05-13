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
    public partial class ReportQtyAkhir : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        DataTable dt = new DataTable();
        string bulan;
        string tahun;
        string site;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 bulan = Request.QueryString["bulan"];
                 tahun = Request.QueryString["tahun"];
                 site = Request.QueryString["site"];
                 BinData(bulan,tahun,site);
            }
        }
        public void BinData(string bulan,string tahun,string site)
        {
            string SelectString = "[SP_WO_CLOSE]'" + bulan + "','"+tahun+ "','" + site + "'";

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
            DataTable dt2 = new DataTable();
            bulan = Request.QueryString["bulan"];
            tahun = Request.QueryString["tahun"];
            site = Request.QueryString["site"];
            string SelectString = "[SP_WO_CLOSE]'" + bulan + "','" + tahun + "','" + site + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt2);

            DataView view = new System.Data.DataView(dt2);
            DataTable selected = view.ToTable("Selected", false, "Wo_No", "Description", "PartNo", "QtyIssued", "QtyUnIssued","QtyToMR","DibuatOleh","CauseDescription","StartDate");

            lvBKKDetail.DataSource = dt;
            lvBKKDetail.DataBind();
            con.Close();

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(selected, "PemakaianPerWo " + bulan+ tahun);
            wb.SaveAs(Server.MapPath("~/tmp/PemakaianPerWO_" + bulan  + tahun+".xlsx"));
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment; filename=PemakaianPerWO_" + bulan  + tahun + ".xlsx");
            // Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(Server.MapPath("~/tmp/PemakaianPerWO_" + bulan + tahun + ".xlsx"));
            Response.End();

        }
    }
}