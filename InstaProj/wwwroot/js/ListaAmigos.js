
$(function () {
    BuscarListaNaoAmigos();
    buscarListaAmigos();
    AdicionarAmigoECarregaListaEmTempoReal();
    $('.modal').modal();
});

function BuscarListaNaoAmigos() {

    $.get("ListarNaoAmigos",function (response) {
        if (response != null) {
        montarListaNaoAmigos(response);
        }
    }).catch(function (response) {
        console.log(response);

        });

}

function montarListaNaoAmigos(lista) {

    var ulNaoAmigos = $("#ul-lista-nao-amigos");
    ulNaoAmigos.children().remove();
    lista.forEach(function(user){

        var liUser = $("<li>").addClass("collection-item");
        var divRow = $("<div>").addClass("row valign-wrapper");
        var divCol1 = $("<div>").addClass("col s2");
        var imgUser = $("<img>").addClass("circle responsive-img").attr("src", "CarregarImagemUsuario/" + user.email);
        var divCol2 = $("<div>").addClass("col s8");
        var spanNomeUser = $("<span>").addClass("black-text").text(user.nome);
        var divCol3 = $("<div>").addClass("col s2");
        var btnAddAmigo = $("<a>").addClass("btn-floating btn-large waves-effect waves-light red");
        var iconeBtn = $("<i>").addClass("material-icons").text("add");


        btnAddAmigo.click(function () {
            $.ajax({
                url: "AddAmigo",
                contentType: "application/json",
                data: JSON.stringify(user.usuarioId),
                method: "post"
            }).done(function (response) {
                ulNaoAmigos.notify("Amigo adicionado ", { className: "success", position: "bottom" });

            }).fail(function () {
                ulNaoAmigos.notify("Falha ", { className: "error", position: "bottom" });
            });

        });

        divCol1.append(imgUser);
        divCol2.append(spanNomeUser);
        divCol3.append(btnAddAmigo.append(iconeBtn));
        divRow.append(divCol1, divCol2, divCol3);
        liUser.append(divRow);
        ulNaoAmigos.append(liUser);
    });

}

function buscarListaAmigos(){


    $.ajax({
        url: "ObterListaDeAmigos",
        contentType: "application/json",
        method: "post"
    }).done(function (response) {
        if (response != null && response != undefined) {
        montarListaAmigos(response);

        }
        }).fail(function (response) {
            $("#lista-amigos").notify("Erro", {className: "error", position:"bottom"});
    });

}

function montarListaAmigos(listaAmigos){
    var ulAmigos = $("#lista-amigos");
    ulAmigos.children().remove();
    listaAmigos.forEach(function (user) {

        var liAmigo = $("<li>").attr("id", user.usuarioAmigo.usuarioId).append($("<span>").addClass("new badge blue").attr("data-badge-caption", "nova(s)").text("0"));
        var aBtn = $("<a>");
        var divRow = $("<div>").addClass("row valign-wrapper");
        var divCol1 = $("<div>").addClass("col s2");
        var img = $("<img>").addClass("circle").css("width", "25px", "heigth", "25px").attr("src", "CarregarImagemUsuario/" + user.usuarioAmigo.email);
        var divCol2 = $("<div>").addClass("col s10");
        var span = $("<span>").addClass("black-text").text(user.usuarioAmigo.nome);

        aBtn.click(function () {
            var modal = $("#"+user.usuarioAmigo.usuarioId+".modal");
            //verifica se o modal existe para não criar outros
            if (modal.length == 0) {
                montarModelChat(user.usuarioAmigo);
            }
        
            modal = $("#" + user.usuarioAmigo.usuarioId + ".modal");

            
            $('.modal').modal();
            modal.modal('open');
            $("li[id=" + user.usuarioAmigo.usuarioId + "]").find("span").first().text("0");

        });

        

        divCol2.append(span);
        divCol1.append(img);
        divRow.append(divCol1, divCol2);
        aBtn.append(divRow);
        liAmigo.append(aBtn);
        ulAmigos.append(liAmigo);

    });

}


