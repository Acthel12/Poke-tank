using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Poke_tank
{
    public partial class Mapa : Form
    {
        // Variables de Posición y Velocidad del tanque
        private int x = 100;
        private int y = 100;
        private int speed = 5;

        // Variables para detectar qué tecla está presionada
        private bool goUp, goDown, goLeft, goRight;

        // Variables de Animación y Spritesheet
        private Bitmap spriteSheet;
        private int sourceFrameWidth = 500;
        private int sourceFrameHeight = 1000;
        private int drawingWidth = 64;
        private int drawingHeight = 64;
        private int currentFrame = 0; // Va del 0 al 7

        //Variable para el mapa
        Bitmap mascaraColisiones;

        //sonido
        System.Media.SoundPlayer sonidoMotor = new System.Media.SoundPlayer(Properties.Resources.SONIDO_TANQUE_MOVIMIENTO);
        bool sonidoEstaReproduciendo = false;

        // Variables para la colision para abrir nuevos forms
        Rectangle zonaTienda = new Rectangle(737, 223, 70, 45); // Define la posición (X, Y) y el tamaño (Ancho, Alto) de la zona
        Rectangle zonaNivel3 = new Rectangle(970, 269, 31, 21);
        Rectangle zonaNivel2 = new Rectangle(508, 111, 31, 21);
        Rectangle zonaNivel1 = new Rectangle(381, 566, 31, 21);
        Rectangle zonaBase = new Rectangle(616, 326, 32, 22); 

        // Un seguro para saber si ya abrimos la ventana
        bool ventanaAbierta = false;

        //PROGRESO DE LOS NIVELES
        int nivelProgreso = 1;

        // El timer del juego, que actúa como el bucle principal para actualizar la lógica y redibujar la pantalla
        private System.Windows.Forms.Timer gameTimer;

        public Mapa()
        {
            InitializeComponent();

            // 1. Evitar el parpadeo al redibujar
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.ClientSize = new Size(1280, 720);

            // 2. Cargar imágenes
            spriteSheet = Properties.Resources.SPRITE_SHEET_FLAVIO_MAPA;
            mascaraColisiones = new Bitmap(Properties.Resources.MASCARA_MAPA_FINAL);

            // 3. Configurar los eventos del teclado
            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
            this.Paint += DrawGame;

            // 4. Configurar e iniciar el bucle del juego (Game Loop)
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // Aproximadamente 50 FPS
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        // --- MANEJO DE CONTROLES ---
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

        //BUCLE PRINCIPAL DEL JUEGO
        private void GameLoop(object sender, EventArgs e)
        {
            bool isMoving = false;
            int nextX = x;
            int nextY = y;
            int nextFrame = currentFrame;

            //DETERMINAR MOVIMIENTO TENTATIVO
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
                // 2. AJUSTE DE ESCALA (Para que coincida la ventana con el tamaño de la imagen)
                float scaleX = (float)mascaraColisiones.Width / this.ClientSize.Width;
                float scaleY = (float)mascaraColisiones.Height / this.ClientSize.Height;

                // Punto de chequeo: El centro de la base del tanque
                int checkX = (int)((nextX + (drawingWidth / 2)) * scaleX);
                int checkY = (int)((nextY + (drawingHeight / 2)) * scaleY);

                // 3. VALIDAR CONTRA LA MÁSCARA
                if (checkX >= 0 && checkX < mascaraColisiones.Width && checkY >= 0 && checkY < mascaraColisiones.Height)
                {
                    Color colorMask = mascaraColisiones.GetPixel(checkX, checkY);

                    // Si el color es Blanco (o muy claro), el camino está libre
                    if (colorMask.R > 200 && colorMask.G > 200 && colorMask.B > 200)
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

                // DETECCIÓN DE MÚLTIPLES ZONAS DE COLISION
                Rectangle rectTanque = new Rectangle(x, y, drawingWidth, drawingHeight);

                // NIVEL 1
                if (rectTanque.IntersectsWith(zonaNivel1))
                {
                    if (nivelProgreso == 1) // Es el nivel que le toca
                    {
                        DialogResult resultado = AbrirFormulario(new nivel_1());

                        if (resultado == DialogResult.OK) // ¡Ganó el nivel!
                        {
                            nivelProgreso = 2; // Desbloqueamos el Nivel 2
                            MessageBox.Show("¡Misión Cumplida! Has desbloqueado el nivel 2!");
                        }
                        // Si perdió (DialogResult.Cancel), nivelProgreso sigue siendo 1 y podrá repetirlo.
                    }

                    // Te teletransporta a un punto válido del camino fuera de la zona

                    // Forzamos a que todas las direcciones se apaguen.
                    goUp = false;
                    goDown = false;
                    goLeft = false;
                    goRight = false;

                    x = 543;
                    y = 438;
                }

                // NIVEL 2
                else if (rectTanque.IntersectsWith(zonaNivel2))
                {
                    if (nivelProgreso < 2) // Intenta entrar sin pasar el nivel 1
                    {
                        // 1. DETENER EL TIEMPO: Evita el bucle infinito de mensajes
                        gameTimer.Stop();

                        // Pausar sonido para que no se quede pegado
                        if (sonidoEstaReproduciendo) { sonidoMotor.Stop(); sonidoEstaReproduciendo = false; }

                        MessageBox.Show("Debes completar el nivel anterior para jugar.");

                        // Te teletransporta a un punto válido del camino fuera de la zona

                        // Forzamos a que todas las direcciones se apaguen.
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = false;

                        x = 543;
                        y = 275;

                        // 3. REANUDAR EL TIEMPO
                        gameTimer.Start();
                    }
                    else if (nivelProgreso == 2) // Es el nivel que le toca
                    {
                        DialogResult resultado = AbrirFormulario(new nivel_2());

                        if (resultado == DialogResult.OK)
                        {
                            nivelProgreso = 3; // Desbloqueamos el Nivel 3
                            MessageBox.Show("¡Castillo Conquistado!");
                        }

                        // Te teletransporta a un punto válido del camino fuera de la zona
                        // Forzamos a que todas las direcciones se apaguen.
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = false;

                        x = 543;
                        y = 275;
                    }
                }

                // NIVEL 3
                else if (rectTanque.IntersectsWith(zonaNivel3))
                {
                    if (nivelProgreso < 3) // Intenta entrar sin pasar el nivel 2
                    {
                        // 1. DETENER EL TIEMPO: Evita el bucle infinito de mensajes
                        gameTimer.Stop();

                        // Pausar sonido para que no se quede pegado
                        if (sonidoEstaReproduciendo) { sonidoMotor.Stop(); sonidoEstaReproduciendo = false; }

                        MessageBox.Show("Debes completar el nivel anterior para jugar.");

                        // Te teletransporta a un punto válido del camino fuera de la zona
                        // Forzamos a que todas las direcciones se apaguen.
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = false;

                        x = 867;
                        y = 321;

                        // 3. REANUDAR EL TIEMPO
                        gameTimer.Start();
                    }
                    else if (nivelProgreso == 3) // Es el nivel que le toca
                    {
                        DialogResult resultado = AbrirFormulario(new nivel_3());

                        if (resultado == DialogResult.OK)
                        {
                            nivelProgreso = 4; // Desbloqueamos el Nivel 4
                            MessageBox.Show("¡Castillo Conquistado!");
                        }

                        // Te teletransporta a un punto válido del camino fuera de la zona
                        // Forzamos a que todas las direcciones se apaguen.
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = false;
                        x = 867;
                        y = 321;
                    }
                }

                //TIENDA (Accesible siempre)
                else if (rectTanque.IntersectsWith(zonaTienda))
                {
                    AbrirFormulario(new tienda());

                    // Te teletransporta a un punto válido del camino fuera de la zona
                    // Forzamos a que todas las direcciones se apaguen.
                    goUp = false;
                    goDown = false;
                    goLeft = false;
                    goRight = false;
                    x = 694;
                    y = 275;
                }

                //BASE (siempre accesible)
                else if (rectTanque.IntersectsWith(zonaBase))
                {
                    AbrirFormulario(new Base_central());

                    // Te teletransporta a un punto válido del camino fuera de la zona
                    // Forzamos a que todas las direcciones se apaguen.
                    goUp = false;
                    goDown = false;
                    goLeft = false;
                    goRight = false;
                    x = 696;
                    y = 381;
                }
            }

            // Detener sonido si el tanque deja de moverse
            if (!isMoving && sonidoEstaReproduciendo)
            {
                sonidoMotor.Stop();
                sonidoEstaReproduciendo = false;
            }

            this.Invalidate();
        }

        //Funcion para abrir un formulario segun la zona de colision
        private DialogResult AbrirFormulario(Form formularioDestino)
        {
            // Si ya hay una ventana abierta, ignoramos la colisión
            if (ventanaAbierta) return DialogResult.Ignore;

            ventanaAbierta = true;

            // Pausamos el motor del juego y el sonido
            gameTimer.Stop();
            if (sonidoEstaReproduciendo)
            {
                sonidoMotor.Stop();
                sonidoEstaReproduciendo = false;
            }

            // Forzamos a que todas las direcciones se apaguen.
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;

            // ShowDialog() pausa el mapa y espera a que el nivel termine.
            // Guardamos el resultado (Ganó o Perdió) en esta variable:
            DialogResult resultado = formularioDestino.ShowDialog();

            // Cuando el jugador cierre esa ventana, reanudamos todo
            ventanaAbierta = false;
            gameTimer.Start();

            return resultado; // Le devolvemos el resultado al GameLoop
        }

        // --- DIBUJADO DE GRÁFICOS ---
        private void DrawGame(object sender, PaintEventArgs e)
        {
            if (spriteSheet != null)
            {
                // 1. DEJAMOS QUE C# CALCULE EL TAMAÑO EXACTO DE CADA FRAME
                int exactFrameWidth = spriteSheet.Width / 4;
                int exactFrameHeight = spriteSheet.Height / 2;

                // 2. Averiguar qué frame toca en la cuadrícula
                int column = currentFrame % 4;
                int row = currentFrame / 4;

                // 3. Recortar la imagen original con precisión milimétrica
                Rectangle sourceRect = new Rectangle(column * exactFrameWidth, row * exactFrameHeight, exactFrameWidth, exactFrameHeight);

                //TAMANO FLAVIO
                int dibujoAncho = 70;

                //Calculamos el alto proporcionalmente para que no se deforme
                // Fórmula: (Alto Original / Ancho Original) * Ancho Nuevo
                float ratio = (float)exactFrameHeight / exactFrameWidth;
                int dibujoAlto = (int)(dibujoAncho * ratio);

                // 3. Dibujamos con el tamaño corregido
                Rectangle destRect = new Rectangle(x, y, dibujoAncho, dibujoAlto);

                e.Graphics.DrawImage(spriteSheet, destRect, sourceRect, GraphicsUnit.Pixel);
            }
            else
            {
                // Fallback: Si aún no tienes la imagen, dibujará un cuadrado verde de prueba
                e.Graphics.FillRectangle(Brushes.Green, x, y, sourceFrameWidth, sourceFrameHeight);
                e.Graphics.DrawString($"Frame: {currentFrame}", this.Font, Brushes.White, x, y);
            }
        }


        //iniciamos la partida según el nivel escogido
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            SeleccionarNivel(0);
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            SeleccionarNivel(1);
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            SeleccionarNivel(2);
        }

        //botón de easter egg flavionística
        private void botonSorpresaFlavio_Click(object sender, EventArgs e)
        {
            Flaviosorpresa from1 = new Flaviosorpresa();
            from1.ShowDialog();
        }

        //al finalizar la aventura, se registra la puntuación total obtenida en la campaña, basada en el número de enemigos derrotados y se muestra al usuario
        private void FinalizarAventura()
        {
            if (DatosGlobales.PartidaActualIndex >= 0 && DatosGlobales.PartidaActualIndex < DatosGlobales.ListaPartidas.Count)
            {
                Partida partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

                if (partida.enemigosDerrotados.Count > 0)
                {
                    //registramos la puntuación una sola vez al final
                    Puntuacion recordFinal = new Puntuacion(
                        partida.tanqueUsuario.Nombre,
                        partida.enemigosDerrotados,
                        partida.Dificultad
                    );

                    DatosGlobales.ListaPuntuaciones.Add(recordFinal);

                    //Terminamos con la partida
                    DatosGlobales.ListaPartidas.Remove(partida);

                    DatosGlobales.GuardarDatos();

                    MessageBox.Show($"Campaña finalizada. ¡Puntaje total: {recordFinal.PuntosTotales} puntos registrados!");
                }
                else
                {
                    MessageBox.Show("Campaña finalizada sin victorias. No se registró puntuación.");
                }
            }

            this.Close(); //para regresar al menú
        }

        //botón para finalizar la aventura y registrar la puntuación obtenida
        private void botonFinalizar_Click(object sender, EventArgs e)
        {
            Partida partidaActual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            if (partidaActual.enemigosDerrotados.Count >= 3)
            {
                FinalizarAventura();
            }
            else
            {
                this.Close(); //para salir sin registrar puntuación
            }
        }

        //método para seleccionar el fondo del mapa automáticamente según el nivel escogido
        private void SeleccionarMapa(Form form)
        {
            form.BackgroundImage = DatosGlobales.NivelSeleccionado switch
            {
                0 => Properties.Resources.fondoNivel1,

                1 => Properties.Resources.fondoNivel2,

                2 => Properties.Resources.fondoNivel3,
            };
        }

        //al cargar el mapa, se habilitan o deshabilitan los botones de los niveles según el progreso del usuario en la campaña
        private void Mapa_Load(object sender, EventArgs e)
        {
            ActualizarMapa();
        }
        private void ActualizarMapa()
        {
            Partida partidaActual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            //Comprobamos el número de enemigos derrotados para determinar qué niveles están disponibles
            //botonNivel1.Enabled = (partidaActual.enemigosDerrotados.Count == 0 || partidaActual.enemigosDerrotados.Count >= 3);
            //botonNivel2.Enabled = (partidaActual.enemigosDerrotados.Count == 1 || partidaActual.enemigosDerrotados.Count >= 3);
            //botonNivel3.Enabled = (partidaActual.enemigosDerrotados.Count >= 2 );

            //Comprobamos si el usuario ha derrotado a los 3 enemigos para mostrar el botón de finalizar campaña, si no se cambia por salir
            if (partidaActual.enemigosDerrotados.Count >= 3)
            {
                botonFinalizar.Text = "Finalizar campaña";
            }
            else
            {
                botonFinalizar.Text = "Salir";
            }
        }
        //método para seleccionar el nivel desde el menú del mapa, se llama desde los botones de cada nivel
        public void SeleccionarNivel(int nivel)
        {
            DatosGlobales.NivelSeleccionado = nivel;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
            ActualizarMapa();
        }

        private void pruebaBatalla_Click(object sender, EventArgs e)
        {
            nivel_1 from1 = new nivel_1();
            from1.ShowDialog();
        }
        
        private void Mapa_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatosGlobales.PartidaActualIndex = -1; //reiniciamos el índice de la partida actual al cerrar el mapa para evitar problemas al regresar al menús
        }
    }
}
