using System;
using System.Collections.Generic;
using System.Linq;

namespace Poke_tank
{
    //puntuación obtenida al finalizar la aventura, basada en el número de enemigos derrotados y se muestra al usuario
    public class Puntuacion
    {
        public string NombreTanque { get; set; } = string.Empty;
        public int TanquesDerrotados { get; set; }
        public DateTime Fecha { get; set; }
        public NivelDificultad Dificultad { get; set; }
        public int PuntosTotales { get; set; } 

        //Constructor vacio para json serializer
        public Puntuacion() {}

        public Puntuacion(string nombre, int derrotados,NivelDificultad dificultad)
        {
            NombreTanque = nombre;
            TanquesDerrotados = derrotados;
            Fecha = DateTime.Now;
            Dificultad = dificultad;

            PuntosTotales = CalcularPuntuacion(dificultad);
        }
        
        private int CalcularPuntuacion(NivelDificultad dificultad)
        {
            
            int multiplicadorDificultad = dificultad switch
            {
                NivelDificultad.Facil => 1,
                NivelDificultad.Normal => 2,
                NivelDificultad.Dificil => 3,
                _ => 1
            };

            return (TanquesDerrotados * 100 * multiplicadorDificultad);
        }
    }
}