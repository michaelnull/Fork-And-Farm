(function () {
    var app = angular.module('forkandfarm', []);

    app.controller('DisplayController', ['$http', function ($http) {

        this.page = 1;
        var display = this;

        this.index = function (page) {
            display.page = page;
            $http.get('/Advertisements/AllAds/').success(function (data) {
                display.all = data;
                var start = (page - 1) * 25;
                var end = start + 25;
                display.show = display.all.slice(start, end);
                return display.show;
            });
        };

        this.index(this.page);

        this.add = function (pick) {
            display.works = " create function called successfully";

            $http.post('/picks/create', { File: pick.file, Description: pick.description, Url: pick.url })
                 .then(function (response) {


                     display.pictures.unshift(response.data);
                     $("#createModal").modal('hide');
                     pick.description = "";
                     pick.url = "";
                     display.addpickform.$setPristine();

                 });

        };
    }]);
    app.controller('CreateController', ['$http', function ($http) {
    }]);
})();