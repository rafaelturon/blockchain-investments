'use strict';

/**
 * @ngdoc function
 * @name expensePointApp.controller:HeaderCtrl
 * @description
 * # HeaderCtrl
 * Controller of the expensePointApp
 */
angular.module('expensePointApp').controller('HeaderCtrl', function ($scope, $location) {
	$scope.isActive = function (viewLocation) { 
        return viewLocation === $location.path();
	};
});
