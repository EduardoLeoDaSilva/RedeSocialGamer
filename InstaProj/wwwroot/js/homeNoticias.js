
$(document).ready(function () {

    $('#sidenav-direito').sidenav({
        edge: 'right'
    });
    $(document).ready(function () {
        $('.collapsible').collapsible();
    });
    receberNoticiaServidor();
    recebeListaNoticiasServidor();
});

//Faz requisições de 10 em 10 segundos para o servidor em busca de novas noticias
function receberNoticiaServidor() {
    setInterval(function () {
        $.get("GetNoticias", function (response) {
            criarPainelNoticias(response);
            isDeletadoNoservidor(response);
        });
        }, 10000);


    }

//recebe a lista das noticias no servidor e chama o metodo criar lista
function recebeListaNoticiasServidor() {
    $.get("GetNoticias", function (response) {
        criarPainelNoticias(response);

    });
}

//verifica se essa noticia existe na pagina pra não repetila novamente
function temEssaNoticia(response) {
    var hasNoticia = true;
    var cardGroup = $(".card.blue-grey");
    if (cardGroup.children().length == 0) {
        hasNoticia = false;
    } else {
        cardGroup.each(function () {
            if ($(this).attr("id") != response.id) {
                hasNoticia = false;
                
            } else {
                hasNoticia = true;
                return false;
            }

        });
    }
    return hasNoticia;
}

//Verifica se foi deletado no servidor e remove da pagina p <li> do card
function isDeletadoNoservidor(response) {
    var aux = 0;
    var noticiasNosite = $("#sidenav-direito").find(".card.blue-grey.darken-1.noticias-card");
    var idsNositeArray = [];
    var idsNoBanco = [];
    noticiasNosite.each(function () {
        idsNositeArray.push($(this).attr("id"));

    });

    if (response != undefined) {
        response.forEach(function (bdId) {
            idsNoBanco.push(bdId.id);
        });

        if (idsNositeArray.length > 0) {
            for (var i = 0; i < idsNositeArray.length; i++) {
                if (!idsNoBanco.includes(parseInt(idsNositeArray[i]))) {
                    $("#sidenav-direito").find("[id=" + idsNositeArray[i] + "]").parent().slideUp(1000, function () {
                        var teste = $(this);
                        teste.remove();
                    });
                }
            }
        }
    } else {
        $("#sidenav-direito").children().slideUp(1000, function () {
            var teste = $(this);
            teste.remove();
        });
    }
}

//cria os a estrutra do painel do mais recente pro vais antigo
function criarPainelNoticias(response) {

    var auxLength = 0;
    if (response != null) {
        while (auxLength < response.length) {
            var temEssaNoticiaVar = temEssaNoticia(response[auxLength]);
            if (temEssaNoticiaVar == false) {
                var elementoLista = $("<li>").css("display", "none");
                var cardNoticia = $("<div>").addClass("card blue-grey darken-1 noticias-card").attr("id", response[auxLength].id);
                var cardNoticiaDentro = $("<div>").addClass("card-content white-text").append($("<span>").addClass("card-title")
                    .text(response[auxLength].titulo)).append($("<p>").text(response[auxLength].texto));
                elementoLista.append(cardNoticia.append(cardNoticiaDentro));
                $("#sidenav-direito").prepend(elementoLista);
                $(elementoLista).slideDown();

            }
            auxLength++;
        }
    }
}
