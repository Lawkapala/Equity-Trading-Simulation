var app = angular.module("Trader", ["ngResource"]);

app.factory('GetOpenOrderService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderOrders/GetOpenOrders/:token", { token: '@t' });
}
);

app.factory('GetodService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderOrders/Getod/:id", { id: '@id' });

});

app.factory('GetOpenBlockService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/GetOpenBlocks/:token", { token: '@t' });

});

app.factory('GetExecutedBlockService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/GetExecutedBlocks/:token", { token: '@t' });
});

app.factory('GetBlockService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/GetBlock/:id", { id: '@id' });
});

app.factory('Entry5', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderOrders/RemoveOrder/:id", { id: '@id' }, { RemoveOrder: { method: 'DELETE' } });
});

app.factory('Entry6', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/EditBlock/:block", {}, { send: { method: 'PUT', isArray: true } });
});

app.factory('Entry7', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/DeleteBlock/:id", { id: '@id' }, { delete: { method: 'DELETE' } });
});


app.factory('creBlock', function ($resource) {

    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/AddToNewBlock/:list", {}, {
        send: {
            method: "POST",
            isArray: true
        }
    });
});


app.factory('existBlock', function ($resource) {

    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/AddToExistingBlock/:list", {}, {
        send: {
            method: "POST",
            isArray: true
        }
    });
});


app.factory('execute', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerblockview/:list", {}, {
        saveData: {
            method: "POST",
            isArray: true
        }
    });

});

app.factory('changeStatus', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/ChangeBlockStatus/:list", {},
        {
            change: {
                method: "POST",
                isArray: true
            }
        });
});


app.factory('allocateBlock', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerhistory/get");
});

app.factory('allocateOrder', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/ReceiveFills/:list", {},
       {
           alloc: {
               method: "POST",
               isArray: true
           }
       });
})