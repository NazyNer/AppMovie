// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function eliminarSeccion(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta seccion?");
    if (eliminar) {
    location.href = "../../Section/DeleteConfirmed/" + id;}
}
function eliminaUbicacion(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Ubicacion?");
    if (eliminar) {
    location.href = "../../Location/DeleteConfirmed/" + id;}
}
function eliminarGenero(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Genero?");
    if (eliminar) {
    location.href = "../../Gender/DeleteConfirmed/" + id;}
}
function eliminarPais(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Pais?");
    if (eliminar) {
    location.href = "../../Country/DeleteConfirmed/" + id;}
}
function eliminarProductor(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Productor?");
    if (eliminar) {
    location.href = "../../Producer/DeleteConfirmed/" + id;}
}
function eliminarSocios(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Socios?");
    if (eliminar) {
    location.href = "../../Partner/DeleteConfirmed/" + id;}
}
function eliminarPelicula(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar esta Peliculas?");
    if (eliminar) {
    location.href = "../../Movie/DeleteConfirmed/" + id;}
}
function eliminarAlquileres(id) {
    console.log(id);
    var eliminar = confirm("Está seguro de eliminar este Alquiler?");
    if (eliminar) {
    location.href = "../../Rental/DeleteConfirmed/" + id;}
}

function AgregarPeliculas() {
    console.log("Funcion de agregar pelicula temporal activada")
    var movieID = $("#MovieID").val();

    $.ajax({
        type: "POST",
        url: "../../Rentals/AgregarPeliculas",
        data: { MovieID: movieID },
        success: function (resultado) {
            if (resultado == true) {
                console.log("Se guardo la pelicula correctamente");
                alert("Se guardo la pelicula correctamente");
                //$("#modal").modal("hide");
                //BuscarPeliculas();
                //Location.href = "../../Rentals/Create"
            } else {
                alert("No se pudo agregar la pelicula, intente nuevamente");
                console.log("No se pudo agregar la pelicula, intente nuevamente");
            }
        },
        error: function(_result) {
            console.log("Error debido a: " + _result)
        },
    });
}

function CancelRental() {
    $.ajax({
        type: "POST",
        url: "../../Rentals/CancelarAlquiler",
        data: {},
        success: function(result){
            if(resultado = true)
            {
                location.href = "../../Rentals/Index";
            }
        },
        error(result){

        }
    })
}