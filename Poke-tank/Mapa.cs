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
        //cambia el fondo del formulario de la batalla según el nivel seleccionado
        private void CambiarFondo(FormBatalla formulario, System.Drawing.Image imagen)
        {
            if (formulario == null || imagen == null) return;
            formulario.BackgroundImage = imagen;
            formulario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        //especificamos las imágenes y las aplicamos al form de batalla
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            FormBatalla from2 = new FormBatalla();
            CambiarFondo(from2, Properties.Resources.fondoNivel1);
            from2.ShowDialog();
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            FormBatalla from3 = new FormBatalla();
            CambiarFondo(from3, Properties.Resources.fondoNivel2);
            from3.ShowDialog();
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            FormBatalla from4 = new FormBatalla();
            CambiarFondo(from4, Properties.Resources.fondoNivel3);
            from4.ShowDialog();
        }

        private void botonSorpresaFlavio_Click(object sender, EventArgs e)
        {
            Flaviosorpresa from1 = new Flaviosorpresa();
            from1.ShowDialog();
        }

     
        private void FinalizarAventura()
        {
            if (DatosGlobales.PartidaActualIndex >= 0 && DatosGlobales.PartidaActualIndex < DatosGlobales.ListaPartidas.Count)
            {
                var partida = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

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
