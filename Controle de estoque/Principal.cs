﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controle_de_estoque.Formularios;
using Controle_de_estoque.Formularios.Cadastrar;

namespace Controle_de_estoque
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastrarUsuario u = new FrmCadastrarUsuario();
            u.ShowDialog();
        }

        private void cadastrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastrarProduto s = new FrmCadastrarProduto();
            s.ShowDialog();
        }

        private void comprarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras c = new Compras();
            c.ShowDialog();
        }
    }
}
