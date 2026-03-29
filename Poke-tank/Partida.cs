using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Poke_tank
{
    public enum NivelDificultad
    {
        Facil,
        Normal,
        Dificil
    }
    public class Partida
    {
        private Random random = new Random();

        public String NombreJugador { get; set; }
        public Tanque TanqueUsuario { get; set; }
        public Puntuacion puntuacion { get; set; } //quiero que se guarde la puntuacion de la partida para mostrarla al cargar partidas
        public DateTime fechaCreacion { get; set; } //para mostrar la fecha de creación de la partida en la lista de partidas guardadas
        public NivelDificultad Dificultad { get; set; }
        public int EnemigosDerrotados { get; set; }
        public int Oro { get; set; }

        

        public Partida() { } //constructor vacio para json serializer
        public Partida(Tanque tanqueUsuario, String nombreJugador)
        {
            this.fechaCreacion = DateTime.Now;

            this.TanqueUsuario = tanqueUsuario;

        }

        //crea una nueva partida
        public static void iniciarPartida(string nombreJugador,int vida, int ataque, NivelDificultad dificultad)
        {
            //crea tanque del jugador
            Tanque jugador = new Tanque(vida, ataque);

            //crea partida y agregar a datos globales
            Partida nueva = new Partida(jugador, nombreJugador);
            nueva.CalcularPuntuacion(); //inicializa la puntuación de la partida
            nueva.Dificultad = dificultad;

            DatosGlobales.ListaPartidas.Add(nueva);
            DatosGlobales.PartidaActualIndex = DatosGlobales.ListaPartidas.Count - 1;
        }

        //Funcion para calcular la puntuación al finalizar la partida, basada en el número de enemigos derrotados y se muestra al usuario
        public void CalcularPuntuacion()
        {
            this.puntuacion = new Puntuacion(this.NombreJugador, this.EnemigosDerrotados ,this.Dificultad);
        }
    }
}