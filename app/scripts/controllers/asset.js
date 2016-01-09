'use strict';

/**
 * @ngdoc function
 * @name expensePointApp.controller:AssetCtrl
 * @description
 * # AssetCtrl
 * Controller of the expensePointApp
 */
angular.module('expensePointApp').controller('AssetCtrl', function ($scope, localStorageService) {
  var assetsInStore = localStorageService.get('assets');
  $scope.assets = assetsInStore || [];
  
  $scope.assets = [
    {
      'name': 'Gold',
      'value': 1564.50,
      'date': '2013-04-23T18:25:43.511Z',
      'percentage': 34
    },
    {
      'name': 'Silver',
      'value': 3564.50,
      'date': '2014-04-23T18:25:43.511Z',
      'percentage': 41
    },
    {
      'name': 'Bitcon',
      'value': 2564.50,
      'date': '2012-04-23T18:25:43.511Z',
      'percentage': 25
    },
    {
      'name': 'Dollar',
      'value': 1564.50,
      'date': '2013-04-23T18:25:43.511Z',
      'percentage': 34
    },
    {
      'name': 'Euro',
      'value': 3564.50,
      'date': '2014-04-23T18:25:43.511Z',
      'percentage': 41
    },
    {
      'name': 'Yen',
      'value': 2564.50,
      'date': '2012-04-23T18:25:43.511Z',
      'percentage': 25
    }
  ];
  
});