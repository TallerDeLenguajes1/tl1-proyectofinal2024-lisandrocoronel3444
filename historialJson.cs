using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            public required Personaje Ganador { get; set; }
            public DateTime Fecha { get; set; }
            public required string InformacionAdicional { get; set; }
        }

        // Guardar información del ganador en formato JSON
        public static void GuardarGanador(RegistroGanador ganador, string nombreArchivo)
        {
            List<RegistroGanador> ganadores = LeerGanadores(nombreArchivo) ?? new List<RegistroGanador>();

            // Verificar si el personaje ya está en la lista de ganadores
            var personajeExistente = ganadores
                .FirstOrDefault(g => g.Ganador.GetNombre() == ganador.Ganador.GetNombre());

            if (personajeExistente != null)
            {
                // Incrementar el conteo de victorias si el personaje ya existe
                string infoAdicional = personajeExistente.InformacionAdicional;
                int victorias;

                // Extraer el conteo de victorias de la cadena
                if (infoAdicional.StartsWith("Victorias: "))
                {
                    string victoriasStr = infoAdicional.Substring("Victorias: ".Length);
                    if (int.TryParse(victoriasStr, out victorias))
                    {
                        victorias++; // Incrementar el conteo de victorias
                    }
                    else
                    {
                        victorias = 1; // Valor predeterminado en caso de error
                    }
                }
                else
                {
                    victorias = 1; // Valor predeterminado si no está en el formato esperado
                }

                personajeExistente.InformacionAdicional = $"Victorias: {victorias}";
            }
            else
            {
                // Si el personaje no existe, agregarlo con la primera victoria
                ganador.InformacionAdicional = "Victorias: 1";
                ganadores.Add(ganador);
            }

            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(ganadores, opciones);
            File.WriteAllText(nombreArchivo, jsonString);

            
            
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

                // Depuración

                return JsonSerializer.Deserialize<List<RegistroGanador>>(jsonString);
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error al deserializar el archivo {nombreArchivo}: {e.Message}");
                return new List<RegistroGanador>();
            }
        }

        // Mostrar el podio de ganadores
        public static void MostrarPodio(string nombreArchivo)
        {
            var ganadores = LeerGanadores(nombreArchivo);
            var conteoVictorias = new Dictionary<string, int>();

            // Contar victorias por personaje
            foreach (var registro in ganadores)
            {
                string nombre = registro.Ganador.GetNombre();
                int victorias = int.Parse(registro.InformacionAdicional.Split(':')[1].Trim());

                if (conteoVictorias.ContainsKey(nombre))
                {
                    conteoVictorias[nombre] += victorias;
                }
                else
                {
                    conteoVictorias[nombre] = victorias;
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