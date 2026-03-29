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
        Rectangle zonaArriba;
        Rectangle zonaCentro;
        Rectangle zonaIzquierda;
        Rectangle zonaDerecha;

        bool ventanaAbierta = false;
        private System.Windows.Forms.Timer gameTimer;

        public Base_central()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.ClientSize = new Size(1280, 720); // Mismo tamaño que el mapa

            spriteSheet = Properties.Resources.SPRITE_SHEET_FLAVIO;
            MASCARA_BASE = new Bitmap(Properties.Resources.MASCARA_MAPA_FINAL); // Puedes reemplazar x MASCARA_BASE_CENTRAL

            // Inicializar al personaje en la parte inferior central
            x = (this.ClientSize.Width / 2) - (drawingWidth / 2);
            y = this.ClientSize.Height - 150; // Justo en la parte inferior visible

            // Definir Puestos (Provisionales, puedes editarlos o dibujarlos luego)
            zonaArriba = new Rectangle((this.ClientSize.Width / 2) - 50, 50, 100, 100);
            zonaIzquierda = new Rectangle(50, (this.ClientSize.Height / 2) - 50, 100, 100);
            zonaDerecha = new Rectangle(this.ClientSize.Width - 150, (this.ClientSize.Height / 2) - 50, 100, 100);
            zonaCentro = new Rectangle((this.ClientSize.Width / 2) - 50, (this.ClientSize.Height / 2) - 50, 100, 100);

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

                if (checkX >= 0 && checkX < MASCARA_BASE.Width && checkY >= 0 && checkY < MASCARA_BASE.Height)
                {
                    Color colorMask = MASCARA_BASE.GetPixel(checkX, checkY);
                    if (true) //(colorMask.R > 200 && colorMask.G > 200 && colorMask.B > 200) //ARREGLAR SEGUN MASCARA
                    {
                        x = nextX;
                        y = nextY;
                        currentFrame = nextFrame;

                        if (!sonidoEstaReproduciendo)
                        {
                            sonidoMotor.PlayLooping();
                            sonidoEstaReproduciendo = true;
                        }
                    }
                    else isMoving = false; // Bloqueado x máscara
                }
                else isMoving = false; // Fuera del mapa

                // COMPROBACIÓN DE PUESTOS
                Rectangle rectTanque = new Rectangle(x, y, drawingWidth, drawingHeight);

                if (rectTanque.IntersectsWith(zonaArriba))
                {
                    EvaluarInteraccion("Minijuego Dron", typeof(MinijuegoDron)); //minijuego dron (simarca)
                }
                else if (rectTanque.IntersectsWith(zonaDerecha))
                {
                    EvaluarInteraccion("Minijuego Buscaminas", typeof(Buscaminas)); //minijuego minas (decanato)
                }
                else if (rectTanque.IntersectsWith(zonaIzquierda))
                {
                    EvaluarInteraccion("Minijuego Responde Redes", typeof(nivel_3)); //minijuego whack-a-mole (direccion de comunicacion)
                }
                else if (rectTanque.IntersectsWith(zonaCentro))
                {
                    EvaluarInteraccion("Pulida de Calva", typeof(Pulir_Calva)); //pagar por una pulida de calva (boost de velocidad de movimiento en los niveles)
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
                msjForm.BackColor = Color.FromArgb(25, 25, 35); // Oscuro premium

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
            // Opcional: Para ver los puestos temporalmente:
            using (SolidBrush transparentBlue = new SolidBrush(Color.FromArgb(100, 0, 0, 255)))
            {
                e.Graphics.FillRectangle(transparentBlue, zonaArriba);
                e.Graphics.FillRectangle(transparentBlue, zonaDerecha);
                e.Graphics.FillRectangle(transparentBlue, zonaIzquierda);
                e.Graphics.FillRectangle(transparentBlue, zonaCentro);
            }
            
            // Opcional: Nombres de puestos temporales
            e.Graphics.DrawString("Minijuego Dron", this.Font, Brushes.White, zonaArriba.Location);
            e.Graphics.DrawString("Minijuego Minas", this.Font, Brushes.White, zonaDerecha.Location);
            e.Graphics.DrawString("Minijuego Redes", this.Font, Brushes.White, zonaIzquierda.Location);
            e.Graphics.DrawString("Pule calvas", this.Font, Brushes.White, zonaCentro.Location);

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
