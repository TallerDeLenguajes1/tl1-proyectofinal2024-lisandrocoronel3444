using personasjesDelJuego;

namespace escenarioBatalla
{
    public class Batalla
    {
        // Método para iniciar una batalla entre dos personajes
        public void IniciarBatalla(Personaje personaje1, Personaje personaje2)
        {
            // Curar completamente al personaje 1 al inicio de la batalla
            personaje1.CurarTotalmente();
            Console.WriteLine($"Comienza la batalla entre {personaje1.GetNombre()} y {personaje2.GetNombre()}");

            // Bucle de la batalla hasta que uno de los personajes quede sin salud
            while (personaje1.GetSalud() > 0 && personaje2.GetSalud() > 0)
            {
                // Mostrar salud actual de ambos personajes
                Console.WriteLine($"{personaje1.GetNombre()} - Salud: {personaje1.GetSalud()}");
                Console.WriteLine($"{personaje2.GetNombre()} - Salud: {personaje2.GetSalud()}");
                Console.WriteLine();

                // Turno de personaje1 (usuario)
                Console.WriteLine($"Turno de {personaje1.GetNombre()}:");
                RealizarAccion(personaje1, personaje2);

                // Verificar si el enemigo sigue vivo después del ataque
                if (personaje2.GetSalud() <= 0)
                {
                    break;
                }

                // Turno de personaje2 (enemigo realiza una acción al azar)
                Console.WriteLine($"Turno de {personaje2.GetNombre()}:");
                personaje2.RealizarAccionAutomatica(personaje1);

                // Verificar si el personaje1 sigue vivo después del ataque
                if (personaje1.GetSalud() <= 0)
                {
                    break;
                }

                Console.WriteLine();
            }

            // Determinar el resultado de la batalla y mostrar el ganador
            if (personaje1.GetSalud() > 0)
            {
                Console.WriteLine($"{personaje1.GetNombre()} ha ganado la batalla!");
            }
            else
            {
                Console.WriteLine($"{personaje2.GetNombre()} ha ganado la batalla!");
            }
        }

        // Método para que el atacante realice una acción (atacar, defender o curarse)
        private void RealizarAccion(Personaje atacante, Personaje defensor)
        {
            // Mostrar opciones disponibles para la acción
            Console.WriteLine("Elige una acción:");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Defender");
            Console.WriteLine("3. Curarse");

            int opcionElegida;
            do
            {
                // Leer opción del usuario
                Console.Write("Selecciona una opción (1, 2 o 3): ");
                int.TryParse(Console.ReadLine(), out opcionElegida);

                // Validar que la opción elegida sea válida
                if (opcionElegida != 1 && opcionElegida != 2 && opcionElegida != 3)
                {
                    Console.WriteLine("Opción no válida. Por favor, elige 1, 2 o 3.");
                }

            } while (opcionElegida != 1 && opcionElegida != 2 && opcionElegida != 3);

            // Ejecutar la acción seleccionada
            switch (opcionElegida)
            {
                case 1:
                    atacante.Atacar(defensor); // Llamar al método Atacar del personaje
                    break;
                case 2:
                    atacante.Defender(); // Llamar al método Defender del personaje
                    break;
                case 3:
                    atacante.Curar(); // Llamar al método Curar del personaje
                    break;
            }
        }
    }
}