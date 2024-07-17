using personasjesDelJuego;
using escenarioBatalla;

            List<Personaje> personajes = new List<Personaje>();
            //VELOCIDAD, DESTREZA, FUERZA, ARMADURA, SALUD;

            personajes.Add(new Personaje("Guerrero", "Leonard", "Espada de Fuego", new DateTime(1990, 1, 1), 4, 7, 10, 3));
            personajes.Add(new Personaje("Mago", "Merlin", "El Sabio", new DateTime(1995, 5, 15), 6, 10, 7, 3));
            personajes.Add(new Personaje("Arquero", "Kay", "La torreta", new DateTime(1992, 7, 20), 10, 8, 6, 2));

// Agregar personajes adicionales
            personajes.Add(new Personaje("Berserker", "Aldric", "El Martillo", new DateTime(1988, 4, 10), 5, 6, 9, 4));
            personajes.Add(new Personaje("Hechicera", "Morgana", "La Hechicera", new DateTime(1994, 3, 25), 7, 9, 6, 3));
            personajes.Add(new Personaje("Asesino", "Robin", "El Ágil", new DateTime(1993, 9, 13), 9, 7, 5, 3));
            personajes.Add(new Personaje("Paladín", "Thorin", "El Valiente", new DateTime(1987, 12, 5), 6, 8, 8, 5));
            personajes.Add(new Personaje("Druida", "Gandalf", "El Blanco", new DateTime(1985, 11, 19), 5, 10, 6, 4));
            personajes.Add(new Personaje("Cazadora", "Eleanor", "La Francotiradora", new DateTime(1991, 6, 22), 8, 9, 7, 3));
            personajes.Add(new Personaje("Barbaro", "Bjorn", "El Feroz", new DateTime(1989, 8, 30), 7, 6, 9, 4));

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
            Batalla batalla = new Batalla();
            batalla.IniciarBatalla(personajeElegido, personajes[1]);
        
    
