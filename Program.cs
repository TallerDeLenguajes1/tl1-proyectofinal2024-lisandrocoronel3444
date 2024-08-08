using personasjesDelJuego;
using escenarioBatalla;
using persistenciaJson;

         MostrarHistoria();
        MostrarMenuPrincipal();
        

        // Muestra la historia de forma lenta
        static void MostrarHistoria()
        {
            string historia = "Hace mucho tiempo, en un reino lejano, diez valientes luchadores se alzaron para reclamar el Trono de Hierro.\n" +
                "Estos diez guerreros, cada uno con habilidades únicas y una fuerza incomparable, se enfrentaron en batallas épicas.\n" +
                "El Guerrero Leonard, con su Espada de Fuego, luchaba con un fervor inquebrantable.\n" +
                "El Mago Merlin, conocido como El Sabio, usaba sus poderes arcanos para dominar a sus enemigos.\n" +
                "Kay, el Arquero apodado La Torreta, disparaba flechas con una precisión letal.\n" +
                "Aldric, el Berserker, aplastaba a sus enemigos con su Martillo colosal.\n" +
                "Morgana, la Hechicera, conjuraba hechizos oscuros que aterrorizaban a sus adversarios.\n" +
                "Robin, el Ágil Asesino, se movía con una velocidad y sigilo insuperables.\n" +
                "Thorin, el Paladín Valiente, defendía la justicia con su escudo y espada.\n" +
                "Gandalf, el Druida Blanco, invocaba la naturaleza para protegerse y atacar.\n" +
                "Eleanor, la Francotiradora Cazadora, cazaba a sus enemigos desde la distancia.\n" +
                "Bjorn, el Bárbaro Feroz, arremetía con una furia que nadie podía detener.\n" +
                "Cada uno de ellos estaba dispuesto a darlo todo en la arena de combate, pues solo uno podría reclamar el Trono de Hierro.\n" +
                "¡Que comience la batalla y que el mejor guerrero prevalezca!\n";

            foreach (char c in historia)
            {
                Console.Write(c);
                Thread.Sleep(10); // Retrasa la impresión de cada carácter para efecto de texto lento
            }

            Console.WriteLine("\n");
        }

        // Muestra el menú principal y maneja la elección del usuario
        static void MostrarMenuPrincipal()
{
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("======== MENU PRINCIPAL ========");
    Console.ResetColor();
    Console.WriteLine("1. Jugar");
    Console.WriteLine("2. Ver historial");
    Console.WriteLine("3. Salir");
    Console.Write("Selecciona una opción: ");

    string opcion = Console.ReadLine();
    switch (opcion)
    {
        case "1":
            Jugar();
            break;
        case "2":
            VerHistorial();
            break;
        case "3":
            Salir();
            break;
        default:
            Console.WriteLine("Opción no válida. Intenta de nuevo.");
            MostrarMenuPrincipal();
            break;
    }
}
static void Salir()
{
    Console.WriteLine("Gracias por jugar. ¡Hasta luego!");
    Environment.Exit(0); // Termina la aplicación
}

        // Maneja la lógica para jugar
        static void Jugar()
        {
            List<Personaje> personajes = CrearPersonajes();

            // Mostrar los personajes y permitir que el usuario elija
            Console.WriteLine("Elige tu personaje para el combate:");
            for (int i = 0; i < personajes.Count; i++)
            {
                Console.WriteLine($"Personaje {i + 1}:");
                personajes[i].MostrarInformacion();
                Console.WriteLine();
            }

            int eleccion;
            do
            {
                Console.Write($"Selecciona un número del 1 al {personajes.Count} para elegir tu personaje: ");
            } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

            Personaje personajeElegido = personajes[eleccion - 1];
            Console.WriteLine("Has elegido a:");
            personajeElegido.MostrarInformacion();

            // Iniciar la batalla
            Batalla batalla = new Batalla();
            for (int i = 0; i < personajes.Count; i++)
            {
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

            // Guardar el ganador en el historial
            Historial.GuardarGanador(new Historial.RegistroGanador
            {
                Ganador = personajeElegido,
                Fecha = DateTime.Now,
                InformacionAdicional = "Victorias: 1" // O puedes calcular el conteo de victorias aquí
            }, "historialGanadores.json");

            // Mostrar el podio
            Historial.MostrarPodio("historialGanadores.json");

            // Preguntar si el usuario quiere jugar otra vez
            Console.WriteLine("¿Quieres jugar otra vez? (s/n)");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                Jugar();
            }
            else
            {
                MostrarMenuPrincipal();
            }
        }

        // Muestra el historial de ganadores
        static void VerHistorial()
{
    // Mostrar el historial de ganadores
    Historial.MostrarPodio("historialGanadores.json");

    // Regresar al menú principal inmediatamente
    MostrarMenuPrincipal();
}

    // Regresar al menú principal
    
        // Crea una lista de personajes
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
    
