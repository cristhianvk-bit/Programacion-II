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
    /// Lógica de interacción para Admin_Panel.xaml
    /// </summary>
    public partial class Admin_Panel : Window
    {
        public Admin_Panel()
        {
            InitializeComponent();
        }
        private void BtnVista_Click(object sender, RoutedEventArgs e)
        {
            Vista ventana = new Vista();
            ventana.Show();
            this.Hide();
        }
        private void BtnGestion_Click(object sender, RoutedEventArgs e)
        {
            GestionaVet ventana = new GestionaVet();
            ventana.Show();
            this.Hide();
        }
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            ClienteMascota ventana = new ClienteMascota();
            ventana.Show();
            this.Hide();
        }
        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow inicial = new MainWindow();
            inicial.Show();
            this.Close();
        }
    }
}
