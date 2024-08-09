using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using personasjesDelJuego; // Asegúrate de que esta clase esté definida correctamente en tu proyecto

namespace persistenciaJson
{
    public class Historial
    {
        // Clase para almacenar información relevante del ganador
        public class RegistroGanador
        {
            public required Personaje Ganador { get; set; } // Objeto Personaje que ha ganado
            public DateTime Fecha { get; set; } // Fecha del registro
            public required string InformacionAdicional { get; set; } // Información adicional del registro
        }

        // Agrega el registro del ganador al historial en el archivo JSON
        public static void AgregarGanador(string nombreArchivo, Personaje ganador)
        {
            // Crea un nuevo registro de ganador con la información actual
            var registroGanador = new RegistroGanador
            {
                Ganador = ganador, // Establece el personaje ganador
                Fecha = DateTime.Now, // Establece la fecha actual
                InformacionAdicional = $"Victorias: 1" // Inicializa las victorias en 1
            };

            // Lee la lista actual de ganadores desde el archivo JSON
            List<RegistroGanador> ganadores = LeerGanadores(nombreArchivo);
            
            // Busca si el ganador ya existe en la lista
            var ganadorExistente = ganadores.FirstOrDefault(g => g.Ganador.GetNombre() == ganador.GetNombre());

            if (ganadorExistente != null)
            {
                // Si el ganador ya existe, actualiza la información adicional con el nuevo conteo de victorias
                ganadorExistente.InformacionAdicional = $"Victorias: {int.Parse(ganadorExistente.InformacionAdicional.Split(':')[1].Trim()) + 1}";
            }
            else
            {
                // Si el ganador no existe, agrega el nuevo registro a la lista
                ganadores.Add(registroGanador);
            }

            // Guarda la lista actualizada de ganadores en el archivo JSON
            GuardarGanadores(ganadores, nombreArchivo);
        }

        // Guarda la lista de ganadores en un archivo JSON
        private static void GuardarGanadores(List<RegistroGanador> ganadores, string nombreArchivo)
        {
            // Configura las opciones de serialización JSON para una representación más legible
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            
            // Serializa la lista de ganadores a una cadena JSON
            string jsonString = JsonSerializer.Serialize(ganadores, opciones);
            
            // Guarda la cadena JSON en el archivo especificado
            File.WriteAllText(nombreArchivo, jsonString);
        }

        // Lee la lista de ganadores desde un archivo JSON
        private static List<RegistroGanador> LeerGanadores(string nombreArchivo)
        {
            // Verifica si el archivo existe antes de intentar leerlo
            if (File.Exists(nombreArchivo))
            {
                // Lee el contenido del archivo JSON
                string jsonString = File.ReadAllText(nombreArchivo);
                
                // Deserializa la cadena JSON a una lista de ganadores, o devuelve una lista vacía si el archivo está vacío
                return JsonSerializer.Deserialize<List<RegistroGanador>>(jsonString) ?? new List<RegistroGanador>();
            }
            
            // Devuelve una lista vacía si el archivo no existe
            return new List<RegistroGanador>();
        }

        // Muestra el podio de ganadores en la consola
        public static void MostrarPodio(string nombreArchivo)
        {
            // Lee la lista de ganadores del archivo
            var ganadores = LeerGanadores(nombreArchivo);
            
            // Ordena la lista de ganadores por el número de victorias en orden descendente
            ganadores = ganadores.OrderByDescending(g => int.Parse(g.InformacionAdicional.Split(':')[1].Trim())).ToList();

            // Muestra el encabezado del podio
            Console.WriteLine("Podio de Ganadores:");
            
            // Recorre cada registro en la lista de ganadores y muestra el nombre del personaje y el número de victorias
            foreach (var registro in ganadores)
            {
                Console.WriteLine($"{registro.Ganador.GetNombre()} - {registro.InformacionAdicional}");
            }
        }

        // Verifica la existencia y validez de un archivo JSON
        public static bool Existe(string nombreArchivo)
        {
            // Devuelve verdadero si el archivo existe y tiene un tamaño mayor a cero
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
    }
}