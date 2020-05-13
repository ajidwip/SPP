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
    public partial class userakses : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public DataTable dt = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataTable dt3 = new DataTable();
        public DataTable dt4 = new DataTable();
        public DataTable dt5 = new DataTable();
        public DataTable dt6 = new DataTable();
        public static DataTable dt7 = new DataTable();
        public static DataTable dt8 = new DataTable();
        public static DataTable dt9 = new DataTable();
        public static DataTable dt10 = new DataTable();
        public DataRow[] result;
        public int c = 0;
        public int d = 0;
        public int b = 0;
        public int row2 = 0;
        static string user = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Master.myIP] == null)
                {
                    // Response.Write(@"<script type='text/javascript'>alert('Session timeout');</script>");
                    HttpContext.Current.Request.Cookies.Clear();
                    Session.Clear();
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "Notify", "alert('Session Timeout');", true);
                    Response.Redirect(ResolveUrl("~/SessionTimeOut.aspx"));

                }
                else
                {
                    user = Session[Master.myIP].ToString();
                    //   akses = crud.GetAkses(user);
                }
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from TrxMenu where UserId='" + user + "' and Menu_Id in(select Menu_Id from T_MsMenu where path like '%userakses.aspx%')", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt6);
                con.Close();
                if (dt6.Rows.Count > 0)
                {
                    GetParentMenu();


                }
                else
                {
                    Response.Redirect(ResolveUrl("~/NotAuthorized.aspx"));

                }

            }
        }

        [WebMethod]
        public static string[] GetMenu(string Owner)
        {
            List<string> tmpmenu = new List<string>();
            con.Open();
            //SqlCommand cmd = new SqlCommand("select T_MsMenu.Menu_Name,T_MsMenu.Menu_Id from T_MsMenu where Parent=''", con);
            SqlCommand cmd = new SqlCommand("select TrxMenu.Menu_Id from TrxMenu  where UserId='" + Owner + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt7);
            con.Close();

            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                tmpmenu.Add(dt7.Rows[i][0].ToString());
            }
            dt7.Clear();
            return tmpmenu.ToArray<string>();

        }

        public void GetParentMenu()
        {
            con.Open();
            //SqlCommand cmd = new SqlCommand("select T_MsMenu.Menu_Name,T_MsMenu.Menu_Id from T_MsMenu where Parent=''", con);
            SqlCommand cmd = new SqlCommand("select T_MsMenu.Menu_Name,T_MsMenu.Menu_Id from T_MsMenu  where Parent=''", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            for (int a = 0; a < dt.Rows.Count; a++)
            {

                // SqlCommand cmd2 = new SqlCommand("select T_MsMenu.Menu_Name,T_MsMenu.Parent,Path from T_MsMenu where Parent='" + dt.Rows[a][1].ToString() + "'", con);
                SqlCommand cmd2 = new SqlCommand("select T_MsMenu.Menu_Name,T_MsMenu.Parent,Path,T_MsMenu.Menu_Id from T_MsMenu where Parent='" + dt.Rows[a][1].ToString() + "'", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);


            }

            con.Close();
        }


        [WebMethod]
        public static string save(string user, string menuid, string row, string rowchild)
        {
            CRUD crud1 = new CRUD();
            crud1.columna = new string[] { "" + menuid + "", "" + user + "" };
            crud1.ExecuteSP("SP_InsertuserAkses", crud1.columna);
            return row + ',' + rowchild;
        }
        [WebMethod]
        public static string delete(string user, string menuid, string row, string rowchild)
        {
            CRUD crud1 = new CRUD();
            crud1.columna = new string[] { "" + menuid + "", "" + user + "" };
            crud1.ExecuteSP("[SP_deleteuserAkses]", crud1.columna);
            return row + ',' + rowchild;
        }
        [WebMethod]
        public static DetailClass[] GetOwner() //GetData function
        {
            List<DetailClass> Detail = new List<DetailClass>();

            string SelectString = "Select UserId from T_MsUser";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtGetData = new DataTable();

            da.Fill(dtGetData);

            foreach (DataRow dtRow in dtGetData.Rows)
            {
                DetailClass DataObj = new DetailClass();
                DataObj.kode = dtRow["UserId"].ToString();
                DataObj.desc = "";
                Detail.Add(DataObj);
            }
            con.Close();
            return Detail.ToArray();
        }

    }
}