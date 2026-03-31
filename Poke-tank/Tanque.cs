using System;

namespace Poke_tank
{
	public class Tanque
	{
		public int vida { get; set; }
		public int ataque { get; set; }


		public Tanque(int vida, int ataque)
		{
			this.vida = vida;
			this.ataque = ataque;
		}
	}
}
