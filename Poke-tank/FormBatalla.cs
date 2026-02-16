using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    
    public partial class FormBatalla : Form
    {
        private Random random = new Random();
        Partida partidaactual;
        Tanque Jugador;
        TanqueEnemigo enemigoactual;
        public FormBatalla()
        {
            InitializeComponent();
        }

        private void FormBatalla_Load(object sender, EventArgs e)
        {
            partidaactual = DatosGlobales.ListaPartidas[DatosGlobales.PartadaActualIndex];
            enemigoactual = partidaactual.enemigos[0];


            Jugador = partidaactual.tanqueUsuario;
            labelNombreJugador.Text = Jugador.Nombre;
            progressBarVidaJugador.Maximum = Jugador.VidaMaxima;
            progressBarVidaJugador.Value = Jugador.Vida;

            ConfigurarEnemigo();
        }
        private void ConfigurarEnemigo()
        {
            labelNombreEnemigo.Text = enemigoactual.Nombre;
            progressBarVidaEnemigo.Maximum = enemigoactual.VidaMaxima;
            progressBarVidaEnemigo.Value = enemigoactual.Vida;
            switch (enemigoactual.Modelo)
            {
                case "T-80":
                    pictureBoxEnemigo.Image = Properties.Resources.T80U;
                    break;
                case "T-72":
                    pictureBoxEnemigo.Image = Properties.Resources.T72;
                    break;
                case "T-90":
                    pictureBoxEnemigo.Image = Properties.Resources.T90A;
                    break;
                case "T-14 Armata":
                    pictureBoxEnemigo.Image = Properties.Resources.T14;
                    MessageBox.Show("A aparecido El JEFE SECRETO", "Alerta!!!");
                    break;
            }
        }

        private void buttonDisparar_Click(object sender, EventArgs e)
        {
            string resultadoJugador;
            int suerte = random.Next(1, 101);
            if (suerte <= 25)
            {
                resultadoJugador = $"{Jugador.Nombre} ha fallado su ataque.";

            }
        }
        private void EscribirLog(string mensaje,Color color)
        {
            richTextBoxCombatLog.SelectionStart = richTextBoxCombatLog.TextLength;
            richTextBoxCombatLog.SelectionLength = 0;

            richTextBoxCombatLog.SelectionColor = color;
            richTextBoxCombatLog.AppendText(mensaje + Environment.NewLine);


        }
    }
}
