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
        private int vidas = 2;
        private int tiempoRestante = 40;
        private Random rnd = new Random();
        private int ultimoIndicePregunta = -1;

        // Controles de Interfaz Estilo Millonario
        private Label lblPregunta;
        private Label lblPuntos;
        private Label lblTiempo;
        private Panel panelPregunta;
        private Button[] botonesOpciones = new Button[4];

        private string respuestaCorrectaActual = "";

        // BANCO DE PREGUNTAS DE PROGRAMACIÓN BÁSICA
        private string[,] bancoPreguntas = {
    // --- Fundamentos y Tipos ---
    { "¿Qué tipo de dato almacena números enteros?", "int", "string", "bool", "float" },
    { "¿Qué tipo de dato almacena texto?", "string", "int", "char", "bool" },
    { "¿Cuáles son los únicos valores de un tipo 'bool'?", "True / False", "0 al 9", "Texto", "Decimales" },
    { "¿Qué tipo de dato se usa para un solo carácter?", "char", "string", "byte", "long" },
    { "¿Qué tipo de dato tiene más precisión decimal?", "double", "float", "int", "short" },
    { "¿Qué palabra define una constante que no cambia?", "const", "static", "void", "public" },

    // --- Estructuras de Control ---
    { "¿Estructura que repite código mientras se cumpla una condición?", "While / For", "If / Else", "Variable", "Clase" },
    { "¿Qué estructura permite tomar decisiones (Si... entonces)?", "If / Else", "For Each", "Constant", "String" },
    { "¿Qué sentencia se usa para salir de un bucle inmediatamente?", "break", "exit", "stop", "return" },
    { "¿Qué estructura es mejor para múltiples opciones fijas?", "switch", "if", "while", "for" },
    { "¿Qué operador se usa para el 'resto' de una división?", "%", "/", "#", "&" },
    { "¿Qué símbolo significa 'DIFERENTE DE' en una comparación?", "!=", "==", "<>", "not" },

    // --- Arreglos y Colecciones ---
    { "¿Cuál es el índice del primer elemento en un Array?", "0", "1", "-1", "10" },
    { "¿Qué propiedad devuelve el tamaño de un Array?", "Length", "Count", "Size", "Total" },
    { "¿Cómo se accede al tercer elemento de un array 'A'?", "A[2]", "A[3]", "A(2)", "A{3}" },

    // --- Programación Orientada a Objetos (POO) ---
    { "¿Cómo se llama el 'molde' para crear objetos?", "Clase", "Método", "Atributo", "Array" },
    { "¿Qué palabra clave se usa para crear un objeto nuevo?", "new", "create", "make", "instance" },
    { "¿Cómo se llama el método que se ejecuta al crear un objeto?", "Constructor", "Main", "Init", "Setter" },
    { "¿Qué concepto de POO oculta los datos internos?", "Encapsulamiento", "Herencia", "Polimorfismo", "Clase" },
    { "¿Qué concepto permite a una clase heredar de otra?", "Herencia", "Abstracción", "Interfaz", "Static" },
    { "¿Qué palabra clave se refiere a la instancia actual?", "this", "self", "base", "me" },

    // --- Sintaxis y Errores ---
    { "¿Qué símbolo finaliza una línea en C#?", ";", ":", ".", "," },
    { "¿Cómo se inicia un comentario de una sola línea?", "//", "/*", "--", "##" },
    { "¿Qué significa 'IDE' en programación?", "Entorno de Desarrollo", "Interfaz de Datos", "Error de Identidad", "Elemento Interno" },
    { "¿Cómo se llama el error al ejecutar el programa?", "Excepción", "Sintaxis", "Compilación", "Lógico" },
    { "¿Qué bloque se usa para capturar errores?", "try / catch", "if / else", "error / fix", "check / get" },

    // --- C# Específico ---
    { "¿Qué método es el punto de entrada de una app C#?", "Main", "Start", "Init", "Run" },
    { "¿Qué palabra indica que un método no devuelve nada?", "void", "null", "empty", "static" },
    { "¿En qué lenguaje estamos programando este tanque?", "C#", "Python", "Java", "C++" },
    { "¿Qué namespace contiene las herramientas de consola?", "System", "System.IO", "System.Net", "System.Web" }
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
           
            Label lblVidas = new Label
            {
                Name = "lblVidas",
                Text = "Intentos: 2",
                ForeColor = Color.White,
                Location = new Point(50, 220),
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true
            };
             this.Controls.Add(lblVidas);
            
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
            lblTiempo = new Label { Text = "40", ForeColor = Color.OrangeRed, Location = new Point(780, 180), Font = new Font("Impact", 26), AutoSize = true };

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
            int nuevoIndice;

            do
            {
                nuevoIndice = rnd.Next(bancoPreguntas.GetLength(0));
            } while (nuevoIndice == ultimoIndicePregunta);

            ultimoIndicePregunta = nuevoIndice; 

            lblPregunta.Text = bancoPreguntas[nuevoIndice, 0];
            respuestaCorrectaActual = bancoPreguntas[nuevoIndice, 1];

            // Mezclar opciones para que la correcta no sea siempre la misma
            List<string> opciones = new List<string>();
            for (int i = 1; i < 5; i++) opciones.Add(bancoPreguntas[nuevoIndice, i]);

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
                // --- RESPUESTA INCORRECTA ---
                vidas--;
                btn.BackColor = Color.Red;

                // Actualizar visual de vidas si tienes el Label
                if (this.Controls.ContainsKey("lblVidas"))
                    this.Controls["lblVidas"].Text = $"Intentos: {vidas}";

                if (vidas > 0)
                {
                    MessageBox.Show($"¡ERROR! Esa no era la respuesta.\nTe queda {vidas} intento. Cambiando pregunta...", "FALLO");

                    // CAMBIO AUTOMÁTICO DE PREGUNTA AL FALLAR
                    SiguientePregunta();
                }
                else
                {
                    // Se acabaron las vidas
                    FinDelJuego(true);
                }
            }
        }

        private void FinDelJuego(bool fallo)
        {
            juegoTimer.Stop();
            int premioFinal = puntuacion / 10;

            if (fallo)
            {
                MessageBox.Show($"¡ERROR DE SINTAXIS!\nGanaste {premioFinal} Oro.", "FIN");
            }
            else
            {
                MessageBox.Show($"¡TIEMPO AGOTADO!\nGanaste {premioFinal} Oro.", "FIN");
            }

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