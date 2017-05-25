using NosTaleKillList.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NosTaleKillList
{
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel;
        internal string CharakterName { get; private set; }
        internal string Beruf { get; private set; }
        internal string Level { get; private set; }
        internal string Familie { get; private set; }
        internal string Beschreibung { get; private set; }
        internal string FamDK { get; private set; }

        internal bool Erfolg { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Erfolg = false;

            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;

            Aktualisieren();
        }

        private void Aktualisieren()
        {
            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string zielPfad = System.IO.Path.Combine(desktopPfad, "NosTale");

            try
            {
                foreach (string f in Directory.GetFiles(zielPfad, "*.nostale", SearchOption.AllDirectories))
                {
                    MainViewModel.Feinde.Add(Feind._ToFeind(f));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Hinzufügen_Click(object sender, RoutedEventArgs e)
        {

            if (tb_Beschreibung.Text.Trim() == "" || tb_CharakterName.Text.Trim() == "" || tb_Familie.Text.Trim() == "" || tb_Level.Text.Trim() == "")
            {
                MessageBoxResult result = MessageBox.Show("Bitte fülle alles aus!");
            }
            else
            {
                CharakterName = tb_CharakterName.Text;
                Beruf = cb_Beruf.Text;
                Level = tb_Level.Text;
                Familie = tb_Familie.Text;
                Beschreibung = tb_Beschreibung.Text;
                Erfolg = true;

                if (cb_FamDK.IsChecked == true)
                {
                    FamDK = "Ja";
                }
                else
                {
                    FamDK = "Nein";
                }

            }

            if (Erfolg)
            {
                var h = new Feind(CharakterName, Beruf, Level, Familie, Beschreibung, FamDK);
                if (h.Save())
                {
                    MainViewModel.Feinde.Add(h);
                }
                else
                {
                    MessageBox.Show(string.Format("Der Held \"{0}\" konnte nicht erstellt werden.", CharakterName));
                }
            }

            tb_CharakterName.Text = "";
            tb_Level.Text = "";
            tb_Familie.Text = "";
            tb_Beschreibung.Text = "";
            cb_FamDK.IsChecked = false;
        }

        internal void btn_Löschen_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.SelectedFeind.Remove();
            MainViewModel.Feinde.Remove(listView.SelectedItem as Feind);
        }
    }
}