function AdicionarAmigoECarregaListaEmTempoReal() {
    connection.on("AoAdicionarAmigo", function (response) {
        console.log(response);
        montarListaAmigos(response);
    });

    connection.on("AtualizarListaNaoAmigos", function (response) {
        console.log(response);
        montarListaNaoAmigos(response);
        $("#ul-lista-nao-amigos").notify("Amigo Adicionado", { className: "success" });
    });
    
}


function montarModelChat(user) {
    var body = $("main");
    var modal = $("<div>").addClass("modal modal-fixed-footer").attr("id", user.usuarioId);
    var divModalContent = $("<div>").addClass("modal-content");
    var imgAmigo = $("<img>").attr("src", "CarregarImagemUsuario/" + user.email).addClass("circle responsive-img");
    var nomeAmigo = $("<blockquote>").text(user.nome);
   // var divContainerCaixa = $("<div>").addClass("container container-chat").text(user.email);
    var ulChat = $("<ul>").addClass("collection");
    var divModalFooter = $("<div>").addClass("modal-footer");
    var divInputDigitacao = $("<textarea>").addClass("materialize-textarea").css({
        "height": "35px",
        "width": "600px",
        "float": "left"
    });
    var spanDigitando = $("<span>").attr("id", "isDigitando").css("float", "left");
    var divBtnEnviar = $("<button>").addClass("btn").text("Enviar");
    divBtnEnviar.attr("disabled", true);
    divBtnEnviar.click(function () {
        connection.invoke("EnviarMensagem", user.email, divInputDigitacao.val()).then(function () {
            var liItem = $("<li>").addClass("collection-item").append($("<h6>").css("float", "right").text(divInputDigitacao.val()));
            ulChat.append(liItem);
            ulChat.append("<br>");
            divInputDigitacao.focus();
            divInputDigitacao.val("");
        }).catch(function (err) {
            return console.error(err.toString());
            });
        divBtnEnviar.attr("disabled", true);
    });

    divInputDigitacao.on("input", function () {
        if (this.value.length < 1 || this.value.length == 0 || this.value == undefined || this.value == null) {
            divBtnEnviar.attr("disabled", true);
            connection.invoke("VerificarDigitacao", user.email, "");

        } else {
            connection.invoke("VerificarDigitacao", user.email, user.nome +" está digitando...");
            divBtnEnviar.attr("disabled", false);

        }
    });


    divModalFooter.append(spanDigitando,"<br>",divInputDigitacao, divBtnEnviar);
    //divContainerCaixa.append(ulChat);
    nomeAmigo.prepend(imgAmigo);
    divModalContent.append(nomeAmigo);
    divModalContent.append(ulChat);
    modal.append(divModalContent, divModalFooter);
    body.append(modal);

}


connection.on("ReceberMensagem", function (usuarioQueEnviou, mensagem) {
    var modal = $("#" + usuarioQueEnviou.usuarioId+ ".modal");
    //verifica se o modal existe para não criar outros
    if (modal.length == 0) {
        montarModelChat(usuarioQueEnviou);
    }

    modal = $("#" + usuarioQueEnviou.usuarioId + ".modal");
    var liItem = $("<li>").addClass("collection-item").append($("<h6>").text(mensagem));
    var ulModal = modal.find("ul");
    ulModal.append(liItem);
    var listaAmigoUsuarioLi = $("li#" + usuarioQueEnviou.usuarioId);
    var span = listaAmigoUsuarioLi.find("span").first();
    var auxPIncrementoNotificacoes = parseInt(span.text()) + 1;
    span.text(auxPIncrementoNotificacoes);

    modal.find("span[id=isDigitando]").text("");


});

connection.on("ReceberDigitacao", function (usuario, msg) {
    var modal = $("#" + usuario.usuarioId + ".modal");
    if (modal.length == 0) {
        montarModelChat(usuario);
    }

    modal.find("span[id=isDigitando]").text(msg);
});