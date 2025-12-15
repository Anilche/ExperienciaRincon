EXTERNAL SetFaseActual(nuevaFase)
EXTERNAL GetFase()
EXTERNAL ReproducirDialogo(numeroLineaDeVoz)
EXTERNAL PasarEscenaA(escena)
EXTERNAL FadeOutVideoBoxset()

-> elegir_fase

=== elegir_fase ===
~ temp fase = GetFase()

{
- fase == 23:
    -> FB1
- fase == 24:
    -> FB12
- fase == 25:
    -> FB2
- fase == 26:
    -> FB22
}

=== FB1 ===
//RINCON BONUS
~ ReproducirDialogo(35)
Pasá y sentate cómodo, vamos a ver una peli. Dejame que te traigo unos pochoclos.
~ SetFaseActual(1)
-> END

=== FB12 ===
//RINCON BONUS
~ ReproducirDialogo(35)
Pasá y sentate cómodo, vamos a ver una peli. Dejame que te traigo unos pochoclos.
-> END

=== FB2 ===
//RINCON BONUS
~ ReproducirDialogo(36)
Me alegra que hayas encontrado tu propio rincón.
~ ReproducirDialogo(37)
Antes de que te vayas, quería mostrarte el mío.
~ ReproducirDialogo(38)
Tomate tu tiempo para verlo y volvé a hablarme cuando ya estés listo para despedirnos.
~ SetFaseActual(1)
-> END

=== FB22 ===
//RINCON BONUS
~ ReproducirDialogo(39)
Bueno, llegó el momento.
~ ReproducirDialogo(40)
Mi trabajo termina acá, pero todavía hay más sorpresas para vos.
~ ReproducirDialogo(41)
¿Te acordás de la caja en la que llegó la llave de tu Rincón? Mirá, te explico:
~ FadeOutVideoBoxset()
//~ PasarEscenaA("VideoBoxset")
-> END