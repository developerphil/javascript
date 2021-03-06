﻿((): void => {
    'use strict';

    angular
        .module('app.usersettings')
        .config(config);

    config.$inject = [
        '$routeProvider',
        '$locationProvider'
    ];
    function config(
        $routeProvider: ng.route.IRouteProvider,
        $locationProvider: ng.ILocationProvider): void {
        $routeProvider
            .when('/admin/usersettings', {
                templateUrl: 'app/usersettings/usersettings.html',
                controller: 'app.usersettings.UserSettingsController',
                controllerAs: 'vm',
                resolve: {
                    user: userResolve
                }
            });
    }

    userResolve.$inject = [
        'currentUser',
        'app.services.UserService'
    ];
    function userResolve(currentUser: ICurrentUser,
        userService: app.services.IUserService): ng.IPromise<app.services.IUser> {
        return userService.getById(currentUser.userId);
    }
})(); 