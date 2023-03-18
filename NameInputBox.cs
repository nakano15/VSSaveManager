using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSSaveManager
{
    public partial class NameInputBox : Form
    {
        public string GetPickedName { get { return namebox.Text; } }

        public NameInputBox()
        {
            InitializeComponent();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if(namebox.Text.Length > 0)
            {
                if (namebox.Text.IndexOfAny(Path.GetInvalidPathChars()) > -1)
                {
                    MessageBox.Show("Profile name contains invalid characters.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Main.ProfilePathExists(namebox.Text))
                {
                    MessageBox.Show("This profile name already exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Close();
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NameInputBox_Shown(object sender, EventArgs e)
        {
            namebox.Text = "";
        }
    }
}
