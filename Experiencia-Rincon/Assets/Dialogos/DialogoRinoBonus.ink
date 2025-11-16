EXTERNAL SetFaseActual(nuevaFase)
EXTERNAL GetFase()
EXTERNAL ReproducirDialogo(numeroLineaDeVoz)
EXTERNAL OcultarBotonesInst1()

-> elegir_fase

=== elegir_fase ===
~ temp fase = GetFase()

{
- fase == 0:
    -> FB1
- fase == 1:
    -> FB1
- fase == 2:
    -> FB2
}


=== FB1 ===
//RINCON BONUS
~ ReproducirDialogo(35)
Pasá y sentate cómodo, vamos a ver una peli. Dejame que te traigo unos pochoclos.
~ SetFaseActual(1)
-> END

=== FB2 ===
//RINCON BONUS
~ ReproducirDialogo(36)
Me alegra que hayas encontrado tu propio rincón.
//~ ReproducirDialogo(37)
//Espero que hayas disfrutado de este proceso tanto como yo disfruté acompañarte.
~ ReproducirDialogo(37)
Mi trabajo termina acá, pero todavía hay más sorpresas para vos.
~ ReproducirDialogo(38)
¿Te acordás de la caja en la que llegó la llave de tu Rincón? Mirá, te explico:
//~ ReproducirDialogo(25)
//Llegó el momento de abrirla y disfrutar de los últimos detalles de tu rincón.
-> END