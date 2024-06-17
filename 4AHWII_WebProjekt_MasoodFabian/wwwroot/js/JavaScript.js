$(() => {
    let formOk = true;

    $("#Username").on("blur", () => {
        let username = $("#Username").val();
        if (checkUsername(username)) {
            $("#errorUsername").html("");
        } else {
            $("#errorUsername").html("Der Benutzername muss <b>mind. 3 Zeichen</b> lang sein");
            formOk = false;
        }
    });

    $("#Passwort").on("blur", () => {
        let password = $("#Passwort").val();
        if (checkPassword(password)) {
            $("#errorPasswort").html("");
        } else {
            $("#errorPasswort").html("Das Passwort muss <b>mind. 8 Zeichen</b> lang sein");
            formOk = false;
        }
    });

    $("#btnSubmit").on("click", (event) => {
        $("#Username").trigger("blur");
        $("#Passwort").trigger("blur");

        if (!formOk) {
            event.preventDefault();
            formOk = true;
        }
    });
});

function checkUsername(username) {
    username = username.trim();
    if (username.length < 3) {
        return false;
    }
    return true;
}

function checkPassword(password) {
    password = password.trim();
    if (password.length < 8) {
        return false;
    }
    return true;
}