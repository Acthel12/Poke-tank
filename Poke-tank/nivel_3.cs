using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class nivel_3 : Form
    {
        //para saber si el juego termino
        bool juegoTerminado = false;

        // Fases de batalla
        int faseBatalla = 1;

        //balas
        List<Bala_Nivel_3> listaBalas = new List<Bala_Nivel_3>();

        // --- IMÁGENES DE LAS BALAS ---
        Bitmap imgBalaJugador;
        Bitmap imgBalaEnemiga;
        Bitmap imgMisil; // Para los drones
        Bitmap imgMisilRebotado; // Misil yendo hacia arriba

        //explosion
        List<Explosion_Nivel_3> listaExplosiones = new List<Explosion_Nivel_3>();
        Image imgExplosion = Properties.Resources.explosion;

        //IMÁGENES PRE-CARGADAS
        Bitmap imgJugador;
        Bitmap imgEnemigo;
        Bitmap imgDefensa;
        Bitmap imgEscudoEnemigo;
        Bitmap imgEnemigoApoyo;
        Bitmap imgDron;
        Image imgAnimacionFin;

        // Definimos los rectángulos de colisión
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

        // --- VARIABLES DE LA IA DEL ENEMIGO PRINCIPAL ---
        Random iaRandom = new Random();
        bool enemigoTieneEscudo = true;
        int cooldownDisparoEnemigo = 0; // Tiempo de espera entre disparos
        int cooldownBastonEnemigo = 0; // Tiempo de espera para disparar pequeñas balas
        int direccionJefeFase3 = 1; // 1 = derecha, -1 = izquierda
        int velocidadJefeFase3 = 5;

        // --- PARA BALAS PEQUEÑAS DE FASE 3 ---
        // AJUSTAR ESTOS DOS VALORES PARA QUE SALGAN EXACTO EN LA PUNTA DEL BASTÓN
        int offsetX_Baston = 50;  // Posición X (ej. un valor positivo lo mueve a la derecha)
        int offsetY_Baston = 100; // Posición Y (hacia abajo)

        // --- LISTAS PARA ENEMIGOS NUEVOS ---
        List<EnemigoApoyo_Nivel_3> listaApoyos = new List<EnemigoApoyo_Nivel_3>();
        List<Dron_Nivel_3> listaDrones = new List<Dron_Nivel_3>();

        // --- CONTROLES DE FINALIZACIÓN ---
        PictureBox pictureBoxFin = new PictureBox();

        // --- CONTROLES VISUALES UI ---
        string textoInstrucciones = "";
        bool keyA_Pressed = false, keyD_Pressed = false, keyQ_Pressed = false, keyE_Pressed = false;
        Image imgKeyANormal, imgKeyAPressed, imgKeyDNormal, imgKeyDPressed;
        Image imgKeyQNormal, imgKeyQPressed, imgKeyENormal, imgKeyEPressed;

        // --- SISTEMA EVENTO FASE 3 ---
        Image imgEventoFase3;
        PictureBox pictureBoxEventoFase3 = new PictureBox();
        bool juegoPausadoEvento = false;

        public nivel_3()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            fondoBuffered = new Bitmap(Properties.Resources.fondo_marcel, this.ClientSize.Width, this.ClientSize.Height);

            imgJugador = new Bitmap(Properties.Resources.flavio_de_espalda_batalla);
            imgEnemigo = new Bitmap(Properties.Resources.marcel_de_frente);
            imgDefensa = new Bitmap(Properties.Resources.flavio_escudo_completo);

            //RECURSOS
            imgEscudoEnemigo = new Bitmap(Properties.Resources.marcel_con_escudo);
            imgEnemigoApoyo = new Bitmap(Properties.Resources.diferencial_secuaz_marcel);
            imgDron = new Bitmap(Properties.Resources.dron_marcel);
            imgMisil = new Bitmap(Properties.Resources.misil_dron_marcel);
            imgMisil.RotateFlip(RotateFlipType.Rotate180FlipNone); // Misil bajando
            
            imgMisilRebotado = new Bitmap(Properties.Resources.misil_dron_marcel); // Misil subiendo (sin voltear)

            // --- GIF PARA LA ANIMACIÓN DE FIN DE BATALLA ---
            imgAnimacionFin = Properties.Resources.marcel_muere; //REEMPLAZAR

            // --- GIF PARA EL EVENTO DE FASE 3 ---
            imgEventoFase3 = Properties.Resources.caraballo_secreto; //REEMPLAZAR

            //BALAS
            imgBalaJugador = new Bitmap(Properties.Resources.bola_de_fuego_arriba);
            imgBalaEnemiga = new Bitmap(Properties.Resources.bala_marcel);
            imgBalaEnemiga.RotateFlip(RotateFlipType.Rotate180FlipNone);

            //CARGAR IMÁGENES DE CONTROLES
            imgKeyANormal = Properties.Resources.botonA_normal; //Tecla A Normal
            imgKeyAPressed = Properties.Resources.botonA_presionado;
            imgKeyDNormal = Properties.Resources.botonD_normal; // Tecla D
            imgKeyDPressed = Properties.Resources.botonD_presionado;
            imgKeyQNormal = Properties.Resources.botonQ_normal; // Tecla Q
            imgKeyQPressed = Properties.Resources.botonQ_presionado;
            imgKeyENormal = Properties.Resources.botonE_normal; // Tecla E
            imgKeyEPressed = Properties.Resources.botonE_presionado;

            int anchoTanque = 320;
            int altoTanque = 300;
            int centroX = (this.ClientSize.Width / 2) - (anchoTanque / 2);

            boundsEnemigo = new Rectangle(centroX, 70, anchoTanque, altoTanque);
            boundsJugador = new Rectangle(centroX, this.ClientSize.Height - altoTanque - 50, anchoTanque, altoTanque);

            // Configurar Animación y Timer final
            pictureBoxFin.Visible = false;
            pictureBoxFin.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxFin.Image = imgAnimacionFin;
            pictureBoxFin.BackColor = Color.Transparent;
            this.Controls.Add(pictureBoxFin);

            // DEFINIR EL CONTADOR DE TIEMPO (en milisegundos) DESPUÉS DE MATAR AL JEFE
            timerFinBatalla.Interval = 8000; //8 SEG
            timerFinBatalla.Tick += TimerFinBatalla_Tick;

            // --- CONFIGURACIÓN DEL EVENTO DE FASE 3 ---
            pictureBoxEventoFase3.Visible = false;
            pictureBoxEventoFase3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEventoFase3.Image = imgEventoFase3;
            pictureBoxEventoFase3.BackColor = Color.Transparent;
            this.Controls.Add(pictureBoxEventoFase3);
            
            //TIEMPO EN MILISEGUNDOS PARA EL EVENTO DE FASE 3
            timerEventoFase3.Interval = 8000;
            timerEventoFase3.Tick += TimerEventoFase3_Tick;

            //INICIO DE LA FASE 1
            IniciarFase1();
        }

        private void IniciarFase1()
        {
            textoInstrucciones = "FASE 1: El jefe es invulnerable. ¡Destruye a los tanques de apoyo!";
            faseBatalla = 1;
            enemigoTieneEscudo = true;

            int anchoA = 190;
            int altoA = 210;
            listaApoyos.Add(new EnemigoApoyo_Nivel_3(boundsEnemigo.Left - 350, 100, anchoA, altoA, 50)); //ubicacion izq/der, altura, ancho, alto, salud
            listaApoyos.Add(new EnemigoApoyo_Nivel_3(boundsEnemigo.Right + 190, 100, anchoA, altoA, 50));
        }

        private void IniciarFase2()
        {
            textoInstrucciones = "FASE 2: ¡Devuélveles los misiles a los drones usando tu ESCUDO (Q)!";
            faseBatalla = 2;
            int anchoDron = 120;
            int altoDron = 100;
            listaDrones.Add(new Dron_Nivel_3(50, 80, anchoDron, altoDron, 60, 1));
            listaDrones.Add(new Dron_Nivel_3(this.ClientSize.Width - 150, 90, anchoDron, altoDron, 60, -1)); //ubicación horizontal, altura, tamaño, salud, dirección inicial
        }

        private void IniciarFase3()
        {
            textoInstrucciones = "FASE 3: ¡El jefe es vulnerable! ¡Acaba con él!";
            faseBatalla = 3;
            enemigoTieneEscudo = false;
            saludEnemigoMax = 200;
            saludEnemigo = 200;

            //Probabilidad del evento (por defecto 10%)
            if (iaRandom.Next(0, 100) < 10)
            {
                //Iniciar el evento
                juegoPausadoEvento = true;
                GameTimer.Stop();

                //GIF (cambiar Width, Height, Left y Top luego)
                pictureBoxEventoFase3.Width = 300;
                pictureBoxEventoFase3.Height = 300;
                pictureBoxEventoFase3.Left = (this.ClientSize.Width / 2) - 150;
                pictureBoxEventoFase3.Top = (this.ClientSize.Height / 2) - 150;
                
                pictureBoxEventoFase3.Visible = true;
                timerEventoFase3.Start();
            }
        }

        private void Batalla_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //Mejor calidad

            //Dibujar el fondo
            if (fondoBuffered != null) g.DrawImageUnscaled(fondoBuffered, 0, 0);

            // DIBUJAR UI MANUALMENTE (OPTIMIZACIÓN PARA EVITAR LAG)
            Font fInstrucciones = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(textoInstrucciones, fInstrucciones, Brushes.Black, 22, 22);
            g.DrawString(textoInstrucciones, fInstrucciones, Brushes.White, 20, 20);

            int pbSize = 60;
            int margin = 10;
            int startX = this.ClientSize.Width - (pbSize * 4) - (margin * 4) - 20; 
            int startY = this.ClientSize.Height - pbSize - margin - 20;

            g.DrawImage(keyA_Pressed ? imgKeyAPressed : imgKeyANormal, startX, startY, pbSize, pbSize);
            g.DrawImage(keyD_Pressed ? imgKeyDPressed : imgKeyDNormal, startX + pbSize + margin, startY, pbSize, pbSize);
            g.DrawImage(keyQ_Pressed ? imgKeyQPressed : imgKeyQNormal, startX + pbSize*2 + margin*2, startY, pbSize, pbSize);
            g.DrawImage(keyE_Pressed ? imgKeyEPressed : imgKeyENormal, startX + pbSize*3 + margin*3, startY, pbSize, pbSize);

            //apoyos
            foreach (var apoyo in listaApoyos)
            {
                g.DrawImage(imgEnemigoApoyo, apoyo.Bounds);
            }

            //drones
            foreach (var dron in listaDrones)
            {
                g.DrawImage(imgDron, dron.Bounds);
            }

            //Jefe (solo si no fue destruido en fase 3)
            if (saludEnemigo > 0)
            {
                Image imgEnemigoActual = enemigoTieneEscudo ? imgEscudoEnemigo : imgEnemigo;
                g.DrawImage(imgEnemigoActual, boundsEnemigo);

                //barra de vida del jefe en la Fase 3
                if (faseBatalla == 3)
                {
                    int anchoBarraEnemigo = 150;
                    int altoBarraEnemigo = 15;
                    float xBarraEnemigo = boundsEnemigo.X + (boundsEnemigo.Width / 2) - (anchoBarraEnemigo / 2);
                    float yBarraEnemigo = boundsEnemigo.Y - 15;
                    g.FillRectangle(Brushes.Gray, xBarraEnemigo, yBarraEnemigo, anchoBarraEnemigo, altoBarraEnemigo);
                    float porcentajeVidaEnemigo = (float)saludEnemigo / saludEnemigoMax;
                    int anchoVidaActualEnemigo = (int)(anchoBarraEnemigo * porcentajeVidaEnemigo);
                    Brush colorBarraEnemigo = Brushes.Green;
                    if (porcentajeVidaEnemigo < 0.3) colorBarraEnemigo = Brushes.Red;
                    else if (porcentajeVidaEnemigo < 0.6) colorBarraEnemigo = Brushes.Yellow;
                    g.FillRectangle(colorBarraEnemigo, xBarraEnemigo, yBarraEnemigo, anchoVidaActualEnemigo, altoBarraEnemigo);
                    g.DrawRectangle(Pens.Black, xBarraEnemigo, yBarraEnemigo, anchoBarraEnemigo, altoBarraEnemigo);
                }
            }

            // Tanque Jugador
            Image imgCuerpo = defendiendo ? imgDefensa : imgJugador;
            g.DrawImage(imgCuerpo, boundsJugador);

            //Barra de vida del jugador
            if (saludJugador > 0)
            {
                int anchoBarraJugador = 150;
                int altoBarraJugador = 15;
                float xBarraJugador = boundsJugador.X + (boundsJugador.Width / 2) - (anchoBarraJugador / 2);
                float yBarraJugador = boundsJugador.Y - 15;
                g.FillRectangle(Brushes.Gray, xBarraJugador, yBarraJugador, anchoBarraJugador, altoBarraJugador);
                float porcentajeVidaJugador = (float)saludJugador / saludJugadorMax;
                int anchoVidaActualJugador = (int)(anchoBarraJugador * porcentajeVidaJugador);
                Brush colorBarraJugador = Brushes.Green;
                if (porcentajeVidaJugador < 0.3) colorBarraJugador = Brushes.Red;
                else if (porcentajeVidaJugador < 0.6) colorBarraJugador = Brushes.Yellow;
                g.FillRectangle(colorBarraJugador, xBarraJugador, yBarraJugador, anchoVidaActualJugador, altoBarraJugador);
                g.DrawRectangle(Pens.Black, xBarraJugador, yBarraJugador, anchoBarraJugador, altoBarraJugador);
            }

            //Balas
            foreach (var b in listaBalas)
            {
                Image imagenBalaActual = imgBalaEnemiga;
                if (b.IDDueño == 1) imagenBalaActual = imgBalaJugador;
                if (b.IDDueño == 4) imagenBalaActual = imgMisil; // Misil bajando
                if (b.IDDueño == 5) imagenBalaActual = imgMisilRebotado; // Misil subiendo tras el bloqueo
                if (b.IDDueño == 6) imagenBalaActual = imgBalaEnemiga; // Bastón (puede ser reemplazada por otra luego)
                
                g.DrawImage(imagenBalaActual, b.X, b.Y, b.Ancho, b.Alto);
            }

            //Explosiones
            foreach (var ex in listaExplosiones)
            {
                g.DrawImage(imgExplosion, ex.X - 20, ex.Y - 20, 60, 60);
            }

            if (juegoTerminado)
            {
                using (SolidBrush velo = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    g.FillRectangle(velo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }

                Font fuenteTitulo = new Font("Arial", 40, FontStyle.Bold);
                Font fuenteSub = new Font("Arial", 14, FontStyle.Regular);

                //Msj victoria o derrota
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

        private void Batalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (juegoPausadoEvento) return; //Bloquear controles si hay evento, pausando el juego

            if (e.KeyCode == Keys.A) { moverIzquierda = true; keyA_Pressed = true; }
            if (e.KeyCode == Keys.D) { moverDerecha = true; keyD_Pressed = true; }

            if (e.KeyCode == Keys.Q && !defendiendo)
            {
                defendiendo = true;
                velocidadTanque = 2 + DatosGlobales.BonusVelocidadGlobal;
                keyQ_Pressed = true;
                this.Invalidate();
            }

            if (e.KeyCode == Keys.E)
            {
                keyE_Pressed = true;
                if (!defendiendo)
                {
                    DispararBolaDeFuego(1, boundsJugador); //1 = jugador
                }
                else
                {
                    aviso.Text = "¡No puedes disparar con el escudo activo!";
                    aviso.Left = boundsJugador.X;
                    aviso.Top = boundsJugador.Y - 30;
                    aviso.Visible = true;
                    timerAviso.Stop();
                    timerAviso.Start();
                }
            }

            if (juegoTerminado && e.KeyCode == Keys.Enter)
            {
                GuardarVictoriaYSalir();
            }
        }

        private void Batalla_KeyUp(object sender, KeyEventArgs e)
        {
            if (juegoPausadoEvento) return; //Bloquear controles si hay evento

            if (e.KeyCode == Keys.A) { moverIzquierda = false; keyA_Pressed = false; }
            if (e.KeyCode == Keys.D) { moverDerecha = false; keyD_Pressed = false; }
            if (e.KeyCode == Keys.E) { keyE_Pressed = false; }

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
            if (juegoTerminado) return;

            //Movimiento Jugador
            if (moverIzquierda && boundsJugador.X > 0) boundsJugador.X -= velocidadTanque;
            if (moverDerecha && (boundsJugador.X + boundsJugador.Width) < this.ClientSize.Width) boundsJugador.X += velocidadTanque;

            //Transiciones de Fases
            if (faseBatalla == 1 && listaApoyos.Count == 0) IniciarFase2();
            if (faseBatalla == 2 && listaDrones.Count == 0) IniciarFase3();

            //IA jefe (movimiento en fase 3 y disparos)
            if (faseBatalla == 3 && saludEnemigo > 0 && !juegoPausadoEvento)
            {
                boundsEnemigo.X += direccionJefeFase3 * velocidadJefeFase3;
                if (boundsEnemigo.X <= 0) direccionJefeFase3 = 1;
                if (boundsEnemigo.Right >= this.ClientSize.Width) direccionJefeFase3 = -1;
            }

            if (saludEnemigo > 0 && cooldownDisparoEnemigo > 0) cooldownDisparoEnemigo--;
            if (faseBatalla == 3 && saludEnemigo > 0 && cooldownBastonEnemigo > 0) cooldownBastonEnemigo--;

            //disparos del jefe (tiene más probabilidad de disparar si está alineado con el jugador)
            if (saludEnemigo > 0 && cooldownDisparoEnemigo <= 0)
            {
                bool alineado = Math.Abs(boundsEnemigo.X - boundsJugador.X) < 40;
                if ((alineado && iaRandom.Next(0, 10) == 1) || iaRandom.Next(0, 60) == 1)
                {
                    DispararBolaDeFuego(2, boundsEnemigo);
                    cooldownDisparoEnemigo = 25;
                }
            }

            //IA disparo bastón en la fase 3
            if (faseBatalla == 3 && saludEnemigo > 0 && cooldownBastonEnemigo <= 0)
            {
                if (iaRandom.Next(0, 30) == 1) //Probabilidad de disparo del bastón
                {
                    DispararBaston(boundsEnemigo);
                    cooldownBastonEnemigo = 15;
                }
            }

            //IA apoyos
            for (int i = listaApoyos.Count - 1; i >= 0; i--)
            {
                var apoyo = listaApoyos[i];
                if (apoyo.CooldownDisparo > 0) apoyo.CooldownDisparo--;
                else if (iaRandom.Next(0, 80) == 1)
                {
                    DispararBolaDeFuego(3, apoyo.Bounds); // 3 = Apoyo
                    apoyo.CooldownDisparo = 40;
                }
            }

            //IA drones
            for (int i = listaDrones.Count - 1; i >= 0; i--)
            {
                var dron = listaDrones[i];
                dron.ActualizarPosicion(2, this.ClientSize.Width); // 2 = Velocidad del Dron
                if (dron.CooldownDisparo > 0) dron.CooldownDisparo--;
                else if (iaRandom.Next(0, 100) == 1)
                {
                    DispararBolaDeFuego(4, dron.Bounds); // 4 = Misil del Dron
                    dron.CooldownDisparo = 60;
                }
            }

            //Actualizar balas
            for (int i = listaBalas.Count - 1; i >= 0; i--)
            {
                var b = listaBalas[i];

                //Mover bala
                if (b.IDDueño == 1 || b.IDDueño == 5) b.Y -= 15; //Bala del jugador o misil rebotado
                else b.Y += (b.IDDueño == 4) ? 12 : 10; //Bala enemiga

                //Movimiento lateral (solo balas con VelocidadX como las del jefe en Fase 3)
                b.X += b.VelocidadX;

                //Posibilidad de que la bala normal del jefe siga al jugador (fase 1 y 2, o central Fase 3)
                if (b.IDDueño == 2 && b.SigueJugador)
                {
                    if (b.X + 40 < boundsJugador.X + boundsJugador.Width / 2) b.X += 2; // Rastreando suavemente
                    if (b.X + 40 > boundsJugador.X + boundsJugador.Width / 2) b.X -= 2;
                }

                bool balaEliminada = false;

                //Colisiones Bala Jugador / Bala Rebote -> Enemigos
                if (b.IDDueño == 1 || b.IDDueño == 5)
                {
                    //Choca con un apoyo (Solo si fase 1)
                    if (faseBatalla == 1)
                    {
                        for (int k = listaApoyos.Count - 1; k >= 0; k--)
                        {
                            if (b.Bounds.IntersectsWith(listaApoyos[k].Bounds))
                            {
                                listaExplosiones.Add(new Explosion_Nivel_3(b.X, b.Y, 15));
                                listaApoyos[k].Salud -= 10;
                                if (listaApoyos[k].Salud <= 0)
                                {
                                    listaExplosiones.Add(new Explosion_Nivel_3(listaApoyos[k].Bounds.X, listaApoyos[k].Bounds.Y, 30));
                                    listaApoyos.RemoveAt(k);
                                }
                                balaEliminada = true;
                                break;
                            }
                        }
                    }

                    // Choca con dron (Fase 2)
                    if (!balaEliminada && faseBatalla == 2)
                    {
                        for (int k = listaDrones.Count - 1; k >= 0; k--)
                        {
                            if (b.Bounds.IntersectsWith(listaDrones[k].Bounds))
                            {
                                listaExplosiones.Add(new Explosion_Nivel_3(b.X, b.Y, 15));
                                balaEliminada = true;
                                
                                if (b.IDDueño == 5) // Solo toman daño del misil rebotado
                                {
                                    listaDrones[k].Salud -= 15; 
                                    if (listaDrones[k].Salud <= 0)
                                    {
                                        listaExplosiones.Add(new Explosion_Nivel_3(listaDrones[k].Bounds.X, listaDrones[k].Bounds.Y, 30));
                                        listaDrones.RemoveAt(k);
                                    }
                                }
                                break;
                            }
                        }
                    }

                    //Choca con jefe (solo en fase 3)
                    if (!balaEliminada && b.Bounds.IntersectsWith(boundsEnemigo))
                    {
                        listaExplosiones.Add(new Explosion_Nivel_3(b.X, b.Y, 15));
                        balaEliminada = true;

                        if (!enemigoTieneEscudo && faseBatalla == 3) // Recibe Daño
                        {
                            saludEnemigo -= 10;
                            if (saludEnemigo <= 0)
                            {
                                TerminarBatallaJefe();
                            }
                        }
                    }
                }

                // Colisión Bala Enemiga -> Jugador
                if (!balaEliminada && (b.IDDueño == 2 || b.IDDueño == 3 || b.IDDueño == 4))
                {
                    if (b.Bounds.IntersectsWith(boundsJugador))
                    {
                        if (defendiendo)
                        {
                            // REBOTE DE MISILES (ID 4)
                            if (b.IDDueño == 4)
                            {
                                b.IDDueño = 5; // Convertir al jugador / misil rebotado
                                b.Y -= 20; // Separarlo inmediatamente del jugador
                                continue;
                            }
                            else
                            {
                                listaExplosiones.Add(new Explosion_Nivel_3(b.X, b.Y, 15));
                                balaEliminada = true;
                                // Con escudo, no recibe daño de otras balas
                            }
                        }
                        else
                        {
                            listaExplosiones.Add(new Explosion_Nivel_3(b.X, b.Y, 15));
                            balaEliminada = true;

                            int dmg = 10; // Bala normal
                            if (b.IDDueño == 4) dmg = 20; // Misil hace mucho daño
                            if (b.IDDueño == 6) dmg = 5;  // Bastón hace menos daño

                            saludJugador -= dmg;
                            if (saludJugador <= 0)
                            {
                                saludJugador = 0;
                                juegoTerminado = true;
                            }
                        }
                    }
                }

                if (balaEliminada || b.Y < -50 || b.Y > this.ClientSize.Height + 50)
                {
                    listaBalas.RemoveAt(i);
                }
            }

            // Actualizar explosiones
            for (int i = listaExplosiones.Count - 1; i >= 0; i--)
            {
                var ex = listaExplosiones[i];
                ex.ContadorVida++;
                if (ex.ContadorVida >= ex.DuracionMaxima) listaExplosiones.RemoveAt(i);
            }

            this.Invalidate();
        }

        private void DispararBolaDeFuego(int idDueño, Rectangle posTirador)
        {
            if (defendiendo && idDueño == 1) return;

            int anchoBala = 80;
            int altoBala = 80;
            int posicionX = posTirador.X + (posTirador.Width / 2) - (anchoBala / 2);
            int posicionY = (idDueño == 1) ? posTirador.Y - altoBala : posTirador.Bottom;

            bool conRastreo = (idDueño == 2 && iaRandom.Next(10) == 0); //Balas principales tienen prob de rastrear 10%

            if (idDueño == 2 && faseBatalla == 3)
            {
                //Disparo de 3 balas en fase 3
                listaBalas.Add(new Bala_Nivel_3 { X = posicionX, Y = posicionY, IDDueño = 2, SigueJugador = false, VelocidadX = 0, Ancho = anchoBala, Alto = altoBala });
                listaBalas.Add(new Bala_Nivel_3 { X = posicionX, Y = posicionY, IDDueño = 2, SigueJugador = false, VelocidadX = -3, Ancho = anchoBala, Alto = altoBala }); // Izquierda
                listaBalas.Add(new Bala_Nivel_3 { X = posicionX, Y = posicionY, IDDueño = 2, SigueJugador = false, VelocidadX = 3, Ancho = anchoBala, Alto = altoBala }); // Derecha
            }
            else
            {
                //Bala normal predeterminada
                listaBalas.Add(new Bala_Nivel_3 { X = posicionX, Y = posicionY, IDDueño = idDueño, SigueJugador = conRastreo, VelocidadX = 0, Ancho = anchoBala, Alto = altoBala });
            }
        }

        private void DispararBaston(Rectangle posJefe)
        {
            float posX = posJefe.X + offsetX_Baston;
            float posY = posJefe.Y + offsetY_Baston;

            listaBalas.Add(new Bala_Nivel_3
            {
                X = posX,
                Y = posY,
                IDDueño = 6, //Identificador de bala bastón
                SigueJugador = false,
                VelocidadX = 0,
                Ancho = 30, //Mucho más pequeñas
                Alto = 30
            });
        }

        private void timerAviso_Tick(object sender, EventArgs e)
        {
            aviso.Visible = false;
            timerAviso.Stop();
        }

        private void TimerEventoFase3_Tick(object sender, EventArgs e)
        {
            timerEventoFase3.Stop();
            pictureBoxEventoFase3.Visible = false;
            
            //bajar la vida del enemigo principal a la mitad
            saludEnemigo = saludEnemigoMax / 2;
            
            //reanudar la partida
            juegoPausadoEvento = false;
            GameTimer.Start();
        }

        private void TerminarBatallaJefe()
        {
            saludEnemigo = 0;
            GameTimer.Stop(); //congelar TODO

            //aparece la animación sobre donde estaba el enemigo
            pictureBoxFin.Width = boundsEnemigo.Width + 50;
            pictureBoxFin.Height = boundsEnemigo.Height + 50;
            pictureBoxFin.Left = boundsEnemigo.X - 25;
            pictureBoxFin.Top = boundsEnemigo.Y - 25;
            pictureBoxFin.Visible = true;

            //inicia el contador antes de salir
            timerFinBatalla.Start();
        }

        private void TimerFinBatalla_Tick(object sender, EventArgs e)
        {
            timerFinBatalla.Stop();
            GuardarVictoriaYSalir();
        }

        private void GuardarVictoriaYSalir()
        {
            //List<TanqueEnemigo> tanquesDerrotados = new List<TanqueEnemigo>();
            //TanqueEnemigo enemigoActual = new TanqueEnemigo("Enemigo Derrotado", "T-72", 0, 0, 0, 0);
            //enemigoActual.Modelo = "T-72";
            //tanquesDerrotados.Add(enemigoActual);

            string nombreJugador = "Jugador 1";
            //Puntuacion nuevaPuntuacion = new Puntuacion(nombreJugador, tanquesDerrotados);

            //DatosGlobales.ListaPuntuaciones.Add(nuevaPuntuacion);
            //DatosGlobales.GuardarDatos();
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

    }

    public class Bala_Nivel_3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int IDDueño { get; set; } 
        //1=Jugador, 2=JefeCentral, 3=Apoyos, 4=Drones(Misiles), 5=MisilRebotado, 6=Baston
        public bool SigueJugador { get; set; }
        public float VelocidadX { get; set; } //Para las balas diagonales
        public int Ancho { get; set; } = 80;
        public int Alto { get; set; } = 80;
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, Ancho, Alto);
    }

    public class Explosion_Nivel_3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int ContadorVida { get; set; }
        public int DuracionMaxima { get; set; }

        public Explosion_Nivel_3(float x, float y, int duracionEnTicks)
        {
            X = x; Y = y;
            ContadorVida = 0;
            DuracionMaxima = duracionEnTicks;
        }
    }

    public class EnemigoApoyo_Nivel_3
    {
        public Rectangle Bounds;
        public int Salud;
        public int CooldownDisparo;

        public EnemigoApoyo_Nivel_3(int x, int y, int width, int height, int saludInicial)
        {
            Bounds = new Rectangle(x, y, width, height);
            Salud = saludInicial;
            CooldownDisparo = 20;
        }
    }

    public class Dron_Nivel_3
    {
        public Rectangle Bounds;
        public int Salud;
        public int Direccion; // 1 o -1
        public int CooldownDisparo;

        public Dron_Nivel_3(int x, int y, int width, int height, int saludInicial, int direccionInicial)
        {
            Bounds = new Rectangle(x, y, width, height);
            Salud = saludInicial;
            Direccion = direccionInicial;
            CooldownDisparo = 30;
        }

        public void ActualizarPosicion(int velocidad, int widthPantalla)
        {
            Bounds.X += Direccion * velocidad;
            if (Bounds.Left <= 0) Direccion = 1;
            if (Bounds.Right >= widthPantalla) Direccion = -1;
        }
    }
}