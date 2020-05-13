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

namespace BarcodeTeknik
{
    public partial class _Default : Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static DetailClass[] GetSite(string filter)
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select Site from T_MsSite";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["Site"].ToString();
                DataObj.desc = "";
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string a = js.Serialize(Detail);
            return Detail.ToArray();
        }
        [WebMethod]
        public static string GetType1()
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select Type,Description from T_MsType";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["Type"].ToString();
                DataObj.desc = dtRow["Description"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static string GetClass()
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select Class,Description from T_MsClass";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["Class"].ToString();
                DataObj.desc = dtRow["Description"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static string GetCause()
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select Cause,Description from T_MsCause";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["Cause"].ToString();
                DataObj.desc = dtRow["Description"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static string GetPerformedActionId()
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select PerformedAction_Id,Description from T_MsPerformedAction";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["PerformedAction_Id"].ToString();
                DataObj.desc = dtRow["Description"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static DetailClass[] GetAT(string filter)
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select AccGroupId,AccGroupDesc from T_MsAccountingGroup";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["AccGroupId"].ToString();
                DataObj.desc = dtRow["AccGroupDesc"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string a = js.Serialize(Detail);
            return Detail.ToArray();
        }
        [WebMethod]
        public static DetailClass[] GetPurchaseGroup(string filter)
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select PurchaseGroupId,PurchaseGroupDesc from T_MsPurchaseGroup";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["PurchaseGroupId"].ToString();
                DataObj.desc = dtRow["PurchaseGroupDesc"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string a = js.Serialize(Detail);
            return Detail.ToArray();
        }

        [WebMethod]
        public static List<string> GetPartNoAutComplete(string filter)
        {
            List<string> empResult = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct PartNo from T_InventoryPart where PartNo like '%" + filter + "%'";
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        empResult.Add(dr["partNo"].ToString());
                    }
                    con.Close();
                    return empResult;
                }
            }
        }
        [WebMethod]
        public static List<string> GetKatalogPartNoAutComplete(string filter)
        {
            List<string> empResult = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Part_No from T_purchase_receipt_new where Part_No like '%" + filter + "%'";
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        empResult.Add(dr["Part_No"].ToString());
                    }
                    con.Close();
                    return empResult;
                }
            }
        }
        [WebMethod]
        public static string GetWoStatusddl()
        {
            List<defaultgetset> Detail = new List<defaultgetset>();

            string SelectString = "Select 'Close' Status union select 'Open' Status";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                defaultgetset DataObj = new defaultgetset();

                DataObj.filter = dtRow["Status"].ToString();

                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static string GetSiteddl()
        {
            List<defaultgetset> Detail = new List<defaultgetset>();

            string SelectString = "Select Site from T_MsSite where Site in('TKBP')";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                defaultgetset DataObj = new defaultgetset();

                DataObj.filter = dtRow["Site"].ToString();

                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
       
        [WebMethod]
        public static List<string> GetDescPartNoAutComplete(string filter)
        {
            List<string> empResult = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Description from T_InventoryPart where Description like '%" + filter + "%'  union select distinct Description from T_PurchasePart where Description like '%" + filter + "%' and InventoryFlag='N'";
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        empResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return empResult;
                }
            }
        }
        [WebMethod]
        public static DetailClass[] GetPartNo(string filter)
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "select distinct PartNo,description from T_InventoryPart where description like '%"+ filter + "%'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();

                DataObj.kode = dtRow["PartNo"].ToString();
                DataObj.desc = dtRow["description"].ToString();
                Detail.Add(DataObj);
            }
            con.Close();
            da.Dispose();
            dtGetData.Dispose();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string a = js.Serialize(Detail);
            return Detail.ToArray();
        }
    }
}