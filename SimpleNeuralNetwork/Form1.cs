﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNeuralNetwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnXORexample_Click(object sender, EventArgs e)
        {
            frmXORexample frmXor = new frmXORexample();
            frmXor.ShowDialog(this);
        }

        private void btnGuessWhat_Click(object sender, EventArgs e)
        {
            frmGuessWhat frmGuess = new frmGuessWhat();
            frmGuess.ShowDialog(this);
        }

    }
}
