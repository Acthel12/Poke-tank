using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Menu_Principal : Form
    {
        public Menu_Principal()
        {
            InitializeComponent();
        }
        private void botonIniciarPartida_Click(object sender, EventArgs e)
        {
            Mapa from1 = new Mapa();
            Partida.iniciarPartida("Comandante Flavio Rosales", "M1A1", 140, 20, 10, 14);

            from1.ShowDialog();
        }

        private void botonPuntuaciones_Click(object sender, EventArgs e)
        {
            Puntuaciones from2 = new Puntuaciones();

            from2.ShowDialog();
        }
        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
