app.controller('UsersController', function ($scope, $http, $location) {
    $scope.users = [];
    
    $scope.viewUser = function (id) {
        $location.path("/userdetails/" + id);
    };

    $http({
        method: 'GET',
        url: '/api/users'
    }).success(function (data, status, headers, config) {
        $scope.users = data;
    });
});