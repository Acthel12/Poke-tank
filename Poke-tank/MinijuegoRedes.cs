using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class MinijuegoRedes : Form
    {
        // Usamos el nombre completo para evitar errores de ambigüedad (CS0104)
        private System.Windows.Forms.Timer juegoTimer;
        private int puntuacion = 0;
        private int tiempoRestante = 20;
        private Random rnd = new Random();

        // Controles de Interfaz Estilo Millonario
        private Label lblPregunta;
        private Label lblPuntos;
        private Label lblTiempo;
        private Panel panelPregunta;
        private Button[] botonesOpciones = new Button[4];

        private string respuestaCorrectaActual = "";

        // BANCO DE PREGUNTAS DE PROGRAMACIÓN BÁSICA
        private string[,] bancoPreguntas = {
            { "¿Qué tipo de dato almacena números enteros?", "int", "string", "bool", "float" },
            { "¿Estructura que repite código mientras se cumpla una condición?", "While / For", "If / Else", "Variable", "Clase" },
            { "¿Cuáles son los únicos valores de un tipo 'bool'?", "True / False", "0 al 9", "Cualquier texto", "Números decimales" },
            { "¿Qué símbolo se usa para comparar si dos valores son IGUALES?", "==", "=", "!=", "++" },
            { "¿Cómo se llama el 'molde' o plantilla para crear objetos?", "Clase", "Método", "Atributo", "Array" },
            { "¿Qué estructura permite tomar decisiones (Si... entonces)?", "If / Else", "For Each", "Constant", "String" },
            { "¿Cuál es el índice del primer elemento en un Array estándar?", "0", "1", "-1", "No tiene" },
            { "¿Qué símbolo se usa para finalizar una línea en C#?", ";", ":", ".", "," }
        };

        public MinijuegoRedes()
        {
            InitializeComponent();
            ConfigurarVentana();
            CrearInterfaz();
            IniciarConcurso();
        }

        private void ConfigurarVentana()
        {
            this.Text = "QUIÉN QUIERE SER PROGRAMADOR - DESAFÍO DE TANQUE";
            this.Size = new Size(900, 650);
            this.BackColor = Color.FromArgb(0, 0, 40); // Azul muy oscuro profundo
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CrearInterfaz()
        {
            // Panel de la Pregunta
            panelPregunta = new Panel
            {
                Size = new Size(800, 120),
                Location = new Point(50, 50),
                BackColor = Color.FromArgb(10, 10, 80),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblPregunta = new Label
            {
                Text = "PREPARANDO COMPILADOR...",
                ForeColor = Color.White,
                Font = new Font("Verdana", 14, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelPregunta.Controls.Add(lblPregunta);

            // Labels de Estado (Oro y Reloj)
            lblPuntos = new Label { Text = "Premio: 0 G", ForeColor = Color.Gold, Location = new Point(50, 180), Font = new Font("Impact", 20), AutoSize = true };
            lblTiempo = new Label { Text = "20", ForeColor = Color.OrangeRed, Location = new Point(780, 180), Font = new Font("Impact", 26), AutoSize = true };

            // Botones de Opciones (2x2)
            int btnAncho = 350, btnAlto = 80;
            for (int i = 0; i < 4; i++)
            {
                botonesOpciones[i] = new Button
                {
                    Size = new Size(btnAncho, btnAlto),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(20, 20, 100),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                botonesOpciones[i].FlatAppearance.BorderSize = 2;
                botonesOpciones[i].FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
                botonesOpciones[i].Click += ValidarEleccion;

                int x = 50 + (i % 2) * (btnAncho + 100);
                int y = 300 + (i / 2) * (btnAlto + 40);
                botonesOpciones[i].Location = new Point(x, y);
                this.Controls.Add(botonesOpciones[i]);
            }

            this.Controls.Add(panelPregunta);
            this.Controls.Add(lblPuntos);
            this.Controls.Add(lblTiempo);
        }

        private void IniciarConcurso()
        {
            juegoTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            juegoTimer.Tick += (s, e) => {
                tiempoRestante--;
                lblTiempo.Text = tiempoRestante.ToString();
                if (tiempoRestante <= 0) FinDelJuego(false);
            };
            juegoTimer.Start();
            SiguientePregunta();
        }

        private void SiguientePregunta()
        {
            int index = rnd.Next(bancoPreguntas.GetLength(0));
            lblPregunta.Text = bancoPreguntas[index, 0];
            respuestaCorrectaActual = bancoPreguntas[index, 1];

            // Mezclar opciones para que la correcta no sea siempre la misma
            List<string> opciones = new List<string>();
            for (int i = 1; i < 5; i++) opciones.Add(bancoPreguntas[index, i]);

            // Algoritmo de barajado
            for (int i = opciones.Count - 1; i > 0; i--)
            {
                int k = rnd.Next(i + 1);
                string value = opciones[k];
                opciones[k] = opciones[i];
                opciones[i] = value;
            }

            for (int i = 0; i < 4; i++)
            {
                botonesOpciones[i].Text = opciones[i];
                botonesOpciones[i].BackColor = Color.FromArgb(20, 20, 100);
                botonesOpciones[i].Enabled = true;
            }
        }

        private void ValidarEleccion(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == respuestaCorrectaActual)
            {
                puntuacion += 500;
                lblPuntos.Text = $"Premio: {puntuacion} G";
                btn.BackColor = Color.LimeGreen;

                // Timer de pausa corta para ver el acierto (usamos nombre completo para evitar errores)
                System.Windows.Forms.Timer tTemp = new System.Windows.Forms.Timer { Interval = 600 };
                tTemp.Tick += (s, ev) => {
                    SiguientePregunta();
                    tTemp.Stop();
                    tTemp.Dispose();
                };
                tTemp.Start();
            }
            else
            {
                btn.BackColor = Color.DarkRed;
                FinDelJuego(true);
            }
        }

        private void FinDelJuego(bool fallo)
        {
            juegoTimer.Stop();
            int premioFinal = puntuacion / 10;

            if (fallo) MessageBox.Show($"¡ERROR DE SINTAXIS!\nTu premio acumulado es de {premioFinal} Oro.", "CONCURSO TERMINADO");
            else MessageBox.Show($"¡TIEMPO AGOTADO!\nGanaste {premioFinal} Oro.", "FIN DEL TIEMPO");

            if (DatosGlobales.PartidaActualIndex != -1)
            {
                var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
                partida.Oro += premioFinal;
                DatosGlobales.GuardarDatos();
            }
            this.Close();
        }
    }
}