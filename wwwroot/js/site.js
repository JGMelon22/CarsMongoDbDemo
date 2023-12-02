// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let carListButton = document.getElementById("carListButton");
let reportButton = document.getElementById("reportButton");
let loadingSpinner = document.getElementById("loading-spinner");

carListButton.onclick = function () {
    loadingSpinner.style.display = "block";
}

reportButton.onclick = function () {
    loadingSpinner.style.display = "block";
}