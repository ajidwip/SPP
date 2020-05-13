using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BarcodeTeknik.Transaction
{
    public partial class FormReportPemakaianPerbulan : System.Web.UI.Page
    {
        static string user = "";
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateMonth();
                populateYear();
                PopulateSite();
            }
        }
        private void populateMonth()
        {
            DataTable MonthTbl = new DataTable();
            MonthTbl.Columns.Add("id", typeof(System.Int32));
            MonthTbl.Columns.Add("Name", typeof(System.String));
            MonthTbl.Rows.Add(0, "");
            MonthTbl.Rows.Add(1, "Januari");
            MonthTbl.Rows.Add(2, "Februari");
            MonthTbl.Rows.Add(3, "Maret");
            MonthTbl.Rows.Add(4, "April");
            MonthTbl.Rows.Add(5, "Mei");
            MonthTbl.Rows.Add(6, "Juni");
            MonthTbl.Rows.Add(7, "Juli");
            MonthTbl.Rows.Add(8, "Agustus");
            MonthTbl.Rows.Add(9, "September");
            MonthTbl.Rows.Add(10, "Oktober");
            MonthTbl.Rows.Add(11, "November");
            MonthTbl.Rows.Add(12, "Desember");

            ddlbulan.DataSource = MonthTbl;
            ddlbulan.DataTextField = "Name";
            ddlbulan.DataValueField = "id";
            ddlbulan.DataBind();

            ddlbulan2.DataSource = MonthTbl;
            ddlbulan2.DataTextField = "Name";
            ddlbulan2.DataValueField = "id";
            ddlbulan2.DataBind();

            MonthTbl.Dispose();
        }

        private void populateYear()
        {
            for (int i = 2010; i <= System.DateTime.Now.Year; i++)
            {
                ddlTahun.Items.Add(i.ToString());
            }
            ddlTahun.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        }

        private void PopulateSite()
        {

            SqlCommand cmd = new SqlCommand("select Site from T_MsSite where Site in('TKBP')");
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbsite.DataSource = dt;
            cmbsite.DataTextField = "Site";
            cmbsite.DataValueField = "Site";
            cmbsite.DataBind();
            cmbsite.Items.Insert(0, new ListItem("- All -", "0"));

        }


        protected void btnview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this,
    this.GetType(), "OpenWindow", "window.open ('" + ResolveClientUrl("~/Transaction/ReportPemakaianBulanan.aspx?bulan=" + ddlbulan.SelectedValue + "&bulan2=" + ddlbulan2.SelectedValue + "&tahun=" + ddlTahun.SelectedValue + "&export=0&site="+ cmbsite.SelectedValue + "") + "','_newTab');", true);
            //Response.Redirect("~/Transaction/ReportQtyAkhir.aspx?bulan=" + ddlbulan.SelectedValue + "&tahun="+ddlTahun.SelectedValue+"");
        }

    }
}