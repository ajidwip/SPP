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

namespace BarcodeTeknik.Master
{
    public partial class KatalogPart : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        static string returna = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetData(string filter)
        {
            string a = "";
            
                List<KatalogGetSet> Detail = new List<KatalogGetSet>();

                string SelectString = "[SP_ViewKatalog]'" + filter+"'";

                con.Open();
                SqlCommand cmd = new SqlCommand(SelectString, con);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Detail = reader.Cast<IDataRecord>()
                                 .Select(r => new KatalogGetSet
                                 {
                                     PartNo = (string)r["Part_No"],
                                     Description = (string)r["Description"],
                                     image = (string)r["ImagePath"],
                                     image2 = (string)r["ImagePath2"],
                                     image3 = (string)r["ImagePath3"]
                                 }).ToList();
                        }
                    }
          

              
                con.Close();
               
                JavaScriptSerializer js = new JavaScriptSerializer();
                 a = js.Serialize(Detail);
            
           
            return a;
        }
    }
}