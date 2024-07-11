using System.Collections;

namespace personasjesDelJuego;
public class Personaje
{
    // Datos
    private string tipo;
    private string nombre;
    private string apodo;
    private DateTime fechaDeNacimiento;
    private int edad;

    // Características
    private int velocidad;
    private int destreza;
    private int fuerza;
    private int armadura;
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
    public void Atacar(Personaje Enemigo){
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
            int ataque = (int)(ataqueBase * multiplicador);
            int defensa = Enemigo.GetArmadura() * Enemigo.GetVelocidad();
            const int constanteDeAjuste = 500;

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
            this.salud += 10; // Cura una cantidad fija de salud
            if (this.salud > 100)
            {
                this.salud = 100;
            }
        }
        public bool EstaVivo()
        {
            return this.salud > 0;
        }

    }
