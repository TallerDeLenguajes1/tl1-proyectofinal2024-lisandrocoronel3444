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
Api consumida: https://ddragon.leagueoflegends.com/cdn/12.6.1/data/en_US/champion.json
De esta API obtenemos nombres del famoso juego League Of leguends y los utilizo para mis 10 personajes, si la API llegara a fallar el programa utiliza nombres predefinidos.
Hago uso de 3 archivos json: "personajes.json" el cual al inciar el programa se crea este archivo con una lista de 10 personajes y sus caracteristicas, a medida que el juego avanza y los personajes van muriendo estos se borran del archivo.
"frases.json" El cual fue creado por mi para guardar diferentes frases de ataque, defensa y curacion para hacer mas entretenido el juego
"historial.json" Este archivo es utilizado para guardar los datos de todos los ganadores pero al mostrarlo solo nos encargamos de mostrar el nombre y su cantidad de victorias.



##Lisandro Coronel 
