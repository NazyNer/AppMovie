function CargarPaginas() {
    SearchMovieTmp();
}

function AgregarPeliculas() {
    // console.log("Funcion de agregar pelicula temporal activada")
    var movieID = $("#MovieID").val();
    $.ajax({
        type: "POST",
        url: "../../Rental/AgregarPeliculas",
        data: { MovieID: movieID },
        success: function (resultado) {
            if (resultado == true) {
                Swal.fire(
                    'Perfecto!',
                    'Se guardo la pelicula correctamente!',
                    'success'
                )
                $("#staticBackdrop").modal("hide");
                SearchMovieTmp();
                Location.href = "../../Rental/Create"
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'No se pudo agregar la pelicula, intente nuevamente!'
                })
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
        url: "../../Rental/CancelarAlquiler",
        data: {},
        success: function(resultado){
            if(resultado = true)
            {
            Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Alquiler cancelado',
                    showConfirmButton: false,
                    timer: 1000
                })
            setTimeout(function(){
                location.href = "../../Rental/Index";
            }, 1010);
        }
        error(result);

        }
    })
}

function SearchMovieTmp() {
    $.ajax({
        type: "GET",
        url: "../../Rental/SearchMovieTmp",
        data: {},
        success: function(ListadoMovieTmp){
            // console.log(ListadoMovieTmp)
            $.each(ListadoMovieTmp, function(index, item){
                $("#tbody-peliculas").append(
                    `<tr>
                        <th>${item.movieName}</th>
                        <th>
                            <button class="btn botonEliminar" onclick="QuitarMovie(${item.movieID});">Quitar Peliculas</button>
                        </th>
                    </tr>`
                );
            });
        },
        error(result){

        }
    })
}

function QuitarMovie(id){
    $.ajax({
        type: "POST",
        url: "../../Rental/QuitarMovie",
        data: {MovieID: id},
        success: function(resultado){
            if(resultado == true){
                location.href = "../../Rental/Create";
            }
        },
        error(result){
            
        }
    })
}

function SearchMovie(rentalID) {
    $('#tbody-peliculasDetail').empty();
    $.ajax({
        type: "POST",
        url: "../../Rental/SearchMovie",
        data: {RentalID: rentalID},
        success: function(ListadoMovie){
            // console.log(ListadoMovie)
            $.each(ListadoMovie, function(index, item){
                $("#tbody-peliculasDetail").append(
                    `<tr>
                        <th>${item.movieName}</th>
                    </tr>`
                );
            });
        },
        error(result){

        }
    })
}
