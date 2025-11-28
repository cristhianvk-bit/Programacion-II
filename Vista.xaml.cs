using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Michis_Veterinaria
{
    /// <summary>
    /// Lógica de interacción para Vista.xaml
    /// </summary>
    public partial class Vista : Window
    {
        public Vista()
        {
            InitializeComponent();
            CargarDashboard();
        }
        private void CargarDashboard()
        {
            // Por ahora son datos falsos
            // Luego los conectamos a la base de datos o archivos JSON

            txtCitasHoy.Text = "5";
            txtProximas.Text = "12";
            txtClientes.Text = "28";
            txtMascotas.Text = "43";
        }
    }
}
