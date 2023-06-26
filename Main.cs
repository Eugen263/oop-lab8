using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        private string currentFileName = null;
        private bool isTextChanged = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "My Text Editor";
            this.languageComboBox.Items.AddRange(new object[] { "English", "French", "German" });
            this.languageComboBox.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            this.currentFileName = null;
            this.textBox.Text = "";
            this.isTextChanged = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currentFileName = openFileDialog.FileName;
                this.textBox.Text = File.ReadAllText(this.currentFileName);
                this.isTextChanged = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            if (this.currentFileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.currentFileName = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            File.WriteAllText(this.currentFileName, this.textBox.Text);
            this.isTextChanged = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.languageComboBox.SelectedIndex)
            {
                case 0:
                    this.textBox.Font = new Font("Microsoft Sans Serif", 12);
                    break;
                case 1:
                    this.textBox.Font = new Font("Arial", 12);
                    break;
                case 2:
                    this.textBox.Font = new Font("Times New Roman", 12);
                    break;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.isTextChanged = true;
        }
    }
}

