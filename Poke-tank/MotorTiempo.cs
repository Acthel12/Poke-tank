using System.Diagnostics;

public static class MotorTiempo
{
	private static Stopwatch cronometro = new Stopwatch();

	public static float DeltaTime { get; private set; }

	public static void Iniciar()
	{
		cronometro.Start();
    }
	public static void Actualizar()
	{
		DeltaTime = (float)cronometro.Elapsed.TotalSeconds;

		cronometro.Restart();
    }
}
