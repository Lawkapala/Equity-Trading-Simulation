var RootApp = angular.module('RootApp', ['LoginApp', 'UiApp', 'Open', 'ApiApp']);

var LoginApp = angular.module('LoginApp', ['ServiceLoginApp']);
LoginApp.controller("LoginController", function ($scope, LogInService,LogOutService, $window) {

    var user = {};
    $scope.SignIn = function (user) {
        
        user.displayName = "";
        user.pmLoginStatus = true;
        user.etLoginStatus = false;
        user.securityAnswer = "";
        user.email = "";
        user.phone = 0;
        //user.role = "";
        console.log(user);
        //alert("Passed accType before show= " + user.role);
        var retval = LogInService.update({ userName: user.userName }, user).$promise;
        retval.then(function onSuccess(response) {
            var role = response.accType;
            var loginStatus = response.loginStatus;
            var token = response.token;
            $scope.value1 = false;
            $scope.val = false;
            //$cookies.put('myFavorite', 'oatmeal');
            console.log("loginStatus = " + response.loginStatus);
            //console.log("Passed accType = " + user.role);
            console.log("Returned accType = " + response.accType);
            if (loginStatus == 1) {

                if (role == "PM") {
                    //$cookies.put('myFavorite', 'oatmeal');
                    $window.location.href = 'http://localhost:35986/PM/PMIndex/'+token;
                }
                else if (role == "ET") {

                    $window.location.href = 'http://localhost:35986/Trader/TraderIndex/'+token;
                }
                else if (role == "BR") {

                    $window.location.href = 'http://localhost:35986/Broker/BrokerIndex/'+token;

                }
                else if (role == "PMT") {
                    $scope.val = true;
                    $scope.value1 = true;
                        //$scope.ShowConfirm = function () {
                        //    if ($window.confirm("Do you wanna log in as PM ?")) {
                        //        $window.location.href = 'http://localhost:35986/PM/PMIndex/' + token;
                        //        $scope.val = true;
                        //    } else {
                        //        $window.location.href = 'http://localhost:35986/Trader/TraderIndex/' + token;
                        //        $scope.val = true;
                        //    }
                        //};

                    //$scope.ShowConfirm();
                        
                    $scope.routepm = function (value) {
                        //user.role = value;
                        //alert("Passed accType = " + user.role);
                        //alert("Passed value = " + value);
                        //alert("token to log out = " + token);
                        user.userName = "";
                        user.password = "";
                        user.role = "PM";
                        LogOutService.update({ token: token }, user);
                        $window.location.href = 'http://localhost:35986/PM/PMIndex/' + token;// + '/t';
                                                                          
                    }

                    $scope.routetrader = function (value) {
                        //user.role = value;
                        //alert("Passed accType = " + user.role);
                        //alert("Passed value = " + value);
                        //alert("token to log out = " + token);
                        user.userName = "";
                        user.password = "";
                        user.role = "ET";
                        LogOutService.update({ token: token }, user);
                        $window.location.href = 'http://localhost:35986/Trader/TraderIndex/' + token;// + '/t';
                    }
                    
                    
                }

            }
            else if (loginStatus == 0) {
                $scope.errorContentP = true;
                $scope.errorMsgP = "wrong password";
                return false;
                //alert("wrong password");
            }


            else if (loginStatus == -1) {
                $scope.errorContent = true;
                $scope.errorMsg = "user does not exist";
                return false;
                //alert("user does not exist");
            }


            else if (loginStatus == 2) {
                $scope.errorContent = true;
                $scope.errorMsg = "Not Properly Logged out";
                return false;
                //alert("Not Properly Logged out");

            }

        },
        function onFail(response) {
            alert("error");
        });
        //console.log($scope.retval);
    };

    $scope.reset = function () {
        $scope.errorContent = false;
        $scope.errorContentP = false;
    }
});

