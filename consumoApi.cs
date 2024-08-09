using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace consumoApi
{
    public class ManejoApi
    {
        // Crear una instancia estática de HttpClient para realizar solicitudes HTTP
        private static readonly HttpClient client = new HttpClient();

        // Método para obtener nombres de personajes desde la API
        public List<string> ObtenerNombresDePersonajes()
        {
            List<string> nombresDePersonajes = new List<string>();
            try
            {
                // Hacer la solicitud GET a la API para obtener datos de los personajes
                HttpResponseMessage response = client.GetAsync("https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json").Result;
                
                // Asegurarse de que la solicitud haya sido exitosa (código de estado 200)
                response.EnsureSuccessStatusCode();

                // Leer el contenido de la respuesta como una cadena
                string responseBody = response.Content.ReadAsStringAsync().Result;

                // Deserializar el contenido JSON de la respuesta en un objeto dinámico JsonElement
                var datos = JsonSerializer.Deserialize<JsonElement>(responseBody);

                // Intentar obtener el objeto "data" del JSON deserializado
                if (datos.TryGetProperty("data", out JsonElement dataElement))
                {
                    // Enumerar cada propiedad del objeto "data" (cada propiedad representa un personaje)
                    foreach (JsonProperty personaje in dataElement.EnumerateObject())
                    {
                        // Agregar el nombre del personaje a la lista
                        nombresDePersonajes.Add(personaje.Name);
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error (por ejemplo, si la API no está disponible), usar nombres predefinidos
                nombresDePersonajes = new List<string>
                {
                    "Guerrero 1",
                    "Guerrero 2",
                    "Guerrero 3",
                    "Guerrero 4",
                    "Guerrero 5",
                    "Guerrero 6",
                    "Guerrero 7",
                    "Guerrero 8",
                    "Guerrero 9",
                    "Guerrero 10"
                };
            }

            // Devolver la lista de nombres de personajes
            return nombresDePersonajes;
        }
    }
}