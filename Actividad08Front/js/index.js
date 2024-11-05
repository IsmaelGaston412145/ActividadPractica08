function cargar_contenido(url, callback = null){
    fetch(url)
    .then((res)=>{
        return res.text();
    })
    .then((txt)=>{
        const $panel_content = document.getElementById('contenido')
        $panel_content.innerHTML = txt
        if(callback){
            callback()
        }
    })
}

const login={
    email: 'correo123@gmail.com',
    clave: 'clave123'
}

function verificar_login(){
    const $email = document.getElementById('email')
    const $clave = document.getElementById('clave')

    if($email.value === ''){
        alert('Debe ingresar in email!')
        return false
    }
    if($clave.value === ''){
        alert('Debe ingresar in contraseña!')
        return false
    }
    if($email.value != login.email || $clave.value != login.clave){
        alert('Email o contraseña incorrectas!')
        return false
    }

    return true
}

async function ingresar_servicios(){
    try {
        const response = await fetch('https://localhost:7069/api/Turnos/Servicios');
        const servicios = await response.json();
        const $div = document.getElementById('contenido');
        const $titulo = document.getElementById('tituloDashboard');
        $titulo.textContent = 'Todos los servicios';
        $div.innerHTML = '';
        let rows = '';

        servicios.forEach(e =>{
            console.log(e);
            rows +=
            `<tr>
                <td style="display: none;">${e.idServicio}</td>
                <td>${e.nombre}</td>
                <td>${e.costo}</td>
                <td>${e.enPromocion}</td>
            </tr>
            ` 
        })

        $div.innerHTML =
        `
        <table class="table">
            <thead>
                <tr>
                    <th style="display: none;">idServicio</th>
                    <th>Nombre</th>
                    <th>Costo</th>
                    <th>EnPromocion</th>
                </tr>
            </thead>
            <tbody>
                ${rows}
            </tbody>
        </table>
        `
    } catch (error) {
        console.error("Ha ocurrido un error en el servidor:", error);
    }
}

function validar_servicio(){
    //Pongo el Id en el formulario porque en la base de datos me olvide de ponerle identity a la tabla servicios
    const $idServicio = document.getElementById('idServicio');
    const $nombre = document.getElementById('nombre');
    const $costo = document.getElementById('costo');
    const $enPromo = document.getElementById('enPromocion');
    let agregado = false;
    let enPromo = '';

    if ($enPromo.checked) {
        enPromo = 'S'
    } else {
        enPromo = 'N'
    }
    nuevoServicio = {
        idServicio : $idServicio.value,
        nombre : $nombre.value,
        costo : $costo.value,
        enPromocion : enPromo
    }

    if($idServicio.value === '' || $idServicio.value<1 || $idServicio.value > 2047483648){
        alert('Debe ingresar un id valido!')
        return false
    }
    if($nombre.value === ''){
        alert('Debe ingresar un nombre!')
        return false
    }
    if($costo.value === '' || $costo.value < 1 || $costo.value > 2047483648){
        alert('Debe ingresar una costo valido!')
        return false
    }
    
    try{
        fetch('https://localhost:7069/api/Turnos/Servicios', {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(nuevoServicio) })
        .then(res => {
            if(res.ok){
                alert('Datos agregados con exito!');
                ingresar_servicios();
            } else {
                alert('Error al ingresar el servicio, ingrese correctamente los datos')
            }
        })
    }
    catch(error){
        console.error("Ha ocurrido un error en el servidor:", error);
    }
}