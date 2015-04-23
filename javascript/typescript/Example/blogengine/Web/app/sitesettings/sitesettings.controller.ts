module app.sitesettings {
    'use strict';

    interface ISiteSettingsScope {
        siteSettings: app.services.ISiteSettings;
        themeNames: string[];
        save: () => void;
    }

    class SiteSettingsController implements ISiteSettingsScope {
        siteSettings: app.services.ISiteSettings;
        themeNames: string[] = [];

        static $inject = [
            'siteSettings',
            'app.services.SiteSettingsService'
        ];
        constructor(siteSettings: app.services.ISiteSettings,
            private siteSettingsService: app.services.ISiteSettingsService) {
            this.siteSettings = siteSettings;
            this.themeNames = siteSettings.availableThemeNames;
        }

        save(): void {
        }
    }
    
    angular
        .module('app.sitesettings')
        .controller('app.sitesettings.SiteSettingsController',
        SiteSettingsController);
}