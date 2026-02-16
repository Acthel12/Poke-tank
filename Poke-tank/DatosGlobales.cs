using System;
using System.Collections.Generic;
using System.IO;          
using System.Text.Json;   

namespace Poke_tank;

public static class DatosGlobales
{

    private static string carpetaBase = AppDomain.CurrentDomain.BaseDirectory;
    private static string carpetaDatos = Path.Combine(carpetaBase,"Partidas");
    private static string archivoPartidas = Path.Combine(carpetaDatos, "partidas.json");
    
    public static List<Partida> ListaPartidas = new List<Partida>();

    public static int PartadaActualIndex { get; set; } = -1;

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


        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error crítico al guardar: " + ex.Message);
        }
    }
    public static void CargarDatos()
    {
        try
        {
            if (File.Exists(archivoPartidas))
            {
                string json = File.ReadAllText(archivoPartidas);
                ListaPartidas = JsonSerializer.Deserialize<List<Partida>>(json) ?? new List<Partida>();
            }
            else
            {
                ListaPartidas = new List<Partida>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error al cargar: " + ex.Message);
            ListaPartidas = new List<Partida>();
        }
    }

}