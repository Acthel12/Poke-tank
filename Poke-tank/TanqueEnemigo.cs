using System;

namespace Poke_tank
{
    public class TanqueEnemigo : Tanque
    {
        //comportamiento del tanque enemigo
        public TanqueEnemigo(string nombre, string modelo, int vida, int ataque, int defensa, int velocidad)
            : base(nombre, modelo, vida, ataque, defensa, velocidad)
        {
        }

        public string elegirAccion(Tanque objetivo, bool hayHumo)
        {
            Random dado = new Random();
            int accion = dado.Next(0, 100);
            int disparo = dado.Next(0, 100);

            int probabilidadFallo = hayHumo ? 75 : 25; 

            
            if (accion < 40)
            {
                if (disparo < probabilidadFallo)
                {
                    if (hayHumo)
                        return "El enemigo dispara pero falla debido al humo.";
                    else
                        return "El enemigo dispara pero falla.";
                }
                else {
                    int dano = objetivo.RecibirAtaque(this.Ataque);
                    return $"El enemigo dispara y te causa {dano} de daño.";
                }
            }
            else if (accion < 65)
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