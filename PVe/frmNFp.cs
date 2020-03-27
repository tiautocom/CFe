using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVe
{
    public partial class frmNFp : Form
    {
        public frmNFp()
        {
            InitializeComponent();
        }

        private void frmNFp_Load(object sender, EventArgs e)
        {
            mtbDtIni.Focus();
            mtbDtIni.SelectAll();
        }
    }
}
