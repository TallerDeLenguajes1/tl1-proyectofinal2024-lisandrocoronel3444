using System;
using System.Collections.Generic;
using System.Threading;
using personasjesDelJuego;
using escenarioBatalla;
using persistenciaJson;
using consumoApi;


    
        MostrarHistoria(); // Muestra la historia de forma lenta
        Thread.Sleep(3000);
        MostrarMenuPrincipal(); // Muestra el menú principal
    

    // Muestra la historia de forma lenta
    static void MostrarHistoria()
{
    string historia = "Hace mucho tiempo, en un reino lejano, diez valientes luchadores se alzaron para reclamar el Trono de Hierro.\n" +
        "Cada uno de estos guerreros poseía habilidades únicas y una fuerza incomparable, y se enfrentaron en batallas épicas.\n" +
        "El Guerrero, con su espada ardiente, luchaba con un fervor inquebrantable.\n" +
        "El Mago, conocido por su sabiduría, usaba sus poderes arcanos para dominar a sus enemigos.\n" +
        "El Arquero, apodado el Preciso, disparaba flechas con una exactitud letal.\n" +
        "El Berserker, con su martillo colosal, aplastaba a sus adversarios con una fuerza implacable.\n" +
        "La Hechicera, conjurando hechizos oscuros, aterrorizaba a sus adversarios con su magia.\n" +
        "El Ágil Asesino se movía con una velocidad y sigilo insuperables.\n" +
        "El Paladín Valiente defendía la justicia con su escudo y espada, luchando con honor.\n" +
        "El Druida, invocador de la naturaleza, usaba su poder para protegerse y atacar.\n" +
        "La Francotiradora cazaba a sus enemigos desde la distancia, con gran precisión.\n" +
        "El Bárbaro Feroz arremetía con una furia que parecía imparable.\n" +
        "Cada uno de ellos estaba dispuesto a darlo todo en la arena de combate, pues solo uno podría reclamar el Trono de Hierro.\n" +
        "¡Que comience la batalla y que el mejor guerrero prevalezca!\n";

    Console.ForegroundColor = ConsoleColor.Cyan;
    foreach (char c in historia)
    {
        Console.Write(c);
        Thread.Sleep(15);
    }
    Console.ResetColor();
    Console.WriteLine("\n");
}

    // Muestra el menú principal y maneja la elección del usuario
    static void MostrarMenuPrincipal()
{
    Console.Clear(); // Limpia la consola para un mejor aspecto
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("======== MENU PRINCIPAL ========");
    Console.ResetColor();
    Console.WriteLine("1. Jugar");
    Console.WriteLine("2. Ver historial");
    Console.WriteLine("3. Salir");
    Console.Write("Selecciona una opción (1, 2 o 3): ");

    int opcion;
    while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 3)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Opción no válida. Intenta de nuevo.");
        Console.ResetColor();
        Console.Write("Selecciona una opción (1, 2 o 3): ");
    }

    switch (opcion)
    {
        case 1:
            Jugar();
            break;
        case 2:
            VerHistorial();
            break;
        case 3:
            Salir();
            break;
    }
}

    // Termina la aplicación
    static void Salir()
    {
        Console.WriteLine("Gracias por jugar. ¡Hasta luego!"); // Mensaje de despedida
        Environment.Exit(0); // Termina la aplicación
    }

    // Maneja la lógica para jugar
    static void Jugar()
{
    string nombArchivo = "personajes.json";
    PersonajesJson.GuardarPersonajes(CrearPersonajes(), nombArchivo);
    List<Personaje> personajes = PersonajesJson.LeerPersonajes(nombArchivo);


    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Elige tu personaje para el combate:");
    Console.ResetColor();
    for (int i = 0; i < personajes.Count; i++)
    {
        Console.WriteLine($"Personaje {i + 1}:");
        personajes[i].MostrarInformacion();
        Console.WriteLine();
    }

    int eleccion;
    do
    {
        Console.Write($"Selecciona un número del 1 al {personajes.Count}: ");
    } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

    Personaje personajeElegido = personajes[eleccion - 1];
    if (personajeElegido != null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Has elegido a:");
        Console.ResetColor();
        personajeElegido.MostrarInformacion();

        Batalla batalla = new Batalla();
        bool ganoTodasLasBatallas = true;

        for (int i = 0; i < personajes.Count; i++)
        {
            if (i != eleccion - 1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nBatalla contra {personajes[i].GetNombre()} ({personajes[i].GetTipo()}):");
                Console.ResetColor();
                batalla.IniciarBatalla(personajeElegido, personajes[i]);

                if (!personajeElegido.EstaVivo())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("¡Has sido derrotado!");
                    Thread.Sleep(2000);
                    Console.ResetColor();
                    ganoTodasLasBatallas = false;
                    break;
                }
            }
        }

        if (ganoTodasLasBatallas)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("¡Has vencido a todos los oponentes!");
            Console.WriteLine("¡Felicidades! 🏆✨ Eres el legítimo ganador del Trono de Hierro. Tu destreza y valentía en el campo de batalla han demostrado ser incomparables. Has vencido a todos tus rivales y reclamado el poder supremo. ¡Disfruta de tu triunfo y que tu reinado sea recordado como el más glorioso de todos!");
            Thread.Sleep(2000);
            Console.ResetColor();
            Historial.AgregarGanador("historialGanadores.json", personajeElegido);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("¿Quieres jugar otra vez? (PRESIONA 1 PARA SEGUIR JUGANDO / PRESIONA 0 PARA DEJAR DE JUGAR)");
        Console.ResetColor();
        int respuesta;
        int.TryParse(Console.ReadLine(), out respuesta);
        if (respuesta == 1)
        {
            Jugar();
        }
        else if (respuesta == 0)
        {
            MostrarMenuPrincipal();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Respuesta no válida. Regresando al menú principal.");
            Console.ResetColor();
            MostrarMenuPrincipal();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El personaje seleccionado no es válido. Regresando al menú principal.");
        Console.ResetColor();
        MostrarMenuPrincipal();
    }
}


