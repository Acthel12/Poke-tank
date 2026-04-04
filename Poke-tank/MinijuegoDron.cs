using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;

namespace Poke_tank
{
    public partial class MinijuegoDron : Form
    {
        List<Dron> listaDrones = new List<Dron>();
        List<Misil> listaMisiles = new List<Misil>();
        List<Explosion> listaExplosiones = new List<Explosion>();
        List<Laser> listaLasers = new List<Laser>();


        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        int puntuacion = 0;

        System.Windows.Forms.Timer timerGenerador = new System.Windows.Forms.Timer();
        Random rndGenerador = new Random();

        int cantidadDronesPorOla = 2;
        int cantidadMisilesPorOla = 1;

        int vidas = 3;
        bool juegoTerminado = false;

        int derribosParaGanar = 15;
        int derribos = 0;
        bool victoria = false;

        Bitmap[] framesDron = new Bitmap[4];

        Bitmap[] framesMisil = new Bitmap[3];

        Bitmap imgFondo;

        Bitmap imgExplosion;

        Bitmap imgTorreta;
        Bitmap imgBaseTorreta;
        Point posicionRaton;

        WaveOutEvent reproductorDrones = new WaveOutEvent();
        WaveFileReader lectorDrones;
        bool dronesSonando = false;

        public MinijuegoDron()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ClientSize = new Size(1280, 720);
            this.Cursor = Cursors.Cross;
            this.Text = "DEFENDED LA BASE!!!";
            this.KeyPreview = true;
            this.KeyDown += MinijuegoDron_KeyDown;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            imgFondo = Properties.Resources.fondo;

            framesDron[0] = Properties.Resources.Dron1;
            framesDron[1] = Properties.Resources.Dron2;
            framesDron[2] = Properties.Resources.Dron3;
            framesDron[3] = Properties.Resources.Dron4;

            framesMisil[0] = Properties.Resources.Misil1;
            framesMisil[1] = Properties.Resources.Misil2;
            framesMisil[2] = Properties.Resources.Misil3;

            imgExplosion = Properties.Resources.explosion;

            imgTorreta = Properties.Resources.Torreta;
            imgBaseTorreta = Properties.Resources.BaseTorreta;

            lectorDrones = new WaveFileReader(Properties.Resources.dron);
            reproductorDrones.Init(lectorDrones);

            reproductorDrones.Volume = 0.2f;

            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;

            timerGenerador.Interval = 2000;
            timerGenerador.Tick += TimerGenerador_Tick;

            this.Paint += MinijuegoDron_Paint;
            this.MouseDown += MinijuegoDron_MouseDown;

            this.MouseMove += MinijuegoDron_MouseMove;

            MotorTiempo.Iniciar();

            gameTimer.Start();
            timerGenerador.Start();

            reproductorDrones.PlaybackStopped += (sender, args) =>
            {
                if (dronesSonando)
                {
                    lectorDrones.Position = 0;
                    reproductorDrones.Play();
                }
            };

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MotorTiempo.Actualizar();

            if (juegoTerminado || victoria ) return;

            float dt = MotorTiempo.DeltaTime;

            float ancho = this.ClientSize.Width;
            float alto = this.ClientSize.Height;

            for (int i = listaDrones.Count - 1; i >= 0; i--)
            {
                listaDrones[i].Actualizar(dt, ancho, alto);

                if (listaDrones[i].Fase == 3)
                {
                    ReproducirExplosion();
                    vidas -= 1;
                    listaExplosiones.Add(new Explosion(listaDrones[i].X, listaDrones[i].Y, 0.4f));

                    listaDrones.RemoveAt(i);
                }
            }

            for (int i = listaMisiles.Count - 1; i >= 0; i--)
            {
                listaMisiles[i].Actualizar(dt, ancho, alto);

                if (listaMisiles[i].Fase == 3)
                {
                    ReproducirExplosion();
                    vidas -= 1;

                    listaExplosiones.Add(new Explosion(listaMisiles[i].X, listaMisiles[i].Y, 0.4f));

                    listaMisiles.RemoveAt(i);
                }
            }

            for (int i = listaLasers.Count - 1; i >= 0; i--)
            {
                listaLasers[i].Actualizar(dt);
                if (listaLasers[i].TiempoRestante <= 0)
                {
                    listaLasers.RemoveAt(i);
                }
            }

            for (int i = listaExplosiones.Count - 1; i >= 0; i--)
            {
                listaExplosiones[i].Actualizar(dt);
                if (listaExplosiones[i].Terminada)
                {
                    listaExplosiones.RemoveAt(i);
                }
            }

