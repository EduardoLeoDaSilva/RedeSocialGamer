
function montarNovaPostagem(texto, imagem) {

    //elementos para Montagem de uma nova postagem no feed// Itens pais
    var feedContainer = $("#container-feed");
    var divCol = $("<div>").addClass("col s8 offset-s2").css("display", "none");
    var divCardFeed = $("<div>").addClass("card feed");
    var divCardImagem = $("<div>").addClass("card-image");
    var divCardContent = $("<div>").addClass("card-content");

    var divCardAction = $("<div>").addClass("card-action");

    
 
    //elementos filhos
    var spanCardTitulo = $("<span>").addClass("card-title").text("Testando ainda");
    var paragrafoTexto = $("<p>").text(texto);
    var btnLike = $("<button>").addClass("btn-floating btn-large waves-effect waves-light blue")
        .append($("<i>").addClass("material-icons").text("thumb_up"));
    var btnRisos = $("<button>").addClass("btn-floating btn-large waves-effect waves-light yellow")
        .append($("<i>").addClass("material-icons").text("tag_faces"));
    var input = $("<input>").attr("type", "text");
    var btnEnviarComentario = $("<button>").addClass("waves-effect waves-light btn").text("Enviar comentario");
    var divCarouselImage  = $("<div>").addClass("carousel carousel-slider center");

   //elementos filhos- Veririfcar se tem mais de uma imagem
    
   if(imagem.length > 1){
       var aux = 0;
       imagem.forEach(img => {
           var itemCarousel = $("<a>").addClass("carousel-item");
           var imagemPostagem = $("<img>").attr("src", img);
           itemCarousel.append(imagemPostagem);
           divCarouselImage.append(itemCarousel);
           aux++;
        });
        
    }else{
        var itemCarousel = $("<a>").addClass("carousel-item");
        var imagemPostagem = $("<img>").attr("src", imagem[0]);
        itemCarousel.append(imagemPostagem);
        divCarouselImage.append(itemCarousel);
        
    }
    
    divCardImagem.append(divCarouselImage);


    //montando card postagem
    divCardAction.append(input, btnEnviarComentario);
    divCardContent.append(paragrafoTexto, btnLike, btnRisos);
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