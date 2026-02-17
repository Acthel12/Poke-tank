using System;
using System.Collections.Generic;
using System.IO;          
using System.Text.Json;   

namespace Poke_tank;

public static class DatosGlobales
{
    private static string carpetaBase = AppDomain.CurrentDomain.BaseDirectory;
    private static string carpetaDatos = Path.Combine(carpetaBase, "Partidas");
    private static string archivoPartidas = Path.Combine(carpetaDatos, "partidas.json");
    private static string archivoPuntuaciones = Path.Combine(carpetaDatos, "puntuaciones.json");
    
    public static List<Partida> ListaPartidas = new List<Partida>();
    public static List<Puntuacion> ListaPuntuaciones = new List<Puntuacion>();
    public static int NivelSeleccionado { get; set; } = 0; //controlar el nivel seleccionado

    public static int PartidaActualIndex { get; set; } = -1;

    //guardar los datos en un archivo json, si no existe la carpeta, se crea, y si no existe el archivo, se crea con una lista vacia
    public static void GuardarDatos()
    {
        try
        {
            if (!Directory.Exists(carpetaDatos)) { 
                Directory.CreateDirectory(carpetaDatos);
            }
            var opciones = new JsonSerializerOptions { WriteIndented = true };

            string jsonMiembros = JsonSerializer.Serialize(ListaPartidas, opciones);
            File.WriteAllText(archivoPartidas, jsonMiembros);

            string jsonPuntos = JsonSerializer.Serialize(ListaPuntuaciones, opciones);
            File.WriteAllText(archivoPuntuaciones, jsonPuntos);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error crítico al guardar: " + ex.Message);
        }
    }

    //cargar los datos desde un archivo json
    public static void CargarDatos()
    {
        try
        {
            //cargar partidas
            if (File.Exists(archivoPartidas))
            {
                string json = File.ReadAllText(archivoPartidas);
                ListaPartidas = JsonSerializer.Deserialize<List<Partida>>(json) ?? new List<Partida>();
            }
            else
            {
                ListaPartidas = new List<Partida>();
            }

            //cargar puntuaciones
            if (File.Exists(archivoPuntuaciones))
            {
                string jsonP = File.ReadAllText(archivoPuntuaciones);
                ListaPuntuaciones = JsonSerializer.Deserialize<List<Puntuacion>>(jsonP) ?? new List<Puntuacion>();
            }
            else
            {
                ListaPuntuaciones = new List<Puntuacion>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error al cargar: " + ex.Message);
            ListaPartidas = new List<Partida>();
            ListaPuntuaciones = new List<Puntuacion>();
        }
    }
}