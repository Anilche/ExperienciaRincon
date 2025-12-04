EXTERNAL SetFaseActual(nuevaFase)
EXTERNAL GetFase()
EXTERNAL ReproducirDialogo(numeroLineaDeVoz)
EXTERNAL OcultarBotonesInst1()
EXTERNAL ApagarLuces()
EXTERNAL ActivarBandejaBebidas()
EXTERNAL ActivarCuadrosTirados()

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
- fase == 11:
    -> F11  
- fase == 12:
    -> F12
- fase == 13:
    -> F13
- fase == 14:
    -> F14  
- fase == 15:
    -> F15
- fase == 16:
    -> F16 
- fase == 17:
    -> F17
- fase == 18:
    -> F18 
- fase == 19:
    -> F19
- fase == 20:
    -> F20  
- fase == 21:
    -> F21
- fase == 22:
    -> F22 
}

=== F0 ===
//Inicio - Cuando esta todo vacio
~ ReproducirDialogo(0)
Comencemos con la personalización. Vení seguime, te va a gustar, vas a ver unos lindos portales.
~ ReproducirDialogo(1)
Todo arranca por el suelo que pisás… ¿Cómo querés que te reciba este lugar?
~ SetFaseActual(1)
-> END

=== F1 ===
//Instancia 1 - Si intentas hablar durante la instancia
~ ReproducirDialogo(2)
Elegí entre las 3 opciones que tenés. ¿Cuál preferís?
-> END

=== F2 ===
//Instancia 1 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
~ ReproducirDialogo(3)
¡Muy buena elección! Me gusta.
~ ReproducirDialogo(4)
No te olvides de mirar un poco tus alrededores al ir armando tu rincón para familiarizarte con el lugar y volvé a hablarme cuando estés listo para continuar.
~ SetFaseActual(1)
-> END

=== F3 ===
//Instancia 1 - Fin - Al hablar de nuevo
~ OcultarBotonesInst1()
~ ReproducirDialogo(5)
Ahora levantá la mirada. Lo que te rodea también es importante. Los colores, las texturas, las formas… todo influye en cómo te sentís. Terminemos de darle vida a este espacio.
~ SetFaseActual(1)
-> END

//Acá bajan las pantallas

=== F4 ===
//Instancia 2 - Si intentas hablar durante la instancia
~ ReproducirDialogo(6)
¿Lindas pantallas no? Acercate a la botonera y elegí lo que más te guste.
-> END

=== F5 ===
//Instancia 2 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
~ ReproducirDialogo(7)
Esoooo, me gusta me gusta. Yo hubiera elegido lo mismo jajaja.
~ ReproducirDialogo(8)
Me encanta como va tomando forma el lugar, ya se va pareciendo más a vos.
~ ReproducirDialogo(9)
Vení seguime, esto te va a interesar bastante.
~ ReproducirDialogo(10)
//[Sonido raro, como de chispazos]
//~ reproducirSonido
Oh, oh ¿escuchás eso?
~ ReproducirDialogo(11)
Parece que algo extraño está sucediendo
~ ApagarLuces()
~ ReproducirDialogo(12)
Uh! Nos quedamos sin luz!
~ ReproducirDialogo(13)
Va a estar difícil seguir si no podemos ver por dónde vamos, ¿podrías arreglarlo?
~ SetFaseActual(1)
-> END

=== F6 ===
//Instancia disruptiva - Si intentas hablar durante la instancia
~ ReproducirDialogo(14)
Todavía no volvió la luz, ¿no pudiste arreglarlo?.
-> END

=== F7 ===
//Instancia disruptiva Fin
~ ReproducirDialogo(15)
Bueno, problema resuelto! A veces crear nuestro propio rincón puede ser un poco caótico, pero el caos es parte del proceso.
~ ReproducirDialogo(16)
Ahora que ya está todo bien, continuemos.
~ SetFaseActual(1)
-> END

//Rino se mueve a inst3

=== F8 ===
///Instancia 3 - Antes del desbloqueo
~ ReproducirDialogo(17)
Acá vamos a poner algunos objetos que te gustaría tener a la vista. Cosas tuyas. Cosas que te acompañen. Elegí lo que querés que viva en esta estantería.
~ SetFaseActual(1)
-> END

=== F9 ===
//Instancia 3 - Si intentas hablar durante la instancia
~ ReproducirDialogo(18)
Está difícil elegir acá, ¿no? Son bastantes objetos, ni yo me decido jaja.
-> END

=== F10 ===
//Instancia 3 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
~ ReproducirDialogo(19)
Tremendo, lo dejaste impecable. De a poco ya va tomando forma este lugar. Continuemos. Podríamos escuchar unas canciones mientras, ¿No? Así se nos hace más divertido. Seguime.
~ SetFaseActual(1)
-> END

