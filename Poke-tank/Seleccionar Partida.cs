using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Seleccionar_Partida : Form
    {
        public Seleccionar_Partida()
        {
            InitializeComponent();
        }

        private void bntCargar_Click(object sender, EventArgs e)
        {
            if (dgvPartidas.SelectedRows.Count > 0)
            {
                int index = dgvPartidas.SelectedRows[0].Index;
                DatosGlobales.PartidaActualIndex = index;
                Mapa form = new Mapa();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una partida para cargar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPartidas.SelectedRows.Count > 0)
            {
                int index = dgvPartidas.SelectedRows[0].Index;
                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta partida?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    DatosGlobales.ListaPartidas.RemoveAt(index);
                    DatosGlobales.GuardarDatos();
                    ActualizarPartidas(); // Recargar la lista de partidas
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una partida para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Seleccionar_Partida_Load(object sender, EventArgs e)
        {
            ActualizarPartidas();
        }

        private void ActualizarPartidas()
        {
            List<Partida> partidas = DatosGlobales.ListaPartidas;

            //limpiamos el DataGridView antes de cargar los datos
            dgvPartidas.DataSource = null;

            //Usamos una funcion LINQ para Filtrar los datos para la DataGriewView
            var partidasMostrar = partidas.Select(p => new
            {
                Nombre_Tanque = p.tanqueUsuario.Nombre,
                Enemigos_Derrotados = p.enemigosDerrotados.Count,
                Fecha_Partida = p.fechaCreacion.ToString("dd/MM/yyyy HH:mm:ss"),
                Puntuacion = p.puntuacion.PuntosTotales
            }).ToList();

            dgvPartidas.DataSource = partidasMostrar;
        }
    }
}
