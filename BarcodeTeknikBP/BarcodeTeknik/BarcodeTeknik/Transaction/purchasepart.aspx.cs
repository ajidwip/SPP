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
using BarcodeTeknik;

namespace BarcodeTeknik.Transaction
{
    public partial class purchasepart : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        static CRUD crud = new CRUD();
        static string returna = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetData(string filter)
        {
            string a = "";
           
                List<PurchasePartGetSet> Detail = new List<PurchasePartGetSet>();

                string SelectString = "[SP_ViewTrxPurchasePart]'" + filter + "'";

                con.Open();
                SqlCommand cmd = new SqlCommand(SelectString, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Detail = reader.Cast<IDataRecord>()
                             .Select(r => new PurchasePartGetSet
                             {
                                 PartNo = (string)r["PartNo"],
                                 Site = (string)r["Contract"],
                                 PurchaseGroup = (string)r["STAT_GRP"],
                                 Unit = (string)r["DEFAULT_BUY_UNIT_MEAS"],
                                 PartDescription = (string)r["Description"],
                                 RakNo = (string)r["Rak_No"]
                             }).ToList();
                    }
                }

              
                con.Close();
              
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                a = js.Serialize(Detail);
            
           
            return a;
        }

        [WebMethod]
        public static string crud1(string PartNo, string Site, string PurchaseGroup, string Unit, string PartDescription,string RakNo, string tipe)
        {
            if (tipe == "1")
            {
                try
                {
                    crud.columna = new string[] { "" + PartNo + "", "" + Site + "", "" + PurchaseGroup + "", "" + Unit + "", "" + PartDescription + "",""+ RakNo + "" };
                    crud.ExecuteSP("[SP_InsertPurchasePartNumber]", crud.columna);

                    string a = "";

                    PurchasePartGetSet DataObj = new PurchasePartGetSet();
                    DataObj.PartNo = PartNo;
                    DataObj.Site = Site;
                    DataObj.PurchaseGroup = PurchaseGroup;
                    DataObj.Unit = Unit;
                    DataObj.PartDescription = PartDescription;
                    DataObj.RakNo = RakNo;
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    a = js.Serialize(DataObj);

                    con.Close();
                    returna = a;
                }
                catch (Exception ex)
                {
                    returna = "err$" + ex.Message.ToString();
                    crud.con.Close();
                }
            }

            return returna;
        }
    }
}