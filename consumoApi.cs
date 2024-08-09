
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace consumoApi;

    public class ManejoApi
    {
        private static readonly HttpClient client = new HttpClient();

        // Lista de nombres de personajes predefinidos para usar en caso de error con la API
        private static readonly List<string> nombresPredefinidos = new List<string>
        {
            "GUERRERO 1", "GUERRERO 2", "GUERRERO 3", "GUERRERO 4", "GUERRERO 5", 
            "GUERRERO 6", "GUERRERO 7", "GUERRERO 8", "GUERRERO 9", "GUERRERO 10"
        };

        // Método para obtener nombres de personajes desde la API
        public async Task<List<string>> ObtenerNombresDePersonajesAsync()
        {
            try
            {
                // Hacer la solicitud GET a la API para obtener datos de los personajes
                HttpResponseMessage response = await client.GetAsync("https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json");

                // Asegurarse de que la solicitud haya sido exitosa (código de estado 200)
                response.EnsureSuccessStatusCode();

                // Leer el contenido de la respuesta como una cadena
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar el contenido JSON de la respuesta en un objeto dinámico JsonElement
                var datos = JsonSerializer.Deserialize<JsonElement>(responseBody);

                List<string> nombresDePersonajes = new List<string>();

                if (datos.TryGetProperty("data", out JsonElement dataElement))
                {
                    foreach (JsonProperty personaje in dataElement.EnumerateObject())
                    {
                        nombresDePersonajes.Add(personaje.Name);
                    }
                }

                return nombresDePersonajes;
            }
            catch (HttpRequestException e)
            {
                // Si ocurre un error durante la solicitud, imprimir el error y usar los nombres predefinidos
                Console.WriteLine($"Error al obtener nombres de la API: {e.Message}");
                Console.WriteLine("Usando nombres predefinidos.");
                return nombresPredefinidos;
            }
            catch (Exception e)
            {
                // Manejar cualquier otra excepción inesperada
                Console.WriteLine($"Error inesperado: {e.Message}");
                Console.WriteLine("Usando nombres predefinidos.");
                return nombresPredefinidos;
            }
        }
    }
