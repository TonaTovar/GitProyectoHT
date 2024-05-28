using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public  class Vuelos
    {
        public int Id_Vuelo { get; set; }
        public int Numero_Vuelo { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set;}
        public string Hora_Salida { get; set;}
        public string Hora_LLegada { get;set; }
        public ML.Aerolinea aerolinia { get; set; }
        public List<ML.Vuelos> ListVuelos { get; set; }
    }
}
