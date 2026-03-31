using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class NuevaPartida : Form
    {
        public NuevaPartida()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el comandante.");
                return;
            }
            string nombre = textBoxNombre.Text;

            int dificultadSeleccionada = 0;

            if (radioButtonFacil.Checked)
                dificultadSeleccionada = 1;
            else if (radioButtonNormal.Checked)
                dificultadSeleccionada = 2;
            else if (radioButtonDificil.Checked)
                dificultadSeleccionada = 3;

            if (SeleccionarDificultad(nombre, dificultadSeleccionada))
            {
                this.Close();
            }
        
        }
        private bool SeleccionarDificultad(string nombre, int chechboxSeleccionada)
        {
            switch (chechboxSeleccionada)
            {
                case 1:
                    Partida.iniciarPartida(nombre, 140, 30, NivelDificultad.Facil);
                    return true;
                case 2:
                    Partida.iniciarPartida(nombre, 120, 25, NivelDificultad.Normal);
                    return true;
                case 3:
                    Partida.iniciarPartida(nombre, 100, 20, NivelDificultad.Dificil);
                    return true;
                default:
                    MessageBox.Show("Seleccione una dificultad válida.");
                    return false;
            }
        }
    }
}
