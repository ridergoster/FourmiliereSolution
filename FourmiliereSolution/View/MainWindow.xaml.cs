using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FourmiliereSolution.Model;
using Microsoft.Win32;
using System.Xml;
using System.Windows.Input;

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
            App.MainVM.Ouvrir = new ActionCommand(OuvrirFichier);
            App.MainVM.Sauvegarder = new ActionCommand(SauvegarderFichier);
            App.MainVM.APropos = new ActionCommand(OuvrirAPropos);
            App.MainVM.Quitter = new ActionCommand(QuitterApp);
            App.MainVM.ActionClick = CaseDetail;
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

        private void SauvegarderFichier()
        {
            App.MainVM.stop();
            while (App.MainVM.Runnin)
            {

            }

            var MainVMXML = new System.Xml.Linq.XElement("MainVM");
            MainVMXML.Add(new System.Xml.Linq.XElement("Dim", App.MainVM.Dim));
            MainVMXML.Add(new System.Xml.Linq.XElement("TitreApplication", App.MainVM.TitreApplication));
            MainVMXML.Add(new System.Xml.Linq.XElement("VitesseExec", App.MainVM.VitesseExec));
            MainVMXML.Add(new System.Xml.Linq.XElement("Runnin", false));

            var TerrainXML = new System.Xml.Linq.XElement("Terrain");
            TerrainXML.Add(new System.Xml.Linq.XElement("NbTours", App.MainVM.Terrain.NbTours));
            var CasesXML = new System.Xml.Linq.XElement("Cases");
            foreach (CaseAbstrait caseAbs in App.MainVM.Terrain.Cases)
            {
                var CaseXML = new System.Xml.Linq.XElement("Case");
                CaseXML.Add(new System.Xml.Linq.XElement("Class", caseAbs.GetType().Name));
                CaseXML.Add(new System.Xml.Linq.XElement("CordX", caseAbs.CordX));
                CaseXML.Add(new System.Xml.Linq.XElement("CordY", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("PheromoneMaison", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("PheromoneNourriture", caseAbs.CordY));
                CaseXML.Add(new System.Xml.Linq.XElement("NbTours", caseAbs.CordY));
                if (caseAbs is CaseNourriture)
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
                foreach (Fourmi fourmi in caseAbs.Fourmis)
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

            SaveFileDialog SFDialog = new SaveFileDialog();
            SFDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            SFDialog.DefaultExt = "xml";
            SFDialog.AddExtension = true;
            SFDialog.Filter = "XML Files|*.xml|All Files|*.*";
            SFDialog.FilterIndex = 1;
            if (SFDialog.ShowDialog() == true && SFDialog.FileName.Length > 0)
            {
                Sauvegarde.Save(SFDialog.FileName);
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Fichier Sauvegardé !", "Sauvegarde", MessageBoxButton.OK);
            }
        }
        private void OuvrirFichier()
        {
            App.MainVM.stop();
            while (App.MainVM.Runnin)
            {

            }

            OpenFileDialog OFDialog = new OpenFileDialog();
            OFDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            OFDialog.DefaultExt = "xml";
            OFDialog.Filter = "XML Files|*.xml|All Files|*.*";
            OFDialog.FilterIndex = 1;
            if (OFDialog.ShowDialog() == true && OFDialog.FileName.Length > 0)
            {
                // CHARGER LE FICHIER ET VERIFIER SA VALIDITE
                XmlDocument Chargement = new XmlDocument();
                Chargement.Load(OFDialog.FileName);
                XmlNode MainVMXML;
                if ((MainVMXML = Chargement.SelectSingleNode("MainVM")) == null)
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Fichier incompatible !", "Chargement", MessageBoxButton.OK);
                    return;
                }

                // RECUPERER DATA DE MAINVM
                int Dim = int.Parse(MainVMXML.SelectSingleNode("Dim").InnerText);
                String TitreApplication = MainVMXML.SelectSingleNode("TitreApplication").InnerText;
                int VitesseExec = int.Parse(MainVMXML.SelectSingleNode("VitesseExec").InnerText);
                bool Runnin = bool.Parse(MainVMXML.SelectSingleNode("Runnin").InnerText);

                Terrain Terrain = new Terrain(Dim);

                // RECUPERER DATA DE TERRAIN
                XmlNode TerrainXML = MainVMXML.SelectSingleNode("Terrain");
                int NbToursTerrain = int.Parse(TerrainXML.SelectSingleNode("NbTours").InnerText);
                XmlNode CasesXML = TerrainXML.SelectSingleNode("Cases");
                foreach (XmlNode CaseXML in CasesXML.SelectNodes("Case"))
                {
                    CaseAbstrait Case;
                    String CaseClass = CaseXML.SelectSingleNode("Class").InnerText;
                    int CordX = int.Parse(CaseXML.SelectSingleNode("CordX").InnerText);
                    int CordY = int.Parse(CaseXML.SelectSingleNode("CordY").InnerText);
                    int PheromoneMaison = int.Parse(CaseXML.SelectSingleNode("PheromoneMaison").InnerText);
                    int PheromoneNourriture = int.Parse(CaseXML.SelectSingleNode("PheromoneNourriture").InnerText);
                    int NbToursCase = int.Parse(CaseXML.SelectSingleNode("NbTours").InnerText);

                    if (CaseClass == "CaseNourriture")
                    {
                        Case = new CaseNourriture(Terrain, CordX, CordY);
                        int NourriturePoids = int.Parse(CaseXML.SelectSingleNode("Nourriture/Poids").InnerText);
                        Nourriture Nourriture = new Nourriture(Case, NourriturePoids);
                        ((CaseNourriture)Case).Nourriture = Nourriture;
                    }
                    else if (CaseClass == "CaseFourmiliere")
                    {
                        Case = new CaseFourmiliere(Terrain, CordX, CordY);
                        int NombreNourritures = int.Parse(CaseXML.SelectSingleNode("Fourmiliere/NombreNourritures").InnerText);
                        int NombreToursFourmiliere = int.Parse(CaseXML.SelectSingleNode("Fourmiliere/NombreTours").InnerText);
                        Fourmiliere Fourmiliere = new Fourmiliere(Case, 0);
                        Fourmiliere.NombreNourritures = NombreNourritures;
                        Fourmiliere.NombreTours = NombreToursFourmiliere;
                        ((CaseFourmiliere)Case).Fourmiliere = Fourmiliere;
                    }
                    else if (CaseClass == "CaseNormal")
                    {
                        Case = new CaseNormal(Terrain, CordX, CordY);
                    }
                    else
                    {
                        System.Media.SystemSounds.Beep.Play();
                        MessageBox.Show("Erreur au chargement", "Chargement", MessageBoxButton.OK);
                        return;
                    }
                    Case.PheromoneMaison = PheromoneMaison;
                    Case.PheromoneNourriture = PheromoneNourriture;
                    Case.NbTours = NbToursCase;

                    XmlNode FourmisXML = CaseXML.SelectSingleNode("Fourmis");
                    foreach (XmlNode FourmiXML in FourmisXML.SelectNodes("Fourmi"))
                    {
                        int Vie = int.Parse(FourmiXML.SelectSingleNode("Vie").InnerText);
                        String StrategieFourmi = FourmiXML.SelectSingleNode("StrategieFourmi").InnerText;
                        Fourmi Fourmi = new Fourmi(Case);
                        Fourmi.Vie = Vie;
                        if (StrategieFourmi == "StrategieRetourMaison")
                        {
                            Fourmi.StrategieFourmi = new StrategieRetourMaison();
                        }
                        Case.Fourmis.Add(Fourmi);
                    }
                    Terrain.Cases[Case.CordX, Case.CordY] = Case;
                }
                Terrain.NbTours = NbToursTerrain;

                XmlNode StatistiqueXML = MainVMXML.SelectSingleNode("Statistique");
                int NombreFourmis = int.Parse(StatistiqueXML.SelectSingleNode("NombreFourmis").InnerText);
                int NombreToursStatistique = int.Parse(StatistiqueXML.SelectSingleNode("NombreTours").InnerText);

                Statistique Statistique = new Statistique(Terrain);
                Statistique.NombreFourmis = NombreFourmis;
                Statistique.NombreTours = NombreToursStatistique;

                App.MainVM.Dim = Dim;
                App.MainVM.Terrain = Terrain;
                App.MainVM.TitreApplication = TitreApplication;
                App.MainVM.VitesseExec = VitesseExec;
                App.MainVM.Statistique = Statistique;
                DessinePlateau();
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Fichier Chargé !", "Chargement", MessageBoxButton.OK);
            }
        }

        private void OuvrirAPropos()
        {
            AProposWindow AProposWindow = new AProposWindow();
            AProposWindow.ShowDialog();
        }
        
        private void QuitterApp()
        {
            App.MainVM.stop();
            while (App.MainVM.Runnin)
            {

            }
            System.Media.SystemSounds.Beep.Play();
            MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder ?", "Quitter", MessageBoxButton.YesNoCancel);
            if(result == MessageBoxResult.Cancel)
            {
                return;
            }
            else if(result == MessageBoxResult.Yes)
            {
                SauvegarderFichier();
            } 
            App.Current.Shutdown();
        }

        private void btnDetail_Mode(object sender, RoutedEventArgs e)
        {
            App.MainVM.ActionClick = CaseDetail;
        }

        private void btnClean_Mode(object sender, RoutedEventArgs e)
        {
            App.MainVM.ActionClick = CleanCase;
        }

        private void btnNourriture_Mode(object sender, RoutedEventArgs e)
        {
            App.MainVM.ActionClick = AjouterNourriture;
        }

        private void btnFourmi_Mode(object sender, RoutedEventArgs e)
        {
            App.MainVM.ActionClick = AjouterFourmi;
        }

        private void AjouterNourriture(int CordX, int CordY)
        {
            FabriqueGeneral.AjouterNourriture(App.MainVM.Terrain, CordX, CordY, 10);
        }

        private void AjouterFourmi(int CordX, int CordY)
        {
            App.MainVM.Terrain.Cases[CordX, CordY].Fourmis.Add(new Fourmi(App.MainVM.Terrain.Cases[CordX, CordY]));
        }

        private void CleanCase(int CordX, int CordY)
        {
            CaseNormal newCase = App.MainVM.Terrain.Cases[CordX, CordY].AdapteurNormal();
            App.MainVM.Terrain.Cases[CordX, CordY] = newCase;
        }

        private void CaseDetail(int CordX, int CordY)
        {

        }

        private void clickTerrain(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(Plateau);
            int cordX = 0;
            int cordY = 0;
            double totalX = 0.0;
            double totalY = 0.0;

            foreach(var rowDef in Plateau.RowDefinitions)
            {
                totalY += rowDef.ActualHeight;
                if (totalY >= point.Y) break;
                cordY++;
            }

            foreach (var colDef in Plateau.ColumnDefinitions)
            {
                totalX += colDef.ActualWidth;
                if (totalX >= point.X) break;
                cordX++;
            }

            App.MainVM.ActionClick(cordX, cordY);
            DessinePlateau();
        }
    }
}
