
function montarNovaPostagem(texto, imagem){

//elementos para Montagem de uma nova postagem no feed// Itens pais
var feedContainer = $("#container-feed");
    var divCol = $("<div>").addClass("col s8 offset-s2").css("display", "none");
    var divCardFeed = $("<div>").addClass("card feed");
    var divCardImagem = $("<div>").addClass("card-image");
    var divCardContent = $("<div>").addClass("card-content");

    var divCardAction = $("<div>").addClass("card-action");

/////////////////////////////////////////////////////////////////////

///elementos filhos///////////////////////////////////////////////////

    var imagemPostagem = $("<img>").attr("src", imagem);
    var spanCardTitulo = $("<span>").addClass("card-title").text("Testando ainda");
    var paragrafoTexto = $("<p>").text(texto);
var btnLike = $("<button>").addClass("btn-floating btn-large waves-effect waves-light blue")
    .append($("<i>").addClass("material-icons").text("thumb_up"));
var btnRisos = $("<button>").addClass("btn-floating btn-large waves-effect waves-light yellow")
    .append($("<i>").addClass("material-icons").text("tag_faces"));
var input = $("<input>").attr("type", "text");
    var btnEnviarComentario = $("<button>").addClass("waves-effect waves-light btn").text("Enviar comentario");

///////////////////////////////////////////////////////////////////////////


    ///////montando card postagem//////
    divCardAction.append(input, btnEnviarComentario);
    divCardContent.append(paragrafoTexto, btnLike, btnRisos);
    divCardImagem.append(imagemPostagem, spanCardTitulo);
    divCardFeed.append(divCardImagem, divCardContent, divCardAction);
    divCol.append(divCardFeed);
    feedContainer.prepend(divCol);
    divCol.slideToggle();


}