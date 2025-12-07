using Microsoft.Win32;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Lógica de interacción para ClienteMascota.xaml
    /// </summary>
    public partial class ClienteMascota : Window
    {
        private List<Cliente> listaClientes = new List<Cliente>();
        private List<Mascota> listaMascotas = new List<Mascota>();
        public ClienteMascota()
        {
            InitializeComponent();
            dgClientes.ItemsSource = listaClientes;
            dgMascotas.ItemsSource = listaMascotas;
        }
        private void Boton_Cliente(object sender, RoutedEventArgs e)
        {
            NuevaCita ventana = new NuevaCita(this);
            ventana.ShowDialog();
        }
        private void Boton_Mascota(object sender, RoutedEventArgs e)
        {
            NuevaCita ventana = new NuevaCita(this);
            ventana.ShowDialog();
        }
        public void AgregarRegistro(Cliente c, Mascota m)
        {
            c.Id = listaClientes.Count + 1;
            m.Id = listaMascotas.Count + 1;

            m.IdDueno = c.Id; // Relacionar mascota con el cliente

            listaClientes.Add(c);
            listaMascotas.Add(m);

            dgClientes.Items.Refresh();
            dgMascotas.Items.Refresh();


            Estadisticas.TotalCitas++;
            Estadisticas.Clientes = listaClientes.ToList();
            Estadisticas.Mascotas = listaMascotas.ToList();
        }
        private void Boton_Guardar(object sender, RoutedEventArgs e)
        {
            // --- GUARDAR CLIENTES.JSON ---
            SaveFileDialog dlgClientes = new SaveFileDialog();
            dlgClientes.Filter = "Archivo JSON (*.json)|*.json";
            dlgClientes.Title = "Guardar clientes.json";
            dlgClientes.FileName = "clientes.json";

            if (dlgClientes.ShowDialog() == true)
            {
                string jsonClientes = JsonSerializer.Serialize(listaClientes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dlgClientes.FileName, jsonClientes);
            }

            // --- GUARDAR CLIENTES.TXT ---
            SaveFileDialog dlgClientesTxt = new SaveFileDialog();
            dlgClientesTxt.Filter = "Archivo de texto (*.txt)|*.txt";
            dlgClientesTxt.Title = "Guardar clientes.txt";
            dlgClientesTxt.FileName = "clientes.txt";

            if (dlgClientesTxt.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(dlgClientesTxt.FileName))
                {
                    foreach (var c in listaClientes)
                        sw.WriteLine($"{c.Id} | {c.Nombre} | {c.Telefono} | {c.Correo}");
                }
            }

            // --- GUARDAR MASCOTA JSON ---
            SaveFileDialog dlgMascotas = new SaveFileDialog();
            dlgMascotas.Filter = "Archivo JSON (*.json)|*.json";
            dlgMascotas.Title = "Guardar mascotas.json";
            dlgMascotas.FileName = "mascotas.json";

            if (dlgMascotas.ShowDialog() == true)
            {
                string jsonMascotas = JsonSerializer.Serialize(listaMascotas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dlgMascotas.FileName, jsonMascotas);
            }

            // --- GUARDAR MASCOTA TXT ---
            SaveFileDialog dlgMascotasTxt = new SaveFileDialog();
            dlgMascotasTxt.Filter = "Archivo de texto (*.txt)|*.txt";
            dlgMascotasTxt.Title = "Guardar mascotas.txt";
            dlgMascotasTxt.FileName = "mascotas.txt";

            if (dlgMascotasTxt.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(dlgMascotasTxt.FileName))
                {
                    foreach (var m in listaMascotas)
                        sw.WriteLine($"{m.Id} | {m.Nombre} | {m.Tipo} | {m.Raza} | Dueño:{m.IdDueno}");
                }
            }

            MessageBox.Show("Archivos guardados correctamente");
            {
                try
                {
                    if (File.Exists("clientes.json"))
                    {
                        string jsonC = File.ReadAllText("clientes.json");
                        listaClientes = JsonSerializer.Deserialize<List<Cliente>>(jsonC);
                    }

                    if (File.Exists("mascotas.json"))
                    {
                        string jsonM = File.ReadAllText("mascotas.json");
                        listaMascotas = JsonSerializer.Deserialize<List<Mascota>>(jsonM);
                    }

                    dgClientes.ItemsSource = listaClientes;
                    dgMascotas.ItemsSource = listaMascotas;

                    dgClientes.Items.Refresh();
                    dgMascotas.Items.Refresh();

                    MessageBox.Show("Datos cargados correctamente");
                }
                catch
                {
                    MessageBox.Show("Error al cargar: solo se permiten archivos JSON generados por el programa.");
                }
            }
        }
        private void Boton_Cargar(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cargar clientes.json
                OpenFileDialog openC = new OpenFileDialog();
                openC.Filter = "Archivo JSON (*.json)|*.json";
                openC.Title = "Seleccionar clientes.json";

                if (openC.ShowDialog() == true)
                {
                    string jsonC = File.ReadAllText(openC.FileName);
                    listaClientes = JsonSerializer.Deserialize<List<Cliente>>(jsonC);
                }

                // Cargar mascotas.json
                OpenFileDialog openM = new OpenFileDialog();
                openM.Filter = "Archivo JSON (*.json)|*.json";
                openM.Title = "Seleccionar mascotas.json";

                if (openM.ShowDialog() == true)
                {
                    string jsonM = File.ReadAllText(openM.FileName);
                    listaMascotas = JsonSerializer.Deserialize<List<Mascota>>(jsonM);
                }

                dgClientes.ItemsSource = listaClientes;
                dgMascotas.ItemsSource = listaMascotas;

                dgClientes.Items.Refresh();
                dgMascotas.Items.Refresh();

                MessageBox.Show("Datos cargados correctamente");
            }
            catch
            {
                MessageBox.Show("Error al cargar: solo se permiten archivos JSON válidos.");
            }
        }
        private void EliminarCliente(object sender, RoutedEventArgs e)
        {
            var cliente = (sender as FrameworkElement).DataContext as Cliente;

            if (cliente != null)
            {
                if (MessageBox.Show("¿Eliminar este cliente?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // eliminar también mascotas relacionadas
                    listaMascotas.RemoveAll(m => m.IdDueno == cliente.Id);

                    listaClientes.Remove(cliente);

                    dgClientes.Items.Refresh();
                    dgMascotas.Items.Refresh();
                }
            }
            Estadisticas.Clientes = listaClientes.ToList();
            Estadisticas.Mascotas = listaMascotas.ToList();
        }

        private void EliminarMascota(object sender, RoutedEventArgs e)
        {
            var mascota = (sender as FrameworkElement).DataContext as Mascota;

            if (mascota != null)
            {
                if (MessageBox.Show("¿Eliminar esta mascota?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    listaMascotas.Remove(mascota);
                    dgMascotas.Items.Refresh();
                }
            }
        }
        private void EditarCliente_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (sender as FrameworkElement).DataContext as Cliente;
            if (cliente != null)
            {
                NuevaCita ventana = new NuevaCita(this);
                ventana.ShowDialog();
            }
        }

        private void EditarMascota_Click(object sender, RoutedEventArgs e)
        {
            var mascota = (sender as FrameworkElement).DataContext as Mascota;
            if (mascota != null)
            {
                NuevaCita ventana = new NuevaCita(this);
                ventana.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_Panel principal = new Admin_Panel();
            principal.Show();   
            this.Close();
        }
    }
}
