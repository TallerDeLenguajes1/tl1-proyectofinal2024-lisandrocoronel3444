using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using personasjesDelJuego;



namespace persistenciaJson
{
    public class PersonajesJson
    {
        // Guarda la lista de personajes en un archivo JSON
        public static void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(personajes, opciones);
            File.WriteAllText(nombreArchivo, jsonString);
        }

        // Lee la lista de personajes desde un archivo JSON
        public static List<Personaje> LeerPersonajes(string nombreArchivo)
        {
            if (!File.Exists(nombreArchivo))
            {
                Console.WriteLine($"El archivo {nombreArchivo} no existe.");
                return new List<Personaje>();
            }

            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString) ?? new List<Personaje>();
        }

        // Verifica si un archivo existe y tiene contenido
        public static bool Existe(string nombreArchivo)
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
        public static void EliminarPersonaje(Personaje personajeAEliminar, string nombreArchivo)
        {
            List<Personaje> personajes = LeerPersonajes(nombreArchivo);
            personajes.RemoveAll(p => p.GetNombre() == personajeAEliminar.GetNombre());
            GuardarPersonajes(personajes, nombreArchivo);
        }
    }
}