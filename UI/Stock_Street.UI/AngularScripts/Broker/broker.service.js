var app = angular.module('BRServiceApp', ["ngResource"])

app.factory('Entry1', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/security")
});

app.factory('InsertSecData', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/security/Post", {}, {
        add: {
            method: 'POST'
        }
    });
});

app.factory('Entry2', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerblockview");

});

app.factory('Entry3', function ($resource) {
    
    return $resource("http://localhost/StockStreet.Service/api/security/:id", { id: '@id' }, { update: { method: 'PUT' } },
                                                                               { remove: { method: 'DELETE' } }, 
                                                                                  { get: { method: 'GET' } }
                                                                                                          );
});

app.factory('Entry33', function ($resource) {
   
    return $resource("http://localhost/StockStreet.Service/api/security/Delete/:id", { id: '@id' }, { remove: { method: 'DELETE' } });
});

app.factory('Entry4', function ($resource) {
    
    return $resource("http://localhost/StockStreet.Service/api/security/get/:id", {}, {
        query: { method: 'GET', params: { id: 'id' } }
    });
});

app.factory('SaveSecData', function ($resource) {
  
    return $resource("http://localhost/StockStreet.Service/api/security/Put", {}, {
        send: {
            method: 'PUT'
        }
    });
});



app.factory('History',function($resource){
    return $resource("http://localhost/StockStreet.Service/api/brokerhistory/get");
});

app.factory('detailHistory', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerhistory/get/:id", {}, {
        query: { method: 'GET', params: {id:'id'},isArray:true}
    });
});

app.factory('Execute1', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerexecute/get", {}, {
        exe: {
            method: 'GET'
        }
    });
});

app.factory('Partial', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/brokerhistory/GetPartial", {}, {
        query: {
            method: 'GET', isArray:true}
        
    });
});

app.factory('SendReport', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/TraderBlocks/ReceiveFills/:block", {}, { send: {method:'PUT', isArray:true}})
}); 