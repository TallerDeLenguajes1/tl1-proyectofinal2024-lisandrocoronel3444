using personasjesDelJuego;

namespace escenarioBatalla
{
    public class Batalla
    {
        // Método para iniciar una batalla entre dos personajes
        public void IniciarBatalla(Personaje personaje1, Personaje personaje2)
        {
            personaje1.CurarTotalmente();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Comienza la batalla entre {personaje1.GetNombre()} y {personaje2.GetNombre()}");
            Console.ResetColor();

            while (personaje1.GetSalud() > 0 && personaje2.GetSalud() > 0)
            {
                Console.WriteLine($"{personaje1.GetNombre()} - Salud: {personaje1.GetSalud()}");
                Console.WriteLine($"{personaje2.GetNombre()} - Salud: {personaje2.GetSalud()}");
                Console.WriteLine();

                // Turno de personaje1 (usuario)
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Turno de {personaje1.GetNombre()}:");
                Console.ResetColor();
                RealizarAccion(personaje1, personaje2);

                if (personaje2.GetSalud() <= 0)
                {
                    Console.WriteLine($"El personaje {personaje2.GetNombre()} Ha muerto!");
                    Thread.Sleep(3000);
                    
                    break;
                }

                // Turno de personaje2 (enemigo realiza una acción al azar)
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Turno de {personaje2.GetNombre()}:");
                Console.ResetColor();
                personaje2.RealizarAccionAutomatica(personaje1);

                if (personaje1.GetSalud() <= 0)
                {
                    break;
                }

                Console.WriteLine();
            }

            // Determinar el resultado de la batalla y mostrar el ganador
            Console.ForegroundColor = personaje1.GetSalud() > 0 ? ConsoleColor.Yellow : ConsoleColor.Red;
            Console.WriteLine($"{(personaje1.GetSalud() > 0 ? personaje1.GetNombre() : personaje2.GetNombre())} ha ganado la batalla!");
            Thread.Sleep(3000);
            Console.ResetColor();
        }

        // Método para que el atacante realice una acción (atacar, defender o curarse)
        private void RealizarAccion(Personaje atacante, Personaje defensor)
        {
            Console.WriteLine("Elige una acción:");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Defender");
            Console.WriteLine("3. Curarse");

            int opcionElegida;
            do
            {
                Console.Write("Selecciona una opción (1, 2 o 3): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                int.TryParse(Console.ReadLine(), out opcionElegida);
                Console.ResetColor();

                if (opcionElegida != 1 && opcionElegida != 2 && opcionElegida != 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida. Por favor, elige 1, 2 o 3.");
                    Console.ResetColor();
                }

            } while (opcionElegida != 1 && opcionElegida != 2 && opcionElegida != 3);

            Console.Clear();
            Console.WriteLine($"{atacante.GetNombre()} elige su acción:");
            switch (opcionElegida)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("¡Ataque en curso!");
                    Console.ResetColor();
                    atacante.Atacar(defensor);
                    Thread.Sleep(2000);
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("¡Defensa en curso!");
                    Console.ResetColor();
                    atacante.Defender();
                    Thread.Sleep(2000);
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("¡Curación en curso!");
                    Console.ResetColor();
                    atacante.Curar();
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}