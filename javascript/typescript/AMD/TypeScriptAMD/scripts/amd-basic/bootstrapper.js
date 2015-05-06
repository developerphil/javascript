define(["require", "exports", "greeter"], function(require, exports, __gt__) {
    var gt = __gt__;

    function run() {
        var el = document.getElementById("content");
        var greeter = new gt.Greeter(el);
        greeter.start();
    }
    exports.run = run;
    ; ;
})
//@ sourceMappingURL=bootstrapper.js.map
