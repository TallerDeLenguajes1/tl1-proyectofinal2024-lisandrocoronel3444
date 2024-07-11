using personasjesDelJuego;

List<Personaje> personajes = new List<Personaje>();
            personajes.Add(new Personaje("Guerrero", "Leonard", "Espada de Fuego", new DateTime(1990, 1, 1), 8, 7, 10, 4));
            personajes.Add(new Personaje("Mago", "Merlin", "El Sabio", new DateTime(1995, 5, 15), 6, 10, 8, 3));
            personajes.Add(new Personaje("Arquero", "Kay", "La torreta", new DateTime(1992, 7, 20), 7, 8, 9, 4));

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
                Console.Write($"Selecciona un número del 1 al {personajes.Count}: ");
            } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

            Personaje personajeElegido = personajes[eleccion - 1];
            Console.WriteLine("Has elegido a:");
            personajeElegido.MostrarInformacion();
        
    
