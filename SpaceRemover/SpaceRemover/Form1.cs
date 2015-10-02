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

namespace SpaceRemover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string d in drags)
            {
                if (!System.IO.File.Exists(d))
                {
                    return;
                }
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (droppedFiles == null) return;
            if (droppedFiles.Length == 0) return;

            progressBar1.Maximum = droppedFiles.Length;
            progressBar1.Value = 0;

            foreach (var droppedFile in droppedFiles)
            {
                string fileExtension = System.IO.Path.GetExtension(droppedFile);

                if (System.IO.Path.GetExtension(droppedFile) == ".txt")
                {
                    using (StreamReader reader = new StreamReader(droppedFile))
                    using (StreamWriter writer = new StreamWriter(droppedFile + ".new"))
                    {
                        string line;
                        string newLine;
                        while ((line = reader.ReadLine()) != null)
                        {
                            newLine = line.Replace(" ", "");
                            writer.WriteLine(newLine);
                        }
                        reader.Close();
                        writer.Close();
                    }

                    // Rename files.
                    System.IO.File.Move(droppedFile, droppedFile + ".bak");
                    System.IO.File.Move(droppedFile + ".new", droppedFile);
                }

                progressBar1.Value++;
            }
        }


        
    }


}
