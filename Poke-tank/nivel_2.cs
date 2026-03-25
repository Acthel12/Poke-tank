using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class nivel_2 : Form
    {
        //para saber si el juego termino
        bool juegoTerminado = false;

        // --- CONTROLES VISUALES UI ---
        string textoInstrucciones = "";
        bool keyA_Pressed = false, keyD_Pressed = false, keyQ_Pressed = false, keyE_Pressed = false;
        Image imgKeyANormal, imgKeyAPressed, imgKeyDNormal, imgKeyDPressed;
        Image imgKeyQNormal, imgKeyQPressed, imgKeyENormal, imgKeyEPressed;

        //balas
        List<Bala_Nivel_2> listaBalas = new List<Bala_Nivel_2>();

        // --- IMÁGENES DE LAS BALAS ---
        Bitmap imgBalaJugador;
        Bitmap imgBalaEnemiga;

        //explosion
        List<Explosion_Nivel_2> listaExplosiones = new List<Explosion_Nivel_2>();
        Image imgExplosion = Properties.Resources.explosion;

        //IMÁGENES PRE-CARGADAS
        Bitmap imgJugador;
        Bitmap imgEnemigo;
        Bitmap imgDefensa;
        Bitmap imgEscudoEnemigo; // La versión con escudo

        // Definimos los rectángulos de colisión para usarlos más fácil
        Rectangle boundsJugador = new Rectangle(100, 400, 60, 60);
        Rectangle boundsEnemigo = new Rectangle(300, 50, 60, 60);

        // Variables de movimiento y estado
        bool moverIzquierda, moverDerecha, defendiendo;
        int velocidadTanque = 5 + DatosGlobales.BonusVelocidadGlobal;
        int saludEnemigoMax = 100;
        int saludEnemigo = 100;
        private Bitmap fondoBuffered;

        // --- SALUD DEL JUGADOR ---
        int saludJugadorMax = 100;
        int saludJugador = 100;

        // --- VARIABLES DE LA IA DEL ENEMIGO ---
        Random iaRandom = new Random();
        int direccionEnemigo = 1; // 1 = Derecha, -1 = Izquierda
        int velocidadEnemigo = 5;
        bool enemigoDefendiendo = false;
        int tiempoDefensaEnemigo = 0;   // Cuánto tiempo le queda al escudo
        int cooldownDisparoEnemigo = 0; // Tiempo de espera entre disparos

        public nivel_2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            // Pre-renderizamos el fondo una sola vez:
            fondoBuffered = new Bitmap(Properties.Resources.fondoNivel1, this.ClientSize.Width, this.ClientSize.Height);
            // --- CARGAR IMÁGENES DESDE RESOURCES ---
            imgJugador = new Bitmap(Properties.Resources.flavio_de_espalda_batalla);
            imgEnemigo = new Bitmap(Properties.Resources.luis_quimica_de_frente);
            imgDefensa = new Bitmap(Properties.Resources.flavio_escudo);
            imgEscudoEnemigo = new Bitmap(Properties.Resources.luis_con_escudo);

            // CARGAMOS LAS BALAS
            imgBalaJugador = new Bitmap(Properties.Resources.bola_de_fuego_arriba);

            imgBalaEnemiga = new Bitmap(Properties.Resources.bala_luis);
            imgBalaEnemiga.RotateFlip(RotateFlipType.Rotate180FlipNone); // Volteada la bala enemiga hacia abajo

            // --- CARGAR IMÁGENES DE CONTROLES (REEMPLAZA POR TUS IMÁGENES DE TECLAS) ---
            imgKeyANormal = Properties.Resources.flavio_escudo; //Tecla A Normal
            imgKeyAPressed = Properties.Resources.explosion;   
            imgKeyDNormal = Properties.Resources.flavio_escudo; // Tecla D
            imgKeyDPressed = Properties.Resources.explosion;
            imgKeyQNormal = Properties.Resources.flavio_escudo; // Tecla Q
            imgKeyQPressed = Properties.Resources.explosion;
            imgKeyENormal = Properties.Resources.flavio_escudo; // Tecla E
            imgKeyEPressed = Properties.Resources.explosion;

            //instruccion
            textoInstrucciones = "NIVEL 2: Dispara al tanque enemigo hasta destruirlo. Usa el escudo (Q) para defenderte.";

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
            if (fondoBuffered != null) g.DrawImageUnscaled(fondoBuffered, 0, 0);

            // DIBUJAR UI MANUALMENTE (OPTIMIZACIÓN PARA EVITAR LAG)
            Font fInstrucciones = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(textoInstrucciones, fInstrucciones, Brushes.Black, 22, 22); // sombra
            g.DrawString(textoInstrucciones, fInstrucciones, Brushes.White, 20, 20);

            int pbSize = 60;
            int margin = 10;
            int startX = this.ClientSize.Width - (pbSize * 4) - (margin * 4) - 20; 
            int startY = this.ClientSize.Height - pbSize - margin - 20;

            g.DrawImage(keyA_Pressed ? imgKeyAPressed : imgKeyANormal, startX, startY, pbSize, pbSize);
            g.DrawImage(keyD_Pressed ? imgKeyDPressed : imgKeyDNormal, startX + pbSize + margin, startY, pbSize, pbSize);
            g.DrawImage(keyQ_Pressed ? imgKeyQPressed : imgKeyQNormal, startX + pbSize*2 + margin*2, startY, pbSize, pbSize);
            g.DrawImage(keyE_Pressed ? imgKeyEPressed : imgKeyENormal, startX + pbSize*3 + margin*3, startY, pbSize, pbSize);

            //Balas
            foreach (var b in listaBalas)
            {
                // Si el dueño es 1 (Jugador) usa la bala que mira hacia arriba.
                // Si el dueño es 2 (Enemigo) usa la bala que mira hacia abajo.
                Image imagenBalaActual = (b.IDDueño == 1) ? imgBalaJugador : imgBalaEnemiga;

                g.DrawImage(imagenBalaActual, b.X, b.Y, 80, 80);
            }
            //Tanques (Dibujamos el Bitmap en la posición del Rectangle)
            Image imgCuerpo = defendiendo ? imgDefensa : imgJugador;
            g.DrawImage(imgCuerpo, boundsJugador);

            if (saludEnemigo > 0)
            {
                Image imgEnemigoActual = enemigoDefendiendo ? imgEscudoEnemigo : imgEnemigo;
                g.DrawImage(imgEnemigoActual, boundsEnemigo);

                //Barra de vida enemiga
                int anchoBarraEnemigo = 150;
                int altoBarraEnemigo = 15;

                //Centrada justo encima del tanque
                float xBarraEnemigo = boundsEnemigo.X + (boundsEnemigo.Width / 2) - (anchoBarraEnemigo / 2);
                float yBarraEnemigo = boundsEnemigo.Y - 15;

                //Fondo gris de la barra
                g.FillRectangle(Brushes.Gray, xBarraEnemigo, yBarraEnemigo, anchoBarraEnemigo, altoBarraEnemigo);

                //Calcular porcentaje de vida
                float porcentajeVidaEnemigo = (float)saludEnemigo / saludEnemigoMax;
                int anchoVidaActualEnemigo = (int)(anchoBarraEnemigo * porcentajeVidaEnemigo);

                //Elegir color según la salud (Verde -> Amarillo -> Rojo)
                Brush colorBarraEnemigo = Brushes.Green;
                if (porcentajeVidaEnemigo < 0.3) colorBarraEnemigo = Brushes.Red;
                else if (porcentajeVidaEnemigo < 0.6) colorBarraEnemigo = Brushes.Yellow;

                //Dibujar la vida restante
                g.FillRectangle(colorBarraEnemigo, xBarraEnemigo, yBarraEnemigo, anchoVidaActualEnemigo, altoBarraEnemigo);

                //Borde negro
                g.DrawRectangle(Pens.Black, xBarraEnemigo, yBarraEnemigo, anchoBarraEnemigo, altoBarraEnemigo);
            }

            //barra de vida del jugador
            if (saludJugador > 0)
            {
                int anchoBarraJugador = 150;
                int altoBarraJugador = 15;

                //Centrada justo encima de tu tanque
                float xBarraJugador = boundsJugador.X + (boundsJugador.Width / 2) - (anchoBarraJugador / 2);
                float yBarraJugador = boundsJugador.Y - 15;

                //Fondo gris de la barra
                g.FillRectangle(Brushes.Gray, xBarraJugador, yBarraJugador, anchoBarraJugador, altoBarraJugador);

                //Calcular porcentaje de vida
                float porcentajeVidaJugador = (float)saludJugador / saludJugadorMax;
                int anchoVidaActualJugador = (int)(anchoBarraJugador * porcentajeVidaJugador);

                //Elegir color según la salud (Verde -> Amarillo -> Rojo)
                Brush colorBarraJugador = Brushes.Green;
                if (porcentajeVidaJugador < 0.3) colorBarraJugador = Brushes.Red;
                else if (porcentajeVidaJugador < 0.6) colorBarraJugador = Brushes.Yellow;

                //Dibujar la vida restante
                g.FillRectangle(colorBarraJugador, xBarraJugador, yBarraJugador, anchoVidaActualJugador, altoBarraJugador);

                //Borde negro
                g.DrawRectangle(Pens.Black, xBarraJugador, yBarraJugador, anchoBarraJugador, altoBarraJugador);
            }

            //Dibujar las explosiones
            foreach (var ex in listaExplosiones)
            {
                g.DrawImage(imgExplosion, ex.X - 20, ex.Y - 20, 60, 60);
            }

            //pantalla de victoria
            if (juegoTerminado)
            {
                //Crear un velo oscuro semitransparente (R, G, B, Alpha)
                using (SolidBrush velo = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    g.FillRectangle(velo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }

                //Configurar la fuente y el mensaje
                Font fuenteTitulo = new Font("Arial", 40, FontStyle.Bold);
                Font fuenteSub = new Font("Arial", 14, FontStyle.Regular);

                //Elegimos el mensaje dependiendo de tu salud
                string mensaje = (saludJugador <= 0) ? "¡GAME OVER!" : "¡VICTORIA!";
                string subMensaje = (saludJugador <= 0) ? "Presiona ENTER para salir" : "Presiona ENTER para continuar";

                Brush colorTexto = (saludJugador <= 0) ? Brushes.Red : Brushes.Gold;

                SizeF tamMsg = g.MeasureString(mensaje, fuenteTitulo);
                SizeF tamSub = g.MeasureString(subMensaje, fuenteSub);

                float xMsg = (this.ClientSize.Width / 2) - (tamMsg.Width / 2);
                float yMsg = (this.ClientSize.Height / 2) - (tamMsg.Height / 2);

                g.DrawString(mensaje, fuenteTitulo, Brushes.Black, xMsg + 3, yMsg + 3);
                g.DrawString(mensaje, fuenteTitulo, colorTexto, xMsg, yMsg);

                g.DrawString(subMensaje, fuenteSub, Brushes.White,
                            (this.ClientSize.Width / 2) - (tamSub.Width / 2), yMsg + tamMsg.Height + 10);
            }
        }

        //EVENTO KEYDOWN: Cuando presionas una tecla
        private void Batalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { moverIzquierda = true; keyA_Pressed = true; }
            if (e.KeyCode == Keys.D) { moverDerecha = true; keyD_Pressed = true; }

            //Activar escudo
            if (e.KeyCode == Keys.Q && !defendiendo)
            {
                defendiendo = true;
                velocidadTanque = 2 + DatosGlobales.BonusVelocidadGlobal; //Reducimos la velocidad mientras el escudo está activo
                keyQ_Pressed = true;
                this.Invalidate();
            }

            //Disparar bola de fuego (Solo si NO está defendiendo)
            if (e.KeyCode == Keys.E)
            {
                keyE_Pressed = true;
                if (!defendiendo)
                {
                    DispararBolaDeFuego(1, boundsJugador); // 1 es el jugador
                }
                else
                {
                    // MOSTRAR MENSAJE EN PANTALLA
                    aviso.Text = "¡No puedes disparar con el escudo activo!";
                    aviso.Left = boundsJugador.X;
                    aviso.Top = boundsJugador.Y - 30; //30 píxeles por encima del tanque
                    aviso.Visible = true;

                    //Reiniciamos el timer por si el usuario presiona muchas veces
                    timerAviso.Stop();
                    timerAviso.Start();
                }
            }

            //Si el juego ya terminó
            if (juegoTerminado)
            {
                //Al presionar Enter, guardamos el JSON y cerramos
                if (e.KeyCode == Keys.Enter)
                {
                    GuardarVictoriaYSalir();
                }
                return; //Detenemos la ejecución para que no intente moverse o disparar
            }
        }

        //EVENTO KEYUP: Cuando sueltas una tecla
        private void Batalla_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { moverIzquierda = false; keyA_Pressed = false; }
            if (e.KeyCode == Keys.D) { moverDerecha = false; keyD_Pressed = false; }
            if (e.KeyCode == Keys.E) { keyE_Pressed = false; }

            //Desactivar escudo
            if (e.KeyCode == Keys.Q)
            {
                defendiendo = false;
                velocidadTanque = 5 + DatosGlobales.BonusVelocidadGlobal;
                keyQ_Pressed = false;
                this.Invalidate();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //Si el juego terminó, no procesamos nada más
            if (juegoTerminado)
            {
                return;
            }

            //Movimiento del jugador
            if (moverIzquierda && boundsJugador.X > 0)
            {
                boundsJugador.X -= velocidadTanque;
            }
            if (moverDerecha && (boundsJugador.X + boundsJugador.Width) < this.ClientSize.Width)
            {
                boundsJugador.X += velocidadTanque;
            }

            //CEREBRO DE LA IA DEL ENEMIGO
            if (saludEnemigo > 0)
            {
                //Movimiento de patrullaje
                int velEnemigoActual = enemigoDefendiendo ? 2 : velocidadEnemigo; // Más lento si usa escudo
                boundsEnemigo.X += direccionEnemigo * velEnemigoActual;

                //Rebotar en las paredes
                if (boundsEnemigo.X <= 0) direccionEnemigo = 1;
                if (boundsEnemigo.Right >= this.ClientSize.Width) direccionEnemigo = -1;

                //Control del escudo
                if (enemigoDefendiendo)
                {
                    tiempoDefensaEnemigo--;
                    if (tiempoDefensaEnemigo <= 0) enemigoDefendiendo = false; //Se le acaba el escudo
                }
                else
                {
                    //1% de probabilidad de activar el escudo en cada tick (aprox. cada 2-3 segs)
                    if (iaRandom.Next(0, 100) == 1)
                    {
                        enemigoDefendiendo = true;
                        tiempoDefensaEnemigo = 80; //Duración del escudo
                    }
                }

                //Lógica de disparo
                if (cooldownDisparoEnemigo > 0) cooldownDisparoEnemigo--;

                if (!enemigoDefendiendo && cooldownDisparoEnemigo <= 0)
                {
                    //Dispara si se alinea contigo o al azar
                    bool alineado = Math.Abs(boundsEnemigo.X - boundsJugador.X) < 40;
                    if ((alineado && iaRandom.Next(0, 10) == 1) || iaRandom.Next(0, 60) == 1)
                    {
                        DispararBolaDeFuego(2, boundsEnemigo);
                        cooldownDisparoEnemigo = 15;
                    }
                }
            }

            //Movimiento y colisión de balas
            for (int i = listaBalas.Count - 1; i >= 0; i--)
            {
                var b = listaBalas[i];
                
                //Si la bala es del jugador sube, si es del enemigo baja
                if (b.IDDueño == 1) b.Y -= 15;
                else if (b.IDDueño == 2) b.Y += 10; //La bala enemiga es un poco más lenta para que puedas esquivarla
                
                
                //Si la bala es del jugador (ID 1) y toca al enemigo
                if (b.IDDueño == 1 && b.Bounds.IntersectsWith(boundsEnemigo))
                {
                    //Centramos la explosión en el punto de impacto. 
                    listaExplosiones.Add(new Explosion_Nivel_2(b.X, b.Y, 15));
                    listaBalas.RemoveAt(i);

                    //Si el enemigo no tiene escudo, recibe daño
                    if (!enemigoDefendiendo)
                    {
                        saludEnemigo -= 10; //DAÑO DE BALA DEL JUGADOR
                        if (saludEnemigo <= 0) { saludEnemigo = 0; juegoTerminado = true; }
                    }

                    continue; //Pasamos a la siguiente bala
                }

                //Colisión Bala Enemiga -> Jugador
                if (b.IDDueño == 2 && b.Bounds.IntersectsWith(boundsJugador))
                {
                    listaExplosiones.Add(new Explosion_Nivel_2(b.X, b.Y, 15));
                    listaBalas.RemoveAt(i);

                    //Si tú no tienes el escudo puesto, recibes daño
                    if (!defendiendo)
                    {
                        saludJugador -= 10; //DAÑO DE BALA DEL ENEMIGO

                        //Derrota
                        if (saludJugador <= 0)
                        {
                            saludJugador = 0;
                            juegoTerminado = true; //Detiene el timer y las acciones
                        }
                    }
                    continue;
                }

                //Limpiar balas que salen de la pantalla
                if (b.Y < -50 || b.Y > this.ClientSize.Height + 50)
                {
                    listaBalas.RemoveAt(i);
                }
            }
            //ACTUALIZAR Y LIMPIAR EXPLOSIONES
            for (int i = listaExplosiones.Count - 1; i >= 0; i--)
            {
                var ex = listaExplosiones[i];
                ex.ContadorVida++; //Envejecer

                //Si su vida termina, la eliminamos de la lista
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
            if (idDueño == 2 && enemigoDefendiendo) return;

            //Tamaño de la bala
            int anchoBala = 80;
            int altoBala = 80;

            //Centrado horizontal
            int posicionX = posTirador.X + (posTirador.Width / 2) - (anchoBala / 2);
            int posicionY = 0;

            if (idDueño == 1)
            {
                //JUGADOR
                posicionY = posTirador.Y - altoBala;
            }
            else if (idDueño == 2)
            {
                // ENEMIGO
                // Uso .Bottom para que salga exactamente por donde le corresponde
                posicionY = posTirador.Bottom;
            }

            Bala_Nivel_2 nuevaBala = new Bala_Nivel_2
            {
                X = posicionX,
                Y = posicionY,
                IDDueño = idDueño
            };

            listaBalas.Add(nuevaBala);
        }
        private void timerAviso_Tick(object sender, EventArgs e)
        {
            aviso.Visible = false; // Oculta el mensaje
            timerAviso.Stop();
        }

        //PROVISIONAL PARA GUARDAR LA VICTORIA EN EL JSON Y REGRESAR AL MENÚ PRINCIPAL. HAY QUE REHACER LA LOGICA DE REGISTRO DE ENEMIGOS.
        private void GuardarVictoriaYSalir()
        {
            //Preparamos la lista de tanques derrotados requerida por tu clase Puntuacion
            List<TanqueEnemigo> tanquesDerrotados = new List<TanqueEnemigo>();

            //instancia del enemigo derrotado
            TanqueEnemigo enemigoActual = new TanqueEnemigo("Enemigo Derrotado", "T-72", 0, 0, 0,0);
            enemigoActual.Modelo = "T-72"; // Puedes cambiar esto después según el nivel

            tanquesDerrotados.Add(enemigoActual);

            //Definimos el nombre del jugador 
            string nombreJugador = "Jugador 1";

            Puntuacion nuevaPuntuacion = new Puntuacion(nombreJugador, tanquesDerrotados);

            //guardado en json
            DatosGlobales.ListaPuntuaciones.Add(nuevaPuntuacion);
            DatosGlobales.GuardarDatos();

            this.Close();
        }

    }
    public class Bala_Nivel_2
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int IDDueño { get; set; }
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, 20, 20);
    }

    public class Explosion_Nivel_2
    //explosion al impactar la bala
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int ContadorVida { get; set; }
        public int DuracionMaxima { get; set; }

        public Explosion_Nivel_2(float x, float y, int duracionEnTicks)
        {
            X = x;
            Y = y;
            ContadorVida = 0;
            DuracionMaxima = duracionEnTicks;
        }
    }
}