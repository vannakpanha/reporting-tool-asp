var ngApp = angular.module("ngApp", []);
ngApp.controller("ctrl", function ($scope, $http) {
    $scope.fromDate = "";
    $scope.toDate = "";
    // get data for current date
    $http.get(burl + "Report/GetSummaryReport").success(function (data) {
        $scope.saleSummary = data;
        $scope.total = 0;
        for(var i=0; i<$scope.saleSummary.length;i++)
        {
            $scope.total += Number($scope.saleSummary[i].Total);
        }
    });
    // filter data
    $scope.search = function () {
        var fdate = $("#fromdate").val();
        var tdate = $("#todate").val();
        var customer = $("#customer").val();
        var user = $("#user").val();
        $http({
            method: 'POST',
            url: burl + "Report/Search",
            data: $.param({
                "fromdate": fdate,
                "todate": tdate,
                "customer": customer,
                "user": user
            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.saleSummary = data;
            $scope.total = 0;
            for (var i = 0; i < $scope.saleSummary.length; i++) {
                $scope.total += Number($scope.saleSummary[i].Total);
            }
        });
    };
});