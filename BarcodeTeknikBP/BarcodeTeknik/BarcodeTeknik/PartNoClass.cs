using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BarcodeTeknik
{
    public class PartNoClass
    {
        public static DataTable GetSite(string Employee_Id)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT A.Site,[desc],B.Emp_No,B.Name FROM [dbo].[T_MsSite] A " +
                "INNER JOIN [T_MsEmployee] B ON A.Site = B.Contract INNER JOIN [T_MsUser] C ON C.Emp_No = B.Emp_No " +
                "WHere [desc] NOT LIKE'%Deleted%' AND C.UserId=@Employee_Id ",
                new string[] { "@Employee_Id" }, new string[] { Employee_Id });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetProdFamilyUmum(string UserAD)
        {
            DataTable dt;
            //dt = DBConnection.ExecSelect("SELECT PART_PRODUCT_FAMILY,PART_PRODUCT_FAMILY + ' - '+DESCRIPTION [description] FROM [dbo].[T_MsProductFamily] " +
            //    " Union ALL SELECT PART_PRODUCT_FAMILY,PART_PRODUCT_FAMILY + ' - '+DESCRIPTION [description] FROM T_MsProductFamilyCustom ",
            dt = DBConnection.ExecSelect("SELECT DISTINCT PART_PRODUCT_FAMILY,PART_PRODUCT_FAMILY + ' - '+DESCRIPTION [description] FROM T_MsProductFamilyCustomUmum " +
                "Where CONTRACT IN(SELECT B.Contract FROM T_MsUser A INNER JOIN T_MsEmployee B ON A.Emp_No=B.Emp_No Where A.UserId=@UserAD) ",
                new string[] { "@UserAD" }, new string[] { UserAD });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetProdFamilyTeknik(string UserAD)
        {
            DataTable dt;
            //dt = DBConnection.ExecSelect("Declare @sql nvarchar(max) " +
            //    "SET @sql =('SELECT DISTINCT PART_PRODUCT_FAMILY,PART_PRODUCT_FAMILY +'' - ''+DESCRIPTION DESCRIPTION FROM [dbo].[T_MsProductFamilyCustomTeknik] where CONTRACT IN('+ @Contract +')') Exec (@sql)",
            //    new string[] { "@Contract" }, new string[] { Contract });
            dt = DBConnection.ExecSelect("SELECT DISTINCT PART_PRODUCT_FAMILY,PART_PRODUCT_FAMILY +' - '+DESCRIPTION DESCRIPTION FROM [dbo].[T_MsProductFamilyCustomTeknik] where CONTRACT IN(SELECT B.Contract FROM T_MsUser A " +
                "INNER JOIN T_MsEmployee B ON A.Emp_No=B.Emp_No Where A.UserId=@UserAD) ",
             new string[] { "@UserAD" }, new string[] { UserAD });
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
        public static DataTable GetProdCodeTeknik(string UserAD)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT DISTINCT PART_PRODUCT_CODE,PART_PRODUCT_CODE +' - ' + DESCRIPTION [description] FROM [dbo].[T_MsProductCodeCustomTeknik] where CONTRACT IN(SELECT B.Contract FROM T_MsUser A " +
                "INNER JOIN T_MsEmployee B ON A.Emp_No=B.Emp_No Where A.UserId=@UserAD)",
                new string[] { "@UserAD" }, new string[] { UserAD });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetUom()
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT '0'[UNIT_CODE],'--Pilih--' [DESCRIPTION] UNION ALL SELECT UNIT_CODE,UNIT_CODE + ' - '+ DESCRIPTION [DESCRIPTION] FROM T_MsUom ",
                new string[] { "" }, new string[] { "" });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetAttachment(string PartNo_id)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("Select attach_id,PartNo_id,filename,filetype  FROM [dbo].[T_PartNumber_Attachment] Where PartNo_id=@PartNo_id ",
                new string[] { "@PartNo_id" }, new string[] { PartNo_id });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }



        public static DataTable GetAcctGrp()
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT '0'[GroupID],'--Pilih--'  [Description] UNION ALL SELECT AccGroupId [GroupID],AccGroupId +' - '+ AccGroupDesc [Description] FROM [dbo].[T_MsAccountingGroup] where AccGroupDesc NOT LIKE'%To be Deleted%' ",
                new string[] { "" }, new string[] { "" });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
        public static DataTable GetPrchGrp()
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT '0'[GroupID],'--Pilih--'  [Description] UNION ALL SELECT PurchaseGroupId [GroupID],PurchaseGroupId + ' - ' + PurchaseGroupDesc [Description] FROM [dbo].[T_MsPurchaseGroup] ",
                new string[] { "" }, new string[] { "" });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        //public static DataTable GetViewTeknik(string filter,string employeeID)
        public static DataTable GetViewTeknik(string filter)
        {
            DataTable dt;
            //dt = DBConnection.ExecSelect("SELECT PartNo,description,site,[Site Des],Status FROM [View_PartNoTeknik] " +
            //    " WHere (PartNo LIKE'%'+ @filter +'%' OR description LIKE'%'+ @filter +'%') AND [Site ] IN(SELECT B.Contract FROM [dbo].[T_MsUser] A " +
            //    " INNER JOIN T_MsEmployee B ON A.Emp_no = B.Emp_no Where A.UserId=@employeeID)",
            //new string[] { "@filter", "@employeeID" }, new string[] { filter, employeeID });

            dt = DBConnection.ExecSelect("SELECT PartNo,description,site,[Site Des],Status FROM [View_PartNoTeknik] " +
            " WHere (PartNo LIKE'%'+ @filter +'%' OR description LIKE'%'+ @filter +'%') "  ,
            new string[] { "@filter" }, new string[] { filter });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetPertanyaan(string PertanyaanId, string Tipe)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("Select 0 Role_id,-1 parent_pertanyaan,0 id_Pertanyaan,'--Pilih--' Pertanyaan,NULL AcctGroup,NULL Tipe,NULL Status " +
                " UNION ALL SELECT Role_id,parent_pertanyaan,id_Pertanyaan,Pertanyaan,AcctGroup,Tipe,Status FROM [View_PertanyaanSPP] " +
                " Where ISNULL(parent_pertanyaan,'')=@PertanyaanId AND Tipe=@Tipe",
                new string[] { "@PertanyaanId", "@Tipe" }, new string[] { PertanyaanId,Tipe });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetAcctGrp(string PertanyaanId, string Tipe)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT Id_Pertanyaan,Pertanyaan,Status,AcctGroup FROM [dbo].[T_MsPertanyaan]  " +
                " where IsActive=1 AND Id_Pertanyaan =@PertanyaanId AND Tipe=@Tipe",
                new string[] { "@PertanyaanId", "@Tipe" }, new string[] { PertanyaanId, Tipe });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static string Create(string mode,string PartNo, string Contract, string Partdescription, string unit_meas, string AccountingGroup, string spesifikasi,
             string part_product_code, string part_product_family, string id_pertanyaanumur, string id_pertanyaanestimasi,string id_pertanyaanfungsi, 
             string status_barang,string employeeid,string tipe,string PartId)
        {
            try
            {
                string strgFlag = "";
                strgFlag = Convert.ToString(DBConnection.ExecScalar("SP_InsertPartNoTeknik",
                                    new string[] { "@mode","@PartNo", "@Contract", "@Partdescription", "@unit_meas", "@AccountingGroup", "@spesifikasi",
                                    "@part_product_code", "@part_product_family", "@id_pertanyaanumur","@id_pertanyaanestimasi","@id_pertanyaanfungsi",
                                    "@status_barang","@createdBy","@tipe","@PartNo_id" },
                                     new string[] { mode,PartNo,Contract,Partdescription,unit_meas,AccountingGroup,spesifikasi,part_product_code,part_product_family,
                                     id_pertanyaanumur,id_pertanyaanestimasi,id_pertanyaanfungsi,status_barang,employeeid,tipe,PartId },
                                     true));
                return strgFlag;
            }
            catch(Exception ex)
            {
                throw new Exception("Create Data Gagal" + ex.Message);
                //return "Error";
            }
          

            //if (intFlag <= 0)
            //{
            //    throw new Exception("Create Data Gagal");
            //}
        }

        //public static void CreateApprove(string Mode, string PartNo,string FieldId,string UserAD, string Reason,string acctgroup, string Contract, string Tipe)
        public static void CreateApprove(string Mode, string PartID, string FieldId, string UserAD, string Reason, string acctgroup, string Tipe)
        {
            string strgFlag = "";
            //strgFlag = Convert.ToString(DBConnection.ExecScalar("SP_CreateApprovePartNo",
            //    new string[] { "@Mode", "@PartNo_Id", "@FieldId", "@UserAD", "@Reason", "@acctgroup", "@Contract", "@Tipe" },
            //      new string[] { Mode,PartNo,FieldId,UserAD,Reason, acctgroup, Contract, Tipe },true));
            strgFlag = Convert.ToString(DBConnection.ExecScalar("SP_CreateApprovePartNo",
            new string[] { "@Mode", "@PartNo_Id", "@FieldId", "@UserAD", "@Reason", "@acctgroup", "@Tipe" },
              new string[] { Mode,  PartID, FieldId, UserAD, Reason, acctgroup, Tipe }, true));
        }

        //public static DataTable viewlistPart(string filter,string contract,string Tipe)
        public static DataTable viewlistPart(string filter, string PartNo_Id, string Tipe,string Status,string UserAd)
        {
            DataTable dt;
            //dt = DBConnection.ExecSelect("SELECT A.PartNo_Id,A.PartNo,A.Contract,B.[desc],A.description,A.unit_meas,A.part_product_family,A.part_product_code,A.spesifikasi,A.accounting_group, " +
            //    "A.id_pertanyaanumur,A.id_pertanyaanestimasi,A.id_pertanyaanfungsi,A.status_barang,A.doc_state_id,C.state_code,A.createdBy,D.UserId, " +
            //    "(SELECT 'Di Reject Oleh '+  T2.UserID + ', Alasan : ' + T1.Reason collate SQL_Latin1_General_CP1_CI_AS FROM [T_Approval] T1 INNER JOIN T_MsUser T2 ON T1.ApproverID=T2.Emp_No  Where T1.ApprID=(SELECT MAX(T0.ApprID) FROM [T_Approval] T0 Where T0.PartNo=A.PartNo AND ISNULL(T0.Reason,'')<>'' AND T0.IsActive=0 )) Alasan " +
            //    "FROM [T_PartNumber] A LEFT JOIN [T_MsSite] B ON B.Site = A.Contract AND B.[desc] NOT LIKE'%Deleted%' " +
            //    "LEFT JOIN [DOC_STATE] C ON C.doc_state_id = A.doc_state_id LEFT JOIN [T_MsUser] D ON D.Emp_No= A.createdBy " +
            //    "Where (A.PartNo LIKE'%'+ @filter +'%' OR A.description LIKE'%'+ @filter +'%') AND A.Contract LIKE'%'+ @Contract +'%' AND A.tipe=@Tipe ",
            //    new string[] { "@filter", "@Contract", "@Tipe" }, new string[] { filter, contract, Tipe });
            dt = DBConnection.ExecSelect("SELECT A.PartNo_Id,A.PartNo,A.Contract,B.[desc],A.description,A.unit_meas,A.part_product_family,A.part_product_code,A.Tipe,A.spesifikasi,A.accounting_group, " +
            "A.id_pertanyaanumur,A.id_pertanyaanestimasi,A.id_pertanyaanfungsi,A.status_barang,A.doc_state_id,C.state_code,A.createdBy,D.UserId, " +
            "(SELECT 'Di Reject Oleh '+  T2.UserID + ', Alasan : ' + T1.Reason collate SQL_Latin1_General_CP1_CI_AS FROM [T_Approval] T1 INNER JOIN T_MsUser T2 ON T1.ApproverID=T2.Emp_No  Where T1.ApprID=(SELECT MAX(T0.ApprID) FROM [T_Approval] T0 Where T0.PartNo_Id=A.PartNo_Id AND ISNULL(T0.Reason,'')<>'' AND T0.IsActive=0 )) Alasan, " +
            "Convert(bit,Case When A.Tipe ='Teknik' THEN 1 Else 0 End) VisTek,Convert(bit,Case When A.Tipe ='Umum' THEN 1 Else 0 End) VisUmum " +
            "FROM [T_PartNumber] A LEFT JOIN [T_MsSite] B ON B.Site = A.Contract AND B.[desc] NOT LIKE'%Deleted%' " +
            "LEFT JOIN [DOC_STATE] C ON C.doc_state_id = A.doc_state_id LEFT JOIN [T_MsUser] D ON D.Emp_No= A.createdBy " +
            "Where (@PartNo_Id =0 or A.PartNo_Id=@PartNo_Id) AND A.tipe LIKE'%'+@Tipe+'%' AND (@Status=0 or A.doc_state_id=@Status) AND (A.PartNo LIKE'%'+ @filter +'%' OR A.description LIKE'%'+ @filter +'%' ) AND  (@UserId ='' or A.Contract IN(SELECT T1.Contract FROM T_MsUser T0 INNER JOIN T_MsEmployee T1 ON T0.Emp_No = T1.Emp_No Where T0.UserId=@UserId)) ",
            new string[] { "@filter", "@PartNo_Id", "@Tipe", "@Status", "@UserId" }, new string[] { filter, PartNo_Id, Tipe, Status, UserAd });
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetNextApprove(string PartNo_Id, string UserAd,string Tipe)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT Emp_No,DeptID,FieldId,Description,UserID FROM [ViewNextApporve] " +
                "Where FieldId =dbo.[fnAutorizeApprove](@PartNo_Id,@Tipe) AND UserID=@UserAd ",
                new string[] { "@PartNo_Id","@UserAd", "@Tipe" }, new string[] { PartNo_Id, UserAd, Tipe });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }


        public static DataTable GetEmployeeDeptTeknik(string UserAd)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT T0.Emp_No,T0.DeptID,T1.Description,T2.UserID FROM [T_MsDepartmentEmployee] T0 " +
                "INNER JOIN [T_MsDepartment] T1 ON T0.DeptID = T1.DeptID INNER JOIN [T_MsUser] T2 ON T2.Emp_No = T0.Emp_No " +
                "Where T2.UserID=@UserAd AND T0.DeptID IN(1,20,21,11,6,5,19,29) ",
                new string[] { "@UserAd" }, new string[] { UserAd });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetEmployeeDeptNonTeknik(string UserAd)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT T0.Emp_No,T0.DeptID,T1.Description,T2.UserID " +
                "FROM [T_MsDepartmentEmployee] T0 INNER JOIN [T_MsDepartment] T1 ON T0.DeptID = T1.DeptID INNER JOIN [T_MsUser] T2 ON T2.Emp_No = T0.Emp_No " +
                "Where T2.UserID=@UserAd AND T0.DeptID IN(1,20,21,7,12,6,11) ",
                new string[] { "@UserAd" }, new string[] { UserAd });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetEmployeeDept(string UserAd)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT T0.Emp_No,T0.DeptID,T1.Description,T2.UserID,Case When T0.DeptID IN(1,6,11) THEN '%' when T0.DeptID IN(5,19,29) THEN 'Teknik' Else 'Umum' END Tipe " +
                "FROM [T_MsDepartmentEmployee] T0 INNER JOIN [T_MsDepartment] T1 ON T0.DeptID = T1.DeptID INNER JOIN [T_MsUser] T2 ON T2.Emp_No = T0.Emp_No " +
                "Where T2.UserID=@UserAd AND T0.DeptID IN(1,20,21,11,6,19,29,7,5,12) ",
                new string[] { "@UserAd" }, new string[] { UserAd });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable GetApproveNm(string PartId, string Tipe)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT A.PartNo_Id,A.FieldId,A.ApproverID,B.UserID FROM [dbo].[T_Approval] A INNER JOIN [T_MsUser] B ON A.ApproverID = B.Emp_No " +
                "Where PartNo_Id=@PartNo_Id AND Tipe=@Tipe AND A.IsActive=1 ",
           new string[] { "@PartNo_Id", "@Tipe" }, new string[] { PartId,  Tipe });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }


        public static DataTable GetCek(string PartId,string Tipe, string UserAd)
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT A.PartNo_Id,A.FieldId,A.ApproverID,B.UserID FROM [dbo].[T_Approval] A INNER JOIN [T_MsUser] B ON A.ApproverID = B.Emp_No " +
                "Where PartNo_Id=@PartNo_Id AND A.Tipe=@Tipe AND B.UserID=@UserAd",
           new string[] { "@PartNo_Id", "@Tipe", "@UserAd" }, new string[] { PartId, Tipe, UserAd });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public static DataTable DocState()
        {
            DataTable dt;
            dt = DBConnection.ExecSelect("SELECT 0 doc_state_id,'ALL' state_code UNION ALL SELECT doc_state_id,state_code FROM [dbo].[DOC_STATE] where doc_state_id<>5 ",
           new string[] { ""}, new string[] { "" });

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

    }
}