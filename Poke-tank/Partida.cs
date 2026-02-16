using System;
using System.Collections.Generic;

namespace Poke_tank
{
    public class Partida
    {
        private Random random = new Random();

        public Tanque tanqueUsuario { get; set; }
        public List<TanqueEnemigo> enemigosDerrotados { get; set; } 
        public List<TanqueEnemigo> enemigos { get; set; } 

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

        private void GenerarEnemigos()
        {
            AgregarEnemigo(nombresT80, "T-80", 100, 20, 10, 15);
            AgregarEnemigo(nombresT72, "T-72", 80, 15, 5, 10);


            if (random.Next(1, 101) <= 25)
            {
                AgregarEnemigo(nombresT14, "T-14 Armata", 150, 35, 20, 25);
            }
            else
            {
                AgregarEnemigo(nombresT90, "T-90", 90, 18, 8, 12);
            }
        }

        private void AgregarEnemigo(string[] listaNombres, string modelo, int vida, int ataque, int defensa, int exp)
        {
            string nombreAlAzar = listaNombres[random.Next(listaNombres.Length)];
            enemigos.Add(new TanqueEnemigo(nombreAlAzar, modelo, vida, ataque, defensa, exp));
        }
    }
}