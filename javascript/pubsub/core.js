var MYAPP = MYAPP || {};

MYAPP.namespace = function (ns_string) {
    var parts = ns_string.split('.'),
        parent = MYAPP,
        i;

    // strip redundant leading global
    if (parts[0] === "MYAPP") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        // create a property if it doesn't exist
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};

MYAPP.namespace('MYAPP.Core');
MYAPP.Core = (function() {
    var components = {};

    function registerComponent(componentName, component) {
        if (MYAPP.Components[component] && !components[componentName]) {
            components[componentName] = new MYAPP.Components[component]();
        } else {
            console.log("Component already defined with " + componentName + " or component doesn't exist");
        }
    }

    function startAllComponents() {
        for (var component in components) {
            if (components.hasOwnProperty(component)) {
                startSpecificComponent(component);
            }
        }
    }

    function startSpecificComponent(componentName) {
        components[componentName].init(componentName);
    }

    return {
        registerComponent:registerComponent,
        startAllComponents:startAllComponents
    };
}());

