
export function scrollToBottom() {
    window.scroll({
        top: 10000,
        behavior: "smooth"
    });
    
}

export function scrollToTop() {
    window.scroll({
        top: 0,
        behavior: "smooth"
    });
}