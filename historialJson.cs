using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using personasjesDelJuego;

namespace persistenciaJson
{
    public class Historial
    {
        // Estructura para almacenar información relevante del ganador
        public class RegistroGanador
        {
            public Personaje Ganador { get; set; }
            public DateTime Fecha { get; set; }
            public string InformacionAdicional { get; set; }
        }

        // Guardar información del ganador en formato JSON
        public static void GuardarGanador(RegistroGanador ganador, string nombreArchivo)
{
    List<RegistroGanador> ganadores = LeerGanadores(nombreArchivo) ?? new List<RegistroGanador>();

    // Verificar si el personaje ya está en la lista de ganadores
    var personajeExistente = ganadores.Find(g => g.Ganador.GetNombre() == ganador.Ganador.GetNombre());

    if (personajeExistente != null)
    {
        // Si el personaje ya existe, actualizar su información
        personajeExistente.InformacionAdicional = ganador.InformacionAdicional;
    }
    else
    {
        // Si el personaje no existe, agregarlo
        ganadores.Add(ganador);
    }

    var opciones = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(ganadores, opciones);
    File.WriteAllText(nombreArchivo, jsonString);
    Console.WriteLine($"Ganador guardado en {nombreArchivo}");
}

        // Leer la lista de ganadores desde un archivo JSON
        public static List<RegistroGanador> LeerGanadores(string nombreArchivo)
        {
            if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
            {
                return new List<RegistroGanador>();
            }

            try
            {
                string jsonString = File.ReadAllText(nombreArchivo);
                return JsonSerializer.Deserialize<List<RegistroGanador>>(jsonString);
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error al deserializar el archivo {nombreArchivo}: {e.Message}");
                return new List<RegistroGanador>();
            }
        }
        public static void MostrarPodio(string nombreArchivo)
{
    var ganadores = LeerGanadores(nombreArchivo);
    var conteoVictorias = new Dictionary<string, int>();

    // Contar victorias por personaje
    foreach (var registro in ganadores)
    {
        string nombre = registro.Ganador.GetNombre();
        if (conteoVictorias.ContainsKey(nombre))
        {
            conteoVictorias[nombre]++;
        }
        else
        {
            conteoVictorias[nombre] = 1;
        }
    }

    // Mostrar el podio
    Console.WriteLine("Podio de Ganadores:");
    foreach (var entry in conteoVictorias.OrderByDescending(entry => entry.Value))
    {
        Console.WriteLine($"{entry.Key} - {entry.Value} victorias");
    }
}

        // Verificar la existencia y validez de un archivo JSON
        public static bool Existe(string nombreArchivo)
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
    }
}