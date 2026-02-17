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
        public static int Nivel { get; private set; } = 0; //control del nivel seleccionado

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

        //iniciamos la partida según el nivel escogido
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            Partida.iniciarPartida("Comandante Flavio Rosales", "T-90", 140, 30, 15, 14, Properties.Resources.fondoNivel1);
            Nivel = 1;
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            Partida.iniciarPartida("Capitán Flavio Rosales", "T-80", 120, 20, 12, 12, Properties.Resources.fondoNivel2);
            Nivel = 2;
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            Partida.iniciarPartida("Coronel Flavio Rosales", "T-72", 100, 15, 10, 10, Properties.Resources.fondoNivel3);
            Nivel = 3;
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
                    DatosGlobales.GuardarDatos();

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
    }
}
