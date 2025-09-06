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
-> END

=== F6 ===
//Instancia 3
Acá podés dejar eso que querés tener a la vista. Cosas tuyas. Cosas que te acompañan, aunque no hablen. Elegí lo que querés que viva en esta estantería.
-> END

=== F7 ===
//Instancia 3 - Fin
fase7 - continuar
-> END