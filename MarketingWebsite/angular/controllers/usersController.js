app.controller('UsersController', function ($scope, $http, $location) {
    $scope.users = [];
    
    $scope.viewUser = function (id) {
        $location.path("/userdetail/" + id);
    };

    $scope.createUserAccount = function () {
        $location.path("/useradd/");
    };

    $http({
        method: 'GET',
        url: 'api/users/get/'
    }).success(function (data, status, headers, config) {
        $scope.users = data;
    });
});