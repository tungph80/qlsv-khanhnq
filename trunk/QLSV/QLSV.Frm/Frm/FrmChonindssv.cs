﻿using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonIndssv : Form
    {
        public bool Update;
        public FrmChonIndssv()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Update = true;
            Close();
        }
    }
}
