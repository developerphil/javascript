/// <reference path="../typings/require-2.1.d.ts" />

require.config({ 
    baseUrl: 'scripts/amd-demo' 
}); 

require(['bootstrapper'],  
    (bootstrapper) => { 
        bootstrapper.run();
});