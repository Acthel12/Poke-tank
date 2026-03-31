using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

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
            this.Size = new Size(420, 510);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(25, 30, 25);

            CargarFotoPersonalizada();
            IniciarTablero();
        }

        private void CargarFotoPersonalizada()
        {
            try
            {
                // Ahora busca específicamente flaviomina.png
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "flaviomina.png");
                if (File.Exists(ruta))
                {
                    Image imgOriginal = Image.FromFile(ruta);
                    fotoMina = new Bitmap(imgOriginal, new Size(35, 35));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Aviso: No se cargó la foto: " + ex.Message);
            }
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

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(40, 40);
                    b.Location = new Point(j * 40 + 10, i * 40 + 60);
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
                ForeColor = Color.Yellow,
                Location = new Point(10, 40), // Debajo del título principal
                Size = new Size(380, 20),
                Font = new Font("Impact", 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblContadorBanderas); 

            Label lblInfo = new Label
            {
                Text = ">>> BUSCA LAS MINAS (O AL FLAVIO) <<<",
                ForeColor = Color.LimeGreen,
                Location = new Point(10, 10),
                Size = new Size(380, 40),
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

                    DatosGlobales.GuardarDatos();
                    MessageBox.Show("¡BOOM! Activaste al flavio sorpresa. Perdiste, vuelve a intentarlo.", "ERROR DE LOGÍSTICA");
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
    }
}