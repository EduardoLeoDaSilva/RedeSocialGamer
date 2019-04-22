
$(document).ready(function () {
    enviarNovaPostagem();
    inicalizaoTempoReal();
});


//Faz uma requisao ajax enviando dados da nova postagem
function enviarNovaPostagem() {
    $("#btn-postar").click(function () {
        event.preventDefault();
        var form = $("#formPostagem").get(0);
        var formData = new FormData(form);

        $.ajax({
            url: "EnviarPostagem",
            contentType: false,
            method: "post",
            data: formData,
            processData: false
        }).done(function () {
            $(".collapsible").collapsible("close");
            $("#btn-postar").parents("form").get(0).reset();
            $("#btn-postar").parents(".collapsible-header");
        });

    });
}

//Inica a comunicação em tempo real com o servidor(serviço postagem)
function inicalizaoTempoReal() {
    var connection = new signalR.HubConnectionBuilder().withUrl("/postagemHub").build();

    connection.on("ReceberPostagem", function (response) {
        console.log(response);
        montarNovaPostagem(response.texto, response.imagem);
        
    });


    connection.start();
}

