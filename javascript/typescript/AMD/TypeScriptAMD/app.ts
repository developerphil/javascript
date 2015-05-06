/// <reference path="scripts/app/greeter.ts" />

import gt = module("scripts/app/greeter");

window.onload = () => {
    var el = document.getElementById('content');
    var greeter = new gt.Greeter(el);
    greeter.start();
};