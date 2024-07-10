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
    public Personaje(string tipo, string nombre, string apodo, DateTime fechaDeNacimiento, int velocidad, int destreza, int fuerza, int nivel, int armadura)
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
}