using System;

namespace Poke_tank
{
    public class TanqueEnemigo : Tanque
    {
        public TanqueEnemigo(string nombre, string modelo, int vida, int ataque, int defensa, int velocidad)
            : base(nombre, modelo, vida, ataque, defensa, velocidad)
        {
        }

        public string elegirAccion(Tanque objetivo)
        {
            Random dado = new Random();
            int chance = dado.Next(0, 100);

            if (chance < 25)
            {
                return "El enemigo falla su ataque.";
            }
            if (chance < 70)
            {
                int dano = objetivo.RecibirAtaque(this.Ataque);
                return $"El enemigo dispara y te causa {dano} de daño.";
            }
            else if (chance < 90)
            {
                this.BloquearSiguienteAtaque();
                return "El enemigo activa su blindaje reactivo.";
            }
            else
            {
                this.Reparar(20);
                return "El enemigo realiza reparaciones de emergencia (+20 HP).";
            }
        }
    }
}