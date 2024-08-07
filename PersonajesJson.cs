using System.Text.Json;
using personasjesDelJuego;

namespace persistenciaJson;

public class PersonajesJson
    {
        public static void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(personajes, opciones);
            File.WriteAllText(nombreArchivo, jsonString);
            Console.WriteLine($"Personajes guardados en {nombreArchivo}");
        }
        public static List<Personaje> LeerPersonajes(string nombreArchivo)
        {
            if (!File.Exists(nombreArchivo))
            {
                Console.WriteLine($"El archivo {nombreArchivo} no existe.");
                return new List<Personaje>();
            }

            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString);
        }
        public static bool Existe(string nombreArchivo)
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
    }

    
