/// <reference path="../typings/require-2.1.d.ts" />

require.config({ 
    baseUrl: "scripts/amd-basic"  
}); 
 
//require(["BootStrapper",  
//    "../lib/jquery-1.7.2",
//    "../lib/underscore", 
//    "../lib/backbone", 
//    "../lib/console" ],  
require(["bootstrapper"],  
    (boot) => { 
        //var bootstrapper = new boot.BootStrapper();
        //bootstrapper.run(); 
        boot.run(); 
});