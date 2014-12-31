﻿using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonindssv : Form
    {
        private bool _ok;
        public FrmChonindssv()
        {
            InitializeComponent();
        }

        private void FrmChonindssv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_ok) return;
            rdoPhongthi.Checked = false;
            rdokhoa.Checked = false;
            rdoLop.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ok = true;
            Close();
        }
    }
}
