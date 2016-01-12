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
      var assetsInStore = localStorageService.get('assets');
      $scope.assets = assetsInStore || [];
      
      $scope.$watch('assets', function(){
          localStorageService.set('assets', $scope.assets);
      }, true);
      
      $scope.addAsset = function() {
          $scope.assets.push($scope.asset);
          $scope.asset = '';  
        };
        
        $scope.removeAsset = function(index) {
          $scope.assets.splice(index, 1);  
        };
  });
