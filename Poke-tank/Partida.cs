using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Poke_tank
{
    public class Partida
    {
        private Random random = new Random();

        public Tanque tanqueUsuario { get; set; }
        public List<TanqueEnemigo> enemigosDerrotados { get; set; } 
        public List<TanqueEnemigo> enemigos { get; set; }

        //nombres para escoger al azar para los tanques enemigos según su modelo

        private static readonly string[] nombresT80 = {
            "Doomhammer", "Hellfire Engine", "Oblivion", "Widowmaker",
            "Chaos Bringer", "Soul Reaper", "Cataclysm", "Iron Guardian",
            "Titan's Wall", "Aegis Prime", "Immortal Shell", "Void Walker",
            "Last Bastion", "Phantom Wrath", "Silent Death", "Storm Chaser",
            "Shadow Tread", "Vanguard Alpha"
        };

        private static readonly string[] nombresT72 = {
            "Iron Workhorse", "Steel Grunt", "Rust Walker", "Grim Vanguard",
            "Bone Crusher", "Old Guard", "Endless March", "Soviet Anvil",
            "Rough Rider", "Scrap Titan"
        };

        private static readonly string[] nombresT90 = {
            "Crimson Gaze", "Shtora's Curse", "Red Reaper", "Vladimir's Wrath",
            "Hell's Iris", "Apex Predator", "Night Hunter", "Demon Core",
            "Tech Terror", "Blood Omen"
        };

        private static readonly string[] nombresT14 = {
            "Armata Prime", "Ghost Turret", "Digital Demon", "Cyber Dreadnought",
            "System Overlord", "Titan Protocol", "Neon Vanguard", "Void Specter",
            "Future Shock", "The Tsar", "Apex Machine"
        };

        public Partida(Tanque tanqueUsuario)
        {
            this.tanqueUsuario = tanqueUsuario;

            this.enemigosDerrotados = new List<TanqueEnemigo>();

            this.enemigos = new List<TanqueEnemigo>();

            GenerarEnemigos();
        }

        //crea una nueva partida
        public static void iniciarPartida(string nombreJugador, string modelo, int vida, int ataque, int defensa, int velocidad)
        {
            //crea tanque del jugador
            Tanque jugador = new Tanque(nombreJugador, modelo, vida, ataque, defensa, velocidad);

            //crea partida y agregar a datos globales
            Partida nueva = new Partida(jugador);
            DatosGlobales.ListaPartidas.Add(nueva);
            DatosGlobales.PartidaActualIndex = DatosGlobales.ListaPartidas.Count - 1;
        }

        //genera los enemigos para la partida según el nivel seleccionado
        private void GenerarEnemigos()
        {

            AgregarEnemigo(nombresT72, "T-72", 80, 15, 5, 10);
            AgregarEnemigo(nombresT80, "T-80", 100, 20, 10, 15);
            if (random.Next(1, 101) <= 25)
            {
                AgregarEnemigo(nombresT14, "T-14 Armata", 150, 35, 15, 25);
            }
            else
            {
                AgregarEnemigo(nombresT90, "T-90", 120, 25, 8, 12);
            }
        }

        //método para agregar un enemigo a la lista, escogiendo un nombre al azar de la lista correspondiente al modelo
        private void AgregarEnemigo(string[] listaNombres, string modelo, int vida, int ataque, int defensa, int exp)
        {
            string nombreAlAzar = listaNombres[random.Next(listaNombres.Length)];
            enemigos.Add(new TanqueEnemigo(nombreAlAzar, modelo, vida, ataque, defensa, exp));
        }

        //se registra la derrota de un enemigo en la lista, agregándolo a la lista de enemigos derrotados y restaurando la vida del usuario
        public void DerrotarEnemigo(TanqueEnemigo enemigo, Tanque usuario)
        {
            enemigosDerrotados.Add(enemigo);
            usuario.Vida = usuario.VidaMaxima;
            enemigo.Vida = enemigo.VidaMaxima;
        }
    }
}