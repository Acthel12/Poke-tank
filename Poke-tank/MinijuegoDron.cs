using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class MinijuegoDron : Form
    {
        List<Dron> listaDrones = new List<Dron>();
        List<Misil> listaMisiles = new List<Misil>();
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


        public MinijuegoDron()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ClientSize = new Size(1280, 720);
            this.Cursor = Cursors.Cross;
            this.Text = "ATAQUE DE DRONES!!!";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            framesDron[0] = Properties.Resources.Dron1;
            framesDron[1] = Properties.Resources.Dron2;
            framesDron[2] = Properties.Resources.Dron3;
            framesDron[3] = Properties.Resources.Dron4;

            framesMisil[0] = Properties.Resources.Misil1;
            framesMisil[1] = Properties.Resources.Misil2;
            framesMisil[2] = Properties.Resources.Misil3;

            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;

            timerGenerador.Interval = 2000;
            timerGenerador.Tick += TimerGenerador_Tick;

            this.Paint += MinijuegoDron_Paint;
            this.MouseDown += MinijuegoDron_MouseDown;

            MotorTiempo.Iniciar();

            gameTimer.Start();
            timerGenerador.Start();
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
                    vidas -= 1; 

                    listaDrones.RemoveAt(i);
                }
            }

            for (int i = listaMisiles.Count - 1; i >= 0; i--)
            {
                listaMisiles[i].Actualizar(dt, ancho, alto);

                if (listaMisiles[i].Fase == 3)
                {
                    vidas -= 1; 

                    listaMisiles.RemoveAt(i);
                }
            }

            if  (vidas <= 0)
            {
                juegoTerminado = true;

                gameTimer.Stop();
                timerGenerador.Stop();
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
            e.Graphics.Clear(Color.SkyBlue);

            e.Graphics.DrawString("Puntos: " + puntuacion, fuente, Brushes.Black, 10, 10);
            e.Graphics.DrawString("Drones restantes: " + derribosParaGanar, fuente, Brushes.Black, 10, 36);

            string textoVidas = "Vidas: " + (vidas < 0 ? 0 : vidas);
            
            float anchoTexto = e.Graphics.MeasureString(textoVidas, fuente).Width;
            e.Graphics.DrawString(textoVidas, fuente, Brushes.Red, this.ClientSize.Width - anchoTexto - 10, 10);

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
                    e.Graphics.FillEllipse(Brushes.Yellow, misil.X, misil.Y, misil.Ancho, misil.Alto);
                }   
            }

        }

        private void MinijuegoDron_MouseDown(object sender, MouseEventArgs e)
        {
            if (juegoTerminado) return;

            if (e.Button == MouseButtons.Left)
            {
                for (int i = listaDrones.Count - 1; i >= 0; i--)
                {
                    if (listaDrones[i].Bounds.Contains(e.Location))
                    {
                        listaDrones.RemoveAt(i);
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
                for (int i = listaMisiles.Count - 1; i >= 0; i--)
                {
                    if (listaMisiles[i].Bounds.Contains(e.Location))
                    {
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
    }
}
