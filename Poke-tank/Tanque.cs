using System;

namespace Poke_tank
{
    public class Tanque
    {
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public int Vida { get; set; }
        public int VidaMaxima { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Velocidad { get; set; }

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
        }

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

        public void BloquearSiguienteAtaque()
        {
            estaDefendiendo = true;
        }

        public void Reparar(int cantidad)
        {
            Vida += cantidad;
            if (Vida > VidaMaxima) Vida = VidaMaxima;
        }

        public bool EstaVivo()
        {
            return Vida > 0;
        }
    }
}