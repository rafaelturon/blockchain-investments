'use strict';

describe('Controller: HeaderCtrl', function () {

  // load the controller's module
  beforeEach(module('expensePointApp'));

  var HeaderCtrl,
    $location,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope, _$location_) {
    $location = _$location_;
    scope = $rootScope.$new();
    
    HeaderCtrl = $controller('HeaderCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should have a method to check if the path is active', function () {
    $location.path('/');
    expect($location.path()).toBe('/');
    expect(scope.isActive('/')).toBe(true);
    expect(scope.isActive('/about')).toBe(false);;
  });
});
