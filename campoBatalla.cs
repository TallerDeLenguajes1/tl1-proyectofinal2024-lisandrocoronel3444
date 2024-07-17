namespace escenarioBatalla;
using personasjesDelJuego;


public class Batalla
{
    public void IniciarBatalla(Personaje personaje1, Personaje personaje2)
    {
        Console.WriteLine($"Comienza la batalla entre {personaje1.GetNombre()} y {personaje2.GetNombre()}");

        while (personaje1.GetSalud() > 0 && personaje2.GetSalud() > 0)
        {
            Console.WriteLine($"{personaje1.GetNombre()} - Salud: {personaje1.GetSalud()}");
            Console.WriteLine($"{personaje2.GetNombre()} - Salud: {personaje2.GetSalud()}");
            Console.WriteLine();

            // Turno de personaje1
            Console.WriteLine($"Turno de {personaje1.GetNombre()}:");
            RealizarAccion(personaje1, personaje2);

            // Verificar si personaje2 sigue vivo después del ataque
            if (personaje2.GetSalud() <= 0)
                break;

            // Turno de personaje2
            Console.WriteLine($"Turno de {personaje2.GetNombre()}:");
            RealizarAccion(personaje2, personaje1);

            // Verificar si personaje1 sigue vivo después del ataque
            if (personaje1.GetSalud() <= 0)
                break;

            Console.WriteLine();
        }

        // Determinar el resultado de la batalla
        if (personaje1.GetSalud() > 0)
        {
            Console.WriteLine($"{personaje1.GetNombre()} ha ganado la batalla!");
        }
        else
        {
            Console.WriteLine($"{personaje2.GetNombre()} ha ganado la batalla!");
        }
    }

    private void RealizarAccion(Personaje atacante, Personaje defensor)
    {
        Console.WriteLine("Elige una acción:");
        Console.WriteLine("1. Atacar");
        Console.WriteLine("2. Defender");
        Console.WriteLine("3. Curarse");

        string opcion;
        do
        {
            Console.Write("Selecciona una opción: ");
            opcion = Console.ReadLine();
        } while (opcion != "1" && opcion != "2" && opcion != "3");

        switch (opcion)
        {
            case "1":
                atacante.Atacar(defensor);
                break;
            case "2":
                atacante.Defender();
                break;
            case "3":
                atacante.Curar();
                break;
        }
    }
}