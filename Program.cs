using System;
using System.Collections.Generic;
using System.Threading;
using personasjesDelJuego;
using escenarioBatalla;
using persistenciaJson;
using consumoApi;


    
        MostrarHistoria(); // Muestra la historia de forma lenta
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

        // Imprime cada carácter de la historia con un retraso para crear un efecto de texto lento
        foreach (char c in historia)
        {
            Console.Write(c);
            Thread.Sleep(10); // Retrasa la impresión de cada carácter
        }

        Console.WriteLine("\n"); // Salto de línea al final de la historia
    }

    // Muestra el menú principal y maneja la elección del usuario
    static void MostrarMenuPrincipal()
    {
        Console.ForegroundColor = ConsoleColor.Green; // Cambia el color del texto
        Console.WriteLine("======== MENU PRINCIPAL ========");
        Console.ResetColor(); // Restaura el color del texto a la configuración predeterminada
        Console.WriteLine("1. Jugar");
        Console.WriteLine("2. Ver historial");
        Console.WriteLine("3. Salir");
        Console.Write("Selecciona una opción (1, 2 o 3): ");

        int opcion;
        // Lee la opción elegida por el usuario y verifica si es válida
        while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 3)
        {
            Console.WriteLine("Opción no válida. Intenta de nuevo.");
            Console.Write("Selecciona una opción (1, 2 o 3): ");
        }

        // Ejecuta la acción correspondiente según la opción elegida
        switch (opcion)
        {
            case 1:
                Jugar(); // Llama a la función para jugar
                break;
            case 2:
                VerHistorial(); // Llama a la función para ver el historial
                break;
            case 3:
                Salir(); // Llama a la función para salir
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
        List<Personaje> personajes = CrearPersonajes(); // Crea la lista de personajes

        // Mostrar los personajes y permitir que el usuario elija
        Console.WriteLine("Elige tu personaje para el combate:");
        for (int i = 0; i < personajes.Count; i++)
        {
            Console.WriteLine($"Personaje {i + 1}:");
            personajes[i].MostrarInformacion(); // Muestra la información del personaje
            Console.WriteLine();
        }

        int eleccion;
        // Lee la selección del usuario y verifica si es válida
        do
        {
            Console.Write($"Selecciona un número del 1 al {personajes.Count}: ");
        } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

        Personaje personajeElegido = personajes[eleccion - 1]; // Selecciona el personaje elegido

        // Verifica si el personajeElegido no es nulo
        if (personajeElegido != null)
        {
            Console.WriteLine("Has elegido a:");
            personajeElegido.MostrarInformacion(); // Muestra la información del personaje elegido

            // Iniciar la batalla
            Batalla batalla = new Batalla();
            bool ganoTodasLasBatallas = true;

            // Lucha contra todos los personajes excepto el elegido
            for (int i = 0; i < personajes.Count; i++)
            {
                if (i != eleccion - 1)
                {
                    Console.WriteLine($"\nBatalla contra {personajes[i].GetNombre()} ({personajes[i].GetTipo()}):");
                    batalla.IniciarBatalla(personajeElegido, personajes[i]);

                    // Verifica si el personajeElegido sigue vivo
                    if (!personajeElegido.EstaVivo())
                    {
                        Console.WriteLine("¡Has sido derrotado!");
                        ganoTodasLasBatallas = false;
                        break;
                    }
                }
            }

            // Si el personajeElegido ha ganado todas las batallas
            if (ganoTodasLasBatallas)
            {
                Console.WriteLine("¡Has vencido a todos los oponentes!");
                Console.WriteLine("¡Felicidades! 🏆✨ Eres el legítimo ganador del Trono de Hierro. Tu destreza y valentía en el campo de batalla han demostrado ser incomparables. Has vencido a todos tus rivales y reclamado el poder supremo. ¡Disfruta de tu triunfo y que tu reinado sea recordado como el más glorioso de todos!");

                // Agregar el ganador al historial
                Historial.AgregarGanador("historialGanadores.json", personajeElegido);
            }

            // Preguntar si el usuario quiere jugar otra vez
            Console.WriteLine("¿Quieres jugar otra vez? (PRESIONA 1 PARA SEGUIR JUGANDO / PRESIONA 0 PARA DEJAR DE JUGAR)");
            int respuesta;
            int.TryParse(Console.ReadLine(), out respuesta);
            if (respuesta == 1)
            {
                Jugar(); // Llama a la función para jugar nuevamente
            }
            else if (respuesta == 0)
            {
                MostrarMenuPrincipal(); // Regresa al menú principal
            }
            else
            {
                Console.WriteLine("Respuesta no válida. Regresando al menú principal.");
                MostrarMenuPrincipal(); // Regresa al menú principal
            }
        }
        else
        {
            Console.WriteLine("El personaje seleccionado no es válido. Regresando al menú principal.");
            MostrarMenuPrincipal(); // Regresa al menú principal
        }
    }

    // Muestra el historial de ganadores
    static void VerHistorial()
    {
        Historial.MostrarPodio("historialGanadores.json"); // Muestra el podio de ganadores

        // Regresar al menú principal inmediatamente
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

        // Crea y devuelve una lista de personajes con los nombres obtenidos
        return new List<Personaje>
    {
        new Personaje("Guerrero", nombresDePersonajes[0], "Espada de Fuego", new DateTime(1990, 1, 1), 4, 7, 100, 3),
        new Personaje("Mago", nombresDePersonajes[1], "El Sabio", new DateTime(1995, 5, 15), 6, 10, 7, 3),
        new Personaje("Arquero", nombresDePersonajes[2], "La Torreta", new DateTime(1992, 7, 20), 10, 8, 6, 2),
        new Personaje("Berserker", nombresDePersonajes[3], "El Martillo", new DateTime(1988, 4, 10), 5, 6, 9, 4),
        new Personaje("Hechicera", nombresDePersonajes[4], "La Hechicera", new DateTime(1994, 3, 25), 7, 9, 6, 3),
        new Personaje("Asesino", nombresDePersonajes[5], "El Ágil", new DateTime(1993, 9, 13), 9, 7, 5, 3),
        new Personaje("Paladín", nombresDePersonajes[6], "El Valiente", new DateTime(1987, 12, 5), 6, 8, 8, 5),
        new Personaje("Druida", nombresDePersonajes[7], "El Blanco", new DateTime(1985, 11, 19), 5, 10, 6, 4),
        new Personaje("Cazadora", nombresDePersonajes[8], "La Francotiradora", new DateTime(1991, 6, 22), 8, 9, 7, 3),
        new Personaje("Barbaro", nombresDePersonajes[9], "El Feroz", new DateTime(1989, 8, 30), 7, 6, 9, 4)
    };
}

    
