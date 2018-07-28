using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace json_csv_convert
{
    public partial class MainWindow : Form
    {
        string[] files = new string[0];

        //Drag-n-Drop implementation
        //https://stackoverflow.com/questions/68598/how-do-i-drag-and-drop-files-into-an-application
        public MainWindow()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Panel1_DragEnter);
            this.DragDrop += new DragEventHandler(Panel1_DragDrop);
            this.DragLeave += new EventHandler(Panel1_DragLeave);
        }

        void ProcessFiles()
        {
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToUpper().Equals(".JSON"))
                {
                    //TODO process json file

                    //prompt user for save path
                    String json_contents = File.ReadAllText(file);

                    //read from json

                    //convert to CSV
                    //option: also convert to xlsx

                    //write to output file

                    Console.WriteLine(json_contents);

                    JObject records = JObject.Parse(json_contents);


                    //todo 
                    foreach (var v in records["resources"])
                    {
                        Console.WriteLine(v["psap_id"].ToString());
                    }

                } else
                {
                    //TODO print message of invalid file
                    Console.WriteLine("Invalid");
                }
            }
        }

        void PickFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files = new string[]{ ofd.FileName };
            }

            ProcessFiles();
        }

        void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;

            Panel1.BackColor = Color.White;
            Panel1Plus.ForeColor = Color.Gray;
        }

        void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            files = (string[])e.Data.GetData(DataFormats.FileDrop);

            Panel1.BackColor = Color.Gray;
            Panel1Plus.ForeColor = Color.White;

            ProcessFiles();
        }

        void Panel1_DragLeave(object sender, EventArgs e)
        {
            Panel1.BackColor = Color.Gray;
            Panel1Plus.ForeColor = Color.White;
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Panel1.BackColor = Color.White;
            Panel1Plus.ForeColor = Color.Gray;
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Panel1.BackColor = Color.Gray;
            Panel1Plus.ForeColor = Color.White;
        }

        private void Panel1_Click(object sender, EventArgs e)
        {
            PickFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutwin = new About();
            this.Enabled = false;
            aboutwin.ShowDialog();
            this.Enabled = true;
        }
    }
}
