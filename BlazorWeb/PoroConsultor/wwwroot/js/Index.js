const imagenes = document.querySelectorAll(".imagen");
export function checkScroll() {
    let scrollTop = document.documentElement.scrollTop;
    for (let i = 0; i < imagenes.length; i++) {
        let alturaImagen = imagenes[i].offsetTop;
        if (alturaImagen - 500 < scrollTop) {
            imagenes[i].style.opacity = 1;
            imagenes[i].classList.add("mostrarDerecha");
        }
      
    }
    
};
window.addEventListener('scroll', checkScroll);


//window.addEventListener('DOMContentLoaded', function () {
//    var video = document.getElementById('myVideo'); // Reemplaza 'myVideo' con el ID de tu elemento de video
//});
export function cargarVideo() {


    const video = document.getElementsByTagName("video")[0];
    video.play();
        //video.addEventListener('loadedmetadata', function () {
        //    video.loop = true; // Establece la propiedad loop en true para que se reproduzca en bucle
        //    video.play(); // Inicia la reproducción del video
        //});
}

