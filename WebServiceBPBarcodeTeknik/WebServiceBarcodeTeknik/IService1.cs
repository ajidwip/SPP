using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServiceBarcodeTeknik
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        PartNoAutoComplete GetPartNoAutoComplete();


        [OperationContract]
        Site GetSite();
        [OperationContract]
        Rak GetRak();

        [OperationContract]
        void TambahNoUrut();

        [OperationContract]
        void synwo();

        [OperationContract]
        void synqty();

        [OperationContract]
        void changepassword(string UserId,string oldpassword,string NewPAssword);

        [OperationContract]
        void updateWoNumber(string TransactionID, string WO);

        [OperationContract]
        cek cekuser(string UserId,string password);

        [OperationContract]
        TrxId getID();

        [OperationContract]
        PartNo getpart(string part);

        [OperationContract]
        cek getdept(string UserId);

        [OperationContract]
        void uploadphoto(byte[] img, string name);

        [OperationContract]
        void uploadphoto2(byte[] img, string name);

        [OperationContract]
        void uploadphoto3(byte[] img, string name);

        [OperationContract]
        void deleteWo(string WO);

        [OperationContract]
        void deletepemakaian(string WO,string partNo,string TransactionId,string contract, string RakNo);

        [OperationContract]
        void deletestokcopname(string partNo, string opanamedate, string contract);

        [OperationContract]
        void closeWO(string WO, string TransactionId);

        [OperationContract]
        void InsertWOTransaction(string qty, string WONo, string contract, string partno,string TransactionId, string userid, string RakNo);

        [OperationContract]
        void stockopname(string qty, string partno,string Contract, string Rak_No, string tanggal);

        [OperationContract]
        void updateqtystockopname(string qty, string partno, string contract, string opanamedate);

        [OperationContract]
        void unissued(string qty, string WONo, string contract, string partno, string TransactionId, string RakNo);

        [OperationContract]
        void updatejumlahpakai(string qty, string WONo, string contract, string partno, string TransactionId,string RakNo);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        Contract GetContract(string wo, string contract);

        [OperationContract]
        Opname GetOpname(string tanggal);

        [OperationContract]
        cekSite ceksite1(string site);

        [OperationContract]
        cekRak cekrak1(string rakno);

        [OperationContract]
        rakno getrakno(string partno);

        [OperationContract]
        opanameitem GetOpanameitem(string partno);
        // TODO: Add your service operations here
    }
    [DataContract]
    public class cekSite
    {
        [DataMember]
        public int count1
        {
            get;
            set;
        }
      
    }
    [DataContract]
    public class cekRak
    {
        [DataMember]
        public int count2
        {
            get;
            set;
        }

    }
    [DataContract]
    public class cek
    {
        [DataMember]
        public int count
        {
            get;
            set;
        }
        [DataMember]
        public string dept { get; set; }
    }
    [DataContract]
    public class TrxId
    {
        [DataMember]
        public int TrxId1
        {
            get;
            set;
        }
      
    }
    [DataContract]
    public class PartNo
    {
        [DataMember]
        public string PartNo1
        {
            get;
            set;
        }

    }
    [DataContract]
    public class rakno
    {
        [DataMember]
        public string rakno1
        {
            get;
            set;
        }

    }
    [DataContract]
    public class opanameitem
    {
        [DataMember]
        public DataTable opanameitemtable
        {
            get;
            set;
        }

    }
    [DataContract]
    public class Opname
    {
        [DataMember]
        public DataTable OpnameTable
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Contract
    {
        [DataMember]
        public DataTable ContracTable
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Rak
    {
        [DataMember]
        public DataTable Raktable
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Site
    {
        [DataMember]
        public DataTable SiteTable
        {
            get;
            set;
        }
    }

    [DataContract]
    public class PartNoAutoComplete
    {
        [DataMember]
        public DataTable PartNoAutoCompleteTable
        {
            get;
            set;
        }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
