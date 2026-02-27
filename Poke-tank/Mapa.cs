using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
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
            SeleccionarNivel(0);
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            SeleccionarNivel(1);
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            SeleccionarNivel(2);
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
                    
                    //Terminamos con la partida
                    DatosGlobales.ListaPartidas.Remove(partida);

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

        //botón para finalizar la aventura y registrar la puntuación obtenida
        private void botonFinalizar_Click(object sender, EventArgs e)
        {
            Partida partidaActual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            if (partidaActual.enemigosDerrotados.Count >= 3)
            {
                FinalizarAventura();
            }
            else
            {
                this.Close(); //para salir sin registrar puntuación
            }
        }

        //método para seleccionar el fondo del mapa automáticamente según el nivel escogido
        private void SeleccionarMapa(Form form)
        {
            form.BackgroundImage = DatosGlobales.NivelSeleccionado switch
            {
                0 => Properties.Resources.fondoNivel1,

                1 => Properties.Resources.fondoNivel2,

                2 => Properties.Resources.fondoNivel3,
            };
        }

        //al cargar el mapa, se habilitan o deshabilitan los botones de los niveles según el progreso del usuario en la campaña
        private void Mapa_Load(object sender, EventArgs e)
        {
            ActualizarMapa();
        }
        private void ActualizarMapa()
        {
            Partida partidaActual = DatosGlobales.ListaPartidas[DatosGlobales.PartidaActualIndex];

            //Comprobamos el número de enemigos derrotados para determinar qué niveles están disponibles
            botonNivel1.Enabled = (partidaActual.enemigosDerrotados.Count == 0 || partidaActual.enemigosDerrotados.Count >= 3);
            botonNivel2.Enabled = (partidaActual.enemigosDerrotados.Count == 1 || partidaActual.enemigosDerrotados.Count >= 3);
            botonNivel3.Enabled = (partidaActual.enemigosDerrotados.Count >= 2 );

            //Comprobamos si el usuario ha derrotado a los 3 enemigos para mostrar el botón de finalizar campaña, si no se cambia por salir
            if (partidaActual.enemigosDerrotados.Count >= 3)
            {
                botonFinalizar.Text = "Finalizar campaña";
            }
            else
            {
                botonFinalizar.Text = "Salir";
            }
        }
        //método para seleccionar el nivel desde el menú del mapa, se llama desde los botones de cada nivel
        public void SeleccionarNivel(int nivel)
        {
            DatosGlobales.NivelSeleccionado = nivel;
            Form form = new FormBatalla();
            SeleccionarMapa(form);
            form.ShowDialog();
            ActualizarMapa();
        }
    }
}