LoginApp.controller("LogoutController", function ($scope, LogOutService, $window, TokenService, $location) {
    
    $scope.SignOut = function () {
        var user = {};
        user.displayName = "";
        user.userName = "";
        user.password = "";
        user.pmLoginStatus = true;
        user.etLoginStatus = false;
        user.securityAnswer = "";
        user.email = "";
        user.phone = 0;
        var accType = $location.absUrl().split('/')[3];

        if (accType == "PM") {
            user.role = "PM";
        }

        else if (accType == "Trader") {
            user.role = "ET";
        }
        //console.log(user);
        var t = TokenService.GetToken($location);
        //console.log(t);
        LogOutService.update({ token: t }, user);
        $window.location.href = 'http://localhost:35986/Index/Login';
    };
});

LoginApp.controller("ChangePasswordController", function ($scope, TokenService, LogOutService, ChangePasswordService, $location, $window) {

    var user = {};
    $scope.ChangePassword = function (user) {
        user.displayName = "";
        user.userName = "";
        user.pmLoginStatus = true;
        user.etLoginStatus = false;
        user.securityAnswer = "";
        user.email = "";
        user.phone = 0;
        user.role = "";
        var t = TokenService.GetToken($location);
        if (user.password == user.cPassword) {
            ChangePasswordService.update({ token: t }, user);
            $window.location.href = 'http://localhost:35986/Index/Login';
        }

        else {
            $scope.errorContentP = true;
            $scope.errorMsgP = "Passwords Does not Match";
            return false;
            //alert("Passwords Does not Match");
        }
    };

    $scope.reset = function () {
        $scope.errorContentP = false;
    }

});


LoginApp.controller("ForgotPasswordController", function ($scope, TokenService, LogOutService, ForgotPasswordService, $location, $window) {

    var user = {};
    $scope.ForgotPassword = function (user) {
        user.displayName = "";
        user.pmLoginStatus = true;
        user.etLoginStatus = false;
        user.email = "";
        user.phone = 0;
        user.role = "";
        var t = TokenService.GetToken($location);
        var retval = ForgotPasswordService.update({ userName: user.userName }, user).$promise;
        retval.then(function onSuccess(response) {
            //console.log(response.DispName);
            var role = response.accType;
            var loginStatus = response.loginStatus;
            var token = response.token;
            if (loginStatus == 1) {
                $window.location.href = 'http://localhost:35986/Index/ChangePassword/'+token;
            }
            else if (loginStatus == 0) {
                $scope.errorContentP = true;
                $scope.errorMsgP = "Security Answer is Wrong";
                return false;
                //alert("Security Answer is Wrong");
            }

            else {
                $scope.errorContent = true;
                $scope.errorMsg = "user does not exist";
                return false;
                //alert("user does not Exist");
            }
        })
    };

    $scope.reset = function () {
        $scope.errorContent = false;
        $scope.errorContentP = false;

    }
});

LoginApp.controller("SignUpController", function ($scope, SignUpService, $window) {

    var user = {};
    $scope.SignUp = function (user) {
        user.dob = "";
        user.active = true;
        user.pmLoginStatus = false;
        user.etLoginStatus = false;

        console.log(user);
        if(user.password == user.cPassword){
            var retval = SignUpService.save(user).$promise;
            retval.then(function onSuccess(response) {
                //var role = response.accType;
                var loginStatus = response.loginStatus;
                //console.log("loginStatus = " + response.loginStatus);
                //console.log("Passed accType = " + user.role);
                //console.log("Returned accType = " + response.accType);
                if (loginStatus == 1) {

                    alert("SignUp Successful");
                    $window.location.href = 'http://localhost:35986/Index/Login';
                }

                else {
                    //ng-blur="CheckUserName()"
                    $scope.errorContent = true;
                    $scope.errorMsg = "User Already Exists, Please Try A Different UserName";
                    return false;
                    //alert("User Already Exists, Please Try A Different UserName");
                }

            },
            function onFail(response) {
                alert("error");
            });
            console.log($scope.retval);
        }
        else {
            $scope.errorContentP = true;
            $scope.errorMsgP = "Both The Passwords Must be Same";
            return false;
            //alert("Both The Passwords Must be Same");
        }

    };

    $scope.reset = function () {
        $scope.errorContent = false;
        $scope.errorContentP = false;

    }
});

