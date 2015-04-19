module.exports = function(grunt) {
	grunt.initConfig({
		clean: {
			options: {force: true},
			all: {
				src: ['../LegacyDotNet/app_built/**/*.*']
			}
		},
		
		ngmin: {
			all: {
				files: [
					{
						expand: true,
						cwd: '../LegacyDotNet/app',
						src: ['**/*.js'],
						dest: '../LegacyDotNet/app_built/',
						ext: '.js'
					}
				]
			}
		},
		
		uglify: {
			all: {
				files: [
					{
						expand: true,
						cwd: '../LegacyDotNet/app_built',
						src: ['**/*.js'],
						dest: '../LegacyDotNet/app_built/',
						ext: '.min.js'
					}
				]
			}
		},
		
		concat: {
			options: {
				separator: ';'
			},
			dist: {
				src: ['../LegacyDotNet/app_built/**/*.min.js'],
				dest: '../LegacyDotNet/app_built/app_built.js'
			}
		}
		
	});


	grunt.registerTask('default', ['build']);
	grunt.registerTask('build', ['clean', 'ngmin', 'uglify', 'concat']);

	grunt.loadNpmTasks('grunt-contrib-clean');
	grunt.loadNpmTasks('grunt-ngmin');
	grunt.loadNpmTasks('grunt-contrib-uglify');
	grunt.loadNpmTasks('grunt-contrib-concat');
};