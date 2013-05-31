app.controller('RootController', function ($scope, $http, $location) {

    // methods
    init();

    function init() {
        $scope.alerts = [];

        $scope.loggedInUser = {};
        getLoggedInUser();
    };

    function getLoggedInUser() {
        $http({
            method: 'GET',
            url: 'api/users/getloggedinuser/'
        }).success(function (data, status, headers, config) {
            $scope.loggedInUser = data;
        });
    }

    $scope.goToMyAccount = function () {
        $location.path("/userdetail/" + $scope.loggedInUser.Id);
    };

    $scope.deleteAlerts = function (type, msg) {
        $scope.alerts = [];
    };

    $scope.addAlert = function (type, msg) {
        $scope.alerts.push({ 'type': type, 'msg': msg });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

});