using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Poke_tank
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Menu_Principal());

            //para probar 
            Partida.iniciarPartida("Waza", "M1A1", 140, 30, 10, 14, NivelDificultad.Facil);
            Application.Run(new Tienda());
            Application.Run(new Buscaminas());
            //
        }
    }
}