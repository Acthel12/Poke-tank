using System;

namespace Poke_tank
{
    //puntuación obtenida al finalizar la aventura, basada en el número de enemigos derrotados y se muestra al usuario
    public class Puntuacion
    {
        public string NombreTanque { get; set; } = string.Empty;
        public int TanquesDerrotados { get; set; }
        public DateTime Fecha { get; set; }

        public int PuntosTotales => TanquesDerrotados * 100; 

        public Puntuacion() { }

        public Puntuacion(string nombre, int derrotados)
        {
            NombreTanque = nombre;
            TanquesDerrotados = derrotados;
            Fecha = DateTime.Now;
        }
    }
}