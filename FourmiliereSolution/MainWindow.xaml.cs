using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FourmiliereSolution
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(redessine_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 200);
            DessinePlateau();
            DataContext = App.FourmiliereVM;
        }

        private void redessine_Tick(object sender, EventArgs e)
        {
            DessinePlateau();
        }

        private void DessinePlateau()
        {

            Plateau.ColumnDefinitions.Clear();
            Plateau.RowDefinitions.Clear();
            Plateau.Children.Clear();

            for (int i = 0; i < App.FourmiliereVM.DimX; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.FourmiliereVM.DimY; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            Plateau.ShowGridLines = true;

            Ellipse e = new Ellipse();
            e.Fill = new SolidColorBrush(Colors.Black);
            Plateau.Children.Add(e);
            Grid.SetColumn(e, 1);
            Grid.SetRow(e, 1);



            foreach(var fourmi in App.FourmiliereVM.FourmisList)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("/fourmi_preview.png", UriKind.Relative));
                Plateau.Children.Add(img);
                Grid.SetColumn(img, fourmi.X);
                Grid.SetRow(img, fourmi.Y);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.ajouterFourmi();
            DessinePlateau();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.supprimerFourmi();
            DessinePlateau();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.tourSuivant();
            DessinePlateau();
        }

        private void btnAvance_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            Thread tt = new Thread(App.FourmiliereVM.avance);
            tt.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            App.FourmiliereVM.stop();
        }
    }
}
