using System;

namespace Poke_tank
{
    //representa un tanque en el juego, con propiedades y comportamientos del tanque
    public class Tanque
    {
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public int Vida { get; set; }
        public int VidaMaxima { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadBase { get; set; }

        private bool estaDefendiendo = false;

        public Tanque(string nombre, string modelo, int vida, int ataque, int defensa, int velocidad)
        {
            Nombre = nombre;
            Modelo = modelo;
            Vida = vida;
            VidaMaxima = vida; 
            Ataque = ataque;
            Defensa = defensa;
            Velocidad = velocidad;
            VelocidadBase = velocidad;
        }

        //recibe un ataque entrante, calcula el daño recibido teniendo en cuenta la defensa y si el tanque está defendiendo. Luego actualiza la vida del tanque
        public int RecibirAtaque(int ataqueEntrante)
        {
            int defensaTotal = Defensa;

            if (estaDefendiendo)
            {
                defensaTotal = Defensa * 2;
                estaDefendiendo = false; 
            }

            int dano = ataqueEntrante - defensaTotal;

            if (dano < 0) dano = 0;

            Vida -= dano;
            if (Vida < 0) Vida = 0;

            return dano;
        }

        //activa el modo defensa para el siguiente ataque recibido, lo que duplica la defensa del tanque para ese ataque
        public void BloquearSiguienteAtaque()
        {
            estaDefendiendo = true;
        }

        //repara el tanque restaurando una cantidad de vida, sin exceder la vida máxima
        public void Reparar(int cantidad)
        {
            Vida += cantidad;
            if (Vida > VidaMaxima) Vida = VidaMaxima;
        }

        //verifica si el tanque sigue vivo, es decir, si su vida es mayor a 0
        public bool EstaVivo()
        {
            return Vida > 0;
        }

        //Reducir la velocidad del tanque por una batalla
        public int DisparoEnLasOrugas(int ataqueEntrante)
        {
            ataqueEntrante /= 2; // El ataque en las orugas es menos efectivo que un ataque directo
            Velocidad -= 10;
            if (Velocidad < 0) Velocidad = 0;
            
            return RecibirAtaque(ataqueEntrante);
        }

        //Funcion para restaurar los stats despues de una batalla
        public void RestaurarStats()
        {
            Vida = VidaMaxima;
            Velocidad = VelocidadBase;
            estaDefendiendo = false;
        }
    }
}