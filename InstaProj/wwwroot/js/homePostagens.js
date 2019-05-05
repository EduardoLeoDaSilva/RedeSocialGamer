
$(document).ready(function () {
    enviarNovaPostagem();
    inicalizaoTempoReal();
    carregaImagensAenviar();
    //ReceberLike();
});

var connection = new signalR.HubConnectionBuilder().withUrl("/postagemHub").build();
connection.start();
//Faz uma requisao ajax enviando dados da nova postagem
function enviarNovaPostagem() {
    $("#btn-postar").click(function () {
        event.preventDefault();
        var form = $("#formPostagem").get(0);
        var formData = new FormData(form);
        var isValid = validarFormPostagem();
        if (isValid == true) {
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
                $(".imgdupload").remove();
            });
        }
    });
}
function validarFormPostagem() {
    var textoTextArea = $("#textarea2");
    var inputFile = $("#imagemPostagem");

    if (textoTextArea.val().length < 5) {

        textoTextArea.focus();
        textoTextArea.notify("Sua publicaçao precisa ter um texto com mais de 5 caracteres", { className: "warn", position: "bottom" });

        return false;

    } 
    if (inputFile.val() < 1) {
        inputFile.focus();
        inputFile.notify("Sua publicaçao precisa ter no minimo uma foto", { className: "warn", position: "bottom" });

        return false;
    }

    return true;
}
function carregaImagensAenviar() {


    $("#imagemPostagem").on("change", function () {

        var files = this.files;
        var imgdupload = $(".imgdupload");
        var colImagensUpload = $("#col-imagens-upload");

        $(imgdupload).toggle(1000, function () {
            this.remove();
        });
        if (files.length > 0) {
            $(files).each(function () {
                var reader = new FileReader();
                reader.onload = function () {
                    var imgs = $("<img>").css("display", "none").addClass("imagemLida imgdupload").attr("src", reader.result);
                    colImagensUpload.append(imgs);
                    $(imgs).toggle(1000);
                }

                reader.readAsDataURL(this);
            })
        }
    });

}

//Inica a comunicacao em tempo real com o servidor(servi�o postagem)
function inicalizaoTempoReal() {
    connection.on("ReceberPostagem", function (response) {
        console.log(response);
        montarNovaPostagem(response);
    });
}

