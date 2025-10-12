EXTERNAL SetFaseActual(nuevaFase)
EXTERNAL GetFase()

-> elegir_fase

=== elegir_fase ===
~ temp fase = GetFase()

{
- fase == 0:
    -> F0
- fase == 1:
    -> F1
- fase == 2:
    -> F2
- fase == 3:
    -> F3
- fase == 4:
    -> F4
- fase == 5:
    -> F5
- fase == 6:
    -> F6
- fase == 7:
    -> F7   
- fase == 8:
    -> F8  
- fase == 9:
    -> F9
- fase == 10:
    -> F10   
}

=== F0 ===
//Inicio - Cuando esta todo vacio
Comencemos con la personalización. Vení seguime, te va a gustar, vas a ver unos lindos portales.
Todo arranca por el suelo que pisás… ¿Cómo querés que te reciba este lugar?
~ SetFaseActual(1)
-> END

=== F1 ===
//Instancia 1 - Si intentas hablar durante la instancia
Elegí entre las 3 opciones que tenés. ¿Cuál preferís?
-> END

=== F2 ===
//Instancia 1 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
¡Muy buena elección! Me gusta. 
Ahora levantá la mirada. Lo que te rodea también es importante. Los colores, las texturas, las formas… todo influye en cómo te sentís. Terminemos de darle vida a este espacio.
~ SetFaseActual(1)
-> END

=== F3 ===
//Instancia 2
¿Lindas pantallas no? Acercate a la botonera y elegí lo que más te guste.
-> END

=== F4 ===
//Instancia 2 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
Esoooo, me gusta me gusta. Yo hubiera elegido lo mismo jajaja. Vení seguime, esto te va a interesar bastante.
~ SetFaseActual(1)
-> END

=== F5 ===
//Instancia 3 - Antes del desbloqueo - Rino se dirige a la instancia 3 y el jugador debe interactuar antes de que se desbloquee
Acá vamos a poner algunos objetos que te gustaría tener a la vista. Cosas tuyas. Cosas que te acompañen. Elegí lo que querés que viva en esta estantería.
~ SetFaseActual(1)
-> END

=== F6 ===
//Instancia 3
Acá podés dejar eso que querés tener a la vista. Cosas tuyas. Cosas que te acompañan, aunque no hablen. Elegí lo que querés que viva en esta estantería.
-> END

=== F7 ===
//Instancia 3 - Fin
fase7 - continuar
~ SetFaseActual(1)
-> END

=== F8 ===
//Instancia 4 
Elegí la musica que quieras y volvé a hablarme cuando encuentres la que mas te guste, igualmente podés cambiarla en cualquier momento de la experiencia
-> END

=== F9 ===
//Instancia 4 - Fin
fase9 - continuar
~ SetFaseActual(1)
-> END

=== F10 ===
//Instancia 5
Instancia5
-> END