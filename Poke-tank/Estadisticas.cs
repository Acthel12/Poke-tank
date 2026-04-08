using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Estadisticas : Form
    {
        public Estadisticas()
        {
            InitializeComponent();
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            

            Partida partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            if (partida == null) return;

            labelNombre.Text = partida.NombreJugador;

            labelVida.Text = partida.TanqueUsuario.vida.ToString();

            labelAtaque.Text = partida.TanqueUsuario.ataque.ToString();

            labelDinero.Text = partida.Oro.ToString();

            labelDerrotados.Text = partida.EnemigosDerrotados.ToString();
        }
    }
}
