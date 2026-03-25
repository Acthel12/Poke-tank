using System;
using System.Drawing;

namespace Poke_tank
{
    public class Dron
    {
        public float X { get;  set; }
        public float Y { get; set; }
        public float Ancho { get; set; }
        public float Alto { get; set; }

        public float VelocidadHorizontal { get; set; }
        public float VelocidadAcercamiento { get; set; }

        public int Fase { get; set; }
        public int DireccionFase1 { get; set; } // 1 para derecha, -1 para izquierda

        public float BaseY { get; set; }
        public float TiempoVuelo { get; set; }

        public float TiempoAnimacion { get; set; }
        public int FrameActual { get; set; } = 0;

        private static Random rnd = new Random();

        private float amplitudAleatoria;
        private float frecuenciaAleatoria;
        private float desfaseAleatorio;

        public Dron(float startingY)
        {
            Ancho = 150;
            Alto = 100;


            X = 0;
            Y= startingY;

            BaseY = startingY;
            TiempoVuelo = 0;

            VelocidadHorizontal = 400;
            VelocidadAcercamiento = 100;

            Fase = 1;
            DireccionFase1 = 1;

            amplitudAleatoria = rnd.Next(30, 90);
            frecuenciaAleatoria = rnd.Next(2, 8);
            desfaseAleatorio = (float)rnd.NextDouble() * 10;
        }

        public void Actualizar(float deltatime,float anchoPantalla, float altoPantalla)
        {
            TiempoAnimacion += deltatime;
            TiempoVuelo += deltatime;

            if (Fase == 1)
            {
                if (TiempoAnimacion >= 0.15f)
                {
                    FrameActual = FrameActual == 0 ? 1 : 0;
                    TiempoAnimacion = 0;
                }

                X += VelocidadHorizontal *  DireccionFase1 * deltatime;

                float ondaPrincipal = (float)(Math.Sin((TiempoVuelo + desfaseAleatorio) * frecuenciaAleatoria) * amplitudAleatoria);
                float ondaTemblor = (float)(Math.Cos(TiempoVuelo * 15) * 10);

                Y = BaseY + ondaPrincipal + ondaTemblor;

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
                if (TiempoAnimacion >= 0.15f)
                {
                    FrameActual = FrameActual == 2 ? 3 : 2;
                    TiempoAnimacion = 0;
                }

                float crecimiento = VelocidadAcercamiento * deltatime;

                Ancho += crecimiento * 2;
                Alto += crecimiento;

                X -= crecimiento / 2;
                Y -= crecimiento / 2;

                Y += 10 * deltatime;

                float fuerzaSway = Ancho * 4.0f; // entre mas cerca mas fuerza
                float velocidadCurva = 4.0f;

                float esquiveX = (float)Math.Sin(TiempoVuelo * velocidadCurva) * fuerzaSway * deltatime;
                X += esquiveX;

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
