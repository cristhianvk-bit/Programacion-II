using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;

namespace Michis_Veterinaria
{
    /// <summary>
    /// Lógica de interacción para GestionaVet.xaml
    /// </summary>
    public partial class GestionaVet : Window
    {
        private ObservableCollection<Veterinario> veterinarios = new ObservableCollection<Veterinario>();
        public GestionaVet()
        {
            InitializeComponent();
            dgVeterinarios.ItemsSource = veterinarios;

            // copia datos a la clase global
            Estadisticas.Veterinarios = veterinarios.ToList();

        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            NuevoVete form = new NuevoVete();
            form.Owner = this;

            if (form.ShowDialog() == true)
            {
                form.VeterinarioResultado.Id = veterinarios.Count + 1;
                veterinarios.Add(form.VeterinarioResultado);
            }
            Estadisticas.Veterinarios = veterinarios.ToList();
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el Veterinario desde el DataContext del botón
            if (sender is Button btn && btn.DataContext is Veterinario vet)
            {
                // Abrimos la ventana pasando el veterinario
                NuevoVete form = new NuevoVete(vet);
                form.Owner = this;

                // Si el usuario guarda cambios
                if (form.ShowDialog() == true)
                {
                    dgVeterinarios.Items.Refresh(); // actualizar tabla
                    Estadisticas.Veterinarios = veterinarios.ToList(); // actualizar estadísticas
                }
            }
            else
            {
                MessageBox.Show("No se pudo obtener los datos del veterinario.", "Error");
            }
        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgVeterinarios.SelectedItem is Veterinario vetSeleccionado)
            {
                if (MessageBox.Show($"¿Eliminar a {vetSeleccionado.Nombre}?", "Confirmar",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    veterinarios.Remove(vetSeleccionado);
                    dgVeterinarios.Items.Refresh();
                    txtContador.Text = $"Total de veterinarios: {veterinarios.Count}";
                }
            }
            Estadisticas.Veterinarios = veterinarios.ToList();
        }
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Serializar en JSON
                var opciones = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(veterinarios, opciones);

                // --- GUARDAR JSON ---
                var guarda = new Microsoft.Win32.SaveFileDialog();
                guarda.Title = "Guardar archivo JSON";
                guarda.Filter = "Archivo JSON (*.json)|*.json";
                guarda.FileName = "veterinarios.json";

                if (guarda.ShowDialog() == true)
                {
                    File.WriteAllText(guarda.FileName, json);
                }

                // --- GUARDAR TXT ---
                var saveTxt = new Microsoft.Win32.SaveFileDialog();
                saveTxt.Title = "Guardar archivo TXT";
                saveTxt.Filter = "Archivo de texto (*.txt)|*.txt";
                saveTxt.FileName = "veterinarios.txt";

                if (saveTxt.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(saveTxt.FileName))
                    {
                        sw.WriteLine("=== LISTA DE VETERINARIOS ===");
                        foreach (var vet in veterinarios)
                        {
                            sw.WriteLine($"ID: {vet.Id}");
                            sw.WriteLine($"Nombre: {vet.Nombre}");
                            sw.WriteLine($"Especialidad: {vet.Especialidad}");
                            sw.WriteLine($"Teléfono: {vet.Telefono}");
                            sw.WriteLine($"Correo: {vet.Correo}");
                            sw.WriteLine(new string('-', 30));
                        }
                    }
                }

                MessageBox.Show("Datos guardados correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Botón Cargar
        private void BtnCargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var abre = new Microsoft.Win32.OpenFileDialog();
                abre.Title = "Cargar archivo JSON de veterinarios";
                abre.Filter = "Archivo JSON (*.json)|*.json";

                if (abre.ShowDialog() == true)
                {
                    string json = File.ReadAllText(abre.FileName);
                    var datos = JsonSerializer.Deserialize<ObservableCollection<Veterinario>>(json);

                    if (datos != null)
                    {
                        veterinarios.Clear();
                        foreach (var vet in datos)
                        {
                            veterinarios.Add(vet);
                        }
                    }

                    MessageBox.Show("Datos cargados correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Buscar veterinarios
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = txtBuscar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(texto))
            {
                dgVeterinarios.ItemsSource = veterinarios;
            }
            else
            {
                var filtrados = veterinarios.Where(v =>
                    v.Nombre.ToLower().Contains(texto) ||
                    v.Especialidad.ToLower().Contains(texto) ||
                    v.Telefono.Contains(texto) ||
                    v.Correo.ToLower().Contains(texto)
                ).ToList();

                dgVeterinarios.ItemsSource = filtrados;
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
