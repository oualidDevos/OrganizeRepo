using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiseFiles
{
    class Constants
    {
        public static string extPath = "../../extensionsFile/extensions.txt";
        public static string[] filesList = { };

        public static void SaveFile(string[] extensionsList)
        {
            if(extensionsList.Length > 0)
                File.WriteAllLines(extPath, extensionsList);
            else
                MessageBox.Show(@"Error the list of extension is empty");
        }

        public static Hashtable LoadExtension()
        {
            string[] data = File.ReadAllLines(path:extPath);
            Hashtable hashtable = null;
            if (data.Length > 0)
            {
                hashtable = new Hashtable();
                foreach (var line in data)
                {
                    string key = line.Split(',')[0];
                    string value = line.Split(',')[1];
                    hashtable.Add(key, value);
                }
            }
            return hashtable;
        }

        public static void Deplacer(string file, string destination)
        {
            string fileName = file.Split('\\')[file.Split('\\').Length - 1];
            string src = destination + "\\" + fileName;
            if (File.Exists(src))
            {
                int apearance = 0;
                foreach (var f in Directory.GetFiles(destination))
                {
                    if (f.Contains(fileName))
                        apearance++;
                }
                File.Move(file, destination + "\\" + apearance + "- " + fileName);
            }
            else
            {
                File.Move(file, src);
            }
        }

        public static void Check(string destination, string file)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
                Deplacer(file, destination);
            }
            else
            {
                Deplacer(file, destination);
            }
        }
    }
}
