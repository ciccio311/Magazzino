using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Server
{
    class ProdottoServer
    {
        public int id { get; set; }
        public string nome { get; set; }

        public int produttore { get; set; }
        public float prezzo { get; set; }
        public int categoria { get; set; }
    }
}
