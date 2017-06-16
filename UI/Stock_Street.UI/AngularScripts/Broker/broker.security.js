var ApiApp = angular.module('ApiApp', ['BRServiceApp', 'ui.router', 'ngRoute']);

//ApiApp.config(function ($routeProvider) {
//    $routeProvider.when("/BrokerEditMarketData1", {
//        templateUrl: "Views/Broker/BrokerEditMarketData.cshtml",
//        controller: "editCtrl"
//    })

//});
//app.config(function ($stateProvider, $urlRouterProvider) {
//    $stateProvider
//    .state('EditSecurity', {
//        url: '../Broker/BrokerEditMarketData',
//        templateUrl: '../Broker/BrokerEditMarketData'
//    })
//    //.state('EditData', {
//    //    url: '/EditData',
//    //    templateUrl: '../Broker/BrokerEditMarketData.cshtml'
//    //})
//});



ApiApp.factory("myFactory", function () {
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

ApiApp.controller("SecurityController", function ($scope, Entry1, $location, myFactory) {
    $scope.securities = Entry1.query();
    $scope.deleteData = {};
    $scope.Edit = function (d) {
    
        myFactory.set(d);
        //$location.path('/EditSecurity');
        //$location.path('BrokerEditMarketData');
    }

    $scope.callToAddToMarketList = function (currObj) {
    var data = currObj.securitySymbol;
        brokerEditService.addMarketData(data);
    };
    $scope.del = function (sec) {
        $scope.security = sec;
        console.log($scope.security);
    }



});

//app.controller("editCtrl", function ($scope, Entry3, $window, $location) {
ApiApp.controller("editCtrl", function ($scope, Entry4, $location) {
    var fData = {};
    //$scope.block = null;
    $scope.data = null;
    fData.symbolId = $location.absUrl().split('/')[6];
    //$scope.block = Entry3.get({ id: fData.blockId });
    $scope.data = Entry4.query({ id: fData.symbolId });


});

ApiApp.controller("saveCtrl", function ($scope, SaveSecData, $location, $window) {


    $scope.save = function (data) {
       
        SaveSecData.send(data);
        $window.location.href = 'http://localhost:35986/Broker/BrokerUpdateMarketData/c2hhcmF0aA==';
        //location.reload($window.location.href);

    };
});



ApiApp.controller("AddSecurityController", function (InsertSecData, $scope, $window) {
    $scope.AddSecurity = function (data) {
        console.log(data);
       

        InsertSecData.add(data);
        $window.location.href = 'http://localhost:35986/Broker/BrokerUpdateMarketData/c2hhcmF0aA==';
    };
});

ApiApp.controller("DeleteSecurityController", function (Entry33, $scope) {
    $scope.confirmDelete = function (id) {
      
        Entry33.remove({ id: id },
            function (success) {
                alert("Deleted Successfully");
               
                location.reload();
            },
            function (error) {
                alert("Server Error");
            });
        

    }
});

ApiApp.controller("HistoryController", function ($scope, History, detailHistory, SendReport) {
    $scope.history = History.query();
    $scope.historyData = {};
    $scope.detailHistory = function (id) {

        $scope.historyData = detailHistory.query({ id: id });
        console.log($scope.historyData);
    }

    $scope.sendReport = function () {
        $scope.block = History.query();
         SendReport.send(history);
    }

});

ApiApp.controller("BlockController", function ($scope, Entry2) {
    $scope.blocks = Entry2.query();
    //console.log($scope.securities);

});

ApiApp.controller("ExecuteController", function ($scope, Execute1) {
    $scope.confirmExecute = function () {
        debugger;

        //$scope.result = Execute1.exe({}, function (success) {
        //    alert("In Execution");
        //    location.reload();
        //});

        $scope.res = Execute1.exe();
        
    }
});

ApiApp.controller("PartialController", function ($scope, Partial, detailHistory) {
    $scope.partial = Partial.query();
    $scope.PartialData = {};
    $scope.detailHistory = function (id)
    {
        $scope.PartialData = detailHistory.query({ id: id });
        console.log($scope.PartialData);
    
    }
    
});

