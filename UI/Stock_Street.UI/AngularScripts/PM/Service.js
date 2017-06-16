var app = angular.module("ServiceApp", ["ngResource"]);

    app.factory('GetPortfolios', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetPortfolios/");
    });

    app.factory('GetOrders', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetPortfolioOrders/");
    });
    
    app.factory('CreatePortfolio', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/CreatePortfolio/");
    });

    app.factory('CreateOrder', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/CreateOrder/");
    });

    app.factory('SymbolLookup', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/DataAccess/Get");
    });

    app.factory('GetET', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetTraderdetails");
    });

    app.factory('GetPending', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetPending/:id");
    });

    app.factory('SendOrder', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/SendOrder/:list", {}, {
            send: {
                method: "POST",
                isArray: true
            }
        });
    });

    app.factory('DeleteOrder', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/DeleteOrders/:list", {}, {
            remove : {
                method: "POST",
                isArray: true
            }
        });
    });

    app.factory('GetOrderDetails', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetOrderDetails/", {}, {
            query: {
                method: "GET",
                isArray: false
            }
        });
    });

    app.factory('EditOrder', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/EditOrder/");
    });

    app.factory('GetExecuted', function ($resource) {
        return $resource("http://localhost/StockStreet.Service/api/PortfolioManager/GetExecuted/");
    });