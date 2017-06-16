var app = angular.module("ServiceLoginApp", ["ngResource"]);

app.factory('LogInService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/SignIn/:userName", { userName: '@userName' }, { update: { method: 'PUT' } }, { delete: { method: 'DELETE' } }
        );
});

app.factory('LogOutService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/SignOut/:token", { token: '@t' }, { update: { method: 'PUT' } }, { delete: { method: 'DELETE' } }
        );
});

app.factory('ChangePasswordService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/ChangePassword/:token", { token: '@t' }, { update: { method: 'PUT' } }, { delete: { method: 'DELETE' } }
        );
});

app.factory('ForgotPasswordService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/ForgotPassword/:userName", { userName: '@userName' }, { update: { method: 'PUT' } }, { delete: { method: 'DELETE' } }
        );
});


app.factory('TokenService', function ($location) {
    var fact ={};
    fact.GetToken = function () { 
        var t =  $location.absUrl().split('/')[5];
        return t;
    }
    return fact;
});

app.factory('DispNameService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/GetDispName/:token", { token: '@t' }, { get: { method: 'GET' } });
});

app.factory('SignUpService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/SignUp", { save: { method: 'POST' } });
});
app.factory('FeedBackService', function ($resource) {
    return $resource("http://localhost/StockStreet.Service/api/Feedback", { save: { method: 'POST' } });
});