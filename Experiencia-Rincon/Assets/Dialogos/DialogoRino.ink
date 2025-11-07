EXTERNAL SetFaseActual(nuevaFase)
EXTERNAL GetFase()
EXTERNAL ReproducirDialogo(numeroLineaDeVoz)
EXTERNAL OcultarBotonesInst1()

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
    -> FB1  
- fase == 16:
    -> FB2 
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
Esoooo, me gusta me gusta. Yo hubiera elegido lo mismo jajaja. Vení seguime, esto te va a interesar bastante.
~ SetFaseActual(1)
-> END

//Cambiar a partir de aca

=== F6 ===
//Instancia 3 - Antes del desbloqueo - Rino se dirige a la instancia 3 y el jugador debe interactuar antes de que se desbloquee
~ ReproducirDialogo(7)
Acá vamos a poner algunos objetos que te gustaría tener a la vista. Cosas tuyas. Cosas que te acompañen. Elegí lo que querés que viva en esta estantería.
~ SetFaseActual(1)
-> END

=== F7 ===
//Instancia 3 - Si intentas hablar durante la instancia
~ ReproducirDialogo(8)
Está difícil elegir acá, ¿no? Son bastantes objetos, ni yo me decido jaja.
-> END

=== F8 ===
//Instancia 3 - Fin - Después de dar confirmación - Si se puede que aparezca automático mejor
~ ReproducirDialogo(9)
Tremendo, lo dejaste impecable. De a poco ya va tomando forma este lugar. Continuemos. Podríamos escuchar unas canciones mientras, ¿No? Así se nos hace más divertido. Seguime.
~ SetFaseActual(1)
-> END

=== F9 ===
//Instancia 4 - Antes del desbloqueo
~ ReproducirDialogo(10)
Elegí la música que quieras y volvé a hablarme cuando encuentres la que más te guste, igualmente podés cambiarla en cualquier momento.
-> END

=== F10 ===
//Instancia 4 - Fin
~ ReproducirDialogo(11)
Uuuuh, que buena canción. Si que tenés buen gusto eeeh.
Continuemos, esto me está gustando cada vez más y empieza a parecerse más a vos. Ya se está sintiendo tuyo.
~ SetFaseActual(1)
-> END

=== F11 ===
//Instancia 5 - Antes del desbloqueo
~ ReproducirDialogo(12)
Todavía no terminamos de arreglar el lugar eh, hay cosas que ni vimos. 
~ ReproducirDialogo(13)
Uh, había preparado unos cuadros pero al parecer se cayeron, ¿podrías ir a colgarlos de nuevo?
-> END

=== F111 ===
//Instancia 5 - Después de colgar los marcos - Si se puede que aparezca automático mejor
~ ReproducirDialogo(14)
¿Qué opinas? Te preparé estos cuadros porque pensé que te podría llegar a gustar tenerlos a la vista.
~ ReproducirDialogo(15)
Ya casi terminas de armar tu rincón, me encanta cómo nos está quedando. Vení que hay una cosita más que te va a interesar.
~ SetFaseActual(1)
-> END

=== F12 ===
//Instancia 6/7 - Antes del desbloqueo
//Sonido de hambre
~ ReproducirDialogo(16)
Che tengo un poco de hambre, y supongo que vos también. ¿Te parece si terminamos con algo rico?
-> END

=== F13 ===
//Instancia 6/7 Después de dar confirmación - Si se puede que aparezca automático mejor
~ ReproducirDialogo(17)
Ufff que ricooo. Me encantó. Bueno, ya llegamos al final, poco a poco fuiste personalizando ese lugar gris y oscuro en el que empezó todo. Me encanta cómo lo dejaste, te armaste un hermoso rincón.
~ ReproducirDialogo(18)
Antes de salir de tu rincón, me gustaría que hagas un pequeño recorrido extra para apreciarlo más a detalle y que sepas que este lugar es tuyo para siempre. Cuando termines de recorrerlo, vení que te llevo a un lugar que nos queda pendiente.
~ SetFaseActual(1)
-> END

=== F14 ===
//Después de recorrer el resultado final
~ ReproducirDialogo(19)
Pasá sin miedo, yo te voy a estar esperando del otro lado.
~ SetFaseActual(1)
//Fase 15 usarla para que se mueva a traves de la puerta
-> END

=== FB1 ===
//RINCON BONUS
~ ReproducirDialogo(20)
Pasá y sentate cómodo, vamos a ver una peli. Dejame que te traigo unos pochoclos.
-> END

=== FB2 ===
//RINCON BONUS
~ ReproducirDialogo(21)
Me alegra que hayas encontrado tu propio rincón.
~ ReproducirDialogo(22)
Espero que hayas disfrutado de este proceso tanto como yo disfruté acompañarte.
~ ReproducirDialogo(23)
Mi trabajo termina acá, pero todavía tengo una sorpresa más para vos.
~ ReproducirDialogo(24)
¿Te acordás de la caja en la que llegó la llave de tu Rincón?
~ ReproducirDialogo(25)
Llegó el momento de abrirla y disfrutar de los últimos detalles de tu rincón.
-> END