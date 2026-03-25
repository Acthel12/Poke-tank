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

        public int FrameActual { get; set; } = 0;
        public float TiempoAnimacion { get; set; } 

        public Misil(float startingY)
        {
            Ancho = 150;
            Alto = 100;


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

            TiempoAnimacion += deltatime;
            TiempoVuelo += deltatime;

            const float gravedad = 200.0f; //ajustar para controlar la caída


            if (Fase == 1)
            {
                if (TiempoAnimacion >= 0.15f) // Cambia el fuego del propulsor cada 0.15 segundos
                {
                    FrameActual = (FrameActual == 0) ? 1 : 0;
                    TiempoAnimacion = 0;
                }
                
                X += VelocidadHorizontal *  DireccionFase1 * deltatime;

                Y += gravedad * TiempoVuelo * deltatime;


                if (X >= anchoPantalla - Ancho)
                {
                    Fase = 2;
                    FrameActual = 2;

                    Alto = 15;
                    Ancho = 30;

                    int minX = (int)(anchoPantalla * 0.1f);
                    int maxX = (int)(anchoPantalla * 0.9f);

                    X = rnd.Next(minX, maxX);
                    Y = altoPantalla / 3; 
                }
            }
            else if (Fase == 2)
            {
                
                float crecimiento = VelocidadAcercamiento * deltatime;




                Ancho += crecimiento * 2.0f; // para que mantenga la proporcion
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
