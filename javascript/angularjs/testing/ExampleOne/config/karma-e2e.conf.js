module.exports = function(config) {
  config.set({
	  
		basePath : '../app',
		
		autoWatch: false,
		
		singleRun: true,
		
		frameworks: ['ng-scenario'],
		
		files:[
		 '../test/e2e/**/*.js'
		],
			
		proxies: {
			'/': 'http://localhost:8000/'
		},
	  
		exclude: [
		],
		
		reporters: ['progress'],
	  
		colors: true,
	  
		logLevel: config.LOG_WARN,
		
		browsers : ['Chrome'],
		
		plugins : [
            'karma-chrome-launcher',
            'karma-ng-scenario'
            ],
			
		captureTimeout: 60000
	
	});
};
