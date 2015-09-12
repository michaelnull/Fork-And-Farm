(function () {
    var app = angular.module('forkandfarm', []);

    app.controller('DisplayController', ['$http', function ($http) {

        var display = this;
        this.all = [];
        var details = [];
        this.offermessage = 'awaiting data';

        this.submitoffer = function (id, info) {
            $http.post('/Deals/SubmitOffer', { OfferId: id, UnitPrice:info.UnitPrice, Quantity: info.Quantity, Delivery: info.Delivery, PaymentTerms: info.PaymentTerms, Memo: info.Memo })
                .then(function (response) {
                    display.offermessage = response.data;
                    display.offerform.$setPristine();
            });
        };

        this.getuserinfo = function () {
            $http.get('/advertisements/getUserInfo').success(function (data) {
                display.userinfo = data;
            })};

        this.goto = function (page) {
            display.page = page;
            var start = (page - 1) * 25;
            var end = start + 25;
            display.show = display.all.slice(start, end);
            console.log(display.page);
            for (i = 0; i < 25; i++) {
                display.show[i].CreatedOn = new Date(parseInt(display.show[i].CreatedOn.substr(6)));
                display.show[i].Delivery = new Date(parseInt(display.show[i].Delivery.substr(6)));
                if (display.show[i].AdType == 1) {
                    display.show[i].AdType = "Supply Ad";
                }
                else {
                    display.show[i].AdType = "Purchase Ad";
                }

            }
        };

       
       
        this.search = function (string, terms) {
            $http.get(string + terms).success(function (data) {
                display.all = data;
                display.goto(1);
            });
        };

        this.details = function (id) {
            $http.get('advertisements/getdetails/'+id).success(function (data) {
                display.showdetails = data;
                
            });
        };

        this.create = function () {
            location.href = ('/Advertisements/Create');
        };
        
        this.getdata = function (string) {
            $http.get(string).success(function (data) {
                display.all = data;
                display.goto(1);
            });
        };

        this.getinfo = function (string) {
            $http.get(string).success(function(data){
                var result = data;
                return result;
            });
            };
       
      
        
       

        this.getdata('/Advertisements/AllAds/');
        this.getuserinfo();

        

      
    }]);
    
})();