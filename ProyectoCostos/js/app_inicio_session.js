// Variables
const txtContrasena = document.getElementById('txtContrasena');
const txtUsuario = document.getElementById('txtUsuario');

const formularioInicioSesion = document.getElementById('formularioInicioSesion');


// Se establece el evento submit para el formulario de editar cliente y prevenimos el envio de datos
formularioInicioSesion.addEventListener('submit', (e) => {

    e.preventDefault();
    e.stopPropagation();
    validarCampos();

})

const validarCampos = () => {
    // Captura los valores ingresados por el usuario
    const contrasenaValor = txtContrasena.value.trim();
    const usuarioValor = txtUsuario.value.trim();

    // Banderas que indican que los datos por el usuario fueron ingresados correctamente
    let banderaUno = false;
    let banderaDos = false;

    !usuarioValor ? (validarErroneo(txtUsuario, 'Campo requerido'), banderaUno = false) : (validarCorrecto(txtUsuario), banderaUno = true)
    !contrasenaValor ? (validarErroneo(txtContrasena, 'Campo requerido'), banderaDos = false) : (validarCorrecto(txtContrasena), banderaDos = true)

    banderaUno && banderaDos ? btnIniciarSesion() : ''

}

// Valida que el usuario haya ingresado datos en el campo de texto indicado
const validarErroneo = (input, mensaje) => {

    const formControl = input.parentElement;
    const advertencia = formControl.querySelector('small');
    advertencia.innerText = mensaje;
    input.className = 'form-control is-invalid';

}

// Valida que el usuario haya ingresado datos en el campo de texto indicado
const validarCorrecto = (input) => {
    const formControl = input.parentElement;
    const advertencia = formControl.querySelector('small');
    advertencia.innerText = '';
    input.className = 'form-control is-valid';
}


const btnIniciarSesion = () => {

    let datosFormularioInicioSesion = new FormData(formularioInicioSesion);
    fetchIniciarSesion(datosFormularioInicioSesion);

}


const fetchIniciarSesion = async (datosFormularioInicioSesion) => {
    try {

        var root = '@Url.Content("~")';
        const url = '/InicioSesion/iniciarSesion';
        const response = await fetch(`${ root,url }`, {
            method: 'POST',
            body: datosFormularioInicioSesion
        })
        const data = await response.json();
        const mensajeError = document.querySelector('.text-danger');

        const url2 = '/Clientes/Index';
        
        // Si la data es 1 redirecciona a la pagina de clientes sino, mostrara un mensaje
        data == 1 ? location.href = `${ root, url2 }` : mensajeError.textContent = data;


    } catch (error) { console.log(error); }
}