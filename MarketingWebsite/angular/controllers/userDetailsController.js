app.controller('UserDetailsController', function ($scope, $http, $location, $routeParams) {

    // viewModel
    $scope.user = {
        EmailAddress: '',
        JobTitle: '',
        FirstName: '',
        Surname: '',
        IsLoggedInUser: '',
        OldPassword: '',
        NewPassword: '',
        NewPasswordConfirm: ''
    };

    // methods
    init();

    function init() {
        $http({
            method: 'GET',
            url: 'api/users/get/' + $routeParams.id
        }).success(function (data, status, headers, config) {
            $scope.user = data;
        });
    };

    $scope.returnToList = function () {
        $location.path("/users");
    };

    $scope.updateUser = function () {
        if ($scope.user.NewPasswordConfirm == $scope.user.NewPassword) {
            $http({
                method: 'PUT',
                url: 'api/users/update/',
                data: $scope.user
            }).success(function (data, status, headers, config) {
                $scope.deleteAlerts();
                $scope.addAlert("success", "The user has been updated successfully");
            }).error(function (data, status, headers, config) {
                $scope.deleteAlerts();
                for (var i = 0; i < data.length; i++) {
                    $scope.addAlert("error", data[i]);
                }
            });
        }
        else {
            $scope.deleteAlerts();
            $scope.addAlert("error", "Password does not match the confirmed new password");
        }
    };
});