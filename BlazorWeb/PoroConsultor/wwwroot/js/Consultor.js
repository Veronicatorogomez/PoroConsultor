﻿export function remover(ganador1) {
    var vida = ganador1;
    vida.classList.add("ganador");
    
}

export function quitarClase() {
    const p = document.getElementsByTagName("p");
    for (var item of p) {
        item.classList.remove("ganador");
    } 
}

