using System;

namespace Poke_tank
{
    public class TanqueEnemigo : Tanque
    {
        private Random random = new Random();
        public TanqueEnemigo(string nombre, int vida, int ataque, int defensa, int velocidad) : base(nombre, vida, ataque, defensa, velocidad)
        {
        }

        public void elegirAccion(Tanque tanque_usuario)
        {
            if (Vida < 30)
            {
                Reparar(20);
                MessageBox.Show($"{Nombre} se ha reparado a sí mismo.");
            }
            else
            {
                if (random.Next(0, 2) == 0)
                {
                    BloquearSiguienteAtaque();
                    MessageBox.Show($"{Nombre} se ha preparado para bloquear el siguiente ataque.");
                }
                else
                {
                    tanque_usuario.RecibirAtaque(Ataque);
                    MessageBox.Show($"{Nombre} ha atacado a {tanque_usuario.Nombre} causando {Ataque} de daño.");

                }
            }
        }
    }
}
