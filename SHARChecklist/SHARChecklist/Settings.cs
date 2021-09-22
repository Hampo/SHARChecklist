using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SHARChecklist
{
    public class Settings
    {
        private static readonly string SettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SHARChecklist", "Settings.xml");
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Settings));

        public Point Location { get; set; }
        public FormBorderStyle BorderStyle { get; set; }
        public bool Topmost { get; set; }
        public uint LevelCount { get; set; }

        public Settings()
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            Size s = Application.OpenForms[0].Size;
            Location = new Point(rect.Width / 2 - s.Width / 2, rect.Height / 2 - s.Height / 2);
            BorderStyle = FormBorderStyle.FixedToolWindow;
            Topmost = false;
            LevelCount = 7;
        }

        public static Settings Load()
        {
            try
            {
                if (File.Exists(SettingsFile))
                {
                    using (StreamReader sr = File.OpenText(SettingsFile))
                    {
                        Settings loadedSettings = (Settings)Serializer.Deserialize(sr);
                        if (loadedSettings.Location.X == -32000 || loadedSettings.Location.Y == -32000)
                        {
                            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                            Size s = Application.OpenForms[0].Size;
                            loadedSettings.Location = new Point(rect.Width / 2 - s.Width / 2, rect.Height / 2 - s.Height / 2);
                        }

                        return loadedSettings;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings file:" + Environment.NewLine + ex.ToString(), "Error loading settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new Settings();
        }

        public void Save()
        {
            try
            {
                string parentDir = Path.GetDirectoryName(SettingsFile);
                if (!Directory.Exists(parentDir))
                    Directory.CreateDirectory(parentDir);
                
                using (StreamWriter sw = new StreamWriter(SettingsFile, false))
                {
                    Serializer.Serialize(sw, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings file:" + Environment.NewLine + ex.ToString(), "Error saving settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
