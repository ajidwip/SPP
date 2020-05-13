using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BarcodeTeknik.Transaction
{
    public partial class PartNo : System.Web.UI.Page
    {
        public static string userAD = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //userAD = "shuluh.mazaaya";
                userAD = Session[Master.myIP].ToString();
                if (userAD == "shuluh.mazaaya" || userAD == "ade.godjali" || userAD == "febrinurandy.ramadha" || userAD == "angela.zhang")
                {
                    if (Request.QueryString["empnm"] != null)
                    {
                        userAD = Request.QueryString["empnm"].ToString();
                    }
                }

                //getSite(Session[Master.myIP].ToString());
                GetDept(userAD);
            }
    

        }

        public void GetDept(string UserAD)
        {
            DataTable dt;
            dt = PartNoClass.GetEmployeeDept(UserAD);
            if (dt != null && dt.Rows.Count > 0)
            {
                lbldept.Text = dt.Rows[0]["DeptID"].ToString();
            }
        }

        public void getViewTeknik(string filter)
        {

            //DataTable dt;
            //dt = PartNoClass.GetViewTeknik(filter, userAD);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    btnshow.Visible = false;
            //}
            //else
            //{
            //    btnshow.Visible = true;
            //}

            btnshow.Visible = true;
            lvviewforecast.DataSource = PartNoClass.GetViewTeknik(filter);
            lvviewforecast.DataBind();

            //lbltes.Text = "tess";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtsearch.Value.Trim() !="")
            {                
                getViewTeknik(txtsearch.Value);              
            }
            //else
            //{
            //    btnshow.Visible = false;
            //}
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "spin()", true);

        }
    }
}