
$(function () {
    BuscarListaNaoAmigos();
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
                data: JSON.stringify(user.usuarioId),
                contentType: "application/json",
                method: "post"
            }).done(function(response) {
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