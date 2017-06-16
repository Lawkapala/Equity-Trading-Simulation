var app = angular.module("Trader", []);

app.controller("OpenOrders", function ($scope, $http) {

    $http.get('http://localhost/StockStreet.Service/api/TraderOrders')

})

