using personasjesDelJuego;

List<Personaje> personajes = new List<Personaje>();
    personajes.Add(new Personaje("Guerrero", "Guerrero 1", "Espada de Fuego", new DateTime(1990, 1, 1), 8, 7, 10, 5, 4));
    personajes.Add(new Personaje("Mago", "Mago 1", "El Sabio", new DateTime(1995, 5, 15), 6, 10, 8, 3, 3));
    
Console.WriteLine("Elige tu personaje para el combate:");
    for (int i = 0; i < personajes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {personajes[i].GetNombre()} - {personajes[i].GetTipo()}");
    }

    // Obtener la elección del usuario
    int eleccion;
    do
    {
        Console.Write("Selecciona un número del 1 al 10: ");
    } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > 10);