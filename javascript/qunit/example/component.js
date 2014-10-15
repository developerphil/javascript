var ExampleComponent = {};

ExampleComponent.InitialiseComponent = function() {

    $("#ExampleButton").click(function() {
        $("#Content").html("Clicked");

        $.getJSON("/example/", function(response) {
            if ( response.status == "success") {
                $("#Result").html( "The result for ajax request is: " + response.result );
            } else {
                $("#Result").html( "The result for ajax request is: " + response.result );
            }
        });
    });

    $("#RedirectButton").click(function() {
        ExampleLibrary.Redirect("http://www.google.com");
    });

    $("#MouseExample").mouseover(function() {
        $(this).removeClass("off").addClass("on");
    });

    $( "#MouseExample" ).mouseout(function() {
        $(this).removeClass("on").addClass("off");
    });
}

var ExampleLibrary = {};

ExampleLibrary.Redirect = function(href) {
    window.location = href;
}