using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michis_Veterinaria
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Raza { get; set; }
        public int IdDueno { get; set; }  
    }
}
