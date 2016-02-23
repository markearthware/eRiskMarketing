app.controller('UserAddController', function ($scope, $http, $location, $routeParams) {

    // viewModel
    $scope.user = {
        EmailAddress: '',
        JobTitle: '',
        FirstName: '',
        Surname: '',
        MembershipRole: ''
    };

    // methods
    init();

    function init() {
        $scope.deleteAlerts();
    };

    $scope.returnToList = function () {
        $location.path("/users");
    };

    $scope.addUser = function () {
        $http({
            method: 'POST',
            url: 'api/users/add/',
            data: $scope.user
        }).success(function (data, status, headers, config) {
            $scope.deleteAlerts();
            $scope.addAlert("success", "The user has been added successfully");
        }).error(function (data, status, headers, config) {
            $scope.deleteAlerts();
            for (var i = 0; i < data.length; i++) {
                $scope.addAlert("error", data[i]);
            }
        });
    };
});