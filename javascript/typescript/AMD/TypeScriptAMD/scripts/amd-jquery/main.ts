/// <reference path="../typings/require-2.1.d.ts" />
/// <reference path="bootstrapper.ts" />

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

require(['bootstrapper', 'jquery', 'toastr'], 
    (bootstrapper, $, toastr) => { 
        bootstrapper.run(); 
});