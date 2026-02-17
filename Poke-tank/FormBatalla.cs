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
        Partida? partidaactual;
        Tanque? Jugador;
        TanqueEnemigo? enemigoactual;

        public FormBatalla()
        {
            InitializeComponent();
        }

        private void FormBatalla_Load(object sender, EventArgs e)
        {
            //validaciones para evitar errores por índices fuera de rango o datos nulos
            if (DatosGlobales.PartidaActualIndex >= 0 && DatosGlobales.PartidaActualIndex < DatosGlobales.ListaPartidas.Count)
            {
                partidaactual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
                
                if (partidaactual != null && partidaactual.enemigos.Count > 0)
                {
                    enemigoactual = partidaactual.enemigos[DatosGlobales.NivelSeleccionado];
                    Jugador = partidaactual.tanqueUsuario;

                    if (Jugador != null)
                    {
                        labelNombreJugador.Text = Jugador.Nombre;
                        progressBarVidaJugador.Maximum = Jugador.VidaMaxima;
                        progressBarVidaJugador.Value = Jugador.Vida;
                    }

                    ConfigurarEnemigo();
                }
            }
        }

        //configura el enemigo en la interfaz según el modelo
        private void ConfigurarEnemigo()
        {
            if (enemigoactual == null) return;

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
                    MessageBox.Show("¡Ha aparecido El JEFE SECRETO!", "Alerta!!!");
                    break;
            }
        }

        //acción de disparar, con una probabilidad de fallo del 25%. Si acierta, inflige daño al enemigo
        private void buttonDisparar_Click(object sender, EventArgs e)
        {
            if (Jugador == null || enemigoactual == null) return;

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

        //método para escribir en el log de combate con un color específico
        private void EscribirLog(string mensaje, Color color)
        {
            richTextBoxCombatLog.SelectionStart = richTextBoxCombatLog.TextLength;
            richTextBoxCombatLog.SelectionLength = 0;
            richTextBoxCombatLog.SelectionColor = color;
            richTextBoxCombatLog.AppendText(mensaje + Environment.NewLine);
            richTextBoxCombatLog.SelectionColor = Color.Black;
            richTextBoxCombatLog.ScrollToCaret();
        }

        //actualiza las barras de vida y verifica si el jugador o el enemigo han sido derrotados
        private bool ActualizarEstado()
        {
            if (Jugador == null || enemigoactual == null || partidaactual == null) return false;

            progressBarVidaJugador.Value = Jugador.Vida;
            progressBarVidaEnemigo.Value = enemigoactual.Vida;

            if (!Jugador.EstaVivo())
            {
                MessageBox.Show($"{Jugador.Nombre} ha sido derrotado. ¡Has perdido!");
                buttonDisparar.Enabled = false;
                this.Close();
                return false;
            }

            if (!enemigoactual.EstaVivo())
            {

                partidaactual.DerrotarEnemigo(enemigoactual,Jugador);

                MessageBox.Show($"{enemigoactual.Nombre} ha sido derrotado. ¡Has ganado la batalla!");
                
                buttonDisparar.Enabled = false;
                this.Close();
                return false;
            }
            return true;
        }

        //método para que el enemigo elija su acción y se ejecute, luego se actualiza el estado del combate
        private void TurnoEnemigo()
        {
            if (enemigoactual == null || Jugador == null) return;
            string resultadoEnemigo = enemigoactual.elegirAccion(Jugador);
            EscribirLog(resultadoEnemigo, Color.Purple);
            ActualizarEstado();
        }

        //defensa, en esta el jugador se prepara para bloquear el próximo ataque del enemigo
        private void buttonDefensa_Click(object sender, EventArgs e)
        {
            if (Jugador == null) return;
            Jugador.BloquearSiguienteAtaque();
            EscribirLog($"{Jugador.Nombre} se prepara para bloquear el próximo ataque.", Color.Blue);
            TurnoEnemigo();
        }

        //repara al jugador, restaurando 20 puntos de vida
        private void buttonReparar_Click(object sender, EventArgs e)
        {
            if (Jugador == null) return;
            Jugador.Reparar(20);
            if (ActualizarEstado())
            {
                EscribirLog($"{Jugador.Nombre} se ha reparado (+20 HP).", Color.Green);
                TurnoEnemigo();
            }
        }

        //acción de huir, con una probabilidad del 50% de éxito. Si falla, el enemigo ataca
        private void buttonHuir_Click(object sender, EventArgs e)
        {
            if (Jugador == null || enemigoactual == null) return;
            int suerte = random.Next(1, 101);
            if (suerte <= 50)
            {
                MessageBox.Show($"{Jugador.Nombre} ha huido exitosamente.");
                enemigoactual.Vida = enemigoactual.VidaMaxima;
                this.Close();
            }
            else
            {
                EscribirLog($"{Jugador.Nombre} ha fallado al intentar huir.", Color.Orange);
                TurnoEnemigo();
            }
        }
    }
}
