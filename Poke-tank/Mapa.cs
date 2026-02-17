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
        // Propiedad que refleja el nivel seleccionado. Setter privado para control centralizado.
        public Mapa()
        {
            InitializeComponent();
            
        }

        //iniciamos la partida según el nivel escogido
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 0;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 1;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 2;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
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
                    //registramos la puntuación una sola vez al final
                    Puntuacion recordFinal = new Puntuacion(
                        partida.tanqueUsuario.Nombre, 
                        partida.enemigosDerrotados.Count
                    );

                    DatosGlobales.ListaPuntuaciones.Add(recordFinal);

                    MessageBox.Show($"Campaña finalizada. ¡Puntaje total: {recordFinal.PuntosTotales} puntos registrados!");
                }
                else
                {
                    MessageBox.Show("Campaña finalizada sin victorias. No se registró puntuación.");
                }
            }

            this.Close(); //para regresar al menú
        }

      
        private void botonFinalizar_Click(object sender, EventArgs e)
        {
            FinalizarAventura();
        }
        private void SeleccionarMapa(Form form )
        {
            if (DatosGlobales.NivelSeleccionado == 0)
            {
                form.BackgroundImage = Properties.Resources.fondoNivel1;
            }
            else if (DatosGlobales.NivelSeleccionado == 1)
            {
                form.BackgroundImage = Properties.Resources.fondoNivel2;
            }
            else if (DatosGlobales.NivelSeleccionado == 2)
            {
                form.BackgroundImage = Properties.Resources.fondoNivel3;
            }
        }
    }
}
