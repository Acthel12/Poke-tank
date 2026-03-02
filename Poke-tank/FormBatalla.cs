using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public enum AccionUsuario
    {
        Disparar,
        AtaqueOrugas,
        CortinaHumo,
        Defender,
        Reparar,
        Huir
    }
    public partial class FormBatalla : Form
    {
        private int cooldownHumo = 0;
        private int turnosDeHumo = 0;
        private AccionUsuario accionSeleccionada;
        private Random random = new Random();
        private AccionUsuario accionAnterior ;
        Partida? partidaactual;
        Tanque? Jugador;
        TanqueEnemigo? enemigoactual;

        private enum OpcionAtaque
        {
            Orugas,
            Normal
        }
        
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

        private void buttonAtacar_Click(object sender, EventArgs e)
        {
            MenuAtaques();
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
            if (Jugador == null || enemigoactual == null || partidaactual == null || this.IsDisposed) return false;

            progressBarVidaJugador.Value = Jugador.Vida;
            progressBarVidaEnemigo.Value = enemigoactual.Vida;

            if (!Jugador.EstaVivo())
            {
                MessageBox.Show($"{Jugador.Nombre} ha sido derrotado. ¡Has perdido!");
                buttonAtacar.Enabled = false;
                this.Close();
                return false;
            }

            if (!enemigoactual.EstaVivo())
            {

                partidaactual.DerrotarEnemigo(enemigoactual, Jugador);

                MessageBox.Show($"{enemigoactual.Nombre} ha sido derrotado. ¡Has ganado la batalla!");

                buttonAtacar.Enabled = false;
                this.Close();
                return false;
            }
            return true;
        }
        //método para deshabilitar los botones de acción al finalizar el combate
        private void ApagarBotones()
        {
            groupBoxAtaques.Enabled = false;
            groupBoxComandos.Enabled = false;
        }
        //metodo para cambiar el groupbox de ataques a comandos al finalizar el combate
        private void VolverAMenu()
        {
            groupBoxAtaques.Visible = false;
            groupBoxAtaques.Enabled = false;
            groupBoxComandos.Enabled = true;
            groupBoxComandos.Visible = true;
        }
        //metodo para pasar al menu de ataques
        private void MenuAtaques()
        {
            groupBoxComandos.Visible = false;
            groupBoxComandos.Enabled = false;
            groupBoxAtaques.Enabled = true;
            groupBoxAtaques.Visible = true;

            if (cooldownHumo > 0)
            {
                buttonHumo.Enabled = false;
                buttonHumo.Text = $"Cortina de Humo (CD: {cooldownHumo})";
            }
            else
            {
                buttonHumo.Enabled = true;
                buttonHumo.Text = "Cortina de Humo";
            }
        }
        //método para que el enemigo elija su acción y se ejecute, luego se actualiza el estado del combate
        private void TurnoEnemigo()
        {
            if (enemigoactual == null || Jugador == null) return;

            bool hayHumo = turnosDeHumo > 0;

            string resultadoEnemigo = enemigoactual.elegirAccion(Jugador, hayHumo, accionAnterior);
            EscribirLog(resultadoEnemigo, Color.Purple);
        }

        //defensa, en esta el jugador se prepara para bloquear el próximo ataque del enemigo
        private void buttonDefensa_Click(object sender, EventArgs e)
        {
            ApagarBotones();

            accionSeleccionada = AccionUsuario.Defender;
            GestionarTurno();
        }

        //repara al jugador, restaurando 20 puntos de vida
        private void buttonReparar_Click(object sender, EventArgs e)
        {
            ApagarBotones();

            accionSeleccionada = AccionUsuario.Reparar;
            GestionarTurno();
        }

        //acción de huir, con una probabilidad del 50% de éxito. Si falla, el enemigo ataca
        private void buttonHuir_Click(object sender, EventArgs e)
        {
            ApagarBotones();

            accionSeleccionada = AccionUsuario.Huir;
            GestionarTurno();
        }

        private void FormBatalla_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (partidaactual == null) return;

            partidaactual.CalcularPuntuacion();
            partidaactual.ReiniciarCombate();
        }

        private void Disparar(OpcionAtaque opcionAtaque)
        {
            if (Jugador == null || enemigoactual == null) return;

            string resultadoJugador;
            Color color;
            int suerte = random.Next(1, 101);
            int probabilidadFallo = turnosDeHumo > 0 ? 75 : 25 ;

            if (suerte <= probabilidadFallo)
            {
                if (turnosDeHumo > 0)
                    resultadoJugador = $"{Jugador.Nombre} dispara a ciegas por el humo... ¡y falla el tiro!";
                else
                    resultadoJugador = $"{Jugador.Nombre} ha fallado su ataque.";

                color = Color.Orange;
            }
            else
            {
                if (opcionAtaque == OpcionAtaque.Orugas)
                {
                    int danoOrugas = enemigoactual.DisparoEnLasOrugas(Jugador.Ataque);
                    resultadoJugador = $"{Jugador.Nombre} ha infligido {danoOrugas} de daño con las orugas a {enemigoactual.Nombre}.";
                }
                else
                {
                    int danoNormales = enemigoactual.RecibirAtaque(Jugador.Ataque);
                    resultadoJugador = $"{Jugador.Nombre} ha infligido {danoNormales} de daño con el disparo normal a {enemigoactual.Nombre}.";
                }

                color = Color.Green;
            }

            EscribirLog(resultadoJugador, color);
        }
        private void Defender()
        {
            if (Jugador == null) return;
            Jugador.BloquearSiguienteAtaque();
            EscribirLog($"{Jugador.Nombre} se prepara para bloquear el próximo ataque.", Color.Blue);
        }
        private void Reparar()
        {
            if (Jugador == null) return;
            Jugador.Reparar(15);
            EscribirLog($"{Jugador.Nombre} se ha reparado (+20 HP).", Color.Green);
        }
        private void Huir()
        {
            if (Jugador == null || enemigoactual == null) return;
            int suerte = random.Next(1, 101);
            if (suerte <= 50)
            {
                MessageBox.Show($"{Jugador.Nombre} ha huido exitosamente.");
                this.Close();
            }
            else
            {
                EscribirLog($"{Jugador.Nombre} ha fallado al intentar huir.", Color.Orange);
            }
        }
        private void CortinaHumo()
        {
            if (Jugador == null) return;

            turnosDeHumo = 3;
            cooldownHumo = 10;

            EscribirLog($"{Jugador.Nombre} ha desplegado una cortina de humo, reduciendo de gran manera la precisión de los ataques durante 3 turnos.", Color.Gray);
        }
        private void TurnoJugador()
        {
            switch (accionSeleccionada)
            {
                case AccionUsuario.Disparar:
                    Disparar(OpcionAtaque.Normal);
                    break;
                case AccionUsuario.AtaqueOrugas:
                    Disparar(OpcionAtaque.Orugas);
                    break;
                case AccionUsuario.Defender:
                    Defender();
                    break;
                case AccionUsuario.Reparar:
                    Reparar();
                    break;
                case AccionUsuario.Huir:
                    Huir();
                    break;
                case AccionUsuario.CortinaHumo:
                    CortinaHumo();
                    break;
            }
        }
        private void GestionarTurno()
        {
            bool combateActivo = true;
            if (Jugador.Velocidad > enemigoactual.Velocidad)
            {
                TurnoJugador();
                combateActivo = ActualizarEstado();
                if (combateActivo)
                {
                    TurnoEnemigo();
                    combateActivo = ActualizarEstado();
                }
            }
            else if (Jugador.Velocidad < enemigoactual.Velocidad)
            {
                TurnoEnemigo();
                combateActivo = ActualizarEstado();
                if (combateActivo)
                {
                    TurnoJugador();
                    combateActivo = ActualizarEstado();
                }
            }
            else
            {
                if (random.Next(0, 2) == 0)
                {
                    TurnoJugador();
                    combateActivo = ActualizarEstado();
                    if (combateActivo)
                    {
                        TurnoEnemigo();
                        combateActivo = ActualizarEstado(); ;
                    }
                }
                else
                {
                    TurnoEnemigo();
                    combateActivo = ActualizarEstado();
                    if (combateActivo)
                    {
                        TurnoJugador();
                        combateActivo = ActualizarEstado();
                    }
                }
            }

            if (combateActivo)
            {
                if (cooldownHumo > 0) cooldownHumo--;
                if (turnosDeHumo > 0) turnosDeHumo--;

                accionAnterior = accionSeleccionada;
                VolverAMenu();
            }
        }

        private void buttonDisparo_Click(object sender, EventArgs e)
        {
            if (Jugador == null || enemigoactual == null) return;

            ApagarBotones();
            accionSeleccionada = AccionUsuario.Disparar;
            GestionarTurno();
        }

        private void buttonOrugas_Click(object sender, EventArgs e)
        {
            if (Jugador == null || enemigoactual == null) return;

            ApagarBotones();
            accionSeleccionada = AccionUsuario.AtaqueOrugas;
            GestionarTurno();
        }

        private void buttonHumo_Click(object sender, EventArgs e)
        {
            if (Jugador == null || enemigoactual == null) return;

            ApagarBotones();
            accionSeleccionada = AccionUsuario.CortinaHumo;
            GestionarTurno();
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            VolverAMenu();
        }
    }
}
