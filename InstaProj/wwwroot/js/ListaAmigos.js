
$(function () {
    BuscarListaNaoAmigos();
    buscarListaAmigos();
    AdicionarAmigoECarregaListaEmTempoReal();
});

function BuscarListaNaoAmigos() {

    $.get("ListarNaoAmigos", function (response) {
        if (response !== null) {
        montarListaNaoAmigos(response);
        }
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

        var liAmigo = $("<li>");
        var aBtn = $("<a>");
        var divRow = $("<div>").addClass("row valign-wrapper");
        var divCol1 = $("<div>").addClass("col s2");
        var img = $("<img>").addClass("circle").css("width", "25px", "heigth", "25px").attr("src", "CarregarImagemUsuario/" + user.usuarioAmigo.email);
        var divCol2 = $("<div>").addClass("col s10");
        var span = $("<span>").addClass("black-text").text(user.usuarioAmigo.nome);

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
