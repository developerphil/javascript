﻿((): void => {
    'use strict';

    angular
        .module('app.users')
        .config([
            '$routeProvider',
            '$locationProvider',
            config]);

    function config(
        $routeProvider: ng.route.IRouteProvider,
        $locationProvider: ng.ILocationProvider): void {
        $routeProvider
            .when('/admin/users', {
                templateUrl: 'app/users/users.html',
                controller: 'app.users.UsersController',
                controllerAs: 'vm'
            });
    }
})(); 