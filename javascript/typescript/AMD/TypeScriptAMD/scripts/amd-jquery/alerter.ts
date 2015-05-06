/// <reference path="../typings/toastr-1.2.d.ts" />
/// <reference path="../typings/jquery.d.ts" />

import ds = module('dataservice');

declare var $: JQueryStatic;
declare var toastr: Toastr;

var dataservice = new ds.DataService();

export interface IAlerter {
    name: string;
    showMessage(): void;
}

export class Alerter implements IAlerter {
    name = 'John';
    showMessage() {
        var msg = dataservice.getMessage();
        //alert(msg + ', ' + this.name);
        $('#messagebox').text(msg + ', ' + this.name);
        toastr.info(msg + ', ' + this.name);
    };
}