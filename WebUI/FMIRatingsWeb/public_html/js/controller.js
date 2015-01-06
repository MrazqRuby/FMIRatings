var fmiRatingsApp = angular.module('fmiRatingsApp', []);

fmiRatingsApp.controller('HomeCtrl', function ($scope) {
  $scope.cources = [
    {'name': 'Analiz',
     'id': '1'},
    {'name': 'Diskretni strukturi',
     'id': '2'},
    {'name': 'Geometriq',
     'id': '3'}
  ];
});