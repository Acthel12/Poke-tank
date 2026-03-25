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
        private string categoriaActual = "Escudos";
        private List<ItemTienda> inventario = new List<ItemTienda>();
        private Rectangle[] pestañas = new Rectangle[4];
        private string[] nombresPestañas = { "Escudos", "Potenciadores", "Misiles", "Balas" };
        private Rectangle botonSalir = new Rectangle(680, 15, 100, 40);

        private TextureBrush pincelFondo;
        private Color colorBorde = Color.FromArgb(180, 190, 150);

        public Tienda()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.None; //esto esta bueno para ponerselo a otras partes del juego Att: Gabriel
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Paint += DibujarTienda;
            this.MouseDown += ClicEnTienda;

            CargarImagenFondo();

            for (int i = 0; i < 4; i++)
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
            // --- ESCUDOS ---
            inventario.Add(new ItemTienda { Nombre = "Escudo Básico", Categoria = "Escudos", Precio = 100, ValorEfecto = 200, Descripcion = "Blindaje estándar de acero +200.", Rect = new Rectangle(25, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Escudo Reforzado", Categoria = "Escudos", Precio = 250, ValorEfecto = 400, Descripcion = "Placas de titanio reforzado +400.", Rect = new Rectangle(280, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Escudo Uranio", Categoria = "Escudos", Precio = 500, ValorEfecto = 600, Descripcion = "Blindaje radiactivo de alta densidad +600.", Rect = new Rectangle(535, 110, 240, 185) });

            // --- POTENCIADORES (PELUCAS) ---
            inventario.Add(new ItemTienda { Nombre = "Calva Brillante", Categoria = "Potenciadores", Precio = 350, ValorEfecto = 0, Descripcion = "Refleja la luz solar para cegar y confundir al oponente.", Rect = new Rectangle(25, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Peluca Afro-Fuego", Categoria = "Potenciadores", Precio = 600, ValorEfecto = 100, Descripcion = "Estilo ardiente que aumenta el daño de ataque en +100.", Rect = new Rectangle(280, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Peluca de Acero", Categoria = "Potenciadores", Precio = 450, ValorEfecto = 250, Descripcion = "Protección capilar blindada que sube la defensa +250.", Rect = new Rectangle(535, 110, 240, 185) });

            // --- MISILES ---
            inventario.Add(new ItemTienda { Nombre = "Misil Térmico", Categoria = "Misiles", Precio = 400, ValorEfecto = 150, Descripcion = "Rastreo de calor para daño crítico +150.", Rect = new Rectangle(25, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Ojiva Nuclear", Categoria = "Misiles", Precio = 800, ValorEfecto = 400, Descripcion = "Poder atómico devastador. Daño Masivo +400.", Rect = new Rectangle(280, 110, 240, 185) });

            // --- BALAS ---
            inventario.Add(new ItemTienda { Nombre = "Bala AP", Categoria = "Balas", Precio = 150, ValorEfecto = 50, Descripcion = "Proyectil perforante de blindaje. Penetración +50.", Rect = new Rectangle(25, 110, 240, 185) });
            inventario.Add(new ItemTienda { Nombre = "Bala Explosiva", Categoria = "Balas", Precio = 300, ValorEfecto = 100, Descripcion = "Munición de alto impacto con radio extendido +100.", Rect = new Rectangle(280, 110, 240, 185) });
        }

        private void DibujarTienda(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (pincelFondo != null) g.FillRectangle(pincelFondo, this.ClientRectangle);
            else g.Clear(Color.FromArgb(45, 50, 40));

            // Botón CERRAR
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 150, 0, 0)), botonSalir);
            g.DrawRectangle(new Pen(Color.White, 2), botonSalir);
            g.DrawString("CERRAR", new Font("Impact", 12), Brushes.White, botonSalir.X + 22, botonSalir.Y + 10);

            // Panel Oro
            var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];
            g.FillRectangle(new SolidBrush(Color.FromArgb(220, 0, 0, 0)), 540, 15, 130, 40);
            g.DrawString($"{partida.Oro} G", new Font("Stencil", 14), Brushes.Gold, 550, 25);

            // Pestañas
            for (int i = 0; i < 4; i++)
            {
                bool activa = categoriaActual == nombresPestañas[i];
                g.FillRectangle(activa ? new SolidBrush(Color.FromArgb(220, 60, 75, 50)) : new SolidBrush(Color.FromArgb(180, 50, 50, 50)), pestañas[i]);
                g.DrawRectangle(new Pen(colorBorde, 2), pestañas[i]);
                g.DrawString(nombresPestañas[i], new Font("Impact", 10), activa ? Brushes.Gold : Brushes.Silver, pestañas[i].X + 10, pestañas[i].Y + 12);
            }

            // Items
            foreach (var item in inventario.Where(i => i.Categoria == categoriaActual))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(235, 15, 15, 15)), item.Rect);
                g.DrawRectangle(new Pen(colorBorde, 3), item.Rect);

                // Icono
                Rectangle rectIcono = new Rectangle(item.Rect.X + 12, item.Rect.Y + 15, 65, 65);
                g.FillRectangle(Brushes.Black, rectIcono);
                g.DrawRectangle(Pens.Gold, rectIcono);

                // Título
                g.DrawString(item.Nombre, new Font("Impact", 12), Brushes.White, item.Rect.X + 85, item.Rect.Y + 20);

                // AJUSTE DE TEXTO AUTOMÁTICO (WRAP)
                RectangleF rectTexto = new RectangleF(item.Rect.X + 85, item.Rect.Y + 45, item.Rect.Width - 95, 75);
                g.DrawString(item.Descripcion, new Font("Consolas", 8, FontStyle.Bold), Brushes.SpringGreen, rectTexto);

                // Botón Compra
                Rectangle btn = new Rectangle(item.Rect.X + 12, item.Rect.Y + 135, item.Rect.Width - 24, 38);
                g.FillRectangle(Brushes.Black, btn);
                g.DrawRectangle(new Pen(Color.Gold, 2), btn);
                g.DrawString($"ADQUIRIR: {item.Precio}G", new Font("Stencil", 10), Brushes.Gold, btn.X + 35, btn.Y + 11);
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
                if (item.Categoria == "Escudos" || item.Categoria == "Potenciadores") partida.tanqueUsuario.Defensa += item.ValorEfecto;
                //else if (item.Categoria == "Misiles") partida.tanqueUsuario.DanoMisil += item.ValorEfecto;
                //else if (item.Categoria == "Balas") partida.tanqueUsuario.DanoBala += item.ValorEfecto;
                DatosGlobales.GuardarDatos();
                this.Invalidate();
            }
            else MessageBox.Show("No tienes oro suficiente, soldado.");
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
    }
}