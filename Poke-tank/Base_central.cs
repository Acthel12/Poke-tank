using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace Poke_tank
{
    public partial class Base_central : Form
    {
        // Variables de Posición y Velocidad del tanque
        private int x = 0;
        private int y = 0;
        private int speed => 5 + DatosGlobales.BonusVelocidadGlobal; // Velocidad Base + Pulida
        // Variables para detectar qué tecla está presionada
        private bool goUp, goDown, goLeft, goRight;
        // Variables de Animación y Spritesheet
        private Bitmap spriteSheet;
        private int drawingWidth = 70; // Tamaño visual del tanque
        private int drawingHeight = 70;
        private int currentFrame = 0;
        // Máscara
        Bitmap MASCARA_BASE;
        // Sonido
        System.Media.SoundPlayer sonidoMotor = new System.Media.SoundPlayer(Properties.Resources.SONIDO_TANQUE_MOVIMIENTO);
        bool sonidoEstaReproduciendo = false;
        // Zonas Interactivas (Puestos temporales)
        Rectangle zonaDron;
        Rectangle zonaRedes;
        Rectangle zonaBuscaminas;
        Rectangle zonaQuiz;
        Rectangle zonaPuleCalvas;
        bool ventanaAbierta = false;
        private System.Windows.Forms.Timer gameTimer;
        public Base_central()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.ClientSize = new Size(1280, 720); // Mismo tamaño que el mapa
            spriteSheet = Properties.Resources.SPRITE_SHEET_FLAVIO;
            MASCARA_BASE = new Bitmap(Properties.Resources.MASCARA_MAPA_BASE_CENTRAL_COMPLETO);
            // Inicializar al personaje en la parte inferior central
            x = (this.ClientSize.Width / 2) - (drawingWidth / 2);
            y = this.ClientSize.Height - 300; // Ajustado más arriba para que caiga dentro del área blanca de la máscara de colisiones
            // Definir Puestos (Provisionales, puedes editarlos o dibujarlos luego)
            zonaDron = new Rectangle(592,121, 51, 62);
            zonaRedes = new Rectangle(261, 236, 51, 62);
            zonaBuscaminas = new Rectangle(944, 236, 51, 62);
            zonaQuiz = new Rectangle(220, 476, 125, 62);
            zonaPuleCalvas = new Rectangle(880, 476, 125, 62);
            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
            this.Paint += DrawGame;
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) goDown = true;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) goRight = true;
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) goUp = false;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) goDown = false;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) goLeft = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) goRight = false;
        }
        private void GameLoop(object sender, EventArgs e)
        {
            bool isMoving = false;
            int nextX = x;
            int nextY = y;
            int nextFrame = currentFrame;
            if (goUp && goRight) { nextY -= speed; nextX += speed; nextFrame = 3; isMoving = true; }
            else if (goDown && goRight) { nextY += speed; nextX += speed; nextFrame = 1; isMoving = true; }
            else if (goDown && goLeft) { nextY += speed; nextX -= speed; nextFrame = 7; isMoving = true; }
            else if (goUp && goLeft) { nextY -= speed; nextX -= speed; nextFrame = 5; isMoving = true; }
            else if (goUp) { nextY -= speed; nextFrame = 4; isMoving = true; }
            else if (goRight) { nextX += speed; nextFrame = 2; isMoving = true; }
            else if (goDown) { nextY += speed; nextFrame = 0; isMoving = true; }
            else if (goLeft) { nextX -= speed; nextFrame = 6; isMoving = true; }
            if (isMoving)
            {
                float scaleX = (float)MASCARA_BASE.Width / this.ClientSize.Width;
                float scaleY = (float)MASCARA_BASE.Height / this.ClientSize.Height;
                int checkX = (int)((nextX + (drawingWidth / 2)) * scaleX);
                int checkY = (int)((nextY + (drawingHeight / 2)) * scaleY);
                //VALIDAR CONTRA LA MÁSCARA
                if (checkX >= 0 && checkX < MASCARA_BASE.Width && checkY >= 0 && checkY < MASCARA_BASE.Height)
                {
                    Color colorMask = MASCARA_BASE.GetPixel(checkX, checkY);
                    // Si el color es Blanco (o muy claro), el camino está libre
                    if (colorMask.R > 200 && colorMask.G > 200 && colorMask.B > 200)
                        // Si el color es Blanco (o bastante claro), el camino está libre
                        if (colorMask.R > 150 && colorMask.G > 150 && colorMask.B > 150)
                        {
                            x = nextX;
                            y = nextY;
                            currentFrame = nextFrame;
                            // Sonido (opcional)
                            if (!sonidoEstaReproduciendo)
                            {
                                sonidoMotor.PlayLooping();
                                sonidoEstaReproduciendo = true;
                            }
                        }
                        else
                        {
                            // Es negro o gris, no se mueve el personaje
                            isMoving = false;
                        }
                }
                // COMPROBACIÓN DE PUESTOS
                Rectangle rectTanque = new Rectangle(x, y, drawingWidth, drawingHeight);
                if (rectTanque.IntersectsWith(zonaDron))
                {
                    EvaluarInteraccion("Minijuego Dron", typeof(MinijuegoDron)); //minijuego dron (simarca)
                }
                else if (rectTanque.IntersectsWith(zonaBuscaminas))
                {
                    EvaluarInteraccion("Minijuego Buscaminas", typeof(Buscaminas)); //minijuego minas (decanato)
                }
                else if (rectTanque.IntersectsWith(zonaRedes))
                {
                    // Aquí usamos el nombre de tu nuevo formulario profesional
                    EvaluarInteraccion("Minijuego Responde Redes", typeof(MinijuegoRedes));
                }
                else if (rectTanque.IntersectsWith(zonaPuleCalvas))
                {
                    EvaluarInteraccion("Pulida de Calva", typeof(Pulir_Calva)); //pagar por una pulida de calva (boost de velocidad de movimiento en los niveles)
                }
                else if (rectTanque.IntersectsWith(zonaQuiz))
                {
                    EvaluarInteraccion("Minijuego Quiz", typeof(MinijuegoQuiz)); //minijuego andres
                }
            }
            if (!isMoving && sonidoEstaReproduciendo)
            {
                sonidoMotor.Stop();
                sonidoEstaReproduciendo = false;
            }
            this.Invalidate();
        }
        private void EvaluarInteraccion(string nombreSitio, Type frmType)
        {
            if (ventanaAbierta) return;
            ventanaAbierta = true;
            // Pausamos el motor del juego y el sonido
            gameTimer.Stop();
            if (sonidoEstaReproduciendo)
            {
                sonidoMotor.Stop();
                sonidoEstaReproduciendo = false;
            }
            // Preguntar al usuario con Dialogo personalizado
            DialogResult respuesta = MostrarMensajePersonalizado(nombreSitio);
            if (respuesta == DialogResult.Yes)
            {
                // Instanciar y abrir la ventana destino (Nivel o tienda, etc)
                Form frmDestino = (Form)Activator.CreateInstance(frmType);
                frmDestino.ShowDialog();
            }
            // TELETRANSPORTE (Retroceso simple rebotando hacia atrás)
            if (goUp) y += speed * 5;
            else if (goDown) y -= speed * 5;
            else if (goLeft) x += speed * 5;
            else if (goRight) x -= speed * 5;
            // Limpieza de Teclas (previene que el personaje vuelva caminando automático)
            goUp = false; goDown = false; goLeft = false; goRight = false;

            ventanaAbierta = false;
            gameTimer.Start();
        }
        private DialogResult MostrarMensajePersonalizado(string destino)
        {
            using (Form msjForm = new Form())
            {
                msjForm.StartPosition = FormStartPosition.CenterParent;
                msjForm.FormBorderStyle = FormBorderStyle.None;
                msjForm.Size = new Size(500, 250);
                msjForm.BackColor = Color.FromArgb(25, 25, 35);
                Label lblMsg = new Label();
                lblMsg.Text = "¿Deseas entrar a " + destino + "?";
                lblMsg.ForeColor = Color.WhiteSmoke;
                lblMsg.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                lblMsg.AutoSize = false;
                lblMsg.TextAlign = ContentAlignment.MiddleCenter;
                lblMsg.Dock = DockStyle.Top;
                lblMsg.Height = 120;
                msjForm.Controls.Add(lblMsg);
                Button btnSi = new Button();
                btnSi.Text = "SÍ";
                btnSi.DialogResult = DialogResult.Yes;
                btnSi.BackColor = Color.SeaGreen;
                btnSi.ForeColor = Color.White;
                btnSi.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                btnSi.FlatStyle = FlatStyle.Flat;
                btnSi.FlatAppearance.BorderSize = 0;
                btnSi.Bounds = new Rectangle(50, 150, 180, 60);
                msjForm.Controls.Add(btnSi);
                Button btnNo = new Button();
                btnNo.Text = "NO";
                btnNo.DialogResult = DialogResult.No;
                btnNo.BackColor = Color.IndianRed;
                btnNo.ForeColor = Color.White;
                btnNo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                btnNo.FlatStyle = FlatStyle.Flat;
                btnNo.FlatAppearance.BorderSize = 0;
                btnNo.Bounds = new Rectangle(270, 150, 180, 60);
                msjForm.Controls.Add(btnNo);
                return msjForm.ShowDialog(this);
            }
        }
        private void DrawGame(object sender, PaintEventArgs e)
        {
            if (spriteSheet != null)
            {
                int exactFrameWidth = spriteSheet.Width / 4;
                int exactFrameHeight = spriteSheet.Height / 2;
                int column = currentFrame % 4;
                int row = currentFrame / 4;
                Rectangle sourceRect = new Rectangle(column * exactFrameWidth, row * exactFrameHeight, exactFrameWidth, exactFrameHeight);
                float ratio = (float)exactFrameHeight / exactFrameWidth;
                drawingHeight = (int)(drawingWidth * ratio);
                Rectangle destRect = new Rectangle(x, y, drawingWidth, drawingHeight);
                e.Graphics.DrawImage(spriteSheet, destRect, sourceRect, GraphicsUnit.Pixel);
            }
        }
    }
}
