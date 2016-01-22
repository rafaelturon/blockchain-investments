'use strict';

/**
 * @ngdoc overview
 * @name expensePointApp
 * @description
 * # expensePointApp
 *
 * Main module of the application.
 */
angular
  .module('expensePointApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'ui.sortable',
    'pascalprecht.translate',
    'LocalStorageModule'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl',
        controllerAs: 'main'
      })
      .when('/asset',{
        templateUrl: 'views/asset.html',
        controller: 'AssetCtrl'
      })
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl',
        controllerAs: 'about'
      })
      .otherwise({
        redirectTo: '/'
      });
  })
  .config(['$translateProvider', function($translateProvider) {
    $translateProvider.translations('en', {
      'HOME': 'Home',
      'ASSET': 'Asset',
      'CONTACT': 'Contact',
      'ABOUT': 'About'
    });
    $translateProvider.translations('pt', {
      'HOME': 'Início',
      'ASSET': 'Ativos',
      'CONTACT': 'Contatos',
      'ABOUT': 'Sobre'
    });
 
    $translateProvider.preferredLanguage('pt');
  }]);
