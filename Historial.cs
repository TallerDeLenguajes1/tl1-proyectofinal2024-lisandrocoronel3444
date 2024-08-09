using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using personasjesDelJuego;

namespace persistenciaJson
{
    public class Historial
    {
        // Estructura para almacenar información relevante del ganador
        public class RegistroGanador
        {
            public required Personaje Ganador { get; set; } // Objeto Personaje que ha ganado
            public DateTime Fecha { get; set; } // Fecha del registro
            public required string InformacionAdicional { get; set; } // Información adicional del registro
        }

        // Agrega el registro del ganador al historial en el archivo JSON
        public static void AgregarGanador(string nombreArchivo, Personaje ganador)
        {
            var registroGanador = new RegistroGanador
            {
                Ganador = ganador,
                Fecha = DateTime.Now,
                InformacionAdicional = $"Victorias: 1" // Se asume que es la primera victoria
            };

            List<RegistroGanador> ganadores = LeerGanadores(nombreArchivo); // Lee la lista actual de ganadores
            var ganadorExistente = ganadores.FirstOrDefault(g => g.Ganador.GetNombre() == ganador.GetNombre());

            if (ganadorExistente != null)
            {
                // Si el ganador ya existe, actualiza la información adicional
                ganadorExistente.InformacionAdicional = $"Victorias: {int.Parse(ganadorExistente.InformacionAdicional.Split(':')[1].Trim()) + 1}";
            }
            else
            {
                // Si el ganador no existe, agrega el nuevo registro
                ganadores.Add(registroGanador);
            }

            GuardarGanadores(ganadores, nombreArchivo); // Guarda la lista actualizada en el archivo
        }

        // Guarda la lista de ganadores en un archivo JSON
        private static void GuardarGanadores(List<RegistroGanador> ganadores, string nombreArchivo)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true }; // Configura las opciones de serialización JSON
            string jsonString = JsonSerializer.Serialize(ganadores, opciones); // Serializa la lista a una cadena JSON
            File.WriteAllText(nombreArchivo, jsonString); // Guarda la cadena JSON en el archivo especificado
        }

        // Lee la lista de ganadores desde un archivo JSON
        private static List<RegistroGanador> LeerGanadores(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo)) // Verifica si el archivo existe
            {
                string jsonString = File.ReadAllText(nombreArchivo); // Lee el contenido del archivo JSON
                return JsonSerializer.Deserialize<List<RegistroGanador>>(jsonString) ?? new List<RegistroGanador>(); // Deserializa la cadena JSON a una lista de ganadores, o devuelve una lista vacía si el archivo está vacío
            }
            return new List<RegistroGanador>(); // Devuelve una lista vacía si el archivo no existe
        }

        // Muestra el podio de ganadores
        public static void MostrarPodio(string nombreArchivo)
        {
            var ganadores = LeerGanadores(nombreArchivo); // Lee la lista de ganadores del archivo

            // Ordena la lista de ganadores por el número de victorias (descendente)
            ganadores = ganadores.OrderByDescending(g => int.Parse(g.InformacionAdicional.Split(':')[1].Trim())).ToList();

            // Muestra el podio de ganadores
            Console.WriteLine("Podio de Ganadores:");
            foreach (var registro in ganadores) // Recorre cada registro en la lista de ganadores
            {
                Console.WriteLine($"{registro.Ganador.GetNombre()} - {registro.InformacionAdicional}"); // Muestra el nombre del personaje y el número de victorias
            }
        }

        // Verifica la existencia y validez de un archivo JSON
        public static bool Existe(string nombreArchivo)
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0; // Devuelve verdadero si el archivo existe y tiene un tamaño mayor a cero
        }
    }
}