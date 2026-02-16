using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Mapa : Form
    {
        public Mapa()
        {
            InitializeComponent();
            
            if (this.botonFinalizar != null)
            {
                this.botonFinalizar.Location = new Point(20, 20); 
                this.botonFinalizar.BackColor = Color.Red;       
                this.botonFinalizar.BringToFront();            
                this.Controls.SetChildIndex(this.botonFinalizar, 0); 
            }
        }

        private void botonSorpresaFlavio_Click(object sender, EventArgs e)
        {
            Flaviosorpresa from1 = new Flaviosorpresa();
            from1.ShowDialog();
        }

     
        private void FinalizarAventura()
        {
            if (DatosGlobales.PartadaActualIndex >= 0 && DatosGlobales.PartadaActualIndex < DatosGlobales.ListaPartidas.Count)
            {
                var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartadaActualIndex];

                if (partida.enemigosDerrotados.Count > 0)
                {
                    // Registramos la puntuación una sola vez al final
                    Puntuacion recordFinal = new Puntuacion(
                        partida.tanqueUsuario.Nombre, 
                        partida.enemigosDerrotados.Count
                    );

                    DatosGlobales.ListaPuntuaciones.Add(recordFinal);
                    DatosGlobales.GuardarDatos();

                    MessageBox.Show($"Campaña finalizada. ¡Puntaje total: {recordFinal.PuntosTotales} puntos registrados!");
                }
                else
                {
                    MessageBox.Show("Campaña finalizada sin victorias. No se registró puntuación.");
                }
            }

            this.Close(); // Regresa al Menú Principal
        }

      
        private void botonFinalizar_Click(object sender, EventArgs e)
        {
            FinalizarAventura();
        }
    }
}
