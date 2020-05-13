using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeTeknik.GetterSetter
{
    public class Pertanyaangetset
    {
        public int Role_id { get; set; }
        public int parent_pertanyaan { get; set; }
        public int id_Pertanyaan { get; set; }
        public string Pertanyaan { get; set; }
        public string AcctGroup { get; set; }
        public string Tipe { get; set; }
        public string Status { get; set; }
    }
}