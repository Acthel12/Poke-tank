using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Buscaminas : Form
    {
        
        private int filas = 10, columnas = 10, minas = 15;
        private Button[,] botones;
        private bool[,] tieneMina;
        private bool[,] revelado;
        private int[,] minasAdyacentes;
        private int celdasRestantes;
        private Image fotoMina;
        private int banderasColocadas = 0;
        private Label lblContadorBanderas;


        public Buscaminas()
        {

            this.Text = "POKE-TANK: DESACTIVACIÓN DE MINAS";
            this.Icon = Properties.Resources.LOGO_FLAVIO_ADVENTURES_SIN_FONDO;
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackgroundImage = Properties.Resources.fondoMina;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            IniciarTablero();
        }


        private void IniciarTablero()
        {
            botones = new Button[filas, columnas];
            tieneMina = new bool[filas, columnas];
            revelado = new bool[filas, columnas];
            minasAdyacentes = new int[filas, columnas];
            celdasRestantes = (filas * columnas) - minas;

            Random rng = new Random();
            int colocadas = 0;
            while (colocadas < minas)
            {
                int r = rng.Next(filas);
                int c = rng.Next(columnas);
                if (!tieneMina[r, c]) { tieneMina[r, c] = true; colocadas++; }
            }

            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                    if (!tieneMina[i, j]) minasAdyacentes[i, j] = ContarMinas(i, j);

            int tamanoBoton = 40;
            int anchoTablero = columnas * tamanoBoton;
            int altoTablero = filas * tamanoBoton;

            int inicioX = (this.ClientSize.Width - anchoTablero) / 2;
            int inicioY = (this.ClientSize.Height - altoTablero) / 2;

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(tamanoBoton, tamanoBoton);
                    b.Location = new Point(inicioX + (j * tamanoBoton), inicioY + (i * tamanoBoton));
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
                    b.BackColor = Color.FromArgb(60, 65, 60);
                    b.ForeColor = Color.Gold;
                    b.Font = new Font("Impact", 12);
                    b.Tag = new Point(i, j);
                    b.MouseDown += ClicCelda;
                    botones[i, j] = b;
                    this.Controls.Add(b);
                }
            }

            lblContadorBanderas = new Label
            {
                Text = $"Banderas: 0 / {minas}",
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Location = new Point(inicioX, inicioY - 50), 
                Font = new Font("Impact", 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblContadorBanderas); 

            Label lblInfo = new Label
            {
                Text = "Despeja el campo minado",
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Location = new Point(inicioX, inicioY - 100),
                Size = new Size(anchoTablero, 40),
                Font = new Font("Stencil", 12),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblInfo);
        }

        private int ContarMinas(int r, int c)
        {
            int cuenta = 0;
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    int ni = r + i, nj = c + j;
                    if (ni >= 0 && ni < filas && nj >= 0 && nj < columnas && tieneMina[ni, nj]) cuenta++;
                }
            return cuenta;
        }

        private void ClicCelda(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            Point p = (Point)b.Tag;
            int r = p.X, c = p.Y;

            
            if (revelado[r, c]) return;


            if (e.Button == MouseButtons.Right)
            {
                if (b.Text == "🚩")
                {
                    b.Text = "";
                    banderasColocadas--;
                }
                else if (banderasColocadas < minas)
                {
                    b.Text = "🚩";
                    b.ForeColor = Color.Yellow;
                    banderasColocadas++;
                }

                
                lblContadorBanderas.Text = $"Banderas: {banderasColocadas} / {minas}";
                return;
            }


            if (e.Button == MouseButtons.Left)
            {
                
                if (b.Text == "🚩") return;

                var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

                if (tieneMina[r, c])
                {
                    if (fotoMina != null)
                    {
                        b.Image = fotoMina;
                        b.Text = "";
                    }
                    else
                    {
                        b.Text = "💣";
                    }
                    b.BackColor = Color.DarkRed;

                    ReproducirExplosion();
                    DatosGlobales.GuardarDatos();
                    MessageBox.Show("¡BOOM! Activaste la mina. Perdiste, vuelve a intentarlo.", "Alerta");
                    this.Close();
                }
                else
                {
                    Revelar(r, c);
                    if (celdasRestantes == 0)
                    {
                        int premio = 200;
                        partida.Oro += premio;
                        DatosGlobales.GuardarDatos();
                        MessageBox.Show($"¡Campo despejado! Ganaste {premio} G.", "VICTORIA");
                        this.Close();
                    }
                }
            }
        }

        private void Revelar(int r, int c)
        {
            if (r < 0 || r >= filas || c < 0 || c >= columnas || revelado[r, c]) return;

            revelado[r, c] = true;
            botones[r, c].BackColor = Color.FromArgb(20, 20, 20);

            int n = minasAdyacentes[r, c];
            if (n > 0)
            {
                botones[r, c].Text = n.ToString();
                if (n == 1) botones[r, c].ForeColor = Color.DeepSkyBlue;
                else if (n == 2) botones[r, c].ForeColor = Color.SpringGreen;
                else if (n == 3) botones[r, c].ForeColor = Color.Red;
            }
            celdasRestantes--;

            if (minasAdyacentes[r, c] == 0)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++) Revelar(r + i, c + j);
            }
        }
        private void ReproducirExplosion()
        {
            // Usamos Task.Run para que no congele el juego ni un milisegundo al cargar
            Task.Run(() => {
                var lector = new WaveFileReader(Properties.Resources.explosion_1);
                var reproductor = new WaveOutEvent();

                reproductor.Volume = 1f;

                reproductor.Init(lector);
                reproductor.Play();

                reproductor.PlaybackStopped += (sender, args) =>
                {
                    lector.Dispose();
                    reproductor.Dispose();
                };
            });
        }
    }
}