$(document).ready(function () {
    var tipo = $('#tipo').data('tipo');

    switch (tipo) {
        case "Banda":
            $('.Banda').show();
            $('.Manager').hide();
            $('.Sala').hide();
            break;
        case "Manager":
            $('.Banda').hide();
            $('.Sala').hide();
            $('.Manager').show();
            break;
        case "Sala":
            $('.Sala').show();
            $('.Manager').hide();
            $('.Banda').hide();
            break;
    }

    //$('#filtro').onclick(function () {
    //    if ($('#banda').is(':checked')) {
    //        $('#tipo').data('tipo')
    //    }
    //    else {

    //    }
    //});

    $('#editarComentario').click(function () {
        $('#comentario').addClass('hidden');
        $('#modificar').removeClass('hidden');
        $('#editarComentario').addClass('hidden');
    });
});