var ngApp = angular.module("ngApp", []);
ngApp.controller("ctrl", function ($scope, $http) {
    // get stock balance
    $http.get(burl + "Report/GetStockBalance").success(function (data) {
        $scope.balance = data;
        $scope.total = 0;
        $scope.totalQty = 0;
        for (var i = 0; i < $scope.balance.length; i++) {
            $scope.total += Number($scope.balance[i].Total);
            $scope.totalQty += Number($scope.balance[i].Qty);
        }
    });
    // filter data
    $scope.search = function () {
        var product = $("#product").val();
        var category = $("#category").val();
        $http({
            method: 'POST',
            url: burl + "Report/SearchStockBalance",
            data: $.param({
                "product": product,
                "category": category
            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.balance = data;
            $scope.total = 0;
            $scope.totalQty = 0;
            for (var i = 0; i < $scope.balance.length; i++) {
                $scope.total += Number($scope.balance[i].Total);
                $scope.totalQty += Number($scope.balance[i].Qty);
            }
        });
    };
});