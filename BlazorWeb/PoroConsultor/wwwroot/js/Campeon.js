﻿export function playVideoBucle() {
    const video = document.getElementsByTagName("video")[0];
    video.play();
    console.log("hola")
}

export function muteVideo() {
    const video = document.getElementsByTagName("video")[0];
    video.muted = true;
}