﻿app.controller('UserDetailsController', function ($scope, $http, $location, $routeParams) {

    // viewModel
    $scope.user = {
        EmailAddress: '',
        JobTitle: '',
        FirstName: '',
        Surname: '',
        IsLoggedInUser: '',
        OldPassword: '',
        NewPassword: '',
        NewPasswordConfirm: '',
        MembershipRole: ''
    };

    // methods
    init();

    function init() {
        $scope.deleteAlerts();
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
    };

    $scope.resetUsersPassword = function () {
        // http GET
        $http({
            method: 'GET',
            url: 'api/users/ResetUsersPassword/' + $routeParams.id
        }).success(function (data, status, headers, config) {
            $scope.deleteAlerts();
            $scope.addAlert("success", "The user has been sent an email containing a new password");
        }).error(function (data, status, headers, config) {
            $scope.deleteAlerts();
            $scope.addAlert("error", "An unexpected error has occurred, please try again later");
        });
    };
});