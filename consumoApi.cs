using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace consumoApi;

    public class ManejoApi
    {
        private static readonly HttpClient client = new HttpClient();

        public List<string> ObtenerNombresDePersonajes()
        {
            List<string> nombresDePersonajes = new List<string>();
            try
            {
                HttpResponseMessage response = client.GetAsync("https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var datos = JsonSerializer.Deserialize<JsonElement>(responseBody);

                if (datos.TryGetProperty("data", out JsonElement dataElement))
                {
                    foreach (JsonProperty personaje in dataElement.EnumerateObject())
                    {
                        nombresDePersonajes.Add(personaje.Name);
                    }
                }
            }
            catch (Exception)
            {
                // Si la API falla, generar nombres predefinidos según la cantidad solicitada
                for (int i = 1; i <= 10; i++)
                {
                    nombresDePersonajes.Add($"Guerrero {i}");
                }
            }

            // Devolver la lista de nombres de personajes
            return nombresDePersonajes;
        }
    }
