QUnit.module( "Test Example",{
    setup: function() {
        $.mockjaxSettings.responseTime = 100;
        ExampleComponent.InitialiseComponent();
    },
    teardown: function(){
        $.mockjax.clear()
    }
});

QUnit.test( "Example Sync Test Click", 1, function( ) {
    $.mockjax({
        url: "/example/",
        responseText: {
            status: "pass",
            result: "true"
        }
    });

    $("#ExampleButton").trigger("click");

    var value = $("#Content").html();

    equal( value, "Clicked", "Passed!" );
});

QUnit.test( "Example Sync Test Mouseover and Mouseout", 6, function( ) {
    equal( $("#MouseExample").hasClass("off"), true, "Passed!" );
    equal( $("#MouseExample").hasClass("on"), false, "Passed!" );

    $("#MouseExample").trigger("mouseover");

    equal( $("#MouseExample").hasClass("on"), true, "Passed!" );
    equal( $("#MouseExample").hasClass("off"), false, "Passed!" );

    $("#MouseExample").trigger("mouseout");

    equal( $("#MouseExample").hasClass("off"), true, "Passed!" );
    equal( $("#MouseExample").hasClass("on"), false, "Passed!" );
});

QUnit.test( "Example Sync Test Window Location", 1, function( ) {
    ExampleLibrary.Redirect = function(href){
        equal( href, "http://www.google.com", "Passed!" );
    };

    $("#RedirectButton").trigger("click");
});

QUnit.asyncTest('Example Async Test 2', 1, function() {

    $.mockjax({
        url: "/example/",
        responseText: {
            status: "success",
            result: "true"
        }
    });

    $("#ExampleButton").trigger("click");

    setTimeout(function() {
        equal($("#Result").html(), "The result for ajax request is: true", "Example Async Test Passed" );
        start();
    }, 200);
});

QUnit.asyncTest('Example Async Test 3', 1, function() {

    $.mockjax({
        url: "/example/",
        responseText: {
            status: "fail",
            result: "false"
        }
    });

    $("#ExampleButton").trigger("click");

    setTimeout(function() {
        equal($("#Result").html(), "The result for ajax request is: false", "Example Async Test Passed" );
        start();
    }, 200);
});


