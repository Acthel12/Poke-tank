using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Seleccionar_Partida : Form
    {
        // --- NUEVA PALETA DE COLORES "PREMIUM TACTICAL" ---
        private readonly Color COLOR_FONDO = Color.FromArgb(20, 20, 22);
        private readonly Color COLOR_TABLA = Color.FromArgb(30, 30, 35);
        private readonly Color COLOR_TABLA_ALT = Color.FromArgb(38, 38, 45); // Para el efecto cebra
        private readonly Color COLOR_ACENTO = Color.FromArgb(46, 204, 113); // Verde Esmeralda elegante
        private readonly Color COLOR_PELIGRO = Color.FromArgb(231, 76, 60); // Rojo táctico
        private readonly Color COLOR_TEXTO = Color.WhiteSmoke;
        private readonly Color COLOR_BORDE = Color.FromArgb(50, 50, 60);

        public Seleccionar_Partida()
        {
            InitializeComponent();
            ConfigurarEstiloVisual();
            EstilizarBotones();
        }

        private void ConfigurarEstiloVisual()
        {
            // 1. Estilo del Formulario
            this.BackColor = COLOR_FONDO;
            this.ForeColor = COLOR_TEXTO;

            // 2. Estilo Maestro de la Tabla
            dgvPartidas.BackgroundColor = COLOR_FONDO; // Para que se mezcle con el fondo
            dgvPartidas.BorderStyle = BorderStyle.None;
            dgvPartidas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPartidas.GridColor = COLOR_BORDE;

            // 3. Encabezados Modernos y Centrados
            dgvPartidas.EnableHeadersVisualStyles = false;
            dgvPartidas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPartidas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 18);
            dgvPartidas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvPartidas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvPartidas.ColumnHeadersHeight = 45;
            dgvPartidas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Centrado

            // 4. Filas y Selección
            dgvPartidas.DefaultCellStyle.BackColor = COLOR_TABLA;
            dgvPartidas.DefaultCellStyle.ForeColor = COLOR_TEXTO;
            dgvPartidas.DefaultCellStyle.SelectionBackColor = COLOR_ACENTO;
            dgvPartidas.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPartidas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Centrado
            dgvPartidas.RowTemplate.Height = 45;

            // 5. Efecto Cebra (Filas alternas oscuras)
            dgvPartidas.AlternatingRowsDefaultCellStyle.BackColor = COLOR_TABLA_ALT;

            // 6. Limpieza Visual y Comportamiento
            dgvPartidas.RowHeadersVisible = false;
            dgvPartidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidas.MultiSelect = false;
            dgvPartidas.ReadOnly = true;
            dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidas.AllowUserToResizeRows = false;
        }

        private void EstilizarBotones()
        {
            // ==========================================
            // DISEÑO DEL BOTÓN "CARGAR PARTIDA"
            // ==========================================
            btnCargar.FlatStyle = FlatStyle.Flat;
            btnCargar.FlatAppearance.BorderSize = 2; // Borde más grueso
            btnCargar.FlatAppearance.BorderColor = COLOR_ACENTO;
            btnCargar.BackColor = COLOR_FONDO;
            btnCargar.ForeColor = COLOR_ACENTO;
            btnCargar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCargar.Cursor = Cursors.Hand;

            // ---> QUITA LAS BARRITAS '//' ABAJO Y PON EL NOMBRE DE TU STICKER <---
            // btnCargar.Image = Properties.Resources.TU_IMAGEN_DE_TANQUE; 

            btnCargar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCargar.ImageAlign = ContentAlignment.MiddleCenter;
            btnCargar.Padding = new Padding(10, 0, 10, 0);

            // Efecto Hover
            btnCargar.MouseEnter += (s, e) => {
                btnCargar.BackColor = COLOR_ACENTO;
                btnCargar.ForeColor = Color.White;
            };
            btnCargar.MouseLeave += (s, e) => {
                btnCargar.BackColor = COLOR_FONDO;
                btnCargar.ForeColor = COLOR_ACENTO;
            };

            // ==========================================
            // DISEÑO DEL BOTÓN "ELIMINAR PARTIDA"
            // ==========================================
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.FlatAppearance.BorderSize = 2; // Borde más grueso
            btnEliminar.FlatAppearance.BorderColor = COLOR_PELIGRO;
            btnEliminar.BackColor = COLOR_FONDO;
            btnEliminar.ForeColor = COLOR_PELIGRO;
            btnEliminar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnEliminar.Cursor = Cursors.Hand;

            // ---> QUITA LAS BARRITAS '//' ABAJO Y PON EL NOMBRE DE TU STICKER <---
            // btnEliminar.Image = Properties.Resources.TU_IMAGEN_DE_BASURA; 

            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.ImageAlign = ContentAlignment.MiddleCenter;
            btnEliminar.Padding = new Padding(10, 0, 10, 0);

            // Efecto Hover
            btnEliminar.MouseEnter += (s, e) => {
                btnEliminar.BackColor = COLOR_PELIGRO;
                btnEliminar.ForeColor = Color.White;
            };
            btnEliminar.MouseLeave += (s, e) => {
                btnEliminar.BackColor = COLOR_FONDO;
                btnEliminar.ForeColor = COLOR_PELIGRO;
            };
        }

        private void ActualizarPartidas()
        {
            List<Partida> partidas = DatosGlobales.ListaPartidas;
            dgvPartidas.DataSource = null;

            var partidasMostrar = partidas.Select(p => new
            {
                Comandante = p.NombreJugador,
                Victorias = p.EnemigosDerrotados,
                Registro = p.fechaCreacion.ToString("dd/MM/yyyy"),
                Puntaje = p.puntuacion.PuntosTotales,
                Rango = p.Dificultad.ToString(), // Columna que se pinta de colores
                Créditos = p.Oro
            }).ToList();

            dgvPartidas.DataSource = partidasMostrar;
        }

        private void bntCargar_Click(object sender, EventArgs e)
        {
            if (dgvPartidas.SelectedRows.Count > 0)
            {
                int index = dgvPartidas.SelectedRows[0].Index;
                DatosGlobales.PartidaActualIndex = index;

                this.Hide();
                Mapa form = new Mapa();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un registro de combate antes de proceder.", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPartidas.SelectedRows.Count > 0)
            {
                int index = dgvPartidas.SelectedRows[0].Index;
                var res = MessageBox.Show("¿CONFIRMAR ELIMINACIÓN DE DATOS?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    DatosGlobales.ListaPartidas.RemoveAt(index);
                    DatosGlobales.GuardarDatos();
                    ActualizarPartidas();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una partida para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Seleccionar_Partida_Load(object sender, EventArgs e)
        {
            ActualizarPartidas();
        }

        private void dgvPartidas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Colores dinámicos por Rango
            if (dgvPartidas.Columns[e.ColumnIndex].Name == "Rango" && e.Value != null)
            {
                string r = e.Value.ToString();
                if (r == "Facil") e.CellStyle.ForeColor = Color.SpringGreen;
                else if (r == "Normal") e.CellStyle.ForeColor = Color.Cyan;
                else if (r == "Dificil") e.CellStyle.ForeColor = Color.Crimson;
            }
        }
    }
}