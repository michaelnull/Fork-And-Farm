(function () {
    var app = angular.module('forkandfarm', []);

    app.controller('DisplayController', ['$http', function ($http) {

        var display = this;
        this.all = [];
        var details = [];
        this.offermessage = 'awaiting data';
        var responsecount = 0;
        

        this.submitoffer = function (id, info) {
            $http.post('/Deals/SubmitOffer', { OfferId: id, UnitPrice:info.UnitPrice, Quantity: info.Quantity, Delivery: info.Delivery, PaymentTerms: info.PaymentTerms, Memo: info.Memo })
                .then(function (response) {
                    display.offermessage = response.data;
                    console.log(response)
                    display.getresponses(id);
            });
        };

        this.getuserinfo = function () {
            $http.get('/advertisements/getUserInfo').success(function (data) {
                display.userinfo = data;
            })};


        this.getresponses = function (id) {
            console.log("get responses called " + id);
            $http.get('/deals/getoffers/' + id).success(function (data) {
                display.allresponses = data;
                var i = 0;
                angular.forEach(display.allresponses, function (value, key){
                    display.allresponses[i].CreatedOn = new Date(parseInt(display.allresponses[i].CreatedOn.substr(6)));
                    display.allresponses[i].Delivery = new Date(parseInt(display.allresponses[i].Delivery.substr(6)));
                    i++;
                });
                responsecount = 0;
                display.showtenmoreresponses();
            })
        };

        this.showtenmoreresponses = function () {
            responsecount += 10;
            display.responses = display.allresponses.slice(0, responsecount)
        }

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
       
        this.deletead = function (id) {
            var msg = '';
            $http.get('deleteAd/' + id).success(function (response) {
                console.log(response);
                msg = 'delete this ad?--Posted on ' + response.CreatedOn +
                    ' Product: ' + response.Product +
                    ' Units: ' + response.Unit +
                    'Quantity: ' + response.Quantity + 
                    'Unit Price: ' + response.UnitPrice +
                    'Delivery Date: ' + response.Delivery +
                    'Memo: ' + response.Memo;
                if (confirm(msg)) {
                    $http.post('DeleteAd/' + id).then(function (response) {
                        window.alert(response.data);
                        display.getdata('allads');
                        display.goto(display.page);
                    });
                }
            });
           
        };

        this.deleteresponse = function (id) {
           
            var adid = '';
            $http.get('/deals/deleteresponse/' + id).success(function (response) {
                adid = response.OfferId;
                console.log(response);
                var msg = 'delete response? ' +
                    ' Quantity: ' + response.Quantity +
                    ' Unit Price: ' + response.UnitPrice +
                    ' Total Price ' + response.ExtPrice + 
                    ' Delivery Date ' + response.Delivery +
                    'Memo: ' + response .Memo;
                if (confirm(msg)) {
                    $http.post('/deals/deleteresponse/' + id).then(function (answer) {
                        window.alert(answer.data);
                        display.getresponses(adid);
                    })
                }
            });
           
        };
        
       

        this.getdata('/Advertisements/AllAds/');
        this.getuserinfo();

        

      
    }]);
    
})();