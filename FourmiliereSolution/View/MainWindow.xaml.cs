using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FourmiliereSolution.Model;
using Microsoft.Win32;

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
            FabriqueGeneral.AjouterFourmiliereAuHasard(App.MainVM.Terrain, App.MainVM.Dim, 10);
            FabriqueGeneral.AjouterNourritureAuHasard(App.MainVM.Terrain, App.MainVM.Dim, 10);
            FabriqueGeneral.AjouterNourritureAuHasard(App.MainVM.Terrain, App.MainVM.Dim, 10);

            DataContext = App.MainVM;
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

            for (int i = 0; i < App.MainVM.Dim; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.MainVM.Dim; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            Plateau.ShowGridLines = true;

            foreach(CaseAbstrait refCase in App.MainVM.Terrain.Cases)
            {
                if(refCase.ContientFourmiliere())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Ressource/maison.png", UriKind.Relative));
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, refCase.CordX);
                    Grid.SetRow(img, refCase.CordY);
                }
                else if (refCase.ContientNourriture())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Ressource/nourriture.png", UriKind.Relative));
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, refCase.CordX);
                    Grid.SetRow(img, refCase.CordY);
                }
                if (refCase.ContientFourmis())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Ressource/fourmi.png", UriKind.Relative));
                    foreach (Model.Fourmi fourmi in refCase.Fourmis)
                    {
                        if (fourmi.StrategieFourmi is StrategieRetourMaison)
                        {
                            img.Source = new BitmapImage(new Uri("/Ressource/fourmi_maison.png", UriKind.Relative));
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
            DessinePlateau();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            DessinePlateau();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            App.MainVM.tourSuivant();
            DessinePlateau();
        }

        private void btnAvance_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            Thread tt = new Thread(App.MainVM.avance);
            tt.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            App.MainVM.stop();
        }

        private void Sauvegarder_Document(object sender, RoutedEventArgs e)
        {

            dt.Stop();
            App.MainVM.stop();

            var MainVMXML = new System.Xml.Linq.XElement("MainVM");
            MainVMXML.Add(new System.Xml.Linq.XElement("Dim", App.MainVM.Dim));
            MainVMXML.Add(new System.Xml.Linq.XElement("TitreApplication", App.MainVM.TitreApplication));
            MainVMXML.Add(new System.Xml.Linq.XElement("VitesseExec", App.MainVM.VitesseExec));
            MainVMXML.Add(new System.Xml.Linq.XElement("Runnin", false));

            var TerrainXML = new System.Xml.Linq.XElement("Terrain");
            TerrainXML.Add(new System.Xml.Linq.XElement("NbTours",App.MainVM.Terrain.NbTours));
            var CasesXML = new System.Xml.Linq.XElement("Cases");
            foreach(CaseAbstrait caseAbs in App.MainVM.Terrain.Cases)
            {
                var CaseXML = new System.Xml.Linq.XElement("Case");
                CaseXML.Add(new System.Xml.Linq.XElement("Class", caseAbs.GetType().Name));
                CaseXML.Add(new System.Xml.Linq.XElement("CordX", caseAbs.CordX));
                CaseXML.Add(new System.Xml.Linq.XElement("CordY", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("PheromoneMaison", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("PheromoneNourriture", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("NbTours", caseAbs.CordY));
                if(caseAbs is CaseNourriture)
                {
                    var NourritureXML = new System.Xml.Linq.XElement("Nourriture");
                    NourritureXML.Add(new System.Xml.Linq.XElement("Poids", ((CaseNourriture)caseAbs).Nourriture.Poids));
                    CaseXML.Add(NourritureXML);
                }
                if (caseAbs is CaseFourmiliere)
                {
                    var FourmiliereXML = new System.Xml.Linq.XElement("Fourmiliere");
                    FourmiliereXML.Add(new System.Xml.Linq.XElement("NombreNourritures", ((CaseFourmiliere)caseAbs).Fourmiliere.NombreNourritures));
                    FourmiliereXML.Add(new System.Xml.Linq.XElement("NombreTours", ((CaseFourmiliere)caseAbs).Fourmiliere.NombreTours));
                    CaseXML.Add(FourmiliereXML);
                }
                var FourmisXML = new System.Xml.Linq.XElement("Fourmis");
                foreach(Fourmi fourmi in caseAbs.Fourmis)
                {
                    var FourmiXML = new System.Xml.Linq.XElement("Fourmi");
                    FourmiXML.Add(new System.Xml.Linq.XElement("Vie", fourmi.Vie));
                    FourmiXML.Add(new System.Xml.Linq.XElement("StrategieFourmi", fourmi.StrategieFourmi.GetType().Name));
                    FourmisXML.Add(FourmiXML);
                }
                CaseXML.Add(FourmisXML);
                CasesXML.Add(CaseXML);
            }
            TerrainXML.Add(CasesXML);
            MainVMXML.Add(TerrainXML);

            var StatistiqueXML = new System.Xml.Linq.XElement("Statistique");
            StatistiqueXML.Add(new System.Xml.Linq.XElement("NombreFourmis", App.MainVM.Statistique.NombreFourmis));
            StatistiqueXML.Add(new System.Xml.Linq.XElement("NombreTours", App.MainVM.Statistique.NombreTours));
            MainVMXML.Add(StatistiqueXML);

            var Sauvegarde = new System.Xml.Linq.XDocument(MainVMXML);

            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            sfDialog.DefaultExt = "xml";
            sfDialog.AddExtension = true;
            sfDialog.Filter = "XML Files|*.xml|All Files|*.*";
            sfDialog.FilterIndex = 1;
            if (sfDialog.ShowDialog() == true && sfDialog.FileName.Length > 0)
            {
                Sauvegarde.Save(sfDialog.FileName);
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Fichier Sauvegardé !", "Sauvegarde", MessageBoxButton.OK);
            }
        }
    }
}
