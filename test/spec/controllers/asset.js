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
});
