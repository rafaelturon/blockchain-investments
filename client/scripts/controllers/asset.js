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
      'percentage': 34,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    },
    {
      'name': 'Silver',
      'value': 3564.50,
      'date': '2014-04-23T18:25:43.511Z',
      'percentage': 41,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    },
    {
      'name': 'Bitcoin',
      'value': 2564.50,
      'date': '2012-04-23T18:25:43.511Z',
      'percentage': 25,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    },
    {
      'name': 'Dollar',
      'value': 1564.50,
      'date': '2013-04-23T18:25:43.511Z',
      'percentage': 34,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    },
    {
      'name': 'Euro',
      'value': 3564.50,
      'date': '2014-04-23T18:25:43.511Z',
      'percentage': 41,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    },
    {
      'name': 'Yen',
      'value': 2564.50,
      'date': '2012-04-23T18:25:43.511Z',
      'percentage': 25,
      'imageSrc': 'https://uphold.com/release/images/icons-cards/BRL@2x.11e93b54.png?v2.1.0'
    }
  ];
  
  $scope.addAsset = function() {
      $scope.assets.push($scope.asset);
      $scope.asset = '';  
  };
  
  $scope.removeAsset = function(index) {
    $scope.assets.splice(index, 1);  
  };
  
});