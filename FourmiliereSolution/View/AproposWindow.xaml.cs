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

namespace FourmiliereSolution
{
    /// <summary>
    /// Logique d'interaction pour AProposWindow.xaml
    /// </summary>
    public partial class AProposWindow : Window
    {
        public AProposWindow()
        {
            InitializeComponent();
            DataContext = new AProposViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
