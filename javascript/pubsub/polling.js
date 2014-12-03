MYAPP.namespace('MYAPP.Polling');
MYAPP.Polling = (function() {

    var messages = {};
	var events = ["event-one", "event-two", "event-three"];
	
	function initialise() {
		setInterval(function(){publish(getRandomEvent())}, 3000);
	}
	
    function publish(name) {
		console.log("Publish Event" + name);
	
		$('#events select').append('<option>'+name+'</option>');
	
        if (messages[name] !== undefined) {
            for (var i = 0; i < messages[name].length; i++) {
                messages[name][i].call({});
            }
        }
    }

	function getRandomEvent() {
		return events[Math.floor((Math.random() * 3))];
	}
	
    function subscribe(name, callback) {
        if (messages[name] === undefined) {
            messages[name] = [callback];
        } else {
            var alreadySubcribed = false;

            for (var i = 0; i < messages[name].length; i++) {
                if (messages[name][i] === callback) {
                    alreadySubcribed = true;
                }
            }

            if (!alreadySubcribed) {
                messages[name].push(callback);
            } else {
                console.log("Component already subscribed to " + name);
            }
        }
    }

    return {
        publish:publish,
        subscribe:subscribe,
		initialise:initialise
    };
}());