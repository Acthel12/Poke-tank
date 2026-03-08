using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Batalla_Mejorada : Form
    {
        //para saber si el juego termino
        bool juegoTerminado = false;

        //balas
        List<Bala> listaBalas = new List<Bala>();
        Image imgBala = Properties.Resources.bola_de_fuego_arriba; // Carga la imagen una sola vez

        //explosion
        List<Explosion> listaExplosiones = new List<Explosion>();
        Image imgExplosion = Properties.Resources.explosion; // Reemplaza por tu imagen real

        //IMÁGENES PRE-CARGADAS
        Bitmap imgJugador;
        Bitmap imgEnemigo;
        Bitmap imgDefensa; // La versión con escudo

        // Definimos los rectángulos de colisión para usarlos más fácil
        Rectangle boundsJugador = new Rectangle(100, 400, 60, 60);
        Rectangle boundsEnemigo = new Rectangle(300, 50, 60, 60);

        // Variables de movimiento y estado
        bool moverIzquierda, moverDerecha, defendiendo;
        int velocidadTanque = 5;
        int saludEnemigoMax = 100;
        int saludEnemigo = 100;
        private Bitmap fondoBuffered;
        public Batalla_Mejorada()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            // Pre-renderizamos el fondo una sola vez:
            // Esto "dibuja" el fondo en una memoria rápida.
            fondoBuffered = new Bitmap(Properties.Resources.fondoNivel1, this.ClientSize.Width, this.ClientSize.Height);
            // --- CARGAR IMÁGENES DESDE RESOURCES ---
            imgJugador = new Bitmap(Properties.Resources.flavio_de_espalda_batalla);
            imgEnemigo = new Bitmap(Properties.Resources.flavio_de_espalda_batalla);
            imgDefensa = new Bitmap(Properties.Resources.flavio_escudo);

            //tamaño de tanques
            int anchoTanque = 250;
            int altoTanque = 300;

            //Calcula el centro horizontal del formulario
            int centroX = (this.ClientSize.Width / 2) - (anchoTanque / 2);

            //Configura los puestos (Arriba para enemigo, Abajo para jugador)
            boundsEnemigo = new Rectangle(centroX, 30, anchoTanque, altoTanque);
            boundsJugador = new Rectangle(centroX, this.ClientSize.Height - altoTanque - 50, anchoTanque, altoTanque);
        }
        private void Batalla_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Mejor calidad
            
            //Dibujar el fondo
            if (fondoBuffered != null) g.DrawImage(fondoBuffered, 0, 0);

            // Dibujamos cada bala de la lista
            foreach (var b in listaBalas)
            {
                g.DrawImage(imgBala, b.X, b.Y, 50, 50);
            }
            //Tanques (Dibujamos el Bitmap en la posición del Rectangle)
            Image imgCuerpo = defendiendo ? imgDefensa : imgJugador;
            g.DrawImage(imgCuerpo, boundsJugador);

            if (saludEnemigo > 0)
            {
                g.DrawImage(imgEnemigo, boundsEnemigo);

                // Barra de vida (usando las coordenadas centradas)
                float porcentaje = (float)saludEnemigo / 100;
                g.FillRectangle(Brushes.Red, boundsEnemigo.X, boundsEnemigo.Y - 15, boundsEnemigo.Width, 8);
                g.FillRectangle(Brushes.Green, boundsEnemigo.X, boundsEnemigo.Y - 15, boundsEnemigo.Width * porcentaje, 8);
            }

            // 4. Dibujar las explosiones (**AHORA ENCIMA DE LOS TANQUES**)
            foreach (var ex in listaExplosiones)
            {
                g.DrawImage(imgExplosion, ex.X - 20, ex.Y - 20, 60, 60);
            }

            //pantalla de victoria
            if (juegoTerminado)
            {
                // 1. Crear un velo oscuro semitransparente (R, G, B, Alpha)
                using (SolidBrush velo = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    g.FillRectangle(velo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }

                // 2. Configurar la fuente y el mensaje
                Font fuenteVictoria = new Font("Arial", 40, FontStyle.Bold);
                Font fuenteContinuar = new Font("Arial", 14, FontStyle.Regular);
                string mensaje = "¡VICTORIA!";
                string subMensaje = "Presiona ENTER para continuar :)";

                // 3. Calcular el centro exacto para el texto
                SizeF tamMsg = g.MeasureString(mensaje, fuenteVictoria);
                SizeF tamSub = g.MeasureString(subMensaje, fuenteContinuar);

                float xMsg = (this.ClientSize.Width / 2) - (tamMsg.Width / 2);
                float yMsg = (this.ClientSize.Height / 2) - (tamMsg.Height / 2);

                // 4. Dibujar el texto con una pequeña sombra para que resalte
                g.DrawString(mensaje, fuenteVictoria, Brushes.Black, xMsg + 3, yMsg + 3); // Sombra
                g.DrawString(mensaje, fuenteVictoria, Brushes.Gold, xMsg, yMsg);        // Texto dorado

                g.DrawString(subMensaje, fuenteContinuar, Brushes.White,
                            (this.ClientSize.Width / 2) - (tamSub.Width / 2), yMsg + tamMsg.Height + 10);
            }
        }

        //EVENTO KEYDOWN: Cuando presionas una tecla
        private void Batalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) moverIzquierda = true;
            if (e.KeyCode == Keys.D) moverDerecha = true;

            // Activar escudo
            if (e.KeyCode == Keys.Q && !defendiendo)
            {
                defendiendo = true;
                velocidadTanque = 2; // Reducimos la velocidad mientras el escudo está activo
                this.Invalidate();
            }

            // Disparar bola de fuego (Solo si NO está defendiendo)
            if (e.KeyCode == Keys.E)
            {
                if (!defendiendo)
                {
                    DispararBolaDeFuego(1, boundsJugador); // 1 es el jugador
                }
                else
                {
                    // MOSTRAR MENSAJE EN PANTALLA
                    aviso.Text = "¡No puedes disparar con el escudo activo!";
                    aviso.Left = boundsJugador.X;
                    aviso.Top = boundsJugador.Y - 30; // 30 píxeles por encima del tanque
                    aviso.Visible = true;

                    // Reiniciamos el timer por si el usuario presiona muchas veces
                    timerAviso.Stop();
                    timerAviso.Start();
                }
            }
        }

        //EVENTO KEYUP: Cuando sueltas una tecla
        private void Batalla_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) moverIzquierda = false;
            if (e.KeyCode == Keys.D) moverDerecha = false;

            // Desactivar escudo
            if (e.KeyCode == Keys.Q)
            {
                defendiendo = false;
                velocidadTanque = 5;
                this.Invalidate();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Si el juego terminó, no procesamos nada más
            if (juegoTerminado)
            {
                return;
            }

            // Movimiento del jugador
            if (moverIzquierda && boundsJugador.X > 0)
            {
                boundsJugador.X -= velocidadTanque;
            }
            if (moverDerecha && (boundsJugador.X + boundsJugador.Width) < this.ClientSize.Width)
            {
                boundsJugador.X += velocidadTanque;
            }

            // Movimiento y colisión de balas
            for (int i = listaBalas.Count - 1; i >= 0; i--)
            {
                var b = listaBalas[i];
                b.Y -= 15; // Velocidad de la bala

                //Si la bala es del jugador (ID 1) y toca al enemigo
                if (b.IDDueño == 1 && b.Bounds.IntersectsWith(boundsEnemigo))
                {
                    saludEnemigo -= 10;

                    // Centramos la explosión en el punto de impacto. 
                    // Usaremos una duración de 15 ticks (aprox 0.3 segundos si el timer va a 20ms).
                    listaExplosiones.Add(new Explosion(b.X, b.Y, 15));

                    listaBalas.RemoveAt(i);
                    continue; // Pasamos a la siguiente bala
                }

                // Eliminar si sale de pantalla
                if (b.Y < -50) listaBalas.RemoveAt(i);
            }
            //ACTUALIZAR Y LIMPIAR EXPLOSIONES
            for (int i = listaExplosiones.Count - 1; i >= 0; i--)
            {
                var ex = listaExplosiones[i];
                ex.ContadorVida++; // Envejecer

                // Si su vida termina, la eliminamos de la lista
                if (ex.ContadorVida >= ex.DuracionMaxima)
                {
                    listaExplosiones.RemoveAt(i);
                }
            }

            //para acabar el juego
            if (saludEnemigo <= 0)
            {
                saludEnemigo = 0;
                juegoTerminado = true;
                GameTimer.Stop(); // congelar todo
            }

            //Refresca el dibujo
            this.Invalidate();
        }
       
        //MÉTODO PARA LA BOLA DE FUEGO
        private void DispararBolaDeFuego(int idDueño, Rectangle posTirador)
        {
            if (defendiendo && idDueño == 1) return;

            Bala nuevaBala = new Bala
            {
                X = posTirador.X + (posTirador.Width / 2) - 10,
                Y = posTirador.Y - 25,
                IDDueño = idDueño
            };
            listaBalas.Add(nuevaBala);
        }
        private void timerAviso_Tick(object sender, EventArgs e)
        {
            aviso.Visible = false; // Oculta el mensaje
            timerAviso.Stop();        // Se detiene a sí mismo
        }
    }
    public class Bala
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int IDDueño { get; set; }
        // Usamos Rectangle (enteros) para evitar el error de RectangleF
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, 20, 20);
    }

    public class Explosion
        //explosion al impactar la bala
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int ContadorVida { get; set; } // Cuántos "ticks" ha estado viva
        public int DuracionMaxima { get; set; } // Cuántos "ticks" durará en total

        public Explosion(float x, float y, int duracionEnTicks)
        {
            X = x;
            Y = y;
            ContadorVida = 0;
            DuracionMaxima = duracionEnTicks;
        }
    }
}