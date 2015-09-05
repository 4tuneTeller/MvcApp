(function () {
    var loginButton = document.getElementById('login-button');
    loginButton.style.visibility = 'hidden';

    var usernameTextBox = document.getElementById('username');
    var passwordTextBox = document.getElementById('password');

    function textboxChangedEventHandler() {
        if (usernameTextBox.value != '' && passwordTextBox.value != '') {
            loginButton.style.visibility = 'visible';
        } else {
            loginButton.style.visibility = 'hidden';
        }
    }

    usernameTextBox.oninput = textboxChangedEventHandler;
    passwordTextBox.oninput = textboxChangedEventHandler;
})();