using System.Collections;

namespace personasjesDelJuego;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


    public class Personaje
    {
        private List<string> frasesAtaque;
        private List<string> frasesDefensa;
        private List<string> frasesCuracion;

        string archivoFrases = "frases.json";
        private Random random1 = new Random();

        // Datos
        [JsonInclude]
        private string tipo;
        [JsonInclude]
        private string nombre;
        [JsonInclude]
        private string apodo;
        [JsonInclude]
        private DateTime fechaDeNacimiento;
        [JsonInclude]
        private int edad;

        // Características
        [JsonInclude]
        private int velocidad;
        [JsonInclude]
        private int destreza;
        [JsonInclude]
        private int fuerza;
        [JsonInclude]
        private int armadura;
        [JsonInclude]
        private int salud;
        private static Dictionary<string, List<string>> CargarFrasesDesdeArchivo(string nombreArchivo)
        {
            if (!File.Exists(nombreArchivo))
            {
                throw new FileNotFoundException("El archivo de frases no se encontró.");
            }

            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString) 
                   ?? throw new InvalidOperationException("Error al deserializar el archivo de frases.");
        }

        // Constructor
        public Personaje(string tipo, string nombre, string apodo, DateTime fechaDeNacimiento, int velocidad, int destreza, int fuerza, int armadura, string nombArchivo)
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.apodo = apodo;
            this.fechaDeNacimiento = fechaDeNacimiento;
            this.edad = DateTime.Now.Year - fechaDeNacimiento.Year;

            this.velocidad = velocidad;
            this.destreza = destreza;
            this.fuerza = fuerza;
            this.armadura = armadura;
            this.salud = 100;
            var frases = CargarFrasesDesdeArchivo(archivoFrases);
            frasesAtaque = frases["frasesAtaque"];
            frasesDefensa = frases["frasesDefensa"];
            frasesCuracion = frases["frasesCuracion"];
        }

        // Métodos Getter
        public string GetTipo() => tipo;
        public string GetNombre() => nombre;
        public string GetApodo() => apodo;
        public DateTime GetFechaDeNacimiento() => fechaDeNacimiento;
        public int GetEdad() => edad;
        public int GetVelocidad() => velocidad;
        public int GetDestreza() => destreza;
        public int GetFuerza() => fuerza;
        public int GetArmadura() => armadura;
        public int GetSalud() => salud;

        // Método para mostrar información del personaje
        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {nombre} ({apodo})");
            Console.WriteLine($"Tipo: {tipo}");
            Console.WriteLine($"Fecha de Nacimiento: {fechaDeNacimiento.ToShortDateString()}");
            Console.WriteLine($"Edad: {edad}");
            Console.WriteLine($"Velocidad: {velocidad}");
            Console.WriteLine($"Destreza: {destreza}");
            Console.WriteLine($"Fuerza: {fuerza}");
            Console.WriteLine($"Armadura: {armadura}");
            Console.WriteLine($"Salud: {salud}");
        }

        // Métodos de ataque, recibir daño, defender, curar, etc.
        public void Atacar(Personaje Enemigo)
        {
            int ataqueBase = GetDestreza() * GetFuerza();
            Random random = new Random();
            int suerte = random.Next(1, 7);
            double multiplicador = 0;

            switch (suerte)
            {
                case 1:
                    multiplicador = 0.15;
                    break;
                case 2:
                    multiplicador = 0.30;
                    break;
                case 3:
                    multiplicador = 0.45;
                    break;
                case 4:
                    multiplicador = 0.60;
                    break;
                case 5:
                    multiplicador = 0.75;
                    break;
                case 6:
                    multiplicador = 1.00;
                    break;
            }

            int ataque = (int)(ataqueBase + (multiplicador * ataqueBase));
            int defensa = Enemigo.GetArmadura() * Enemigo.GetVelocidad();
            const int constanteDeAjuste = 4;

            int danio = ((ataque) - defensa) / constanteDeAjuste;
            if (danio > 0)
            {
                
                Console.WriteLine($"{this.nombre} provoca {danio} de daño a {Enemigo.GetNombre()} con un golpe de dado {suerte} (multiplicador {multiplicador * 100}%)");
                MostrarFrase(frasesAtaque);
                Thread.Sleep(2000);
                if(Enemigo.GetSalud() <= 0){
                    Console.WriteLine($"{Enemigo.GetNombre()} Ha muerto a manos de {this.nombre}");

                }
                
                Thread.Sleep(2000);
                Enemigo.RecibirDanio(danio);
                
                if (suerte == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("¡Golpe crítico!");
                    Thread.Sleep(2000);
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine($"{Enemigo.GetNombre()} bloqueó el ataque!");
                Thread.Sleep(2000);
            }
        }

        public void RecibirDanio(int danio)
        {
            this.salud -= danio;
            if (this.salud <= 0)
            {

                this.salud = 0;
            }
        }

        public void Defender()
        {
            this.armadura += 1; // Incrementa la armadura temporalmente
            MostrarFrase(frasesDefensa);

        }

        public void Curar()
        {
            this.salud += 12; // Cura una cantidad fija de salud
            MostrarFrase(frasesCuracion);
            if (this.salud > 100)
            {
                this.salud = 100;
                

            }
        }

        public bool EstaVivo() => this.salud > 0;

        public void RealizarAccionAutomatica(Personaje enemigo)
        {
            Random random = new Random();
            int num = random.Next(1, 7);

            if (num == 6)
            {
                Defender();
                Console.WriteLine($"{this.nombre} se defiende.");
                Thread.Sleep(2000);
            }
            else if (num == 3 || num == 1 && enemigo.GetSalud() < 60)
            {
                Curar();
                Console.WriteLine($"{this.nombre} se cura 12 puntos de salud.");
                Thread.Sleep(2000);
            }
            else
            {
                Atacar(enemigo);
                Console.WriteLine($"{this.nombre} ataca a {enemigo.GetNombre()}.");
                Thread.Sleep(2000);
            }
        }

        public void CurarTotalmente()
        {
            this.salud = 100;
        }
        private void MostrarFrase(List<string> frases)
        {
            int index = random1.Next(frases.Count);
            Console.WriteLine(frases[index]);
        }
    }




