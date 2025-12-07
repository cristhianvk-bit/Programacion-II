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
    /// Lógica de interacción para NuevoVete.xaml
    /// </summary>
    public partial class NuevoVete : Window
    {
        public Veterinario VeterinarioResultado { get; private set; }

        // Constructor para nuevo veterinario
        public NuevoVete()
        {
            InitializeComponent();
            txtTitulo.Text = "Nuevo Veterinario";
        }

        // Constructor para editar veterinario
        public NuevoVete(Veterinario veterinario)
        {
            InitializeComponent();
            txtTitulo.Text = "Editar Veterinario";
            txtNombre.Text = veterinario.Nombre;
            txtEspecialidad.Text = veterinario.Especialidad;
            txtTelefono.Text = veterinario.Telefono;
            txtCorreo.Text = veterinario.Correo;
            VeterinarioResultado = veterinario;
        }


        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                MessageBox.Show("La especialidad es obligatoria", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (VeterinarioResultado == null)
            {
                VeterinarioResultado = new Veterinario();
            }

            VeterinarioResultado.Nombre = txtNombre.Text;
            VeterinarioResultado.Especialidad = txtEspecialidad.Text;
            VeterinarioResultado.Telefono = txtTelefono.Text;
            VeterinarioResultado.Correo = txtCorreo.Text;

            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }   
}
