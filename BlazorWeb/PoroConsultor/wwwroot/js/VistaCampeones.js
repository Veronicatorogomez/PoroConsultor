
export function goUp() {
    scrollToTop();
}


export function goDown() {
    scrollToBottom();
}


var isScrollingDown = true;

function scrollToBottom() {
    var alturaActual = document.documentElement.scrollTop;
    var alturaPagina = document.documentElement.scrollHeight - window.innerHeight;

    if (isScrollingDown && alturaActual < alturaPagina) {
        document.documentElement.scrollTop += 200;
        window.requestAnimationFrame(scrollToBottom);
    } else {
        isScrollingDown = false;
        if (alturaActual >= 0) {
            isScrollingDown = true;
        }
    }
}



var isScrollingUp = true;

function scrollToTop() {
    var alturaActual = document.documentElement.scrollTop;

    if (isScrollingUp && alturaActual > 0) {
        document.documentElement.scrollTop -= 100;
        window.requestAnimationFrame(scrollToTop);
    } else {
        isScrollingUp = false;
        var alturaPagina = document.documentElement.scrollHeight - window.innerHeight;
        if (alturaActual <= alturaPagina) {
            isScrollingUp = true;
        }
    }
}