// Muestra el historial de ganadores
static void VerHistorial()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("======== HISTORIAL DE GANADORES ========");
    Console.ResetColor();
    Historial.MostrarPodio("historialGanadores.json");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nRegresar al menú principal...");
    Console.ResetColor();
    // Espera un momento antes de regresar al menú
    Thread.Sleep(6000);
    MostrarMenuPrincipal();
}

    // Crea una lista de personajes
    static List<Personaje> CrearPersonajes()
    {
        ManejoApi consumoApi = new ManejoApi(); // Crea una instancia de ManejoApi
        List<string> nombresDePersonajes = consumoApi.ObtenerNombresDePersonajes(); // Obtiene nombres de personajes de la API

        // Verifica si hay suficientes nombres para crear personajes
        if (nombresDePersonajes.Count < 10)
        {
            throw new Exception("No hay suficientes nombres de personajes en la API."); // Lanza una excepción si no hay suficientes nombres
        }

        // Cre una lista de personajes con los nombres obtenidos
        return new List<Personaje>
    {
        new Personaje("Guerrero", nombresDePersonajes[0], "Espada de Fuego", new DateTime(1990, 1, 1), 4, 10, 30, 10, "frases.json"),
        new Personaje("Mago", nombresDePersonajes[1], "El Sabio", new DateTime(1995, 5, 15), 6, 10, 7, 3,"frases.json"),
        new Personaje("Arquero", nombresDePersonajes[2], "La Torreta", new DateTime(1992, 7, 20), 10, 8, 6, 2,"frases.json"),
        new Personaje("Berserker", nombresDePersonajes[3], "El Martillo", new DateTime(1988, 4, 10), 5, 6, 9, 4,"frases.json"),
        new Personaje("Hechicera", nombresDePersonajes[4], "La Hechicera", new DateTime(1994, 3, 25), 7, 9, 6, 3,"frases.json"),
        new Personaje("Asesino", nombresDePersonajes[5], "El Ágil", new DateTime(1993, 9, 13), 9, 7, 5, 3,"frases.json"),
        new Personaje("Paladín", nombresDePersonajes[6], "El Valiente", new DateTime(1987, 12, 5), 6, 8, 8, 5,"frases.json"),
        new Personaje("Druida", nombresDePersonajes[7], "El Blanco", new DateTime(1985, 11, 19), 5, 10, 6, 4,"frases.json"),
        new Personaje("Cazadora", nombresDePersonajes[8], "La Francotiradora", new DateTime(1991, 6, 22), 8, 9, 7, 3,"frases.json"),
        new Personaje("Barbaro", nombresDePersonajes[9], "El Feroz", new DateTime(1989, 8, 30), 7, 6, 9, 4,"frases.json")
    };
    

}

    
