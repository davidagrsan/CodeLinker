const formPaso1 = document.getElementById('form-paso-1')
const formPaso2 = document.getElementById('form-paso-2')
const formPaso3 = document.getElementById('form-paso-3')
const paso1 = document.getElementById('paso-1')
const paso2 = document.getElementById('paso-2')
const paso3 = document.getElementById('paso-3')
const botonesContinuar = document.querySelectorAll('.boton-continuar')
const botonesAtras = document.querySelectorAll('.boton-atras')

function mostrarPaso1() {
    paso1.classList.add('tarjeta__paso-1')
    paso1.classList.remove('tarjeta__paso-1--hidden')
}

function ocultarPaso1() {
    paso1.classList.add('tarjeta__paso-1--hidden')
    paso1.classList.remove('tarjeta__paso-1')
}

function mostrarPaso2() {
    paso2.classList.add('tarjeta__paso-2')
    paso2.classList.remove('tarjeta__paso-2--hidden')
}

function ocultarPaso2() {
    paso2.classList.add('tarjeta__paso-2--hidden')
    paso2.classList.remove('tarjeta__paso-2')
}

function mostrarPaso3() {
    paso3.classList.add('tarjeta__paso-3')
    paso3.classList.remove('tarjeta__paso-3--hidden')
}

function ocultarPaso3() {
    paso3.classList.add('tarjeta__paso-3--hidden')
    paso3.classList.remove('tarjeta__paso-3')
}

botonesContinuar.forEach(boton => {
    boton.addEventListener('click', () => {
        if (paso1.classList.contains('active')) {
            paso1.classList.remove('active')
            paso2.classList.add('active')
    
            ocultarPaso1()
            mostrarPaso2()
    
            formPaso1.classList.add('ocultar')
            formPaso2.classList.remove('ocultar')
        } else if (paso2.classList.contains('active')) {
            paso2.classList.remove('active')
            paso3.classList.add('active')
            
            ocultarPaso2()
            mostrarPaso3()
    
            formPaso2.classList.add('ocultar')
            formPaso3.classList.remove('ocultar')
        }
    })
})

botonesAtras.forEach(boton => {
    boton.addEventListener('click', () => {
        if (paso2.classList.contains('active')) {
            paso2.classList.remove('active')
            paso1.classList.add('active')

            ocultarPaso2()
            mostrarPaso1()

            formPaso2.classList.add('ocultar')
            formPaso1.classList.remove('ocultar')
        } else if (paso3.classList.contains('active')) {
            paso3.classList.remove('active')
            paso2.classList.add('active')

            ocultarPaso3()
            mostrarPaso2()

            formPaso3.classList.add('ocultar')
            formPaso2.classList.remove('ocultar')
        }
    })
})
