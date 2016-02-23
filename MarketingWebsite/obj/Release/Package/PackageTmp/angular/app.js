var app = angular.module('iriskApp', ['iriskApp.services', 'ui.bootstrap']);

//This configures the routes and associates each route with a view and a controller
app.config(function ($routeProvider) {
    $routeProvider

        .when('/users',
            {
                controller: 'UsersController',
                templateUrl: '/angular/partials/users.html'
            })

        .when('/userdetail/:id',
            {
                controller: 'UserDetailsController',
                templateUrl: '/angular/partials/userDetail.html'
            })

        .when('/useradd/',
            {
                controller: 'UserAddController',
                templateUrl: '/angular/partials/userAdd.html'
            })

        .otherwise({ redirectTo: '/users' });
});




