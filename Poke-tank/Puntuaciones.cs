using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Puntuaciones : Form
    {
        public Puntuaciones()
        {
            InitializeComponent();
            // Cargar datos cuando se abra el formulario
            this.Load += Puntuaciones_Load;
        }

        private void Puntuaciones_Load(object? sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                if (DatosGlobales.ListaPuntuaciones == null || !DatosGlobales.ListaPuntuaciones.Any())
                {
                    dataGridView1.DataSource = null;
                    return;
                }

                // Seleccionamos los datos incluyendo la columna puntos
                var top10 = DatosGlobales.ListaPuntuaciones
                    .OrderByDescending(p => p.PuntosTotales)
                    .Take(10)
                    .Select(p => new
                    {
                        Tanque = p.NombreTanque,
                        Destruidos = p.TanquesDerrotados.Count,
                        Puntos = p.PuntosTotales,
                        Fecha = p.Fecha.ToShortDateString(),
                        Dificultad= p.Dificultad
                    })
                    .ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = top10;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
                MessageBox.Show($"Error al cargar puntuaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}