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
            VistaGeneral();
        }
        private void VistaGeneral()
        {
            ListaVete.Text = Estadisticas.Veterinarios.Count.ToString();
            Clientes.Text = Estadisticas.Clientes.Count.ToString();
            Mascotas.Text = Estadisticas.Mascotas.Count.ToString();
            Citas.Text = Estadisticas.TotalCitas.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_Panel principal = new Admin_Panel();  
            principal.Show();
            this.Close();
        }
    }
}