LoginApp.controller("FeedBackController", function ($scope, FeedBackService, $window) {
    var data = {};
    $scope.SendFeedBack = function (data) {
        data.timestamp = "";
        FeedBackService.save(data);
        alert("Your Response has been Recorded");

    };
});


LoginApp.controller("SwitchRoleController", function ($scope, TokenService, LogOutService, DispNameService, $location, $window) {
    /*var showContent = $location.absUrl().split('/')[6];
    //alert(showContent);
    if (showContent == 't') {
        //alert($scope.val);
        $scope.val = true;
    }
    else {
        $scope.val = false;
    }*/
    $scope.SwitchRole = function () {
        var token = $location.absUrl().split('/')[5];
        var accType = $location.absUrl().split('/')[3];
        
        if (accType == "PM") {
            var user = {};
            user.displayName = "";
            user.userName = "";
            user.password = "";
            user.pmLoginStatus = true;
            user.etLoginStatus = false;
            user.securityAnswer = "";
            user.email = "";
            user.phone = 0;
            user.role = "PM";
            LogOutService.update({ token: token }, user);
            var user2 = {};
            user2.displayName = "";
            user2.userName = "";
            user2.password = "";
            user2.pmLoginStatus = true;
            user2.etLoginStatus = false;
            user2.securityAnswer = "";
            user2.email = "";
            user2.phone = 0;
            user2.role = "ET";
            LogOutService.update({ token: token }, user2);
            $window.location.href = 'http://localhost:35986/Trader/TraderIndex/' + token;// + '/t';
        }

        else if (accType == "Trader") {
            var user = {};
            user.displayName = "";
            user.userName = "";
            user.password = "";
            user.pmLoginStatus = true;
            user.etLoginStatus = false;
            user.securityAnswer = "";
            user.email = "";
            user.phone = 0;
            user.role = "ET";
            LogOutService.update({ token: token }, user);
            var user2 = {};
            user2.displayName = "";
            user2.userName = "";
            user2.password = "";
            user2.pmLoginStatus = true;
            user2.etLoginStatus = false;
            user2.securityAnswer = "";
            user2.email = "";
            user2.phone = 0;
            user2.role = "PM";
            LogOutService.update({ token: token }, user2);
            $window.location.href = 'http://localhost:35986/PM/PMIndex/' + token;// + '/t';
        }
    }
});


LoginApp.controller("SetUserController", function ($scope, TokenService, DispNameService, $location) {

    var t = TokenService.GetToken($location)
    //console.log(t);
    $scope.usr = DispNameService.get({ token: t }, function () {
        //console.log($scope.usr.accType);
        if ($scope.usr.accType == "PMT") {
            //console.log($scope.usr.accType);
            $scope.val = true;
        }
    });
    

});

LoginApp.controller("GetTokenController", function ($scope, TokenService, DispNameService, $location) {

    var t = TokenService.GetToken($location)

    $scope.token = t;

});


var dvtrader = document.getElementById('trader-content');

angular.element(document).ready(function () {
    angular.bootstrap(dvtrader, ['Open']);
});

var dvPM = document.getElementById('pm-container');

angular.element(document).ready(function () {
    angular.bootstrap(dvPM, ['UiApp']);
});

//var dvLogin = document.getElementById('login-container');

//angular.element(document).ready(function(){
//    angular.bootstrap(dvLogin,[LoginApp]);
//});

var dvBroker = document.getElementById('br-container');

angular.element(document).ready(function () {
    angular.bootstrap(dvBroker, ['ApiApp']);
});