            if  (vidas <= 0)
            {
                juegoTerminado = true;

                DetenerSonidoDrones(); // Aseguramos que el sonido de los drones se detenga al perder

                gameTimer.Stop();
                timerGenerador.Stop();
            }
            
            if (listaDrones.Count > 0 && !dronesSonando && !juegoTerminado)
            {
                dronesSonando = true;
                lectorDrones.Position = 0;
                reproductorDrones.Play();
            }
            else if (listaDrones.Count == 0 && dronesSonando)
            {
                dronesSonando = false;
                reproductorDrones.Stop();
            }
            this.Invalidate();
        }
        private void TimerGenerador_Tick(object sender, EventArgs e)
        {
            int distanciaEntreObjetosRandom = rndGenerador.Next(0, 300);
            int limiteInferior = this.ClientSize.Height - 300;

            for (int i = 0; i < cantidadDronesPorOla; i++)
            {
                int alturaAleatoria = rndGenerador.Next(50, limiteInferior);

                Dron nuevoDron = new Dron(alturaAleatoria);

                nuevoDron.X = -distanciaEntreObjetosRandom * (i + 1); // para que salgan uno detras de otro

                listaDrones.Add(nuevoDron);
            }

            for (int i = 0; i < cantidadMisilesPorOla; i++)
            {
                int alturaAleatoria = rndGenerador.Next(50, limiteInferior - 300);

                Misil nuevoMisil = new Misil(alturaAleatoria);

                nuevoMisil.X = -distanciaEntreObjetosRandom * (i + 1);

                listaMisiles.Add(nuevoMisil);
            }
        }
        private void MinijuegoDron_Paint(object sender, PaintEventArgs e)
        {
            Font fuente = new Font("Arial", 16, FontStyle.Bold);
            if (imgFondo != null) 
            {
                e.Graphics.DrawImage(imgFondo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            else
            {
                e.Graphics.Clear(Color.SkyBlue);
            }

            e.Graphics.DrawString("Puntos: " + puntuacion, fuente, Brushes.Black, 10, 10);
            e.Graphics.DrawString("Drones restantes: " + derribosParaGanar, fuente, Brushes.Black, 10, 36);

            string textoVidas = "Vidas: " + (vidas < 0 ? 0 : vidas);
            
            float anchoTexto = e.Graphics.MeasureString(textoVidas, fuente).Width;
            e.Graphics.DrawString(textoVidas, fuente, Brushes.Red, this.ClientSize.Width - anchoTexto - 10, 10);

            foreach (var laser in listaLasers)
            {
                // Calculamos qué tan transparente debe ser (de 0 a 255)
                int transparencia = (int)((laser.TiempoRestante / laser.TiempoMaximo) * 255);
                if (transparencia < 0) transparencia = 0;
                if (transparencia > 255) transparencia = 255;

                // Creamos un lápiz color Cyan (celeste) semitransparente, de 4 píxeles de grosor
                using (Pen penLaser = new Pen(Color.FromArgb(transparencia, 255, 0, 0), 4f))
                {
                    e.Graphics.DrawLine(penLaser, laser.InicioX, laser.InicioY, laser.FinX, laser.FinY);
                }
            }

            float centroTorretaX = this.ClientSize.Width / 2f;
            float centroTorretaY = this.ClientSize.Height - 100; // A 50 píxeles del borde inferior
            int anchoTorreta = 250;
            int altoTorreta = 100;

            float diferenciaX = posicionRaton.X - centroTorretaX;
            float diferenciaY = posicionRaton.Y - centroTorretaY;

            double anguloRadianes = Math.Atan2(diferenciaY, diferenciaX);
            float anguloGrados = (float)(anguloRadianes * (180 / Math.PI));

            var estadoLienzo = e.Graphics.Save();

            e.Graphics.DrawImage(imgBaseTorreta, centroTorretaX - 128, centroTorretaY -30, 250, 100);


            e.Graphics.TranslateTransform(centroTorretaX, centroTorretaY);

            e.Graphics.RotateTransform(anguloGrados);

            if (imgTorreta != null)
            {
                e.Graphics.DrawImage(imgTorreta, -anchoTorreta / 2, -altoTorreta / 2, anchoTorreta, altoTorreta);
            }
            else
            {
                // Si no tienes imagen aún, dibujamos un rectángulo que simule el cañón
                e.Graphics.FillRectangle(Brushes.Gray, 0, -10, 60, 20); // Cañón largo hacia la derecha
                e.Graphics.FillEllipse(Brushes.DarkGray, -25, -25, 50, 50); // Base redonda
            }

            e.Graphics.Restore(estadoLienzo);


            foreach (var exp in listaExplosiones)
            {
                if (imgExplosion != null)
                {
                    e.Graphics.DrawImage(imgExplosion, exp.X, exp.Y, 200, 200);
                }
            }
            if (juegoTerminado) 
            {
                using (SolidBrush velo = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(velo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }

                Font fuenteTitulo = new Font("Arial", 40, FontStyle.Bold);
                string mensaje = "¡GAME OVER!";
                SizeF tamMsg = e.Graphics.MeasureString(mensaje, fuenteTitulo);

                float xMsg = (this.ClientSize.Width / 2) - (tamMsg.Width / 2);
                float yMsg = (this.ClientSize.Height / 2) - (tamMsg.Height / 2);

                e.Graphics.DrawString(mensaje, fuenteTitulo, Brushes.Black, xMsg + 3, yMsg + 3);
                e.Graphics.DrawString(mensaje, fuenteTitulo, Brushes.Red, xMsg, yMsg);

                Font fuenteSubtitulo = new Font("Arial", 20, FontStyle.Bold);
                string mensajeContinuar = "Presione cualquier tecla para salir";
                SizeF tamSub = e.Graphics.MeasureString(mensajeContinuar, fuenteSubtitulo);

                float xSub = (this.ClientSize.Width / 2) - (tamSub.Width / 2);
                float ySub = yMsg + tamMsg.Height + 20; // Lo colocamos 20 píxeles por debajo del título

                e.Graphics.DrawString(mensajeContinuar, fuenteSubtitulo, Brushes.Black, xSub + 2, ySub + 2);
                e.Graphics.DrawString(mensajeContinuar, fuenteSubtitulo, Brushes.White, xSub, ySub);

                return; 
            }
            if (victoria)
            {
                using (SolidBrush velo = new SolidBrush(Color.FromArgb(150, 0, 255, 0)))
                {
                    e.Graphics.FillRectangle(velo, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }

                Font fuenteTitulo = new Font("Arial", 40, FontStyle.Bold);
                string mensaje = "¡VICTORIA!";
                SizeF tamMsg = e.Graphics.MeasureString(mensaje, fuenteTitulo);

                float xMsg = (this.ClientSize.Width / 2) - (tamMsg.Width / 2);
                float yMsg = (this.ClientSize.Height / 2) - (tamMsg.Height / 2);

                e.Graphics.DrawString(mensaje, fuenteTitulo, Brushes.Black, xMsg + 3, yMsg + 3);
                e.Graphics.DrawString(mensaje, fuenteTitulo, Brushes.Green, xMsg, yMsg);

                Font fuenteSubtitulo = new Font("Arial", 20, FontStyle.Bold);
                string mensajeContinuar = "Presione cualquier tecla para continuar";
                SizeF tamSub = e.Graphics.MeasureString(mensajeContinuar, fuenteSubtitulo);

                float xSub = (this.ClientSize.Width / 2) - (tamSub.Width / 2);
                float ySub = yMsg + tamMsg.Height + 20; // Lo colocamos 20 píxeles por debajo del título

                e.Graphics.DrawString(mensajeContinuar, fuenteSubtitulo, Brushes.Black, xSub + 2, ySub + 2);
                e.Graphics.DrawString(mensajeContinuar, fuenteSubtitulo, Brushes.White, xSub, ySub);

                return;
            }

            

            foreach (var dron in listaDrones)
            {

                Bitmap imagenADibujar = framesDron[dron.FrameActual];
                
                if (imagenADibujar != null)
                {
                    e.Graphics.DrawImage(imagenADibujar, dron.X, dron.Y, dron.Ancho, dron.Alto);
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.Yellow, dron.X, dron.Y, dron.Ancho, dron.Alto);
                }

            }
            foreach (var misil in listaMisiles)
            {
                Bitmap imagenADibujar = framesMisil[misil.FrameActual];

                if (imagenADibujar != null)
                {
                    e.Graphics.DrawImage(imagenADibujar, misil.X, misil.Y, misil.Ancho, misil.Alto);
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.Red, misil.X, misil.Y, misil.Ancho, misil.Alto);
                }   
            }
        }

        private void MinijuegoDron_MouseDown(object sender, MouseEventArgs e)
        {
            if (juegoTerminado || victoria) return;

            if (e.Button == MouseButtons.Left)
            {
                ReproducirDisparo();
                // Coordenadas de la punta de la torreta
                float centroTorretaX = this.ClientSize.Width / 2f;
                float centroTorretaY = this.ClientSize.Height - 100;

                // Creamos el láser visual
                listaLasers.Add(new Laser(centroTorretaX, centroTorretaY, e.Location.X, e.Location.Y));

                for (int i = listaDrones.Count - 1; i >= 0; i--)
                {
                    if (listaDrones[i].Bounds.Contains(e.Location))
                    {
                        float anchoExplosion = 200;
                        float altoExplosion = 200;
                        float centerX = listaDrones[i].X - (anchoExplosion / 2);
                        float centerY = listaDrones[i].Y - (altoExplosion / 2);

                        ReproducirExplosion();
                        listaExplosiones.Add(new Explosion(centerX, centerY, 0.3f));

                        listaDrones.RemoveAt(i);
                        derribos++;
                        derribosParaGanar--;
                        puntuacion += 10;

                        if (derribosParaGanar == 0)
                        {
                            victoria = true;

                            DetenerSonidoDrones(); // Aseguramos que el sonido de los drones se detenga al ganar

                            this.Invalidate();
                            gameTimer.Stop();
                            timerGenerador.Stop();
                        }

                        return; 
                    }
                }
                for (int i = listaMisiles.Count - 1; i >= 0; i--)
                {
                    if (listaMisiles[i].Bounds.Contains(e.Location))
                    {
                        float anchoExplosion = 200;
                        float altoExplosion = 200;
                        float centerX = listaMisiles[i].X - (anchoExplosion / 2);
                        float centerY = listaMisiles[i].Y - (altoExplosion / 2);

                        ReproducirExplosion();
                        listaExplosiones.Add(new Explosion(centerX, centerY, 0.3f));

                        listaMisiles.RemoveAt(i);
                        derribos++;
                        derribosParaGanar--;
                        puntuacion += 10;

                        if (derribosParaGanar == 0)
                        {
                            victoria = true;

                            this.Invalidate();
                            gameTimer.Stop();
                            timerGenerador.Stop();
                        }

                        return;
                    }
                }
            }
        }

        private void ReproducirExplosion()
        {
            // Usamos Task.Run para que no congele el juego ni un milisegundo al cargar
            Task.Run(() => {
                var lector = new WaveFileReader(Properties.Resources.explosion_1);
                var reproductor = new WaveOutEvent();
                
                reproductor.Volume = 0.15f;

                reproductor.Init(lector);
                reproductor.Play();

                reproductor.PlaybackStopped += (sender, args) =>
                {
                    lector.Dispose();
                    reproductor.Dispose();
                };
            });
        }

        private void MinijuegoDron_MouseMove(object sender, MouseEventArgs e)
        {
            // Guardamos la posición actual del ratón
            posicionRaton = e.Location;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DetenerSonidoDrones(); // Aseguramos que el sonido de los drones se detenga al cerrar el juego

            if (lectorDrones != null)
            {
                lectorDrones.Dispose();
            }

            // Llamamos a la base para que el Form se cierre normalmente
            base.OnFormClosing(e);
        }

        private void DetenerSonidoDrones()
        {
            // 1. Detenemos el bucle de sonido para que no intente reiniciarse
            dronesSonando = false;

            // 2. Detenemos el reproductor de los drones
            if (reproductorDrones != null)
            {
                reproductorDrones.Stop();
                reproductorDrones.Dispose(); // Libera la memoria del hardware de audio
            }

        }

        private void ReproducirDisparo()
        {
            // Usamos Task.Run para disparos rápidos sin congelar el juego
            Task.Run(() => {
                var lector = new WaveFileReader(Properties.Resources.laser);
                var reproductor = new WaveOutEvent();

                // Volumen bajito (10%) porque el jugador hará muchos clics seguidos
                reproductor.Volume = 0.10f;

                reproductor.Init(lector);
                reproductor.Play();

                reproductor.PlaybackStopped += (sender, args) =>
                {
                    lector.Dispose();
                    reproductor.Dispose();
                };
            });
        }
        private void Ganaste() { 
            MessageBox.Show("¡Felicidades, has ganado el minijuego de drones! \n Has ganado : 200$", "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            if (DatosGlobales.ListaPartidas.Count > 0 && DatosGlobales.PartidaActualIndex >= 0)
            { 
                Partida partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
                partida.Oro += 200;
            }

        }

        private void MinijuegoDron_KeyDown(object sender, KeyEventArgs e)
        {
            // Solo permitimos cerrar con tecla si ya ganaste o perdiste
            if (juegoTerminado || victoria)
            {
                if (victoria)
                {
                    // Llamamos a tu método para dar el oro justo antes de salir
                    Ganaste();
                }

                // Cierra el formulario y te devuelve a tu menú/juego principal
                this.Close();
            }
        }
    }
}
