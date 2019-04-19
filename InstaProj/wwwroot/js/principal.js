
$(function () {
    //matearilzed elements functions
    $('.datepicker').datepicker({
        "format": "dd-mm-yyyy"
    });
    $('.materialboxed').materialbox();


    validarLogin();
    enviarCadastro();
    validarImagem();
});

var spinner = $("#spinner-login");
var spinnerCad = $("#spinner-cadastro");
var btnFormLogin = $("#btn-entrar");
var formLogin = $("#form-login");
var formCadastro = $("#form-cadastro").get(0);
var btnFormCadastro = $("#cadastrarBtn");


//valida utilizando as funções enviarRequestLogin e ValidarCamposLogin
function validarLogin() {
    btnFormLogin.click(function () {
        event.preventDefault();
        btnFormLogin.attr("disabled", true);
        var isValid = validarCamposLogin();
        if (isValid == true) {
            enviarRequestLogin();

        } else {
            btnFormLogin.attr("disabled", false);

        }

        



    });

}

//realiza a requisao ajax
function enviarRequestLogin() {
    spinner.addClass("active");
    var inputsForm = formLogin.find('input');
    var email = inputsForm.get(0).value;
    var senha = inputsForm.get(1).value;
    var usuario = {
        email: email,
        password: senha
    };

    $.ajax({
        url: "/Usuario/EfetuarLogIn",
        contentType: "application/json",
        method: "post",
        data: JSON.stringify(usuario)

    }).done(function (response) {
        console.log(response);
        if (response == "Success") {
            btnFormLogin.notify("Usuario logado com sucesso", { className: "success", position: "right" });
            setTimeout(function () {
                location.assign("/home/home");
                btnFormLogin.attr("disabled", false);

            }, 1500);
        } else if (response == "LockedOut") {
            btnFormLogin.notify("Usuario bloqueado por 3 min", { className: "warn", position: "right" });
            btnFormLogin.attr("disabled", false);

        } else if (response == "NotAllowed") {
            btnFormLogin.notify("Acesso não permitido", { className: "warn", position: "right" });
            btnFormLogin.attr("disabled", false);

        } else {
            btnFormLogin.notify("Email ou senha incorretos", { className: "error", position: "right" });
            btnFormLogin.attr("disabled", false);

        }
    }).always(function (response) {
        console.log(response);
        spinner.removeClass("active");

    }).fail(function () {
        btnFormLogin.notify("Ocorreu um erro inesperado, por favor tente mais tarde", { className: "error", position: "right" });
        btnFormLogin.attr("disabled", false);

    });
}

//valida os campos do formulario Login
function validarCamposLogin() {
    var formLogin = $("#form-login");
    var inputs = formLogin.find("input");
    var email = inputs.get(0);
    var senha = inputs.get(1);
    var regexEmail = /[A-Za-z0-5]{1,}\@[A-Za-z]{1,}\.[a-z]{1,3}/;
    var retorno = regexEmail.exec(email.value);
    if ((retorno == null) || !(retorno[0] == email.value)) {
        var spanEmail = formLogin.find("span");
        email.value = "";
        $(email).focus();
        $(email).notify("Email inválido", { className: "error", position: "bottom" });
        spinner.removeClass("active");
        return false;
    } else if (!(senha.value.length > 3)) {
        senha.value = "";
        $(senha).focus();
        $(senha).notify("Senha inválido", { className: "error", position: "bottom" });

        spinner.removeClass("active");
        return false;

    }
    return true;

}

//valida utilizando as funções enviarRequestCadastro e ValidarCamposCadastro
function enviarCadastro() {
    btnFormCadastro.click(function () {
        event.preventDefault();
        btnFormCadastro.attr("disabled", true);
        var isValid = validarCamposCadastro();
        if (isValid) {
            enviarRequestCadastro();
            $(formCadastro).each(function () {
                this.reset();
            });
            $("#imagemPrevia").attr("src", "");

            btnFormCadastro.attr("disabled", false);
        }
        btnFormCadastro.attr("disabled", false);

    });

    
}

//realiza a requisao ajax
function enviarRequestCadastro() {
    spinnerCad.addClass("active");

        var data = new FormData(formCadastro);


        $.ajax({
            url: "/Usuario/Cadastrar",
            contentType: "application/json",
            method: "post",
            data: data,
            processData: false,
            contentType: false
        }).done(function (response) {
            $(btnFormCadastro).notify(response, { className: "success", position: "bottom" });

        }).always(function () {
            spinnerCad.removeClass("active");
            btnFormCadastro.attr("disabled", false);

        }).fail(function (response) {
            $(btnFormCadastro).notify(response, { className: "error", position: "bottom" });

        });

    

}

//valida os campos do formulario cadastro
function validarCamposCadastro() {

    event.preventDefault();
    var nome = $(formCadastro).find("#Nome");
    var email = $(formCadastro).find("#email");
    var nascimento = $(formCadastro).find("#nascimento");
    var senha = $(formCadastro).find("#senha");
    var confSenha = $(formCadastro).find("#confirmPassword");
    var fotoImg = $(formCadastro).find("#foto");

    var regexEmail = /[A-Za-z0-5]{1,}\@[A-Za-z]{1,}\.[a-z]{1,3}/;
    var retorno = regexEmail.exec(email.val());

    if (!(nome.val().length > 3)) {
        $(nome).notify("Nome tem que ter no minimo 3 caracteres", { className: "error", position: "bottom" });
        nome.focus();
    } else if (!(retorno != null) || !(retorno[0] == email.val())) {
        $(email).notify("Email inválido, exemplo: seuemail@host.com", { className: "error", position: "bottom" });
        email.focus();
    } else if (nascimento.val() == "" || nascimento.val() == null) {
        $(nascimento).notify("Por favor preencha o campo nascimento", { className: "error", position: "bottom" });
        nascimento.focus();
    } else if (!(senha.val().length > 5) || senha.val() == "") {
        $(senha).notify("A senha tem que ter no minimo 5 caracteres", { className: "error", position: "bottom" });
        senha.focus();

    } else if (!(senha.val() == confSenha.val()) || confSenha.val() == "") {
        $(confSenha).notify("As senha são diferentes", { className: "error", position: "bottom" });
        confSenha.focus();
    } else if (fotoImg.val() == null || fotoImg.val() == "") {
        $(fotoImg).notify("Necessário uma Imagem", { className: "error", position: "bottom" });
        fotoImg.focus();
    } else {
        return true;
    }
    return false;
}

//valida a imagem
function validarImagem() {
    $("#foto").change(function () {
        var imagemUploade = $("#foto")[0].files[0];
        var pattern = /[A-Za-z0-9]+\.png|jpeg|jpg/;
        if (pattern.test(imagemUploade.name)) {
            var imagemPrevia = $("#imagemPrevia");
            var reader = new FileReader();
            reader.onloadend = function () {
                imagemPrevia.attr("src", reader.result);
            }

            if (imagemUploade) {
                reader.readAsDataURL(imagemUploade);
            } else {
                imagemPrevia.src = "";
            }
        } else {
            $(this).focus();
            $(this).val("");
            $(this).notify("Deve ser uma imagem no formato PNG ou JPEG", { className: "warn", position: "bottom" });

        }
        btnFormCadastro.attr("disabled", false);
        
    });


}