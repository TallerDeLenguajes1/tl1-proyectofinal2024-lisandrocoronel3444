using System.Collections;

namespace personasjesDelJuego;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

    public class Personaje
    {
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

        // Constructor
        public Personaje(string tipo, string nombre, string apodo, DateTime fechaDeNacimiento, int velocidad, int destreza, int fuerza, int armadura)
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
            double multiplicador = suerte 
            switch
            {
                1 => 0.15,
                2 => 0.30,
                3 => 0.45,
                4 => 0.60,
                5 => 0.75,
                6 => 1.00,
                _ => 0
            };

            int ataque = (int)(ataqueBase + (multiplicador * ataqueBase));
            int defensa = Enemigo.GetArmadura() * Enemigo.GetVelocidad();
            const int constanteDeAjuste = 5;

            int danio = ((ataque) - defensa) / constanteDeAjuste;
            if (danio > 0)
            {
                Enemigo.RecibirDanio(danio);
                Console.WriteLine($"{this.nombre} provoca {danio} de daño a {Enemigo.GetNombre()} con un golpe de dado {suerte} (multiplicador {multiplicador * 100}%)");
            }
            else
            {
                Console.WriteLine($"{Enemigo.GetNombre()} bloqueó el ataque!");
            }
        }

        public void RecibirDanio(int danio)
        {
            this.salud -= danio;
            if (this.salud < 0)
            {
                this.salud = 0;
            }
        }

        public void Defender()
        {
            this.armadura += 2; // Incrementa la armadura temporalmente
        }

        public void Curar()
        {
            this.salud += 15; // Cura una cantidad fija de salud
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
            }
            else if (num == 3 || num == 1)
            {
                Curar();
                Console.WriteLine($"{this.nombre} se cura 15 puntos de salud.");
            }
            else
            {
                Atacar(enemigo);
                Console.WriteLine($"{this.nombre} ataca a {enemigo.GetNombre()}.");
            }
        }

        public void CurarTotalmente()
        {
            this.salud = 100;
        }
    }




