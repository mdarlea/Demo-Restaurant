module.exports = function (grunt) {
    grunt.initConfig({
        dist: 'dist',
        swVersion: '1.0.19',
        pkg: grunt.file.readJSON('package.json'),
        
        ngdocs: {
            options: {
                dest: '<%= dist %>/docs',
                title: '<%= pkg.name %>',
                html5Mode: false,
                scripts: [
                    'angular.js',
                    'Scripts/angular-ui/ui-bootstrap-tpls.min.js',
                    'Scripts/sw-ui-bootstrap/sw-ui-bootstrap-tpls-<%= swVersion %>.min.js'
                ],
                sourceLink: 'https://github.com/mdarlea/Demo_Restaurant/blob/master/{{file}}'
            },
            api: {
                src: ['app/**/*.js', '!app/**/*.spec.js', 'Scripts/sw-ui-bootstrap/sw-ui-bootstrap-<%= swVersion %>.js', 'Scripts/sw-*.js'],
                title: 'API Documentation'
            }
        },
        connect: {
            options: {
                keepalive: true
            },
            server: {}
        },
        clean: ['<%= dist %>']
    });
    
    grunt.loadNpmTasks('grunt-ngdocs');
    grunt.loadNpmTasks('grunt-contrib-connect');
    grunt.loadNpmTasks('grunt-contrib-clean');
    
    grunt.registerTask('test', ['clean', 'ngdocs']);

    grunt.registerTask('default', ['test', 'connect']);
};