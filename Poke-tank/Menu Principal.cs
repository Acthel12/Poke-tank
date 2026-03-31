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
        //iniciar partida con el boton
        private void botonIniciarPartida_Click(object sender, EventArgs e)
        {
            Mapa from1 = new Mapa();
            NuevaPartida from2 = new NuevaPartida();

            AbrirForm(from2);

            if (DatosGlobales.PartidaActualIndex >= 0 ) 
                AbrirForm(from1);
        }
        //mostrar puntuaciones con el boton
        private void botonPuntuaciones_Click(object sender, EventArgs e)
        {
            Puntuaciones from2 = new Puntuaciones();

            AbrirForm(from2);
        }
        //salir del juego con el boton
        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCargarPartida_Click(object sender, EventArgs e)
        {
            Seleccionar_Partida form = new Seleccionar_Partida();
            AbrirForm(form);
        }

        private void Menu_Principal_Load(object sender, EventArgs e)
        {
            DatosGlobales.CargarDatos();
        }

        private void Menu_Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatosGlobales.GuardarDatos();
        }

        //Pausa el gif al abrir un Form y lo reanuda al cerrarlo
        private void AbrirForm(Form formulario)
        {
            Image gifAnimado = fondoMenuPrincipal.Image;

            fondoMenuPrincipal.Image = Properties.Resources.FondoMenuPrincipal;

            formulario.ShowDialog();

            fondoMenuPrincipal.Image = gifAnimado;
        }
    }
}
