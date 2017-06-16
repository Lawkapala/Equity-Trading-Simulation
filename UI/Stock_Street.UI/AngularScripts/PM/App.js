var UiApp = angular.module("UiApp", ["ServiceApp"]);

UiApp.factory("myFactory", function () {
    var savedData = {}
    function set(data) {
        this.savedData = data;
    }
    function get() {
        return this.savedData;
    }

    return {
        set: set,
        get: get
    }
});


UiApp.controller("PortFolioController", function ($scope, GetPortfolios, GetOrders, TokenService, $location) {
    var t = TokenService.GetToken($location);
    $scope.Portfolios = GetPortfolios.query({ token: t });
    $scope.val = 0;
    $scope.addOrder = function (id) {
        debugger;
        $scope.val = id;
        $scope.Orders = GetOrders.query({ id: $scope.val });
    };
});

UiApp.controller("CreatePortfolioController", function ($scope, CreatePortfolio, TokenService, $location) {
    var t = TokenService.GetToken($location);
    $scope.create = function (data) {
        console.log(data);
        data.userName = t;
        CreatePortfolio.save(data);        
    };
});

//UiApp.controller("OrderController", function ($scope, GetOrders, sharedProperties) {//need dynamic
//    $scope.$watch(function () {
//        return sharedProperties.getId()
//    }, function (newValue, oldValue) {
//        if (newValue != oldValue) {
//            $scope.item = newValue;
//            $scope.Orders = GetOrders.query({ id: 25 });
//        }
//    });
//});

UiApp.controller("CreateOrderController", function ($scope, CreateOrder, GetET, GetPortfolios, $window, TokenService, $location, myFactory) { //need dynamic
    var t = TokenService.GetToken($location);
    console.log(t);
    $scope.createOrder = function (data) {
        data.orderStatus = "New";
        data.createTStamp = new Date();
        data.openQuantity = data.totalQuantity;
        data.executedQuantity = 0;
        console.log(t);
        debugger;
        CreateOrder.save(data);
    };
    $scope.ETs = GetET.query();
    $scope.PMPortfolios = GetPortfolios.query({ token: t });

    var data = {};
    $scope.fillPrice = function (data) {
        if (data.orderType == "Market") {
            var symbol = document.getElementById("symbol").value; 
            //$scope.Symbols = SymbolLookup.query();
            document.getElementById("price").value = myFactory.get();
            $scope.toggle = true;
        }
        else {
            $scope.toggle = false;
        }
    }
});

UiApp.controller("SymbolController", function ($scope, SymbolLookup) {
    $scope.Symbols = SymbolLookup.query();
});

UiApp.controller("PendingController", function ($scope, GetPending, SendOrder, DeleteOrder, TokenService, $location) {
    var t = TokenService.GetToken($location);
    $scope.PendingOrders = GetPending.query({token: t });
    $scope.pendingArray = [];
    $scope.amend = "Open";
    $scope.save = function () {
        angular.forEach($scope.PendingOrders, function (pending) {
            if (pending.selected) $scope.pendingArray.push(pending.orderId);
        });
        console.log($scope.pendingArray);
        SendOrder.send($scope.pendingArray);
    };

    $scope.deleteArray = [];

    $scope.del = function () {
        angular.forEach($scope.PendingOrders, function (pending) {
            //debugger;
            if (pending.selected) $scope.deleteArray.push(pending.orderId);
        });
        console.log($scope.deleteArray);
        DeleteOrder.remove($scope.deleteArray);
    };
});

UiApp.controller("ExecutedController", function ($scope, GetExecuted, TokenService, $location) {
    var t = TokenService.GetToken($location);
    $scope.Executed = GetExecuted.query({ token: t });
});

UiApp.controller("FillDataController", function ($scope, myFactory) {
    var symbol = {};
    $scope.fill = function (symbol) {
        document.getElementById("symbol").value = symbol.symbol;
        document.getElementById("price").value = symbol.price;
        myFactory.set(symbol.price);

    }
});

UiApp.controller("EditPendingOrdersController", function ($scope, GetOrderDetails, GetET, GetPortfolios, EditOrder, $window, $location,TokenService) { //need dynamic
    var fData = {};
    $scope.block = null;
    var t = TokenService.GetToken($location);
    fData.orderId = $location.absUrl().split('/')[6];
    debugger;
    console.log(fData.orderId);
    $scope.block = GetOrderDetails.query({ orderId: fData.orderId });
    console.log($scope.block);
    $scope.buy = false;
    if($scope.block.side == "Buy")
        $scope.buy = true;

    $scope.ETs = GetET.query();
    $scope.PMPortfolios = GetPortfolios.query({ token: t });

    $scope.editOrder = function (block) {
        EditOrder.save(block);
        $window.location.href = 'http://localhost:35986/PM/PMPendingOrders/' + t;

    }

    $scope.amendOrder = function (block) {
        EditOrder.save(block);
        $window.location.href = 'http://localhost:35986/PM/PMPendingOrders/' + t;
    }

    $scope.redirect = function () {
        $window.location.href = 'http://localhost:35986/PM/PMPendingOrders/' + t;
    }
});


//UiApp.controller("AmendPendingOrdersController", function ($scope, GetOrderDetails, $window, $location) {
//    var fData = {};
//    $scope.block = null;
    
//    fData.blockId = $location.absUrl().split('/')[6];
//    console.log(fData.blockId);
//    $scope.order = GetOrderDetails.query({ orderId: fData.blockId });
//    console.log($scope.order);
//});

