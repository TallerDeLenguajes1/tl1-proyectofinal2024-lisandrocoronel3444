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
            // Configuración de opciones para el formato del JSON
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            
            // Serializa la lista de personajes a una cadena JSON
            string jsonString = JsonSerializer.Serialize(personajes, opciones);
            
            // Guarda la cadena JSON en el archivo especificado
            File.WriteAllText(nombreArchivo, jsonString);
            
        }

        // Lee la lista de personajes desde un archivo JSON
        public static List<Personaje> LeerPersonajes(string nombreArchivo)
        {
            // Verifica si el archivo existe
            if (!File.Exists(nombreArchivo))
            {
                Console.WriteLine($"El archivo {nombreArchivo} no existe.");
                return new List<Personaje>(); // Devuelve una lista vacía si el archivo no existe
            }

            // Lee el contenido del archivo JSON
            string jsonString = File.ReadAllText(nombreArchivo);

            // Deserializa la cadena JSON a una lista de personajes
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        // Verifica si un archivo existe y tiene contenido
        public static bool Existe(string nombreArchivo)
        {
            // Devuelve verdadero si el archivo existe y tiene un tamaño mayor a cero
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
    }
}