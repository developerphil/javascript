MYAPP.namespace('MYAPP.Components.ComponentTwo');
MYAPP.Components.ComponentTwo = function() {
    var componentname; 

    function init(name) {
		var componentname;
	
        console.log("Initialise component" + name);
		MYAPP.Polling.subscribe("event-one", function(){callbackOne(name)});
    }
	
	function callbackOne(name) {
		$('#'+ name +' select').append('<option>Received event one</option>');
		
		console.log("component two event fired 1 " + name);
	}

    return {
        init:init
    }
};