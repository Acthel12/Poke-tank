public class Explosion
{
    public float X { get; set; }
    public float Y { get; set; }
    public bool Terminada { get; private set; } = false;

    private float tiempoTranscurrido = 0;
    private float duracion; // Tiempo total que la explosión estará visible (en segundos)

    public Explosion(float x, float y, float duracion = 0.3f) // Valor por defecto 0.3 segundos
    {
        this.X = x;
        this.Y = y;
        this.duracion = duracion;
    }

    public void Actualizar(float dt)
    {
        tiempoTranscurrido += dt;
        if (tiempoTranscurrido >= duracion)
        {
            Terminada = true;
        }
    }
}