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

        private void botonCargarPartida_Click_1(object sender, EventArgs e)
        {
            Seleccionar_Partida from1 = new Seleccionar_Partida();

            from1.Show();
        }

        private void botonIniciarPartida_Click(object sender, EventArgs e)
        {
            Mapa from2 = new Mapa();
            Partida.iniciarPartida("Comandante Flavio Rosales", "M1A1", 140, 30, 15, 14);

            from2.Show();
        }

        private void botonPuntuaciones_Click(object sender, EventArgs e)
        {
            PuntuacionesForm from3 = new PuntuacionesForm();

            from3.Show();
        }

        private void fondoMenuPrincipal_Click(object sender, EventArgs e)
        {
            DatosGlobales.CargarDatos();
        }
    }
}
