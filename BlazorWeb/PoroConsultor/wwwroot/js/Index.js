
export function checkScroll() {
    const imagenes = document.querySelectorAll(".imagen");
    let scrollTop = document.documentElement.scrollTop;
    for (let i = 0; i < imagenes.length; i++) {
        let alturaImagen = imagenes[i].offsetTop;
        if (alturaImagen - 500 < scrollTop) {
            imagenes[i].classList.add("mostrarDerecha");
        }
    }
};
window.addEventListener('scroll', checkScroll);

const alerta = document.getElementById("alerta");
export function cargarAlerta() {

   
    alerta.style.display = "block";

}

export function cerrarAlerta() {

    const alerta = document.getElementById("alerta");
    alerta.style.display = "none";
    const video = document.getElementsByTagName("video")[0];
    video.play();

}

export function quitarSonido() {

    const video = document.getElementsByTagName("video")[0];
    if (video.muted == true) {
        video.muted = false;
    }
    else {
        video.muted = true;
    }

}





