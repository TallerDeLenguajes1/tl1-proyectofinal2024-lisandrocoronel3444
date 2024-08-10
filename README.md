# LA BATALLA DE LOS 10 GUERREROS LEGENDARIOS
### Historia:
En un mundo en guerra, diez guerreros legendarios se enfrentaron por el trono de hierro. Solo uno sería el elegido, encargado de derrotar a los otros nueve para reclamar el trono y llevar la paz al reino. Con cada batalla, el campo se teñía de sangre y leyendas se forjaban. Al final, solo un guerrero quedó en pie, empuñando su arma con orgullo y mirando hacia el trono. Su victoria no solo le otorgó el trono, sino también la responsabilidad de unir un reino dividido. Con el trono de hierro bajo su control, el guerrero victorioso se convertira en el símbolo de esperanza y poder, marcando el inicio de una nueva era.
### Modalidad:
El usuario elije entre 10 guerreros legendarios con diferentes caracteristicas y el guerrero seleccionado es el encargado de enfrentarse a los 9 restantes en una batalla por turnos.
Tienes tres opciones atacar, defender o curarte.
Al atacar se lanzara un dado del 1 al 6 para aumentar el porcentaje de daño.
Defenderte aumenta la defensa de tu armadura en 1 punto
y por ultimo, curarse regenera 12 puntos de vida.
Cada vez que derrotes un guerrero legendario tu salud volvera a 100 para enfrentarte al siguiente.
¿Podras ser el ultimo guerrero que quede en pie?
### Datos tecnicos:
NET Version: .NET 6.0 o superior
Editor de Código: Visual Studio 2022 / Visual Studio Code / Otro IDE compatible con .NET
# Estructura del Proyecto
Lenguaje de Programación: C#
Framework: .NET Core / .NET 6
# Organización del Código:
campoBatalla.cs: Implementa la lógica del campo de batalla.
HistorialJson.cs: Maneja la persistencia de datos de los ganadores en formato JSON.
personajes.cs: Define la clase Personaje con sus propiedades y métodos.
personajesJson.cs: Maneja la carga y guardado de personajes en formato JSON.
program.cs: Contiene el punto de entrada principal y el flujo del programa.
# API Externa
API Utilizada: Dragon API de League of Legends
URL: https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json
Uso: Obtiene nombres de personajes para el juego.
# Archivos JSON
frases.json: Contiene frases de ataque, defensa y curación utilizadas por los personajes.
# Métodos y Funcionalidades
Métodos de Personaje:

Atacar(Personaje enemigo): Realiza un ataque al enemigo y muestra una frase de ataque.
Defender(): Incrementa temporalmente la armadura y muestra una frase de defensa.
Curar(): Recupera salud y muestra una frase de curación.
RealizarAccionAutomatica(Personaje enemigo): Determina la acción automática que el personaje realizará en el turno.
Manejo de Excepciones: La carga de frases y la obtención de nombres de personajes están protegidas contra fallos y excepciones.



# Lisandro Coronel 
