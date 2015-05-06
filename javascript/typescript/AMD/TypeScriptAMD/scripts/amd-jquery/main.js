require.config({
    baseUrl: 'scripts/amd-jquery',
    shim: {
        jquery: {
            exports: '$'
        }
    },
    paths: {
        'jquery': '../jquery-1.8.3',
        'toastr': '../toastr'
    }
});
require([
    'bootstrapper', 
    'jquery', 
    'toastr'
], function (bootstrapper, $, toastr) {
    bootstrapper.run();
});
//@ sourceMappingURL=main.js.map
