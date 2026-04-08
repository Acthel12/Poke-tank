using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace Poke_tank
{
    public partial class MinijuegoRedes : Form
    {
        private List<PictureBox> hoyos;
        private PictureBox pbPersonas;
        private PictureBox pbMazo;
        private Label lblPuntuacion;
        private PictureBox pbNPC1;
        private Label lblDialogoNPC1;
        private PictureBox pbNPC2;
        private Label lblDialogoNPC2;
        private PictureBox pbNPC3; // Inicia nulo hasta que se desbloquee
        private Label lblDialogoNPC3;

        private Label lblTiempoGlobal;
        private int tiempoGlobal = 60;

        // --- Easter Egg NPC ---
        private int golpesAlNPC1 = 0;
        private int golpesAlNPC2 = 0;
        private int estadoRebelion = 0; // 0=Normal, 1=Rebelion_NPC1, 2=Rebelion_NPC2, 3=Pacifico_NPC3
        private string dialogoBaseNPC1 = "¡trabaja becario!";
        private string dialogoBaseNPC2 = "¡rápidoo!";
        private string dialogoBaseNPC3 = "ME LLEVA EL DIABLO";

        private int targetY;
        private Random rnd;
        private int mensajesRespondidos = 0;
        private readonly int MAX_PERSONAS = 6;
        public MinijuegoRedes()
        {
            InitializeComponent();
            ConfigurarJuego();
        }
        private void ConfigurarJuego()
        {
            this.Size = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterParent;

            // Configurar fondo como imagen
            object imgFondo = Properties.Resources.ResourceManager.GetObject("fondo_comunicacion");
            if (imgFondo != null)
            {
                this.BackgroundImage = (Image)imgFondo;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            this.DoubleBuffered = true; // Prevenir parpadeo de pantalla al dibujar
            rnd = new Random();
            hoyos = new List<PictureBox>();
            lblPuntuacion = new Label();
            lblPuntuacion.Text = "Mensajes respondidos: 0 / " + MAX_PERSONAS;
            lblPuntuacion.Font = new Font("Arial", 16, FontStyle.Bold);
            lblPuntuacion.ForeColor = Color.Black;
            lblPuntuacion.Location = new Point(20, 20);
            lblPuntuacion.AutoSize = true;
            this.Controls.Add(lblPuntuacion);
            lblTiempoGlobal = new Label();
            lblTiempoGlobal.Text = "Tiempo: 60s";
            lblTiempoGlobal.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTiempoGlobal.ForeColor = Color.OrangeRed;
            lblTiempoGlobal.Location = new Point(this.ClientSize.Width - 200, 20);
            lblTiempoGlobal.AutoSize = true;
            this.Controls.Add(lblTiempoGlobal);
            // Crear el NPC 1
            pbNPC1 = new PictureBox();
            pbNPC1.Size = new Size(50, 120);
            pbNPC1.BackColor = Color.Transparent; // Placeholder NPC1
            object imgAna = Properties.Resources.ResourceManager.GetObject("imagen_ana");
            if (imgAna != null) pbNPC1.Image = (Image)imgAna;
            pbNPC1.Location = new Point(this.ClientSize.Width - 300, this.ClientSize.Height - 150);
            pbNPC1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbNPC1.MouseDown += IntentoGolpe;
            this.Controls.Add(pbNPC1);
            lblDialogoNPC1 = new Label();
            lblDialogoNPC1.Text = dialogoBaseNPC1;
            lblDialogoNPC1.Font = new Font("Arial", 8, FontStyle.Italic);
            lblDialogoNPC1.ForeColor = Color.Black;
            lblDialogoNPC1.Location = new Point(pbNPC1.Location.X - 20, pbNPC1.Location.Y - 30);
            lblDialogoNPC1.AutoSize = true;
            this.Controls.Add(lblDialogoNPC1);
            // Crear el NPC 2
            pbNPC2 = new PictureBox();
            pbNPC2.Size = new Size(50, 120);
            pbNPC2.BackColor = Color.Transparent; // Placeholder NPC2
            object imgSoraya = Properties.Resources.ResourceManager.GetObject("imagen_soraya");
            if (imgSoraya != null) pbNPC2.Image = (Image)imgSoraya;
            pbNPC2.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 150);
            pbNPC2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbNPC2.MouseDown += IntentoGolpe;
            this.Controls.Add(pbNPC2);
            lblDialogoNPC2 = new Label();
            lblDialogoNPC2.Text = dialogoBaseNPC2;
            lblDialogoNPC2.Font = new Font("Arial", 8, FontStyle.Italic);
            lblDialogoNPC2.ForeColor = Color.Black;
            lblDialogoNPC2.Location = new Point(pbNPC2.Location.X - 20, pbNPC2.Location.Y - 30);
            lblDialogoNPC2.AutoSize = true;
            this.Controls.Add(lblDialogoNPC2);
            // Crear los hoyos en una grilla de 3x3
            int startX = 150;
            int startY = 150;
            int offsetX = 200;
            int offsetY = 150;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int col = 0; col < 3; col++)
                {
                    PictureBox pbHoyo = new PictureBox();
                    pbHoyo.Size = new Size(100, 50);
                    pbHoyo.Location = new Point(startX + col * offsetX + 100, startY + fila * offsetY);
                    pbHoyo.BackColor = Color.Transparent;
                    pbHoyo.MouseDown += IntentoGolpe;
                    this.Controls.Add(pbHoyo);
                    hoyos.Add(pbHoyo);
                    pbHoyo.Tag = "Hoyo";
                }
            }
            // Crear el estudiante ladilloso
            pbPersonas = new PictureBox();
            pbPersonas.Size = new Size(60, 150);
            pbPersonas.BackColor = Color.Transparent;
            pbPersonas.Visible = false;
            pbPersonas.Cursor = Cursors.Hand;
            pbPersonas.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonas.MouseDown += IntentoGolpe;
            this.Controls.Add(pbPersonas);
            pbPersonas.BringToFront();

            // Crear el Mazo (Sigue al cursor)
            pbMazo = new PictureBox();
            pbMazo.Size = new Size(80, 80);
            pbMazo.BackColor = Color.Transparent;
            object imgMazo = Properties.Resources.ResourceManager.GetObject("mazo_normal");
            if (imgMazo != null) pbMazo.Image = (Image)imgMazo;
            pbMazo.Visible = true; // Siempre visible ahora
            pbMazo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMazo.MouseDown += IntentoGolpe;
            this.Controls.Add(pbMazo);
            pbMazo.BringToFront();
            this.MouseDown += IntentoGolpe;
            // Ocultar cursor de Windows para usar el mazo
            Cursor.Hide();
            this.FormClosed += (s, e) => Cursor.Show(); // Asegurar restauración
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Asegurarnos de que todos los timers arranquen, tengan su intervalo base estricto y estén reconectados,
            // previendo cualquier problema o desconexión proveniente del Designer de Windows Forms.
            if (cursorTimer != null)
            {
                cursorTimer.Interval = 10;
                cursorTimer.Tick -= cursorTimer_Tick;
                cursorTimer.Tick += cursorTimer_Tick;
                cursorTimer.Start();
            }
            if (timerGlobal != null)
            {
                timerGlobal.Interval = 1000;
                timerGlobal.Tick -= timerGlobal_Tick;
                timerGlobal.Tick += timerGlobal_Tick;
                timerGlobal.Start();
            }
            if (mazoTimer != null)
            {
                mazoTimer.Interval = 300;
                mazoTimer.Tick -= mazoTimer_Tick;
                mazoTimer.Tick += mazoTimer_Tick;
            }
            if (estudiantesTimer != null)
            {
                estudiantesTimer.Interval = 600;
                estudiantesTimer.Tick -= estudiantesTimer_Tick;
                estudiantesTimer.Tick += estudiantesTimer_Tick;
            }

            MostrarSiguienteTopo();
        }
        private void MostrarSiguienteTopo()
        {
            // Elegir hoyo al azar
            int indice = rnd.Next(hoyos.Count);
            PictureBox hoyoElegido = hoyos[indice];
            // -- SELECCIÓN ALEATORIA DE IMAGEN DEL TOPO --
            // Aquí debes colocar los nombres exactos de tus imágenes en los Recursos
            string[] nombresPersonas = new string[] { "persona1", "persona2", "persona3", "persona4" };
            string personaAleatorio = nombresPersonas[rnd.Next(nombresPersonas.Length)];
            // Intenta extraer la imagen de las propiedades del proyecto mediante el nombre
            object recursoElegido = Properties.Resources.ResourceManager.GetObject(personaAleatorio);
            if (recursoElegido != null)
            {
                pbPersonas.Image = (Image)recursoElegido;
                pbPersonas.BackColor = Color.Transparent; // Quita el fondo de placeholder
            }
            else
            {
                // Fallback por si la imagen requerida aún no existe en los recursos
                pbPersonas.Image = null;
                pbPersonas.BackColor = Color.Gray;
            }
            // ---------------------------------------------
            // Aparecer INSTANTÁNEAMENTE en su posición final (sin delay)
            targetY = hoyoElegido.Location.Y - pbPersonas.Height + 20;

            pbPersonas.Location = new Point(
                hoyoElegido.Location.X + (hoyoElegido.Width / 2) - (pbPersonas.Width / 2),
                targetY
            );
            pbPersonas.Visible = true;
            pbPersonas.BringToFront();
            // Detener timer general e iniciarlo de inmediato
            estudiantesTimer.Stop();
            estudiantesTimer.Start();

            // Se detiene animSalidaTimer ya que se ha prescindido de él
            animSalidaTimer.Stop();
        }
        private void animSalidaTimer_Tick(object sender, EventArgs e)
        {
            pbPersonas.Top -= 4; // Velocidad de subida
            // Si alcanzó o superó el punto final
            if (pbPersonas.Top <= targetY)
            {
                pbPersonas.Top = targetY;       // Alinear a posición final
                animSalidaTimer.Stop();     // Fin de la animación
                estudiantesTimer.Start();          // Contar los 2 segundos a partir de aquí
            }
        }
        private void cursorTimer_Tick(object sender, EventArgs e)
        {
            // Mover el mazo al cursor
            Point p = this.PointToClient(Cursor.Position);
            pbMazo.Location = new Point(p.X - (pbMazo.Width / 2), p.Y - (pbMazo.Height / 2));
            pbMazo.BringToFront(); // Evitar que quede por debajo de topos
        }
        private void timerGlobal_Tick(object sender, EventArgs e)
        {
            tiempoGlobal--;
            lblTiempoGlobal.Text = "Tiempo: " + tiempoGlobal + "s";
            if (tiempoGlobal <= 0)
            {
                timerGlobal.Stop();
                estudiantesTimer.Stop();
                animSalidaTimer.Stop();
                MessageBox.Show("¡Se acabó el tiempo! Te botaron del departamento por ineficiente.", "Fin del Juego", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }
        private void estudiantesTimer_Tick(object sender, EventArgs e)
        {
            // Pasaron 2 segundos y el jugador no lo aplastó
            // Se mueve a otro hoyo
            MostrarSiguienteTopo();
        }
        private void IntentoGolpe(object sender, MouseEventArgs e)
        {
            // Siempre animar el golpe visualmente
            object imgGolpe = Properties.Resources.ResourceManager.GetObject("mazo_golpe");
            if (imgGolpe != null) pbMazo.Image = (Image)imgGolpe;
            else pbMazo.BackColor = Color.Orange; // Fallback visual

            mazoTimer.Stop(); // Reiniciar por si se hacen múltiples clicks muy rápidos
            mazoTimer.Start();
            // Averiguar en qué coordenada exacta de la pantalla se cloqueó
            Point cursorLoc = this.PointToClient(Cursor.Position);
            // 1) Verificar si golpearon al NPC 1
            if (pbNPC1.Bounds.Contains(cursorLoc))
            {
                if (estadoRebelion == 0)
                {
                    golpesAlNPC1++;
                    VerificarEasterEgg();
                    if (estadoRebelion == 0)
                    {
                        lblDialogoNPC1.Text = "¿Te pica la mano?";
                    }
                }
            }
            // 2) Verificar si golpearon al NPC 2
            else if (pbNPC2.Bounds.Contains(cursorLoc))
            {
                if (estadoRebelion == 0)
                {
                    golpesAlNPC2++;
                    VerificarEasterEgg();
                    if (estadoRebelion == 0)
                    {
                        lblDialogoNPC2.Text = "No quieres verme arrecha";
                    }
                }
            }
            // 3) Verificar si ya existía NPC3 y lo golpean
            else if (pbNPC3 != null && pbNPC3.Visible && pbNPC3.Bounds.Contains(cursorLoc))
            {
                // Con estadoRebelion == 3 se ignoran los daños, así que no hace falta reaccionar
            }
            // 4) Validar si la punta del cursor está chocando físicamente contra la "hitbox" del topo
            else if (pbPersonas.Visible && pbPersonas.Bounds.Contains(cursorLoc))
            {
                // ¡Lo aplastó!
                estudiantesTimer.Stop();
                pbPersonas.Visible = false;
                // Modificar los NPCs visualmente si nadie está en modo rebelión
                if (estadoRebelion == 0)
                {
                    string[] dialogos1 = { "¡Buena respuesta!", "¡Sigue así!", "¡Qué rápido!" };
                    string[] dialogos2 = { "¡Wow!", "¡Increíble!", "¡Uno menos!" };
                    dialogoBaseNPC1 = dialogos1[rnd.Next(dialogos1.Length)];
                    dialogoBaseNPC2 = dialogos2[rnd.Next(dialogos2.Length)];

                    if (lblDialogoNPC1.Text != "¿Te pica la mano?") lblDialogoNPC1.Text = dialogoBaseNPC1;
                    if (lblDialogoNPC2.Text != "No quieres verme arrecha") lblDialogoNPC2.Text = dialogoBaseNPC2;
                    if (lblDialogoNPC3 != null && lblDialogoNPC3.Text != "ME LLEVA EL DIABLO") lblDialogoNPC3.Text = dialogoBaseNPC3;
                }
                mensajesRespondidos++;
                lblPuntuacion.Text = "Mensajes respondidos: " + mensajesRespondidos + " / " + MAX_PERSONAS;
                // Verificar victoria internamente al instante de dar el golpe
                if (mensajesRespondidos >= MAX_PERSONAS)
                {
                    MessageBox.Show("¡Felicidades! Has respondido 6 mensajes del DM de Unimar.", "Minijuego Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MostrarSiguienteTopo(); // Llama al siguiente al instante sin esperar el mazo
                }
            }
        }
        private void VerificarEasterEgg()
        {
            // Evento C: Si ambos completan exactamente 3 golpes, aparece NPC 3
            if (golpesAlNPC1 >= 3 && golpesAlNPC2 >= 3 && pbNPC3 == null && estadoRebelion == 0)
            {
                estadoRebelion = 3;

                pbNPC3 = new PictureBox();
                pbNPC3.Size = new Size(50, 120);
                pbNPC3.BackColor = Color.Transparent;
                pbNPC3.Location = new Point(this.ClientSize.Width - 450, this.ClientSize.Height - 150);
                pbNPC3.SizeMode = PictureBoxSizeMode.StretchImage;
                pbNPC3.MouseDown += IntentoGolpe;
                object img3 = Properties.Resources.ResourceManager.GetObject("joa_arrecha");
                if (img3 != null) pbNPC3.Image = (Image)img3;
                this.Controls.Add(pbNPC3);
                pbNPC3.BringToFront();
                lblDialogoNPC3 = new Label();
                lblDialogoNPC3.Text = dialogoBaseNPC3;
                lblDialogoNPC3.Font = new Font("Arial", 8, FontStyle.Italic);
                lblDialogoNPC3.ForeColor = Color.Black;
                lblDialogoNPC3.Location = new Point(pbNPC3.Location.X - 20, pbNPC3.Location.Y - 30);
                lblDialogoNPC3.AutoSize = true;
                this.Controls.Add(lblDialogoNPC3);
                lblDialogoNPC3.BringToFront();
                
                estudiantesTimer.Stop();
                estudiantesTimer.Interval = 200;
                estudiantesTimer.Start();

                // Conjelar diálogos de los NPC 1 y 2
                lblDialogoNPC1.Text = "...";
                lblDialogoNPC2.Text = "...";
            }
            // Evento A: NPC 1 alcanza 5 golpes
            if (golpesAlNPC1 >= 5 && estadoRebelion == 0)
            {
                estadoRebelion = 1;

                // --- CAMBIA IMAGENES Y DIALOGOS (NPC 1 SE ENOJA) ---
                object imgEnojado1 = Properties.Resources.ResourceManager.GetObject("ana_arrecha");
                if (imgEnojado1 != null) pbNPC1.Image = (Image)imgEnojado1; else pbNPC1.BackColor = Color.Transparent;
                lblDialogoNPC1.Text = "TU TE LO BUSCASTE";

                object imgReaccion2 = Properties.Resources.ResourceManager.GetObject("soraya_asustada");
                if (imgReaccion2 != null) pbNPC2.Image = (Image)imgReaccion2; else pbNPC2.BackColor = Color.Transparent;
                lblDialogoNPC2.Text = "oh oh";
                // --- AJUSTAR VELOCIDAD DE LOS TOPOS AQUÍ (Para NPC1 Enojado) ---
                estudiantesTimer.Stop();
                estudiantesTimer.Interval = 500; // <- MODIFICAR AQUI
                estudiantesTimer.Start();
            }
            // Evento B: NPC 2 alcanza 5 golpes
            else if (golpesAlNPC2 >= 5 && estadoRebelion == 0)
            {
                estadoRebelion = 2;

                // --- CAMBIA IMAGENES Y DIALOGOS (NPC 2 SE ENOJA) ---
                object imgEnojado2 = Properties.Resources.ResourceManager.GetObject("soraya_arrecha");
                if (imgEnojado2 != null) pbNPC2.Image = (Image)imgEnojado2; else pbNPC2.BackColor = Color.Transparent;
                lblDialogoNPC2.Text = "VOY A LLAMAR A ANGELINA";

                object imgReaccion1 = Properties.Resources.ResourceManager.GetObject("ana_asustada");
                if (imgReaccion1 != null) pbNPC1.Image = (Image)imgReaccion1; else pbNPC1.BackColor = Color.Transparent;
                lblDialogoNPC1.Text = "uy";
                // --- AJUSTAR VELOCIDAD DE LOS TOPOS AQUÍ (Para NPC2 Enojado) ---
                estudiantesTimer.Stop();
                estudiantesTimer.Interval = 400; // <- MODIFICAR AQUI
                estudiantesTimer.Start();
            }
        }
        private void mazoTimer_Tick(object sender, EventArgs e)
        {
            mazoTimer.Stop();

            // Revertir imagen del mazo a la normal (solo función visual ahora)
            object imgNormal = Properties.Resources.ResourceManager.GetObject("mazo_normal");
            if (imgNormal != null) pbMazo.Image = (Image)imgNormal;
            else pbMazo.BackColor = Color.Yellow; // Fallback
        }
    }
}
