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

        public int PuntosTotales = 0 ; 


        public Puntuacion(string nombre, List<TanqueEnemigo> derrotados)
        {
            NombreTanque = nombre;
            TanquesDerrotados = derrotados;
            Fecha = DateTime.Now;

            PuntosTotales = CalcularPuntuacion();
        }
        
        private int CalcularPuntuacion()
        {
            int cantidadT72 = TanquesDerrotados.Count(t => t.Modelo == "T-72");
            int cantidadT80 = TanquesDerrotados.Count(t => t.Modelo == "T-80");
            int cantidadT90 = TanquesDerrotados.Count(t => t.Modelo == "T-90");
            int cantidadT14 = TanquesDerrotados.Count(t => t.Modelo == "T-14 Armata");

            int puntosT72 = cantidadT72 * 25;
            int puntosT80 = cantidadT80 * 50;
            int puntosT90 = cantidadT90 * 100;
            int puntosT14 = cantidadT14 * 200;

            return puntosT72 + puntosT80 + puntosT90 + puntosT14;
        }
    }
}