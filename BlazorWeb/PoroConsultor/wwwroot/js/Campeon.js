export function muteVideo() {
    const video = document.getElementsByTagName("video")[0];
    video.muted = true;
}

export function loadAnotherVideo(linkVideo) {
    var video = document.getElementsByTagName('video')[0];
    var sources = video.getElementsByTagName('source');
    sources[0].src = linkVideo;
    video.load();
}


export function checkScroll() {
    const imagenes = document.querySelectorAll(".animacion");
    let scrollTop = document.documentElement.scrollTop;
    for (let i = 0; i < imagenes.length; i++) {
        let alturaImagen = imagenes[i].offsetTop;
        if (alturaImagen - 500 < scrollTop) {
            imagenes[i].classList.add("mostrarDerecha");
        }
    }
};
window.addEventListener('scroll', checkScroll);

export function existeEasteregg() {
    const poro = document.querySelector(".poroContainer")
    let existe = false;
    if (poro != null) {
        existe = true;
    }
    return existe;
}