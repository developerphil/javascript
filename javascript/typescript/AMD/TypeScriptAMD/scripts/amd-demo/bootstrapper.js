define(["require", "exports", "alerter"], function(require, exports, __al__) {
    var al = __al__;

    function run() {
        var alerter = new al.Alerter();
        alerter.showMessage();
    }
    exports.run = run;
    ; ;
})
//@ sourceMappingURL=bootstrapper.js.map
