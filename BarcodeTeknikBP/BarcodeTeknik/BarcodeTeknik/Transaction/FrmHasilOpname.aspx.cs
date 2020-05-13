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

    public partial class FrmHasilOpname : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
        if(!IsPostBack)
            {
                PopulateSite();
                txtTanggal.Text =DateTime.Now.ToString("yyyy-MM-dd");
        }
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
            //ReportViewer1.LocalReport.Refresh();
          //  Response.Redirect("~/Transaction/Reporthasilopname.aspx?tanggal="+ txtTanggal.Text + "");
            //Response.Write("<script>window.open ('default.aspx','_blank');</script>");
            ScriptManager.RegisterStartupScript(this,
   this.GetType(), "OpenWindow", "window.open ('"+ResolveClientUrl("~/Transaction/Reporthasilopname.aspx?tanggal=" + txtTanggal.Text + "&site=" + cmbsite.SelectedValue + "") +"','_newTab');", true);
        }
    }
}