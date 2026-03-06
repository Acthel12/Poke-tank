using System;
using System.Collections.Generic;
using System.Linq;

namespace Poke_tank
{
    //puntuación obtenida al finalizar la aventura, basada en el número de enemigos derrotados y se muestra al usuario
    public class Puntuacion
    {
        public string NombreTanque { get; set; } = string.Empty;
        public List<TanqueEnemigo> TanquesDerrotados { get; set; }
        public DateTime Fecha { get; set; }
        public NivelDificultad Dificultad { get; set; }
        public int PuntosTotales { get; set; } 

        //Constructor vacio para json serializer
        public Puntuacion() {}

        public Puntuacion(string nombre, List<TanqueEnemigo> derrotados,NivelDificultad dificultad)
        {
            NombreTanque = nombre;
            TanquesDerrotados = derrotados;
            Fecha = DateTime.Now;
            Dificultad = dificultad;

            PuntosTotales = CalcularPuntuacion(dificultad);
        }
        
        private int CalcularPuntuacion(NivelDificultad dificultad)
        {
            int cantidadT72 = TanquesDerrotados.Count(t => t.Modelo == "T-72");
            int cantidadT80 = TanquesDerrotados.Count(t => t.Modelo == "T-80");
            int cantidadT90 = TanquesDerrotados.Count(t => t.Modelo == "T-90");
            int cantidadT14 = TanquesDerrotados.Count(t => t.Modelo == "T-14 Armata");

            int puntosT72 = cantidadT72 * 25;
            int puntosT80 = cantidadT80 * 50;
            int puntosT90 = cantidadT90 * 100;
            int puntosT14 = cantidadT14 * 200;

            int multiplicadorDificultad = dificultad switch
            {
                NivelDificultad.Facil => 1,
                NivelDificultad.Normal => 2,
                NivelDificultad.Dificil => 3,
                _ => 1
            };

            return (puntosT72 + puntosT80 + puntosT90 + puntosT14) * multiplicadorDificultad;
        }
    }
}