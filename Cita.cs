using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michis_Veterinaria
{
    public class Cita
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Mascota { get; set; }
        public string Estado { get; set; }
    }
}
