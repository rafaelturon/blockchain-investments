'use strict';

/**
 * @ngdoc function
 * @name expensePointApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the expensePointApp
 */
angular.module('expensePointApp')
  .controller('MainCtrl', function ($scope, localStorageService) {
    this.awesomeThings = [
    'HTML5 Boilerplate',
    'AngularJS',
    'Karma'
      ];
  });
