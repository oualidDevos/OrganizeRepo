using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiseFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void processThenAction()
        {
            // lets start moving stuff
            //FileDialog file = new OpenFileDialog();
            //file.ShowDialog();
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult answer = folder.ShowDialog();
            if (answer == DialogResult.OK)
            {
                string path = folder.SelectedPath;
                string[] files = Directory.GetFiles(path); // gets only files

                if (files.Length > 0)
                {
                    Constants.filesList = files;
                    foreach (var file in files)
                    {
                        string destination = path;
                        if (Path.HasExtension(file) && !file.Contains("desktop.ini"))
                        {
                            string ext = file.Split('.')[file.Split('.').Length - 1];
                            string folderName = Program.Data[ext] + "";
                            Constants.Check(path + "\\" + folderName, file);
                        }
                    }
                }

            }
        }

        public void Display(bool settings)
        {
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                    control.Visible = settings;
            }
        }

        bool CheckExtensions(GroupBox grpBox, string nameProperty)
        {
            int isOneChecked = 0;
            string folderName = grpBox.Controls.Find(nameProperty, false)[0].Text;
            foreach (Control control in grpBox.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox chk = control as CheckBox;
                    if (chk.Checked)
                    {
                        if (Program.Data[chk.Text.ToLower()] == null && folderName != null)
                        {
                            Program.Data.Add(chk.Text.ToLower(), folderName);
                            isOneChecked++;
                        }
                        else if(Program.Data[chk.Text.ToLower()] != null)
                        {
                            isOneChecked++ ;
                        }
                    }
                }
            }
            if (isOneChecked > 0)
                return true;
            return false;
        }
        bool VerifyTextbox(GroupBox grpBox)
        {
            foreach (Control control in grpBox.Controls)
            {
                if (control is TextBox)
                {
                    return control.Text.Trim() != "";
                }
            }
            return false;
        }
        List<GroupBox> grpBoxsList = new List<GroupBox>();
        bool VerifyAll()
        {
            grpBoxsList = new List<GroupBox>();
            foreach (Control control in this.Controls)
            {
                GroupBox box = control as GroupBox;
                if (box != null)
                {
                    bool isEmpty = VerifyTextbox(box); // checks weather a the textbox inside groupbox is empty or not
                    if (isEmpty)
                    {
                        // list of groupboxes that are not empty that we desire to work with
                        grpBoxsList.Add(box);
                    }
                }
            }
            if (grpBoxsList.Count != 0)
                return true;
            MessageBox.Show(@"au moin un de des champs du nom de dossier ne doit pas étre vide ");
            return false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (Program.useExtensionsFile)
            {
                processThenAction();
            }
            else if(VerifyAll())
            {
                bool isAllGood = true;
                foreach (GroupBox control in grpBoxsList)
                {
                    if (!CheckExtensions(control, control.Tag.ToString()))
                    {
                        MessageBox.Show(@"at least one ext needs to be checked");
                        isAllGood = false;
                        break;
                    }
                    else
                    {
                        isAllGood = true;
                    }
                }

                if (isAllGood)
                {
                    processThenAction();
                }
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Would you like to save those settings ?", "Info", MessageBoxButtons.YesNo))
            {
                int length = Program.Data.Count;
                string[] extensionsList = new string[length];
                int i = 0;
                foreach (var key in Program.Data.Keys)
                {
                    extensionsList[i] = key + "," + Program.Data[key];
                    i++;
                }
                Constants.SaveFile(extensionsList);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Display(true);
            Hashtable extensions = Constants.LoadExtension(); 
            //extensions will be loaded in lines like mp3,Music <--(foldername)
            if (extensions != null && extensions.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Would you like to use your saved settings ?", "Question",
                        MessageBoxButtons.YesNo))
                {
                    Program.useExtensionsFile = true;
                    Program.Data = extensions;
                    Display(false);
                }
            }
        }

        private void Database_Enter(object sender, EventArgs e)
        {

        }
    }
}
