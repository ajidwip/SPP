using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace WebServiceBarcodeTeknik
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        Site site1 = new Site();
        cek cek1 = new cek();
        Contract contract1 = new Contract();
        Opname Opname1 = new Opname();
        TrxId trx = new TrxId();
        PartNo part1 = new PartNo();
        cekSite ceksiteA = new cekSite();
        cekRak cekRakA = new cekRak();
        rakno raknoA = new rakno();
        Rak rak = new Rak();
        PartNoAutoComplete partNoAutoComplete = new PartNoAutoComplete();
        opanameitem opnameItem = new opanameitem();
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void synqty()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec [SP_syncqty]", con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 60000000;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public PartNoAutoComplete GetPartNoAutoComplete()
        {
            SqlCommand cmd = new SqlCommand("select PartNo from T_InventoryPart", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            partNoAutoComplete.PartNoAutoCompleteTable = dt;
            return partNoAutoComplete;
        }
        public Rak GetRak()
        {
            SqlCommand cmd = new SqlCommand("select Rak_No from T_MsRak", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            rak.Raktable = dt;
            return rak;
        }
        public cekRak cekrak1(string rakno)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from T_MsRak where Rak_No='" + rakno + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            if (Int32.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                cekRakA.count2 = 1;
            }

            return cekRakA;
        }
        public rakno getrakno(string partno)
        {
            SqlCommand cmd = new SqlCommand("select Rak_No_Barcode from T_InventoryPart where PartNoToBarcode='" + partno + "' union select PartNo from T_Purchasepart where PartNoToBarcode='" + partno + "' and InventoryFlag <>'Y'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            raknoA.rakno1 = dt.Rows[0][0].ToString();
            return raknoA;
        }
        public cekSite ceksite1(string site)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from T_MsSite where Site='" + site + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            if (Int32.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                ceksiteA.count1 = 1;
            }

            return ceksiteA;
        }
        public PartNo getpart(string part)
        {
            SqlCommand cmd = new SqlCommand("select PartNo from T_InventoryPart where PartNoToBarcode='" + part + "' union select PartNo from T_Purchasepart where PartNoToBarcode='" + part + "' and InventoryFlag <>'Y'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            part1.PartNo1 = dt.Rows[0][0].ToString();
            return part1;
        }
        public opanameitem GetOpanameitem(string partno)
        {
            SqlCommand cmd = new SqlCommand("select PartNo,unit_meas from T_InventoryPart where PartNoToBarcode='" + partno + "' union select PartNo,Default_Buy_Unit_Meas from T_Purchasepart where PartNoToBarcode='" + partno + "' and InventoryFlag <>'Y'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            opnameItem.opanameitemtable = dt;
            return opnameItem;
        }
        public TrxId getID()
        {
            SqlCommand cmd = new SqlCommand("select NoUrut from T_NoUrut", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            trx.TrxId1 = int.Parse(dt.Rows[0][0].ToString());
            return trx;

        }
        public void TambahNoUrut()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update T_NoUrut set NoUrut=NoUrut+1", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void uploadphoto(byte[] img, string name)
        {
            MemoryStream ms = new MemoryStream(img);
            Image i = Image.FromStream(ms);


            SaveJpeg(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name + "_1.jpg", i, 50);
            //System.IO.File.WriteAllBytes(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name+"_1.jpg", img);

            con.Open();
            SqlCommand cmd = new SqlCommand("update T_InventoryPart set imagepath='img/" + name + "_1.jpg' where PartNo='" + name + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void SaveJpeg(string path, Image img, int quality)
        {

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }
        public void synwo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec [SP_syncwo]", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Site GetSite()
        {
            SqlCommand cmd = new SqlCommand("select site from T_MsSite where site in('TKBP')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            site1.SiteTable = dt;
            return site1;
        }
        public void uploadphoto2(byte[] img, string name)
        {
            MemoryStream ms = new MemoryStream(img);
            Image i = Image.FromStream(ms);



            SaveJpeg(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name + "_2.jpg", i, 50);
            //System.IO.File.WriteAllBytes(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name + "_2.jpg", img);

            con.Open();
            SqlCommand cmd = new SqlCommand("update T_InventoryPart set imagepath2='img/" + name + "_2.jpg' where PartNo='" + name + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void uploadphoto3(byte[] img, string name)
        {
            MemoryStream ms = new MemoryStream(img);
            Image i = Image.FromStream(ms);

            SaveJpeg(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name + "_3.jpg", i, 50);
            //System.IO.File.WriteAllBytes(@"C:\inetpub\wwwroot\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\img\" + name + "_3.jpg", img);


            con.Open();
            SqlCommand cmd = new SqlCommand("update T_InventoryPart set imagepath3='img/" + name + "_3.jpg' where PartNo='" + name + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Opname GetOpname(string Tanggal)
        {
            SqlCommand cmd = new SqlCommand("select distinct Part_No,TotalFisik,T_StockOpname.Contract,T_StockOpname.Rak_No,unit_meas from T_StockOpname inner join T_InventoryPart on T_InventoryPart.PartNo=T_StockOpname.Part_No and T_InventoryPart.Contract=T_StockOpname.Contract where convert(date,OpnameDate)='" + Tanggal + "' order by TotalFisik asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            Opname1.OpnameTable = dt;

            return Opname1;
        }
        public void updateqtystockopname(string qty, string partno, string contract, string opanamedate)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update T_StockOpname set TotalFisik='" + qty + "' where Part_No='" + partno + "' and Contract='" + contract + "' and  convert(date,Opnamedate)='" + opanamedate + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deletepemakaian(string WO, string partNo, string TransactionId, string contract, string RakNo)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("[SP_deletepemakaian]'"+WO+"','"+partNo+"','"+contract+"','"+RakNo+"'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deletestokcopname(string partNo, string opanamedate, string contract)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from T_StockOpname where  Part_No='" + partNo + "' and convert(date,Opnamedate)='" + opanamedate + "' and Contract='" + contract + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void stockopname(string qty, string partNo, string Contract, string Rak_No, string tanggal)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("[SP_InserOpname] '" + partNo + "','" + qty + "','" + Contract + "','" + Rak_No + "','" + tanggal + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void changepassword(string UserId, string oldpassword, string NewPAssword)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update T_MsUser set M_Password='" + NewPAssword + "' where M_Password='" + oldpassword + "' and NIK='" + UserId + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deleteWo(string wo)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from T_WO_Transaction where Wo_NO='" + wo + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public cek cekuser(string UserId, string password)
        {
            SqlCommand cmd = new SqlCommand("select count(NIK) from T_MsUser where NIK='" + UserId + "' and M_Password='" + password + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);

            if (Int32.Parse(dt.Rows[0][0].ToString()) == 1)
            {
                cek1.count = 1;
            }


            return cek1;
        }

        public cek getdept(string UserId)
        {

            SqlCommand cmd = new SqlCommand("select Departemen from T_MsUser where NIK='" + UserId + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                cek1.dept = dt.Rows[0][0].ToString();
            }

            return cek1;
        }
        //public void deleteopname(string date)
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("delete from T_StockOpname where opnamedate='" + date + "'", con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
        public void InsertWOTransaction(string qty, string WONo, string contract, string partno, string TransactionID, string userid, string RakNo)
        {
            SqlCommand cmd = new SqlCommand("select isnull(Quantity,0) from T_InventoryPart where PartNo='" + partno + "' and Rak_No='" + RakNo + "' and Contract='" + contract + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            if (Convert.ToInt16(dt.Rows[0][0].ToString()) >= Convert.ToInt16(qty))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("[SP_IssuedWOTransaction] '" + WONo + "','" + partno + "','" + contract + "'," + qty + ",'" + userid + "','" + RakNo + "'", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                throw new System.ArgumentException("Quantity tidak cukup, PartNo " + partno + " Rak " + RakNo + " Contract " + contract + " Quantity OnHand " + Convert.ToInt16(dt.Rows[0][0].ToString()) + "", "Quantity tidak cukup, PartNo " + partno + " Rak " + RakNo + " Contract " + contract + " Quantity OnHand " + Convert.ToInt16(dt.Rows[0][0].ToString()) + "");
                //OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                //response.StatusCode = HttpStatusCode.NotFound;
                //response.StatusDescription = "Quantity tidak cukup,PartNo " + partno + " Rak " + RakNo + " Contract " + contract + " Quantity OnHand " + Convert.ToInt16(dt.Rows[0][0].ToString()) + "";

            }
        }
        public void closeWO(string WO, string TransactionID)
        {
            con.Open();
            //SqlCommand cmd = new SqlCommand("update T_WO_Transaction set Status='C' where Wo_No='" + WO + "' and TransactionId='"+TransactionID+"'", con);
            //cmd.CommandType = CommandType.Text;
            //cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("update T_MsWo set withoutpart='1' where Wo_No='" + WO + "' and Contact='" + TransactionID + "'", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            con.Close();
        }
        public void unissued(string qty, string WONo, string contract, string partno, string TransactionID, string RakNo)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("[SP_UnIssuedWOTransaction]'" + WONo + "','" + partno + "','" + contract + "',"+qty+",'" + RakNo + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void updateWoNumber(string TransactionID, string WO)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update T_WO_Transaction set Wo_No='" + WO + "',TransactionID='" + WO + "' where Wo_No='' and TransactionID='" + TransactionID + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void updatejumlahpakai(string qty, string WONo, string contract, string partno, string TransactionId,string RakNo)
        {
            SqlCommand cmd2 = new SqlCommand("select isnull(Quantity,0) from T_InventoryPart where PartNo='" + partno + "' and Rak_No='" + RakNo + "' and Contract='" + contract + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            if (Convert.ToInt16(dt.Rows[0][0].ToString()) >= Convert.ToInt16(qty))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[SP_UpdateIssuedWOTransaction]'" + WONo + "','" + partno + "','" + contract + "'," + qty + ",'" + RakNo + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                throw new System.ArgumentException("Quantity tidak cukup, PartNo " + partno + " Rak " + RakNo + " Contract " + contract + " Quantity OnHand " + Convert.ToInt16(dt.Rows[0][0].ToString()) + "", "Quantity tidak cukup, PartNo " + partno + " Rak " + RakNo + " Contract " + contract + " Quantity OnHand " + Convert.ToInt16(dt.Rows[0][0].ToString()) + "");
            }
        }
        public Contract GetContract(string wo, string contract)
        {
            // DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select WO_NO,Description,isnull(withoutpart,0) from T_MsWO where WO_NO<>'' and Status <> 'C' and (WO_NO like '%" + wo + "%' or Description like '%" + wo + "%') and Contact='" + contract + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Paging");
            da.Fill(dt);
            contract1.ContracTable = dt;

            return contract1;
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
