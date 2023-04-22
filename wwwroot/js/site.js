// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let btnListCars = document.getElementById("CarsList");
let btnLoading = document.getElementById("LoadingButton");

btnListCars.onclick = function () {
    btnLoading.style.display = "block";
}
