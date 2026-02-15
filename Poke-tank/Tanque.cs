
namespace Poke_tank
{
    public class Tanque
    {
        public string Nombre { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Velocidad { get; set; }
        public Tanque(string nombre, int vida, int ataque, int defensa, int velocidad)
        {
            Nombre = nombre;
            Vida = vida;
            Ataque = ataque;
            Defensa = defensa;
            Velocidad = velocidad;
        }
        public void RecibirAtaque(int dano)
        {
            int danoRecibido = dano - Defensa;
            if (danoRecibido < 0) danoRecibido = 0;
            Vida -= danoRecibido;
            if (Vida < 0) Vida = 0;
        }
        public void reparar(int cantidad)
        {
            Vida += cantidad;
        }
        public bool EstaVivo()
        {
            return Vida > 0;
        }
    }
}