//Rino se mueve a la instancia 4

=== F11 ===
//Instancia 4
~ ReproducirDialogo(20)
Elegí la música que quieras y volvé a hablarme cuando encuentres la que más te guste, igualmente podés cambiarla en cualquier momento.
-> END
 
=== F12 ===
//Instancia 4 - Fin
~ ReproducirDialogo(21)
Uuuuh, que buena canción. Si que tenés buen gusto eeeh.
~ ReproducirDialogo(22)
Esto me está gustando cada vez más y empieza a parecerse más a vos. Ya se está sintiendo tuyo.
~ ReproducirDialogo(23)
No te olvides de interactuar un poco con tus alrededores.
~ SetFaseActual(1)
-> END

//Instancia 4.1 - Al interactuar Fase+1 e Inicia instancia descanso

=== F13 ===
//Instancia descanso
~ ReproducirDialogo(24)
Una parte importante de tu rincón también es el descanso.
~ ActivarBandejaBebidas()
~ ReproducirDialogo(25)
No sabía que te gustaba tomar, así que preparé de todo, ¿Con qué te relajás más?
~ SetFaseActual(1)
-> END

=== F14 ===
//Instancia descanso - Si intentas hablar durante la instancia
~ ReproducirDialogo(25)
No sabía que te gustaba tomar, así que preparé de todo, ¿Con qué te relajás más?
-> END

//F14 - Outlines en bebidas, animación bebida elegida, después F+1

=== F15 ===
//Instancia descanso - Fin
~ ReproducirDialogo(26)
¡Qué buena decisión! Ya tenemos las energías renovadas para continuar y hacer de este espacio tu lugar en el mundo. Vení seguime.
~ SetFaseActual(1)
-> END

//f16 Rino se mueve a inst5

=== F16 ===
//Instancia 5 - Antes del desbloqueo
~ ReproducirDialogo(27)
Todavía no terminamos de arreglar el lugar eh, hay cosas que ni vimos. 
~ ReproducirDialogo(28)
Uh, había preparado unos cuadros pero al parecer se cayeron, ¿podrías ir a colgarlos de nuevo?
~ ActivarCuadrosTirados()
~ SetFaseActual(1)
-> END

=== F17 ===
//Instancia 5 - Si intentas hablar durante la instancia
~ ReproducirDialogo(28)
Uh, había preparado unos cuadros pero al parecer se cayeron, ¿podrías ir a colgarlos de nuevo?
-> END

=== F18 ===
//Instancia 5 - Después de colgar los marcos - Si se puede que aparezca automático mejor
~ ReproducirDialogo(29)
¿Qué opinas? Te preparé estos cuadros porque pensé que te podría llegar a gustar tenerlos a la vista.
~ ReproducirDialogo(30)
Ya casi terminas de armar tu rincón, me encanta cómo nos está quedando. Vení que hay una cosita más que te va a interesar.
~ SetFaseActual(1)
-> END

//F19 - Rino se mueve a instancia 6 - Base central

=== F19 ===
//Instancia 6 - Antes del desbloqueo
~ ReproducirDialogo(31)
Che tengo un poco de hambre, y supongo que vos también. ¿Te parece si terminamos con algo rico?
~ SetFaseActual(1)
-> END

=== F20 ===
//Instancia 6 - Si intentas hablar durante la instancia
~ ReproducirDialogo(31)
Che tengo un poco de hambre, y supongo que vos también. ¿Te parece si terminamos con algo rico?
-> END

//F20 - Aparece la pantalla para seleccionar la “comida” (base central) / Usamos el sistema de elección del prototipo. Desde acá aumenta a fase 21. Que no se pueda salir del seleccionador

=== F21 ===
//Instancia 6 Después de dar confirmación
~ ReproducirDialogo(32)
Ufff que ricooo. Me encantó. Bueno, ya llegamos al final, poco a poco fuiste personalizando ese lugar gris y oscuro en el que empezó todo. Me encanta cómo lo dejaste, te armaste un hermoso rincón.
~ ReproducirDialogo(33)
Antes de salir de tu rincón, me gustaría que hagas un pequeño recorrido extra para apreciarlo más a detalle y que sepas que este lugar es tuyo para siempre. Cuando termines de recorrerlo, vení que te llevo a un lugar que nos queda pendiente.
~ SetFaseActual(1)
-> END

//F22 - Aparece la puerta del rincón bonus y Rino se mueve hasta la puerta del rincón bonus

=== F22 ===
//Después de recorrer el resultado final
~ ReproducirDialogo(34)
Pasá sin miedo, yo voy atrás tuyo.
-> END

//El jugador va hacia la puerta, cambia la escena