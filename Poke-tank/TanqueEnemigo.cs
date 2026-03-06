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

        public string elegirAccion(Tanque objetivo, bool hayHumo, AccionUsuario accionAnterior)
        {
            Random dado = new Random();
            int disparo = dado.Next(0, 100);

            int probabilidadFallo = hayHumo ? 75 : 25;

            if (this.Modelo == "T-14 Armata" && hayHumo)
            {
                probabilidadFallo = 50; // El jefe tiene sensores térmicos y falla menos
            }

            //pesos base
            int pesoAtacar = 60;
            int pesoDefender = 10;
            int pesoReparar = 10;
            int pesoFrenarOrugas = 20;

            //ajustes según el estado del tanque
            //Supervivencia: Si la vida es baja, aumenta la probabilidad de defender o reparar
            if (this.Vida < this.VidaMaxima * 0.5)
            {
                pesoReparar += 40;
                pesoAtacar -= 10;
                pesoFrenarOrugas -= 10;
            }

            //Castigo por reparar:
            if (accionAnterior == AccionUsuario.Reparar)
            {
                pesoReparar -= 20; 
                pesoAtacar += 20;
                pesoFrenarOrugas += 10;
                pesoDefender -= 20;
            }

            //Ajuste por humo:
            if (hayHumo)
            {
                pesoAtacar -= 20; 
                pesoDefender += 10; 
                pesoReparar += 40; 
                pesoFrenarOrugas += 10;
            }

            //si la velocidad es menor al enemigo, aumenta la probabilidad de usar el ataque de orugas para reducir aún más su velocidad
            if (this.Velocidad < objetivo.Velocidad)
            {
                pesoFrenarOrugas += 20;
                pesoAtacar -= 10;
                pesoDefender -= 10;
                pesoReparar -= 10;
            }

            //si el usuario ataco y es mas rapido , aumenta la probabilidad de defender para anticipar el siguiente ataque
            if (accionAnterior == AccionUsuario.Disparar && this.Velocidad > objetivo.Velocidad)
            {
                pesoDefender += 20;
                pesoAtacar -= 10;
                pesoReparar -= 10;
                pesoFrenarOrugas -= 10;
            }

            //el jugador intenta defender , el enemigo aprovecha para reparar o usar el ataque de orugas
            if (accionAnterior == AccionUsuario.Defender)
            {
                pesoReparar += 20;
                pesoFrenarOrugas += 20;
                pesoAtacar -= 10;
                pesoDefender -= 30;
            }

            //seleccion basada en pesos
            int totalPeso = pesoAtacar + pesoDefender + pesoReparar + pesoFrenarOrugas;
            int accion = dado.Next(0, totalPeso);

            if (accion < pesoAtacar)
            {
                return Atacar(objetivo, disparo, probabilidadFallo);
            }
            else if (accion < pesoAtacar + pesoFrenarOrugas)
            {
                return DisparoOrugas(objetivo, disparo, probabilidadFallo); 
            }
            else if (accion < pesoAtacar + pesoFrenarOrugas + pesoDefender)
            {
                return Defender();
            }
            else
            {
                return Reparar();
            }

        }
        private string Atacar(Tanque objetivo, int dado, int probabilidadFallo)
        {
            if (dado >= probabilidadFallo)
            {
                int dano = objetivo.RecibirAtaque(this.Ataque);
                return $"{this.Nombre} ataca a {objetivo.Nombre} causando {dano} de daño.";
            }
            else
            {
                return $"{this.Nombre} falla el ataque a {objetivo.Nombre}.";
            }
        }
        private string Defender()
        {
            this.BloquearSiguienteAtaque();
            return $"{this.Nombre} se prepara para defender el siguiente ataque.";
        }
        private string Reparar()
        {
            int cantidadReparacion = 20; 
            base.Reparar(cantidadReparacion);
            return $"{this.Nombre} repara el tanque restaurando {cantidadReparacion} de vida.";
        }
        private string DisparoOrugas(Tanque objetivo, int dado, int probabilidadFallo)
        {
            if (dado >= probabilidadFallo)
            {
                int dano = objetivo.DisparoEnLasOrugas(this.Ataque); 
                return $"{this.Nombre} realiza un ataque de orugas a {objetivo.Nombre} causando {dano} de daño y reduciendo su velocidad.";
            }
            else
            {
                return $"{this.Nombre} falla el ataque de orugas a {objetivo.Nombre}.";
            }
        }
    }
}