var app = angular.module("Open", ["Trader"]);

app.controller("OpenOrders", function ($scope, Entry) {
    $scope.GetOrders = Entry.query();
    console.log($scope.GetOrders);
});

app.controller("BlockedOrders", function ($scope, Entry1) {
    $scope.Orders = Entry1.query();
    console.log($scope.Orders);

});

app.controller("OpenBlocks", function ($scope, Entry2) {
    $scope.Blocks = Entry2.query();
    console.log($scope.Blocks);

})