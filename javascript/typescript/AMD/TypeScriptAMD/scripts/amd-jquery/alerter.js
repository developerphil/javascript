define(["require", "exports", 'dataservice'], function(require, exports, __ds__) {
    var ds = __ds__;

    var dataservice = new ds.DataService();
    var Alerter = (function () {
        function Alerter() {
            this.name = 'John';
        }
        Alerter.prototype.showMessage = function () {
            var msg = dataservice.getMessage();
            $('#messagebox').text(msg + ', ' + this.name);
            toastr.info(msg + ', ' + this.name);
        };
        return Alerter;
    })();
    exports.Alerter = Alerter;    
})
//@ sourceMappingURL=alerter.js.map
