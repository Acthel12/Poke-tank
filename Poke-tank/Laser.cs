using System;
using System.Collections.Generic;
using System.Text;

namespace Poke_tank
{
    public class Laser
    {
        public float InicioX { get; set; }
        public float InicioY { get; set; }
        public float FinX { get; set; }
        public float FinY { get; set; }

        public float TiempoMaximo { get; private set; }
        public float TiempoRestante { get; private set; }

        public Laser(float inicioX, float inicioY, float finX, float finY, float duracion = 0.15f)
        {
            this.InicioX = inicioX;
            this.InicioY = inicioY;
            this.FinX = finX;
            this.FinY = finY;
            this.TiempoMaximo = duracion;
            this.TiempoRestante = duracion;
        }

        public void Actualizar(float dt)
        {
            TiempoRestante -= dt;
        }
    }
}
