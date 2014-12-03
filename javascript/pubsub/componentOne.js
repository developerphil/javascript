MYAPP.namespace('MYAPP.Components.ComponentOne');
MYAPP.Components.ComponentOne = function() {
    var componentname; 

    function init(name) {
        console.log("Initialise component - " + name);
		MYAPP.Polling.subscribe("event-one", function(){callbackOne(name)});
		MYAPP.Polling.subscribe("event-three", function(){callbackTwo(name)});
    }
	
	function callbackOne(name) {
		$('#'+ name +' select').append('<option>Received event one</option>');
		
		console.log("component one event fired 1 " + name);
	}
	
	function callbackTwo(name) {
		$('#'+ name +' select').append('<option>Received event three</option>');
		
		console.log("component one event fired 2 " + name);
	}

    return {
        init:init
    }
};