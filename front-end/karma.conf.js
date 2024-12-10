module.exports = (config) => {
  config.set({
    basePath: '../..',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    browsers: ['Edge'],
    plugins: [
      require('karma-jasmine'),
      require('karma-jasmine-html-reporter'),
      require('@angular-devkit/build-angular/plugins/karma'),
      require('@chiragrupani/karma-chromium-edge-launcher')
    ],
    reporters: ['kjhtml']
  });
}
