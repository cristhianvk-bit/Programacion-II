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
    /// Lógica de interacción para log_in.xaml
    /// </summary>
    public partial class log_in : Window
    {
        public log_in()
        {
            InitializeComponent();
        } 

        private void Btn_IniciarSesion(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string clave = txtclave.Password;

            // Validación básica temporal (la lógica real vendrá después)
            if (usuario == "admin" && clave == "1234")
            {
                MessageBox.Show("Ingreso exitoso", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);

                // Aquí abres la ventana del panel administrador
                Admin_Panel ventana = new Admin_Panel();
                ventana.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }  
    }
}
