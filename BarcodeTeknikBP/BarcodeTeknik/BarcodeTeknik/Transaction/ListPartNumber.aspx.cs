using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BarcodeTeknik.Transaction
{
    public partial class ListPartNumber : System.Web.UI.Page
    {
        public static string userAD = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //userAD = "justin.kusuma";
                userAD = Session[Master.myIP].ToString();
                if (userAD == "shuluh.mazaaya" || userAD == "ade.godjali" || userAD == "febrinurandy.ramadha" || userAD == "angela.zhang")
                {
                    if (Request.QueryString["empnm"] != null)
                    {
                        userAD = Request.QueryString["empnm"].ToString();
                    }
                }
                //getSite(Session[Master.myIP].ToString());
                viewlistPart("%");
                viewDocState();
            }
        }

        public void viewlistPart(string filter)
        {
            DataTable dtdept;
            dtdept = PartNoClass.GetEmployeeDept(userAD);
            if (dtdept != null && dtdept.Rows.Count > 0)
            {
                lvlistpart.DataSource = PartNoClass.viewlistPart(filter, "0", dtdept.Rows[0]["Tipe"].ToString(),ddlstate.SelectedValue,userAD);
                lvlistpart.DataBind();
            }
            else
            {
                //lvlistpart.DataSource = PartNoClass.viewlistPart(filter, "0", "Umum");
                Response.Redirect("NotAuthorized.htm");
            }
           
        }

        public void viewDocState()
        {
            ddlstate.DataSource = PartNoClass.DocState();
            ddlstate.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            viewlistPart(txtsearch.Value);
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewlistPart(txtsearch.Value);
        }
    }
}