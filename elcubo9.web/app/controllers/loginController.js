
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', 'toastrService', 'blockUI',
function ($scope, $location, authService, ngAuthSettings, toastrService, blockUI) {
    $scope.$parent.showNav = false;
    $scope.login = function () {
        $scope.loggingIn = true;
        /*THIS LINE IS FOR FIX BUG ON AUTOCOMPLETE FORM*/
        $scope.$broadcast("autofill:update");
        authService.login($scope.loginData).then(function (response) {
            console.log("login");
            window.location = InternUrl;
        },
         function (err) {
             $scope.loggingIn = false;
             toastrService.error({ Message: 'El email o la clave son incorrectos' });
         });
    };
    $scope.signup = function () {
        $scope.loggingIn = true;
        authService.saveRegistration($scope.registration).then(function (response) {
            var loginData = {
                userName: $scope.registration.Email,
                password: $scope.registration.Password
            }
            authService.login(loginData).then(function (response) {
                toastrService.success("El usuario se ha creado ya puedes hacer tu 1ra orden.")
                window.location = InternUrl;
            },
            function (err) {
                $scope.loggingIn = false;
                $scope.message = err.error_description;
            });
        },
     function (response) {
         toastrService.error(response.data);
         $scope.loggingIn = false;
     });
    };

    $scope.authExternalProvider = function (provider) {
        blockUI.start();
        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + RedirectUri;
        window.$windowScope = $scope;
        var windowSize = "width=" + window.innerWidth + ",height=" + window.innerHeight + ",scrollbars=no";
        window.location = externalProviderUrl;
    };
}
]);
