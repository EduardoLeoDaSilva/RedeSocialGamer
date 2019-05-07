
$(function () {
    ReceberLike();
    ReceberComentario();
    CarregarPostagens();
});

function montarNovaPostagem(postagem) {

    //elementos para Montagem de uma nova postagem no feed// Itens pais
    var feedContainer = $("#container-feed");
    var divCol = $("<div>").addClass("col s8 offset-s2").css("display", "none");
    var divCardFeed = $("<div>").addClass("card feed").attr("idPostagem", postagem.postagemId);
    var divCardImagem = $("<div>").addClass("card-image");
    var divCardContent = $("<div>").addClass("card-content");

    var divCardAction = $("<div>").addClass("card-action");

    var ulComentarios = $("<ul>").attr("id", "ul-comentarios");


    //elementos filhos
    var spanCardTitulo = $("<span>").addClass("card-title");
    var paragrafoTexto = $("<p>").text(postagem.texto);
    var btnLike = $("<button>").addClass("btn-floating btn-large waves-effect waves-light blue")
        .append($("<i>").addClass("material-icons").text("thumb_up"));
    var span = $("<span>").text("0").attr("id", "spanLike");
    var input = $("<input>").attr("type", "text");

    btnLike.click(EnviarLike);

    var btnEnviarComentario = $("<button>").addClass("waves-effect waves-light btn").text("Enviar comentario");

    btnEnviarComentario.click(EnviarComentario);
    var divCarouselImage = $("<div>").addClass("carousel carousel-slider center");

    //elementos filhos- Veririfcar se tem mais de uma imagem

    if (postagem.imagens.length > 1) {
        var aux = 0;
        postagem.imagens.forEach(img => {
            var itemCarousel = $("<a>").addClass("carousel-item");
            var imagemPostagem = $("<img>").attr("src", img);
            itemCarousel.append(imagemPostagem);
            divCarouselImage.append(itemCarousel);
            aux++;
        });

    } else {
        var itemCarousel = $("<a>").addClass("carousel-item");
        var imagemPostagem = $("<img>").attr("src", postagem.imagens[0]);
        itemCarousel.append(imagemPostagem);
        divCarouselImage.append(itemCarousel);

    }

    divCardImagem.append(divCarouselImage);


    //montando card postagem
    divCardAction.append(ulComentarios, input, btnEnviarComentario);
    divCardContent.append(paragrafoTexto, btnLike, span);
    divCardImagem.append(spanCardTitulo);
    divCardFeed.append(divCardImagem, divCardContent, divCardAction);
    divCol.append(divCardFeed);
    feedContainer.prepend(divCol);
    divCol.slideToggle();
    $('.carousel.carousel-slider').carousel({
        fullWidth: true,
        indicators: true
    });


}

function EnviarLike() {
    var idPostagem = $(this).parents("[idPostagem]").attr("idPostagem");
    connection.invoke("SendALike", idPostagem).then(function () {
        console.log("Like Enviado");
    }).catch(function (err) {
        return console.error(err.toString());
    });
}


function EnviarComentario() {
    var idPostagem = $(this).parents("[idPostagem]").attr("idPostagem");
    var texto = $(this).parent().find("input").val();
    connection.invoke("SendComentario", idPostagem, texto).then(function () {
        console.log("Like Enviado");
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

function ReceberLike() {
    connection.on("ReceberLikeReload", MontarLike);
}

function ReceberComentario() {
    connection.on("ReceberComentario", MontarComentario);

}

function MontarLike(postagem) {
    var cardPostagem = $(".card[idPostagem=" + postagem.postagemId + "]");
    var spanLike = cardPostagem.find("#spanLike");

        if (postagem.isLiked == true) {
            var likes = spanLike.text();
            if (likes > 0) {
                likes = parseInt(spanLike.text()) - 1;

            }
        } else {
            likes = parseInt(spanLike.text()) + 1;

        }
        spanLike.text(likes);

     if (postagem.likes.length != undefined) {
        spanLike.text(postagem.likes.length);
    }

}


function MontarComentario(comentario) {
    var card = $("[idPostagem=" + comentario.postagem.postagemId + "]");
    var ulComentarios = $(card).find("#ul-comentarios");
    var dataComentario = new Date(comentario.comentarioData);

    var liComentario = $("<li>").addClass("collection-item").css("display", "none");
    var divRow = $("<div>").addClass("row valign-wrapper");
    var divCol1 = $("<div>").addClass("col s2");
    var imgUser = $("<img>").addClass("circle responsive-img").attr({ "src": "CarregarImagemUsuario/" + comentario.usuarioAutor.email, "alt": comentario.usuarioAutor.nome });
    var divCol2 = $("<div>").addClass("col s8");
    var spanComentarioTexto = $("<span>").addClass("black-text").text(comentario.comentarioTexto + "          Data/Horário: " + dataComentario.toLocaleString());
    divCol1.append(imgUser);
    divCol2.append(spanComentarioTexto);
    divRow.append(divCol1, divCol2);


    imgUser.mouseover(function () {
        imgUser.notify(comentario.usuarioAutor.nome, { className: "success", position: "right" });
    });

    liComentario.append(divRow);
    ulComentarios.prepend(liComentario);
    liComentario.show(1000);


}

function CarregarPostagens() {

    $.post("GetPostagens", function (lista) {


        lista.forEach(function (postagem) {
            montarNovaPostagem(postagem);
            MontarLike(postagem);
            postagem.comentarios.forEach(function (comentario) {
                MontarComentario(comentario);

            });

        });

    });

}