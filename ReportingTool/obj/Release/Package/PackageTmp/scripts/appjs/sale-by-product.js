var ngApp = angular.module("ngApp", []);
ngApp.controller("ctrl", function ($scope, $http) {
    $scope.fromDate = "";
    $scope.toDate = "";
    $scope.totalQty = 0;
    $scope.total = 0;
    // get default sale by product
    $http.get(burl + "Report/GetSaleByProduct").success(function (data) {
        $scope.products = data;
        $scope.total = 0;
        $scope.totalQty = 0;
        for (var i = 0; i < $scope.products.length; i++) {
            $scope.total += Number($scope.products[i].Total);
            $scope.totalQty += Number($scope.products[i].Qty);
        }
    });
    // search sale by product
    $scope.search = function () {
        var fdate = $("#fromdate").val();
        var tdate = $("#todate").val();
        var user = $("#user").val();
        $http({
            method: 'POST',
            url: burl + "Report/SearchSaleByProduct",
            data: $.param({
                "fromdate": fdate,
                "todate": tdate,
                "user": user
            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.products = data;
            $scope.total = 0;
            $scope.totalQty = 0;
            for (var i = 0; i < $scope.products.length; i++) {
                $scope.total += Number($scope.products[i].Total);
                $scope.totalQty += Number($scope.products[i].Qty);
            }
        });
    };
});