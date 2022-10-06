function CargarPaginasDevolucion() {
    SearchReturnTmp();
}

function AgregarPeliculasDevolucion() {
    // console.log("Funcion de agregar pelicula temporal activada")
    var movieID = $("#MovieID").val();
    $.ajax({
        type: "POST",
        url: "../../Return/AgregarPeliculas",
        data: { MovieID: movieID },
        success: function (resultado) {
            if (resultado == true) {
                $("#staticBackdrop").modal("hide");
                SearchMovieTmp();
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Se selecciono la pelicula correctamente!',
                    showConfirmButton: false,
                    timer: 1000
                })
                setTimeout(function(){
                    Location.href = "../../Return/Create"
                }, 1010);
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

function CancelReturn() {
    $.ajax({
        type: "POST",
        url: "../../Return/CancelarDevolucion",
        data: {},
        success: function(resultado){
            if(resultado = true)
            {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Devolucion cancelada',
                    showConfirmButton: false,
                    timer: 1000
                })
                setTimeout(function(){
                    location.href = "../../Return/Index";
                }, 1010);
            }
        },
        error(result){

        }
    })
}

function SearchReturnTmp() {
    $.ajax({
        type: "GET",
        url: "../../Return/SearchMovieTmp",
        data: {},
        success: function(ListadoMovieTmp){
            // console.log(ListadoMovieTmp)
            $.each(ListadoMovieTmp, function(index, item){
                $("#tbody-Devolucion").append(
                    `<tr>
                        <th>${item.movieName}</th>
                        <th>
                            <button class="btn botonEliminar" onclick="QuitarMovieReturn(${item.movieID});">Quitar Peliculas</button>
                        </th>
                    </tr>`
                );
            });
        },
        error(result){

        }
    })
}

function QuitarMovieReturn(id){
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })
        
        swalWithBootstrapButtons.fire({
            title: 'Estas segur@?',
            text: "Desea eliminar la pelicula de la devolucion?!",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Si, Eliminar!',
            cancelButtonText: 'No, cancelar!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "../../Return/QuitarMovie",
                    data: {MovieID: id},
                    success: function(resultado){
                        if(resultado == true){
                            location.href = "../../Return/Create";
                        }
                    }}),
                swalWithBootstrapButtons.fire(
                'Eliminado!',
                'La pelicula se elimino de la lista',
                'success'
                )}
        })
    }

function SearchMovieReturn(ReturnID) {
    $('#tbody-peliculasReturn').empty();
    $.ajax({
        type: "POST",
        url: "../../Return/SearchMovie",
        data: {ReturnID: ReturnID},
        success: function(ListadoMovie){
            // console.log(ListadoMovie)
            $.each(ListadoMovie, function(index, item){
                $("#tbody-peliculasReturn").append(
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
