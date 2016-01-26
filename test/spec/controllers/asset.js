'use strict';

describe('Controller: AssetCtrl', function () {

  // load the controller's module
  beforeEach(module('expensePointApp'));

  var AssetCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    AssetCtrl = $controller('AssetCtrl', {
      $scope: scope
      // place here mocked dependencies
      
    });
  }));

it('should have no items to start', function () {
    expect(scope.assets.length).toBe(6);
});

it('should add items to the list', function(){
    scope.todo = 'Test 1';
    scope.asset = {'name': 'Yen', 'value': 2564.50, 'date': '2012-04-23T18:25:43.511Z', 'percentage': 25, 'imageSrc': ''};
    scope.addAsset(scope.asset);
    expect(scope.assets.length).toBe(7);
});

it('should add then remove an item from the list', function(){
    scope.todo = 'Test 1';
    scope.asset = {'name': 'Yen', 'value': 2564.50, 'date': '2012-04-23T18:25:43.511Z', 'percentage': 25, 'imageSrc': ''};
    scope.addAsset(scope.asset);
    scope.removeAsset(0);
    expect(scope.assets.length).toBe(6); 
});
  
});
