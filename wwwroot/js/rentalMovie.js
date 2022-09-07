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
                console.log("Se guardo la pelicula correctamente");
                alert("Se guardo la pelicula correctamente");
                $("#staticBackdrop").modal("hide");
                SearchMovieTmp();
                Location.href = "../../Rental/Create"
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
        url: "../../Rental/CancelarAlquiler",
        data: {},
        success: function(result){
            if(resultado = true)
            {
                location.href = "../../Rental/Index";
            }
        },
        error(result){

        }
    })
}

function SearchMovieTmp() {
    $.ajax({
        type: "GET",
        url: "../../Rental/SearchMovieTmp",
        data: {},
        success: function(ListadoMovieTmp){
            console.log(ListadoMovieTmp)
            $.each(ListadoMovieTmp, function(index, item){
                $("#tbody-peliculas").append(
                    `<tr>
                        <th>${item.movieName}</th>
                        <th>
                            <button class="btn botonEliminar" onclick="QuitarMovie"("${item.movieID}");>Quitar Peliculas</button>
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

function SearchMovie() {
    $.ajax({
        type: "GET",
        url: "../../Rental/SearchMovie",
        data: {RentalID: rentalID},
        success: function(ListadoMovie){
            // console.log(ListadoMovie)
            $.each(ListadoMovie, function(index, item){
                $("#tbody-peliculas").append(
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
