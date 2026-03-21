using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Poke_tank
{
    public class Misil
    {
        public float X { get;  set; }
        public float Y { get; set; }
        public float Ancho { get; set; }
        public float Alto { get; set; }

        public float VelocidadHorizontal { get; set; }
        public float VelocidadAcercamiento { get; set; }

        public int Fase { get; set; }
        public int DireccionFase1 { get; set; } // 1 para derecha, -1 para izquierda

        public float TiempoVuelo { get; set; }

        private static Random rnd = new Random();


        public Misil(float startingY)
        {
            Ancho = 50;
            Alto = 50;


            X = 0;
            Y= startingY;

            TiempoVuelo = 0;

            VelocidadHorizontal = 800;
            VelocidadAcercamiento = 400;

            Fase = 1;
            DireccionFase1 = 1;

        }

        public void Actualizar(float deltatime,float anchoPantalla, float altoPantalla)
        {
            const float gravedad = 200.0f; //ajustar para controlar la caída
            if (Fase == 1)
            {
                TiempoVuelo += deltatime;



                X += VelocidadHorizontal *  DireccionFase1 * deltatime;

                Y += gravedad * TiempoVuelo * deltatime;


                if (X >= anchoPantalla - Ancho)
                {
                    Fase = 2;

                    Alto = 15;
                    Ancho = 15;

                    int minX = (int)(anchoPantalla * 0.1f);
                    int maxX = (int)(anchoPantalla * 0.9f);

                    X = rnd.Next(minX, maxX);
                    Y = altoPantalla / 3; 
                }
            }
            else if (Fase == 2)
            {
                TiempoVuelo += deltatime;

                float crecimiento = VelocidadAcercamiento * deltatime;

                Ancho += crecimiento;
                Alto += crecimiento;

                X -= crecimiento / 2;
                Y -= crecimiento / 2;

                Y += 10 * deltatime;


                float intensidadSacudida = Ancho * 0.8f;

                float sacudidaX = (float)Math.Sin(TiempoVuelo * 45) * intensidadSacudida * deltatime;
                float sacudidaY = (float)Math.Cos(TiempoVuelo * 55) * intensidadSacudida * deltatime;

                X += sacudidaX;
                Y += sacudidaY;

                if (Ancho >= 300)
                    {
                        Fase = 3;
                }
            }
        }
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, (int)Ancho, (int)Alto);
    }
}
