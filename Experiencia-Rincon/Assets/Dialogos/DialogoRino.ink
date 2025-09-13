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
//Inicio
Bienvenido
~ SetFaseActual(1)
-> END

=== F1 ===
//Instancia 1
Todo arranca por el suelo que pisás… ¿Cómo querés que te reciba este lugar?
-> END

=== F2 ===
//Instancia 1 - Fin
fase2 - continuar
Ahora levantá la mirada. Lo que te rodea también importa. Colores, texturas, formas… todo influye en cómo te sentís. Vamos a terminar de darle vida a este espacio.
~ SetFaseActual(1)
-> END

=== F3 ===
//Instancia 2
Ahora levantá la mirada. Lo que te rodea también importa. Colores, texturas, formas… todo influye en cómo te sentís. Vamos a terminar de darle vida a este espacio.
-> END

=== F4 ===
//Instancia 2 - Fin
fase4 - continuar
~ SetFaseActual(1)
-> END

=== F5 ===
//Rino se dirige al centro
Así está mejor, ¿no? Este lugar empieza a parecerte familiar. Y eso es porque ya tiene algo tuyo. Vamos al próximo paso.
~ SetFaseActual(1)
-> END

=== F6 ===
//Instancia 3
INSTANCIA 3
Acá podés dejar eso que querés tener a la vista. Cosas tuyas. Cosas que te acompañan, aunque no hablen. Elegí lo que querés que viva en esta estantería.
~ SetFaseActual(1)
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