using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Michis_Veterinaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btn_Admin(object sender, RoutedEventArgs e)
        {
            log_in ventana = new log_in();
            ventana.Show();
            this.Hide();
        }
        private void Btn_Usuario(object sender, RoutedEventArgs e)
        {

        }
    }
}