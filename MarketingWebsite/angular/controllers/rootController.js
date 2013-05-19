app.controller('RootController', function ($scope) {

    // methods
    init();

    function init() {
        $scope.alerts = [];
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