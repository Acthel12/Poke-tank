using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Pulir_Calva : Form
    {
        // Controles de UI
        private PictureBox pbSilla;
        private PictureBox pbNPC;
        private PictureBox pbPersonaje;
        private PictureBox pbEfectoFinal;
        
        private Panel panelDialogo;
        private Label lblTexto;
        private Button btnAvanzar;
        private Button btnRetroceder;
        private Button btnSi;
        private Button btnNo;
        private Button btnSalir;

        private System.Windows.Forms.Timer timerAnimacion;

        // Textos del Diálogo
        private string[] dialogos = new string[]
        {
            "¡Hola! Bienvenido a la mejor peluquería de calvas.",
            "Tengo una crema especial que te hará moverte más rápido por el campo de batalla.",
            "Dicen que los reflejos en una calva bien pulida intimidan al enemigo...",
            "El tratamiento te costará algunas monedas, pero vale la pena.",
            "¿Deseas pulir tu cabello ahora mismo?"
        };

        private int dialogoIndex = 0;

        public Pulir_Calva()
        {
            InitializeComponent();
            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            this.ClientSize = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Pulidora de Calvas";
            this.BackColor = Color.FromArgb(40, 20, 20); // Un fondo cálido

            // 1. Silla
            pbSilla = new PictureBox();
            pbSilla.BackColor = Color.Transparent;
            pbSilla.SizeMode = PictureBoxSizeMode.Zoom;
            pbSilla.Image = Properties.Resources.explosion; // REEMPLAZAR POR SILLA REAL
            pbSilla.Bounds = new Rectangle(200, 250, 150, 150);

            // 2. Personaje (Sentado en la silla)
            pbPersonaje = new PictureBox();
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonaje.Image = Properties.Resources.flavio_de_espalda_batalla; // REEMPLAZAR
            pbPersonaje.Bounds = new Rectangle(225, 200, 100, 150);

            // 3. NPC Peluquero
            pbNPC = new PictureBox();
            pbNPC.BackColor = Color.Transparent;
            pbNPC.SizeMode = PictureBoxSizeMode.Zoom;
            pbNPC.Image = Properties.Resources.enemigo_prueba; // REEMPLAZAR
            pbNPC.Bounds = new Rectangle(400, 150, 150, 250);

            // 4. Efecto Final - Oculto por ahora
            pbEfectoFinal = new PictureBox();
            pbEfectoFinal.BackColor = Color.Transparent;
            pbEfectoFinal.SizeMode = PictureBoxSizeMode.Zoom;
            pbEfectoFinal.Image = Properties.Resources.explosion; // REEMPLAZAR
            pbEfectoFinal.Bounds = new Rectangle(225, 120, 100, 80);
            pbEfectoFinal.Visible = false;

            // --- PANEL DE DIÁLOGO ---
            panelDialogo = new Panel();
            panelDialogo.BackColor = Color.White;
            panelDialogo.Bounds = new Rectangle(150, 450, 800, 150);
            panelDialogo.BorderStyle = BorderStyle.FixedSingle;

            lblTexto = new Label();
            lblTexto.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTexto.ForeColor = Color.Black;
            lblTexto.Bounds = new Rectangle(60, 20, 680, 100);
            lblTexto.Text = dialogos[0];
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            panelDialogo.Controls.Add(lblTexto);

            // Botones de Navegación
            btnRetroceder = new Button();
            btnRetroceder.Text = "<";
            btnRetroceder.Font = new Font("Arial", 16, FontStyle.Bold);
            btnRetroceder.Bounds = new Rectangle(10, 50, 40, 50);
            btnRetroceder.Click += BtnRetroceder_Click;
            btnRetroceder.Enabled = false;
            panelDialogo.Controls.Add(btnRetroceder);

            btnAvanzar = new Button();
            btnAvanzar.Text = ">";
            btnAvanzar.Font = new Font("Arial", 16, FontStyle.Bold);
            btnAvanzar.Bounds = new Rectangle(750, 50, 40, 50);
            btnAvanzar.Click += BtnAvanzar_Click;
            panelDialogo.Controls.Add(btnAvanzar);

            // Botones de Decisión (Ocultos inicialmente)
            btnSi = new Button();
            btnSi.Text = "SÍ, PULIR";
            btnSi.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSi.BackColor = Color.MediumSeaGreen;
            btnSi.ForeColor = Color.White;
            btnSi.Bounds = new Rectangle(650, 200, 200, 50);
            btnSi.Visible = false;
            btnSi.Click += BtnSi_Click;

            btnNo = new Button();
            btnNo.Text = "NO, GRACIAS";
            btnNo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnNo.BackColor = Color.IndianRed;
            btnNo.ForeColor = Color.White;
            btnNo.Bounds = new Rectangle(650, 270, 200, 50);
            btnNo.Visible = false;
            btnNo.Click += BtnNo_Click;

            // Botón Salir Global
            btnSalir = new Button();
            btnSalir.Text = "SALIR";
            btnSalir.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSalir.BackColor = Color.DarkGray;
            btnSalir.Bounds = new Rectangle(20, 20, 100, 40);
            btnSalir.Click += (s, e) => this.Close();

            // Timer de Animación
            timerAnimacion = new System.Windows.Forms.Timer();
            timerAnimacion.Interval = 3000; // 3 Segundos trabajando
            timerAnimacion.Tick += TimerAnimacion_Tick;

            // Agregar todo al Form
            this.Controls.Add(btnSalir);
            this.Controls.Add(btnSi);
            this.Controls.Add(btnNo);
            this.Controls.Add(panelDialogo);

            this.Controls.Add(pbEfectoFinal);
            this.Controls.Add(pbPersonaje);
            this.Controls.Add(pbSilla);
            this.Controls.Add(pbNPC);
        }

        private void ActualizarDialogo()
        {
            lblTexto.Text = dialogos[dialogoIndex];
            btnRetroceder.Enabled = (dialogoIndex > 0);
            
            if (dialogoIndex == dialogos.Length - 1)
            {
                btnAvanzar.Enabled = false;
                btnSi.Visible = true;
                btnNo.Visible = true;
            }
            else
            {
                btnAvanzar.Enabled = true;
                btnSi.Visible = false;
                btnNo.Visible = false;
            }
        }

        private void BtnAvanzar_Click(object sender, EventArgs e)
        {
            if (dialogoIndex < dialogos.Length - 1)
            {
                dialogoIndex++;
                ActualizarDialogo();
            }
        }

        private void BtnRetroceder_Click(object sender, EventArgs e)
        {
            if (dialogoIndex > 0)
            {
                dialogoIndex--;
                ActualizarDialogo();
            }
        }

        private void BtnSi_Click(object sender, EventArgs e)
        {
            // TODO: Descontar cierta cantidad de monedas del inventario cuando exista
            // ej: if (dinero < 50) { lblTexto.Text = "No tienes dinero."; return; }
            // dinero -= 50;

            btnSi.Visible = false;
            btnNo.Visible = false;
            btnAvanzar.Visible = false;
            btnRetroceder.Visible = false;

            lblTexto.Text = "*Sonidos de pulidora rápidos y furiosos...*";

            // Cambiar NPC a GIF animado de trabajo
            pbNPC.Image = Properties.Resources.explosion; // REEMPLAZAR POR EL GIF
            
            timerAnimacion.Start();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            btnSi.Visible = false;
            btnNo.Visible = false;
            btnAvanzar.Visible = false;
            btnRetroceder.Visible = false;
            lblTexto.Text = "Ah bueno, regresa cuando tengas dinero. ¡Tacaño!";
        }

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            timerAnimacion.Stop();
            
            // Restaurar imagen del NPC
            pbNPC.Image = Properties.Resources.enemigo_prueba; // REEMPLAZAR POR SU NORMAL

            lblTexto.Text = "¡LISTO! Tu calva brilla como un faro. ¡Eres más rápido ahora!";
            pbEfectoFinal.Visible = true;

            // DAR LA RECOMPENSA GLOBAL (Añadirá +2 a la velocidad de Base y Batallas)
            DatosGlobales.BonusVelocidadGlobal += 2;
        }
    }
}
