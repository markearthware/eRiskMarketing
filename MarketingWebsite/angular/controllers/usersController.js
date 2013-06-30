app.controller('UsersController', function ($scope, $http, $location) {

    init();

    $scope.users = [];
    
    $scope.viewUser = function (id) {
        $location.path("/userdetail/" + id);
    };

    $scope.createUserAccount = function () {
        $location.path("/useradd/");
    };

    $scope.deleteUserAccount = function (id) {
        if (confirm("Are you sure you want to delete this user?")) {
            $http({
                method: 'DELETE',
                url: 'api/users/delete/' + id
            }).success(function (data, status, headers, config) {
                $scope.deleteAlerts();
                $scope.addAlert("success", "The user has been deleted successfully");
                init();
            });
        }
    };

    function init() {
        $http({
            method: 'GET',
            url: 'api/users/get/'
        }).success(function (data, status, headers, config) {
            $scope.users = data;
        });
    };
});