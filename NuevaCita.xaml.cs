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
    /// Lógica de interacción para NuevaCita.xaml
    /// </summary>
    public partial class NuevaCita : Window
    {
        private ClienteMascota ventanaPrincipal;
        public NuevaCita(ClienteMascota ventana)
        {
            InitializeComponent();
            this.ventanaPrincipal = ventana;
        }
        private void GuardarRe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtNombreMascota.Text) ||
                string.IsNullOrWhiteSpace(txtTipoMascota.Text) ||
                string.IsNullOrWhiteSpace(txtRazaMascota.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            Cliente c = new Cliente()
            {
                Nombre = txtNombreCliente.Text,
                Telefono = txtTelefonoCliente.Text,
                Correo = txtCorreoCliente.Text
            };

            Mascota m = new Mascota()
            {
                Nombre = txtNombreMascota.Text,
                Tipo = txtTipoMascota.Text,
                Raza = txtRazaMascota.Text
            };

            ventanaPrincipal.AgregarRegistro(c, m);

            MessageBox.Show("Registro añadido correctamente");
            this.Close();
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
