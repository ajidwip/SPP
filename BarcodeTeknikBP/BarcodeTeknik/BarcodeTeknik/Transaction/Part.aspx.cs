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
    public partial class Part : System.Web.UI.Page
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
           
                List<Partgetset> Detail = new List<Partgetset>();

                string SelectString = "[SP_ViewTrxPart]'" + filter + "'";

                con.Open();
                SqlCommand cmd = new SqlCommand(SelectString, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Detail = reader.Cast<IDataRecord>()
                             .Select(r => new Partgetset
                             {
                                 PartNo = (string)r["PartNo"],
                                 Site = (string)r["Contract"],
                                 AccountingGroup = (string)r["Accounting_Group"],
                                 PartProductCode = (string)r["Part_Product_Code"],
                                 PartProductFamily = (string)r["Part_Product_Family"],
                                 PrimeCommodity = (string)r["Prime_Commodity"],
                                 Unit = (string)r["Unit_Meas"],
                                 PartDescription = (string)r["description"],
                                 RakNo = (string)r["Rak_No"],
                                 Barcode = (string)r["PartNoToBarcode"],
                                 qty = (string)r["Quantity"]
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
        public static string syncqty()
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("[SP_syncqty]", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();

            return "success";
        }
        [WebMethod]
        public static string crud1(string PartNo,string Site,string AccountingGroup,string Unit,string PartDescription,string RakNo,string Barcode, string tipe)
        {
            if (tipe == "1")
            {
                try
                {
                    crud.columna = new string[] { "" + PartNo + "", "" + Site + "", "" + AccountingGroup + "", "" + Unit + "", "" + PartDescription + "",""+ RakNo + "" };
                    crud.ExecuteSP("[SP_InsertPartNumber]", crud.columna);
                  
                    string a = "";
                 
                        Partgetset DataObj = new Partgetset();
                        DataObj.PartNo = PartNo;
                        DataObj.Site = Site;
                        DataObj.AccountingGroup = AccountingGroup;
                        DataObj.Unit = Unit;
                        DataObj.PartDescription = PartDescription;
                        DataObj.RakNo = RakNo;
                        DataObj.Barcode = Barcode;
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
            //else if (tipe == "2")
            //{

            //}
            //else
            //{
            //    try
            //    {
            //        crud.columna = new string[] { "" + PartNo + "","" + Site + "" };
            //        crud.ExecuteSP("[SP_deleteGaAlias]", crud.columna);
            //        returna = PartNo;
            //    }
            //    catch (Exception ex)
            //    {
            //        returna = "err$" + ex.Message.ToString();
            //        crud.con.Close();
            //    }

            //}

            return returna;
        }
    }
}