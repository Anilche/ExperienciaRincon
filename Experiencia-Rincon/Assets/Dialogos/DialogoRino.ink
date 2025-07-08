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
Bienvenido
Inicio
~ SetFaseActual(1)
-> END

=== F1 ===
fase1 - eleccion
-> END

=== F2 ===
fase2 - continuar
~ SetFaseActual(1)
-> END

=== F3 ===
fase3 - eleccion
-> END

=== F4 ===
fase4 - continuar
~ SetFaseActual(1)
-> END

=== F5 ===
fase5 - simulacro final
-> END

=== F6 ===
fase6 - simulacro final
-> END