using personasjesDelJuego;
using escenarioBatalla;
using persistenciaJson;

        List<Personaje> personajes = CrearPersonajes();
        PersonajesJson.GuardarPersonajes(personajes, "personajes.json");
         if (PersonajesJson.Existe("personajes.json"))
        {
            Console.WriteLine("El archivo de personajes existe y tiene datos.");
        }
        else
        {
            Console.WriteLine("El archivo de personajes no existe o está vacío.");
        }

        List<Personaje> personajesLeidos = PersonajesJson.LeerPersonajes("personajes.json");
        

        static List<Personaje> CrearPersonajes()
        {
          return new List<Personaje>
          {
            new Personaje("Guerrero", "Leonard", "Espada de Fuego", new DateTime(1990, 1, 1), 4, 7, 100, 3),
            new Personaje("Mago", "Merlin", "El Sabio", new DateTime(1995, 5, 15), 6, 10, 7, 3),
            new Personaje("Arquero", "Kay", "La Torreta", new DateTime(1992, 7, 20), 10, 8, 6, 2),
            new Personaje("Berserker", "Aldric", "El Martillo", new DateTime(1988, 4, 10), 5, 6, 9, 4),
            new Personaje("Hechicera", "Morgana", "La Hechicera", new DateTime(1994, 3, 25), 7, 9, 6, 3),
            new Personaje("Asesino", "Robin", "El Ágil", new DateTime(1993, 9, 13), 9, 7, 5, 3),
            new Personaje("Paladín", "Thorin", "El Valiente", new DateTime(1987, 12, 5), 6, 8, 8, 5),
            new Personaje("Druida", "Gandalf", "El Blanco", new DateTime(1985, 11, 19), 5, 10, 6, 4),
            new Personaje("Cazadora", "Eleanor", "La Francotiradora", new DateTime(1991, 6, 22), 8, 9, 7, 3),
            new Personaje("Barbaro", "Bjorn", "El Feroz", new DateTime(1989, 8, 30), 7, 6, 9, 4)
          };
        }

            ContarHistoria();

            Console.WriteLine("Elige tu personaje para el combate:");
            Console.WriteLine("Personajes:");

            for (int i = 0; i < personajes.Count; i++)
            {
                Console.WriteLine($"Personaje {i + 1}:");
                personajes[i].MostrarInformacion();
                Console.WriteLine();
            }

            // Obtener la elección del usuario
            int eleccion;
            do
            {
                Console.Write($"Selecciona un número del 1 al {personajes.Count} para elegir tu personaje: ");
            } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

            Personaje personajeElegido = personajes[eleccion - 1];
            Console.WriteLine("Has elegido a:");
            personajeElegido.MostrarInformacion();
            Batalla batalla = new Batalla();

for (int i = 0; i < personajes.Count; i++){
    if (i != eleccion - 1)
    {
        Console.WriteLine($"\nBatalla contra {personajes[i].GetNombre()} ({personajes[i].GetTipo()}):");
        batalla.IniciarBatalla(personajeElegido, personajes[i]);

        if (!personajeElegido.EstaVivo())
        {
            Console.WriteLine("¡Has sido derrotado!");
            break;
        }
    }
}

if (personajeElegido.EstaVivo())
{
    Console.WriteLine("¡Has vencido a todos los oponentes!");
}
Historial.GuardarGanador(new Historial.RegistroGanador
{
    Ganador = personajeElegido,
    Fecha = DateTime.Now,
    InformacionAdicional = "Victoria en combate"
}, "historialGanadores.json");

// Mostrar el podio
Historial.MostrarPodio("historialGanadores.json");
        
    
static void ContarHistoria()
        {
            Console.WriteLine("Hace mucho tiempo, en un reino lejano, diez valientes luchadores se alzaron para reclamar el Trono de Hierro.");
            Console.WriteLine("Estos diez guerreros, cada uno con habilidades únicas y una fuerza incomparable, se enfrentaron en batallas épicas.");
            Console.WriteLine("El Guerrero Leonard, con su Espada de Fuego, luchaba con un fervor inquebrantable.");
            Console.WriteLine("El Mago Merlin, conocido como El Sabio, usaba sus poderes arcanos para dominar a sus enemigos.");
            Console.WriteLine("Kay, el Arquero apodado La Torreta, disparaba flechas con una precisión letal.");
            Console.WriteLine("Aldric, el Berserker, aplastaba a sus enemigos con su Martillo colosal.");
            Console.WriteLine("Morgana, la Hechicera, conjuraba hechizos oscuros que aterrorizaban a sus adversarios.");
            Console.WriteLine("Robin, el Ágil Asesino, se movía con una velocidad y sigilo insuperables.");
            Console.WriteLine("Thorin, el Paladín Valiente, defendía la justicia con su escudo y espada.");
            Console.WriteLine("Gandalf, el Druida Blanco, invocaba la naturaleza para protegerse y atacar.");
            Console.WriteLine("Eleanor, la Francotiradora Cazadora, cazaba a sus enemigos desde la distancia.");
            Console.WriteLine("Bjorn, el Bárbaro Feroz, arremetía con una furia que nadie podía detener.");
            Console.WriteLine("Cada uno de ellos estaba dispuesto a darlo todo en la arena de combate, pues solo uno podría reclamar el Trono de Hierro.");
            Console.WriteLine("¡Que comience la batalla y que el mejor guerrero prevalezca!\n");
        }