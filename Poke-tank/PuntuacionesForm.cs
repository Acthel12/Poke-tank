using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Poke_tank
{
    public class PuntuacionesForm : Form
    {
        private DataGridView tablaPuntos;
        private Button botonCerrar;

        public PuntuacionesForm()
        {
            // Configuración de la Ventana
            this.Text = "TOP 10 - Puntuaciones Más Altas";
            this.Size = new Size(500, 450);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crear el DataGridView
            tablaPuntos = new DataGridView();
            tablaPuntos.Location = new Point(20, 20);
            tablaPuntos.Size = new Size(440, 300);
            tablaPuntos.BackgroundColor = Color.Gray;
            tablaPuntos.ForeColor = Color.Black;
            tablaPuntos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaPuntos.ReadOnly = true;
            tablaPuntos.AllowUserToAddRows = false;
            
            // Botón Regresar
            botonCerrar = new Button();
            botonCerrar.Text = "Regresar al Menú";
            botonCerrar.Location = new Point(180, 340);
            botonCerrar.Size = new Size(130, 40);
            botonCerrar.BackColor = Color.DodgerBlue;
            botonCerrar.ForeColor = Color.White;
            botonCerrar.FlatStyle = FlatStyle.Flat;
            botonCerrar.Click += (s, e) => this.Close();

            this.Controls.Add(tablaPuntos);
            this.Controls.Add(botonCerrar);

            CargarDatos();
        }

        private void CargarDatos()
        {
            if (DatosGlobales.ListaPuntuaciones == null) return;

            // Seleccionamos los datos incluyendo la columna Puntos
            var top10 = DatosGlobales.ListaPuntuaciones
                .OrderByDescending(p => p.TanquesDerrotados)
                .Take(10)
                .Select(p => new { 
                    Tanque = p.NombreTanque, 
                    Destruidos = p.TanquesDerrotados,
                    Puntos = p.PuntosTotales, // Muestra el cálculo de 100 x tanque
                    Fecha = p.Fecha.ToShortDateString() 
                })
                .ToList();

            tablaPuntos.DataSource = top10;
        }
    }
}