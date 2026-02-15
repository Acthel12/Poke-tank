
namespace Poke_tank
{
    public class Partida
    {
        public Tanque tanque_usuario { get; set; }
        public int enemigos_derrotados { get; set; }
        public List<TanqueEnemigo> enemigos { get; set; }

        public Partida(Tanque tanque_usuario)
        {
            this.tanque_usuario = tanque_usuario;
            enemigos_derrotados = 0;
            enemigos = new List<TanqueEnemigo>();
        }
    }
}
