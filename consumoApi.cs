using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace consumoApi;

public class ManejoApi{
    // Crear instancia de HttpClient
    private static readonly HttpClient client = new HttpClient();

      public List<string> ObtenerNombresDePersonajes()
        {
            // Hacer la solicitud GET a la API
            HttpResponseMessage response = client.GetAsync("https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json").Result;
            response.EnsureSuccessStatusCode();

            // Leer el contenido de la respuesta
            string responseBody = response.Content.ReadAsStringAsync().Result;

            // Deserializar la respuesta JSON en un objeto dinámico
            var datos = JsonSerializer.Deserialize<JsonElement>(responseBody);

            // Extraer los nombres de los personajes
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
}

