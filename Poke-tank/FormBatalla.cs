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
                    EscribirLog("¡Un T-80 ha aparecido!", Color.Red);
                    break;
                case "T-72":
                    pictureBoxEnemigo.Image = Properties.Resources.T72;
                    EscribirLog("¡Un T-72 ha aparecido!", Color.Red);
                    break;
                case "T-90":
                    pictureBoxEnemigo.Image = Properties.Resources.T90A;
                    EscribirLog("¡Un T-90 ha aparecido!", Color.Red);
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
            Color color;
            int suerte = random.Next(1, 101);
            if (suerte <= 25)
            {
                resultadoJugador = $"{Jugador.Nombre} ha fallado su ataque.";
                color = Color.Orange;
            }
            else
            {
                int dano = enemigoactual.RecibirAtaque(Jugador.Ataque);
                resultadoJugador = $"{Jugador.Nombre} ha infligido {dano} de daño a {enemigoactual.Nombre}.";
                color = Color.Green;
            }
            EscribirLog(resultadoJugador, color);
            if (!ActualizarEstado()) return;
            TurnoEnemigo();
        }
        private void EscribirLog(string mensaje, Color color)
        {
            richTextBoxCombatLog.SelectionStart = richTextBoxCombatLog.TextLength;
            richTextBoxCombatLog.SelectionLength = 0;

            richTextBoxCombatLog.SelectionColor = color;
            richTextBoxCombatLog.AppendText(mensaje + Environment.NewLine);

            richTextBoxCombatLog.SelectionColor = Color.Black;
            richTextBoxCombatLog.ScrollToCaret();
        }
        private bool ActualizarEstado()
        {
            progressBarVidaJugador.Value = Jugador.Vida;
            progressBarVidaEnemigo.Value = enemigoactual.Vida;
            if (!Jugador.EstaVivo())
            {
                MessageBox.Show($"{Jugador.Nombre} ha sido derrotado. ¡Has perdido!");
                buttonDisparar.Enabled = false;
                FormBatalla.ActiveForm.Close();
                return false;
            }
            if (!enemigoactual.EstaVivo())
            {
                MessageBox.Show($"{enemigoactual.Nombre} ha sido derrotado. ¡Has ganado!");
                buttonDisparar.Enabled = false;
                partidaactual.DerrotarEnemigo(enemigoactual);
                FormBatalla.ActiveForm.Close();
                return false;
            }
            return true;
        }
        private void TurnoEnemigo()
        {
            string resultadoEnemigo = enemigoactual.elegirAccion(Jugador);
            EscribirLog(resultadoEnemigo, Color.Purple);
            ActualizarEstado();
        }

        private void buttonDefensa_Click(object sender, EventArgs e)
        {
            Jugador.BloquearSiguienteAtaque();
            EscribirLog($"{Jugador.Nombre} se prepara para bloquear el próximo ataque.", Color.Blue);
            TurnoEnemigo();

        }

        private void buttonReparar_Click(object sender, EventArgs e)
        {
            Jugador.Reparar(20);
            if (ActualizarEstado())
            {
                EscribirLog($"{Jugador.Nombre} se ha reparado (+20 HP).", Color.Green);
                TurnoEnemigo();
            }
        }

        private void buttonHuir_Click(object sender, EventArgs e)
        {
            int suerte = random.Next(1, 101);
            if (suerte <= 50)
            {
                MessageBox.Show($"{Jugador.Nombre} ha huido exitosamente.");
                enemigoactual.Vida = enemigoactual.VidaMaxima;
                FormBatalla.ActiveForm.Close();
            }
            else
            {
                EscribirLog($"{Jugador.Nombre} ha fallado al intentar huir.", Color.Orange);
                TurnoEnemigo();
            }
        }
    }
}
