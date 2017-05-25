using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NosTaleKillList.Models
{

    public class Feinde : ObservableCollection<Feind>
    {

    }

    public class Feind
    {
        public string CharakterName { get; set; }
        public string Beruf { get; set; }
        public string Level { get; set; }
        public string Familie { get; set; }
        public string Beschreibung { get; set; }
        public string FamDK { get; set; }

        public Feind(string charaktername, string beruf, string level, string familie, string beschreibung, string famdk)
        {
            CharakterName = charaktername;
            Beruf = beruf;
            Level = level;
            Familie = familie;
            Beschreibung = beschreibung;
            FamDK = famdk;
        }

        public bool Save()
        {
            bool erfolg = true;
            try
            {
                string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string zielPfad = Path.Combine(desktopPfad, "NosTale", Beruf);
                string file = Path.Combine(zielPfad, CharakterName + ".nostale");
                if (!Directory.Exists(zielPfad))
                {
                    Directory.CreateDirectory(zielPfad);
                }

                File.WriteAllText(Path.Combine(zielPfad, CharakterName + ".nostale"), ToJson());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                erfolg = false;
            }

            return erfolg;
        }

        public void Remove()
        {
            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string zielPfad = Path.Combine(desktopPfad, "NosTale", Beruf);
            string file = Path.Combine(zielPfad, CharakterName + ".nostale");

            if (File.Exists(file))
            {
                File.Delete(file);

                if (Directory.GetFiles(zielPfad).Length == 0)
                {
                    try
                    {
                        Directory.Delete(zielPfad);
                    }
                    catch
                    {
                        MessageBox.Show("Ordner konnte nicht gelöscht werden");
                    }
                }
            }
            else
            {
                MessageBox.Show("Datei konnte nicht gelöscht werden");
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Feind ToFeind(string beruf, string charakterName)
        {
            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string zielPfad = Path.Combine(desktopPfad, "NosTale", beruf);
            string file = Path.Combine(zielPfad, charakterName + ".nostale");

            return _ToFeind(file);
        }

        public static Feind _ToFeind(string pfad)
        {
            string json = File.ReadAllText(pfad);
            return JsonConvert.DeserializeObject<Feind>(json);

        }
    }
}
