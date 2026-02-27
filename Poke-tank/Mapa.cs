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

        }

        //iniciamos la partida según el nivel escogido
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 0;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
            Mapa_Load(sender, e); //para actualizar el estado de los botones después de cada batalla
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 1;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
            Mapa_Load(sender, e); //para actualizar el estado de los botones después de cada batalla
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            DatosGlobales.NivelSeleccionado = 2;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
            Mapa_Load(sender, e); //para actualizar el estado de los botones después de cada batalla
        }

        //botón de easter egg flavionística
        private void botonSorpresaFlavio_Click(object sender, EventArgs e)
        {
            Flaviosorpresa from1 = new Flaviosorpresa();
            from1.ShowDialog();
        }

        //al finalizar la aventura, se registra la puntuación total obtenida en la campaña, basada en el número de enemigos derrotados y se muestra al usuario
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

        //botón para finalizar la aventura y registrar la puntuación obtenida
        private void botonFinalizar_Click(object sender, EventArgs e)
        {
            FinalizarAventura();
        }

        //método para seleccionar el fondo del mapa automáticamente según el nivel escogido
        private void SeleccionarMapa(Form form)
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

        //al cargar el mapa, se habilitan o deshabilitan los botones de los niveles según el progreso del usuario en la campaña
        private void Mapa_Load(object sender, EventArgs e)
        {
            Partida partidaActual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            if (partidaActual == null)
            {
                return;
            }
            if (partidaActual.enemigosDerrotados.Count == 0)
            {
                botonNivel1.Enabled = true;
                botonNivel2.Enabled = false;
                botonNivel3.Enabled = false;
            }
            else if (partidaActual.enemigosDerrotados.Count == 1)
            {
                botonNivel1.Enabled = false;
                botonNivel2.Enabled = true;
                botonNivel3.Enabled = false;
            }
            else if (partidaActual.enemigosDerrotados.Count == 2)
            {
                botonNivel1.Enabled = false;
                botonNivel2.Enabled = false;
                botonNivel3.Enabled = true;
            }
            else 
            {
                botonNivel1.Enabled = true;
                botonNivel2.Enabled = true; 
                botonNivel3.Enabled = true;
            }
        }
    }
}
