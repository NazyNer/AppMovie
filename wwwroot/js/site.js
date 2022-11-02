function eliminarSeccion(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar esta seccion?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Section/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'La seccion se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste la seccion en la lista',
              'error'
          )
          }

    })
}
function eliminaUbicacion(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar esta Ubicacion?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Location/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'La Ubicacion se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste la Ubicacion en la lista',
              'error'
          )
          }

    })
}

function eliminarGenero(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar este Genero?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Gender/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'el Genero se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste el Genero en la lista',
              'error'
          )
          }

    })
}
function eliminarPais(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar este Pais?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Country/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'el Pais se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste el Pais en la lista',
              'error'
          )
          }

    })
}
function eliminarProductor(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar este Productor?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Producer/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'el Productor se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste el Productor en la lista',
              'error'
          )
          }

    })
}

function eliminarSocios(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar este Socio?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Partner/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'el Socio se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste la pelicula en la lista',
              'error'
          )
          }

    })
}

function eliminarPelicula(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar esta Peliculas?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Movie/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'La pelicula se elimino de la lista',
                'success'
                );
        }
        else{
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel,
          swalWithBootstrapButtons.fire(
              'Cancelado',
              'Dejaste la pelicula en la lista',
              'error'
          )
          }

    })
}


