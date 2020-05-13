using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BarcodeTeknik.GetterSetter;

namespace BarcodeTeknik.Transaction
{
    public partial class katalogdetail : System.Web.UI.Page
    {
      
        public string img1 = "", img2 = "", img3 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                img1 = Request.QueryString["img1"].ToString();
                img2 = Request.QueryString["img2"].ToString();
                img3 = Request.QueryString["img3"].ToString();
            }
        }
   
    }
}