using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Tienda : Form
    {
        private string categoriaActual = "Vida";
        private List<ItemTienda> inventario = new List<ItemTienda>();
        private Rectangle[] pestañas = new Rectangle[2];
        private string[] nombresPestañas = { "Vida", "Ataque" };
        private Rectangle botonSalir = new Rectangle(680, 15, 100, 40);

        private TextureBrush pincelFondo;
        private Color colorBorde = Color.FromArgb(180, 190, 150);
        private Image[] imagenesVida = new Image[3];
        private Image[] imagenesAtaque = new Image[3];

        public Tienda()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.None; //esto esta bueno para ponerselo a otras partes del juego Att: Gabriel
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Paint += DibujarTienda;
            this.MouseDown += ClicEnTienda;

            CargarImagenes();

            CargarImagenFondo();

            for (int i = 0; i < 2; i++)
                pestañas[i] = new Rectangle(20 + (i * 130), 15, 120, 40);

            CargarItems();
        }

        private void CargarImagenFondo()
        {
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fondotienda.png");
                if (File.Exists(ruta))
                {
                    pincelFondo = new TextureBrush(Image.FromFile(ruta), WrapMode.Tile);
                }
            }
            catch { }
        }

        private void CargarItems()
        {
            // --- VIDA ---
            inventario.Add(new ItemTienda { Nombre = "Kit de Salud", Categoria = "Vida", Precio = 100, ValorEfecto = 50, Descripcion = "Aumenta 50 puntos de vida al instante.", Rect = new Rectangle(25, 110, 240, 185), Icono = imagenesVida[0] });
            inventario.Add(new ItemTienda { Nombre = "Nano-Reparador", Categoria = "Vida", Precio = 250, ValorEfecto = 150, Descripcion = "Nanobots que reparan chasis +150 vida.", Rect = new Rectangle(280, 110, 240, 185), Icono = imagenesVida[1] });
            inventario.Add(new ItemTienda { Nombre = "Automatic-Repair", Categoria = "Vida", Precio = 500, ValorEfecto = 350, Descripcion = "Módulo regenerativo: +350 vida .", Rect = new Rectangle(535, 110, 240, 185), Icono = imagenesVida[2] });

            // --- ATAQUE ---
            inventario.Add(new ItemTienda { Nombre = "Cañón Mejorado", Categoria = "Ataque", Precio = 200, ValorEfecto = 40, Descripcion = "Mejora el cañón principal: +40 de daño.", Rect = new Rectangle(25, 110, 240, 185), Icono = imagenesAtaque[0] });
            inventario.Add(new ItemTienda { Nombre = "Foco de Precisión", Categoria = "Ataque", Precio = 350, ValorEfecto = 100, Descripcion = "Aumenta la puntería y el daño crítico +100.", Rect = new Rectangle(280, 110, 240, 185), Icono = imagenesAtaque[1] });
            inventario.Add(new ItemTienda { Nombre = "Carga de Energía", Categoria = "Ataque", Precio = 600, ValorEfecto = 250, Descripcion = "Sobrealimenta las armas para daño masivo +250.", Rect = new Rectangle(535, 110, 240, 185), Icono = imagenesAtaque[2] });
        }

        private void DibujarTienda(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (pincelFondo != null) g.FillRectangle(pincelFondo, this.ClientRectangle);
            else g.Clear(Color.FromArgb(237, 201, 175)); // fondo tipo arena

            // Botón SALIR (tono marrón oscuro sobre arena)
            g.FillRectangle(new SolidBrush(Color.FromArgb(220, 92, 64, 51)), botonSalir);
            g.DrawRectangle(new Pen(Color.FromArgb(120, 80, 50), 2), botonSalir);
            g.DrawString("SALIR", new Font("Impact", 12), new SolidBrush(Color.FromArgb(255, 237, 201, 175)), botonSalir.X + 26, botonSalir.Y + 10);

            // Panel Oro (semisólido tipo arena oscura para contraste)
            var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 210, 180, 140)), 540, 15, 130, 40);
            g.DrawRectangle(new Pen(Color.FromArgb(120, 80, 50), 2), 540, 15, 130, 40);
            g.DrawString($"{partida.Oro} G", new Font("Stencil", 14), Brushes.Black, 550, 25);

            // Pestañas
            for (int i = 0; i < 2; i++)
            {
                bool activa = categoriaActual == nombresPestañas[i];
                // Pestañas en tonos ocre / arena
                g.FillRectangle(activa ? new SolidBrush(Color.FromArgb(230, 204, 119, 34)) : new SolidBrush(Color.FromArgb(255, 237, 201, 175)), pestañas[i]);
                g.DrawRectangle(new Pen(Color.FromArgb(160, 120, 80), 2), pestañas[i]);
                // Texto en marrón oscuro
                g.DrawString(nombresPestañas[i], new Font("Impact", 10), new SolidBrush(Color.FromArgb(60, 40, 20)), pestañas[i].X + 10, pestañas[i].Y + 12);
            }

            // Items
            foreach (var item in inventario.Where(i => i.Categoria == categoriaActual))
            {
                // caja con tono arena
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 237, 201, 175)), item.Rect);
                g.DrawRectangle(new Pen(colorBorde, 3), item.Rect);

                // Icono
                Rectangle rectIcono = new Rectangle(item.Rect.X + 12, item.Rect.Y + 15, 65, 65);

                if (item.Icono != null)
                {
                    // Si la imagen cargó correctamente, la dibuja adaptándola al rectángulo
                    g.DrawImage(item.Icono, rectIcono);
                }
                else
                {
                    // Si no hay imagen (es null), dibuja el cuadrado de color como placeholder
                    g.FillRectangle(new SolidBrush(Color.FromArgb(200, 210, 180, 140)), rectIcono);
                }

                // Dibuja el borde ocre alrededor del icono (sea imagen o placeholder)
                g.DrawRectangle(new Pen(Color.FromArgb(204, 119, 34), 2), rectIcono);

                // Título en marrón oscuro
                g.DrawString(item.Nombre, new Font("Impact", 12), new SolidBrush(Color.FromArgb(60, 40, 20)), item.Rect.X + 85, item.Rect.Y + 20);

                // AJUSTE DE TEXTO AUTOMÁTICO (WRAP) en color marrón
                RectangleF rectTexto = new RectangleF(item.Rect.X + 85, item.Rect.Y + 45, item.Rect.Width - 95, 75);
                g.DrawString(item.Descripcion, new Font("Consolas", 8, FontStyle.Bold), new SolidBrush(Color.FromArgb(80, 50, 30)), rectTexto);

                // Botón Compra con acento ocre
                Rectangle btn = new Rectangle(item.Rect.X + 12, item.Rect.Y + 135, item.Rect.Width - 24, 38);
                g.FillRectangle(new SolidBrush(Color.FromArgb(204, 119, 34)), btn);
                g.DrawRectangle(new Pen(Color.FromArgb(120, 80, 50), 2), btn);
                g.DrawString($"ADQUIRIR: {item.Precio}G", new Font("Stencil", 10), new SolidBrush(Color.FromArgb(60, 40, 20)), btn.X + 35, btn.Y + 11);
            }
        }

        private void ClicEnTienda(object sender, MouseEventArgs e)
        {
            if (botonSalir.Contains(e.Location)) this.Close();
            for (int i = 0; i < pestañas.Length; i++)
                if (pestañas[i].Contains(e.Location)) { categoriaActual = nombresPestañas[i]; this.Invalidate(); return; }

            foreach (var item in inventario.Where(i => i.Categoria == categoriaActual))
            {
                if (item.Rect.Contains(e.Location))
                {
                    if (MessageBox.Show($"¿Confirmar compra de {item.Nombre}?", "TIENDA", MessageBoxButtons.YesNo) == DialogResult.Yes) AplicarCompra(item);
                }
            }
        }

        private void AplicarCompra(ItemTienda item)
        {
            var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
            if (partida.Oro >= item.Precio)
            {
                partida.Oro -= item.Precio;
                // Aplicar efecto según la categoría actualizada (Vida / Ataque)
                
                if (item.Categoria == "Vida")
                {
                    if (partida.TanqueUsuario == null)
                        return;
                    else
                        partida.TanqueUsuario.vida += item.ValorEfecto;

                    MessageBox.Show($"Compra exitosa. +{item.ValorEfecto} vida aplicada.", "TIENDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (item.Categoria == "Ataque")
                {
                    if (partida.TanqueUsuario == null)
                        return;
                    else
                        partida.TanqueUsuario.ataque += item.ValorEfecto;

                    MessageBox.Show($"Compra exitosa. +{item.ValorEfecto} ataque aplicado.", "TIENDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

                DatosGlobales.GuardarDatos();
                this.Invalidate();
            }
            else MessageBox.Show("No tienes oro suficiente, soldado.");
        }
        private void CargarImagenes()
        {
            try
            {
                imagenesVida[0] = Properties.Resources.Vida1;
                imagenesVida[1] = Properties.Resources.Vida2;
                imagenesVida[2] = Properties.Resources.Vida3;   

                imagenesAtaque[0] = Properties.Resources.Ataque1;
                imagenesAtaque[1] = Properties.Resources.Ataque2;
                imagenesAtaque[2] = Properties.Resources.Ataque3;
            }
            catch { return; }
        }
    }

    public class ItemTienda
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int ValorEfecto { get; set; }
        public Rectangle Rect { get; set; }
        public Image Icono { get; set; }
    }
    
}