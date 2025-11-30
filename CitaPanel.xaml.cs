using System;
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
    /// Lógica de interacción para CitaPanel.xaml
    /// </summary>
    public partial class CitaPanel : Window
    {
        private List<Cita> citas = new List<Cita>();
        private string rutaJson = "citas.json";
        private string rutaTxt = "citas.txt";

        public CitaPanel()
        {
            InitializeComponent();
            TablaCitas.ItemsSource = citas; // Enlazar la lista al DataGrid

        }
        private void Btn_NuevaCita(object sender, RoutedEventArgs e)
        {
            NuevaCita ventana = new NuevaCita();
            if (ventana.ShowDialog() == true) // Si guardó
            {
                citas.Add(ventana.CitaCreada); // Agregar la nueva cita
                TablaCitas.Items.Refresh();    // Refrescar la tabla
            }

        }

        private void Btn_Editar(object sender, RoutedEventArgs e)
        {
            if (TablaCitas.SelectedItem is Cita citaSeleccionada)
            {
                NuevaCita ventana = new NuevaCita(citaSeleccionada);
                if (ventana.ShowDialog() == true)
                {
                    // Actualizar la cita editada
                    int index = citas.IndexOf(citaSeleccionada);
                    citas[index] = ventana.CitaCreada;
                    TablaCitas.Items.Refresh();
                }
            }

        }
        private void Btn_Eliminar(object sender, RoutedEventArgs e)
        {
            if (TablaCitas.SelectedItem is Cita citaSeleccionada)
            {
                citas.Remove(citaSeleccionada);
                TablaCitas.Items.Refresh();
            }
        }

        private void Btn_Guardar(object sender, RoutedEventArgs e)
        {
            try
            {
                // Guardar JSON
                string json = JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(rutaJson, json);

                // Guardar TXT
                using (StreamWriter sw = new StreamWriter(rutaTxt))
                {
                    foreach (var cita in citas)
                    {
                        sw.WriteLine($"{cita.Fecha:dd/MM/yyyy}|{cita.Cliente}|{cita.Mascota}|{cita.Estado}");
                    }
                }

                MessageBox.Show("Citas guardadas en JSON y TXT correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error");
            }

        }

        private void Btn_Cargar(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(rutaJson))
                {
                    string json = File.ReadAllText(rutaJson);
                    citas = JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
                }
                else if (File.Exists(rutaTxt))
                {
                    var nuevasCitas = new List<Cita>();
                    foreach (var linea in File.ReadAllLines(rutaTxt))
                    {
                        var partes = linea.Split('|');
                        if (partes.Length == 4)
                        {
                            nuevasCitas.Add(new Cita
                            {
                                Fecha = DateTime.Parse(partes[0]),
                                Cliente = partes[1],
                                Mascota = partes[2],
                                Estado = partes[3]
                            });
                        }
                    }
                    citas = nuevasCitas;
                }
                else
                {
                    MessageBox.Show("No existe archivo JSON ni TXT para cargar.", "Aviso");
                }

                TablaCitas.ItemsSource = citas;
                TablaCitas.Items.Refresh();
                MessageBox.Show("Citas cargadas correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error");
            }

        }
    }
}
