app.controller('UserDetailsController', function ($scope, $http, $location, $routeParams) {

    $scope.user = {
        JobTitle: '',
        FirstName: '',
        Surname: '',
        IsLoggedInUser: ''
    };

    $scope.returnToList = function () {
        $location.path("/users");
    };

    $http({
        method: 'GET',
        url: 'api/users/' + $routeParams.id
    }).success(function (data, status, headers, config) {
        $scope.user = data;
    });

});