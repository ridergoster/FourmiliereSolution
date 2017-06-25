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
using FourmiliereSolution.Model;

namespace FourmiliereSolution
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        public FabriqueGeneral FabriqueGeneral { get; set; } = new FabriqueGeneral();
        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(redessine_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 200);
            FabriqueGeneral.AjouterFourmiliereAuHasard(App.FourmiliereVM.Terrain, App.FourmiliereVM.Dim, 10);
            FabriqueGeneral.AjouterNourritureAuHasard(App.FourmiliereVM.Terrain, App.FourmiliereVM.Dim, 10);
            FabriqueGeneral.AjouterNourritureAuHasard(App.FourmiliereVM.Terrain, App.FourmiliereVM.Dim, 10);

            DataContext = App.FourmiliereVM;
            DessinePlateau();
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

            for (int i = 0; i < App.FourmiliereVM.Dim; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.FourmiliereVM.Dim; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            Plateau.ShowGridLines = true;

            foreach(CaseAbstrait refCase in App.FourmiliereVM.Terrain.Cases)
            {
                if(refCase.ContientFourmiliere())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/maison.png", UriKind.Relative));
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, refCase.CordX);
                    Grid.SetRow(img, refCase.CordY);
                }
                else if (refCase.ContientNourriture())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/nourriture.png", UriKind.Relative));
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, refCase.CordX);
                    Grid.SetRow(img, refCase.CordY);
                }
                if (refCase.ContientFourmis())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/fourmi.png", UriKind.Relative));
                    foreach (Model.Fourmi fourmi in refCase.Fourmis)
                    {
                        if (fourmi.StrategieFourmi is StrategieRetourMaison)
                        {
                            img.Source = new BitmapImage(new Uri("/fourmi_maison.png", UriKind.Relative));
                            break;
                        }
                    }
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, refCase.CordX);
                    Grid.SetRow(img, refCase.CordY);
                }
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
