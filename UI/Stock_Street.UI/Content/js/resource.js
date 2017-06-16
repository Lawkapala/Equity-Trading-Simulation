var app = angular.module("Trader", ["ngResource"]);

app.factory('Entry', function ($resource) {
    return $resource("http://localhost:61347/api/TraderOrders");
}
);

app.factory('Entry1', function ($resource) {
    return $resource("http://localhost:61347/api/TraderOrders/Getod/204");

});

app.factory('Entry2', function ($resource) {
    return $resource("http://localhost:61347/api/TraderBlocks/GetOpenBlocks");

})
