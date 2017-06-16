
var Open = angular.module("Open", ["Trader"]);


Open.controller("OpenOrders", function ($scope, GetOpenOrderService, creBlock, existBlock, GetOpenBlockService,$location, TokenService) {

    var t = TokenService.GetToken($location)
    $scope.GetOrders = GetOpenOrderService.query({ token: t });
    $scope.orderArray = [];
    $scope.blockData = GetOpenBlockService.query({ token: t });
    $scope.blockIdSelected = null;
    $scope.save = function () {
        debugger;

        angular.forEach($scope.GetOrders, function (G) {
            if (G.selected) $scope.orderArray.push(G.orderId);
        });
        console.log($scope.orderArray);
        creBlock.send($scope.orderArray);
    }

    $scope.saveToBlock = function (selectedId) {
        debugger;
      
        $scope.orderArray.push(selectedId);
        console.log($scope.orderArray);
        angular.forEach($scope.GetOrders, function (G) {
            if (G.selected) $scope.orderArray.push(G.orderId);
        });
        
        console.log($scope.orderArray);        
        existBlock.send($scope.orderArray);
    }


    console.log($scope.orderArray);
});

Open.controller("EditBlock", function ($scope, GetBlockService, $window, $location, GetodService, Entry5, Entry6) {
    var fData = {};
    $scope.BlockOrders = null;
    $scope.block = null;
    fData.blockId = $location.absUrl().split('/')[6];
    $scope.block = GetBlockService.get({ id: fData.blockId });
    console.log($scope.block);
    $scope.BlockOrders = GetodService.query({ id: fData.blockId });
    $scope.RemoveOrder = function (G) {

        $scope.O = G;
        console.log($scope.O);
    }

    $scope.save = function (block) {
        debugger;
        Entry6.send(block);
    }


});

Open.controller("OpenBlocks", function ($scope, GetOpenBlockService, GetodService, $window, $location, GetBlockService, Entry7, execute, changeStatus, TokenService) {
    $scope.val1 = false;
    var t = TokenService.GetToken($location);
    $scope.Blocks = GetOpenBlockService.query({ token: t });
    $scope.Orders = null;
    $scope.orderArray = [];
    debugger;
    console.log($scope.Blocks);
    $scope.x = function (id) {


        $scope.Orders = GetodService.query({ id: id });
        console.log($scope.Orders)
        $scope.val1 = true;

    }

    $scope.cancel = function (obj) {

        $scope.O = obj;
        console.log($scope.O);
    }

    $scope.confirmDelete = function (id) {
        debugger;
        Entry7.delete({ id: id },

            function (success) {
                alert("Deleted Successfully");
                location.reload();
            },
            function (error) {
                alert("Server Error");
            });

    }

    $scope.Execute = function () {

        angular.forEach($scope.Blocks, function (obj) {
            debugger;
            if (obj.selected) $scope.orderArray.push(obj);
        });

        console.log($scope.orderArray);
        debugger;
        changeStatus.change($scope.orderArray);
        execute.saveData($scope.orderArray);
    }


});

Open.controller("ExecutedBlocks", function ($scope, GetExecutedBlockService, GetodService, allocateBlock, allocateOrder,$location, TokenService) {
    var t = TokenService.GetToken($location);
    $scope.val = false;
    var data = {};
    data = allocateBlock.query({},
        function (success) {
            console.log(data);
            allocateOrder.alloc(data);
            $scope.ExeBlocks = GetExecutedBlockService.query({ token: t });
            console.log($scope.ExeBlocks);
        }
        );
    $scope.Orders = null;

    $scope.y = function (id) {


        $scope.Orders = GetodService.query({ id: id });
        debugger;
        $scope.val = true;

    }


});

Open.controller("DeleteController", function (Entry5, $scope) {
    $scope.confirmDelete = function (id) {
        Entry5.RemoveOrder({ id: id },

            function (success) {
                alert("Deleted Successfully");
                location.reload();
            },
            function (error) {
                alert("Server Error");
            });

    }
});

