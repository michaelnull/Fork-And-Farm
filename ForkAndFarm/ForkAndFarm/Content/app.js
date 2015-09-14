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
                    window.alert(response.data);
                    document.getElementById('offerform').reset();
                    display.getdata('/advertisements/allads');
            });
        };

        this.getuserinfo = function () {
            $http.get('/portalVM/getUserInfo').success(function (data) {
                display.userinfo = data;
            })};

        this.goto = function (page) {
            display.page = page;
            var start = (page - 1) * 25;
            var end = start + 25;
            display.show = display.all.slice(start, end);
            console.log(display.page);
           
        };

        this.search = function (string, terms) {
            $http.get(string + terms).success(function (data) {
                display.all = data;
                var i = 0;
                angular.forEach(display.all, function (value, key) {
                    if (display.all[i].AdType == 1) {
                        display.all[i].AdType = "Supply Ad";
                    }
                   else if (display.all[i].AdType == 0) {
                        display.all[i].AdType = "Purchase Ad";
                   }
                    if (display.all[i].AdType == null) {
                        display.all[i].AdType = "response";
                    }
                    i++;
                })
                display.goto(1);
            });
        };

      

        this.create = function (ad, info) {
            window.alert('ad create function called');
            $http.post('/advertisements/submitad',{Product: ad.Product, Quantity: ad.Quantity, Unit: ad.Unit, UnitPrice: ad.UnitPrice, PaymentTerms: ad.PaymentTerms, Invoice: ad.Invoice, Memo: ad.Memo, Delivery: ad.Delivery})
            .then(function (response) {
                
                $("#myModal").modal('hide');
                document.getElementById('createform').reset();
                window.alert(response.data);
                display.getdata('/advertisements/allads');

            });
        };
        
        this.getdata = function (string) {
            console.log("get data called on " + string)
            $http.get(string).success(function (data) {
                display.all = data;
                var i = 0;
                angular.forEach(display.all, function (value, key) {
                    if (display.all[i].AdType == 1) {
                        display.all[i].AdType = "Supply Ad";
                    }
                   else if (display.all[i].AdType == 0) {
                        display.all[i].AdType = "Purchase Ad";
                   }
                    if (display.all[i].AdType == null)
                    {
                        display.all[i].AdType = "response";
                    }
                    i++;
                })
                display.goto(1);
            });
        };

        this.getjsdate = function (csdate) {
            var jsdate = '';
            jsdate = new Date(parseInt(csdate.substr(6)));
            return jsdate;
        };

        this.deletead = function (id) {
            var msg = '';
            $http.get('/advertisements/deleteAd/' + id).success(function (response) {
                console.log(response);
                msg = 'delete this ad?--Posted on ' + display.getjsdate(response.CreatedOn) +
                    '\n Product: ' + response.Product +
                    '\n Units: ' + response.Unit +
                    '\n Quantity: ' + response.Quantity + 
                    '\n Unit Price: ' + response.UnitPrice +
                    '\n Delivery Date: ' + display.getjsdate(response.Delivery) +
                    '\n Memo: ' + response.Memo;
                if (confirm(msg)) {
                    $http.post('DeleteAd/' + id).then(function (response) {
                        window.alert(response.data);
                        display.getdata('/advertisements/allads');
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
                var msg = 'delete response? --created on: ' + display.getjsdate(response.CreatedOn) +
                    '\n Quantity: ' + response.Quantity +
                    '\n Unit Price: ' + response.UnitPrice +
                    '\n Total Price ' + response.ExtPrice + 
                    '\n Delivery Date ' + display.getjsdate(response.Delivery) +
                    '\n Memo: ' + response .Memo;
                if (confirm(msg)) {
                    $http.post('/deals/deleteresponse/' + id).then(function (answer) {
                        window.alert(answer.data);
                        display.getdata('/advertisements/allads');
                        display.goto(display.page);
                    })
                }
            });
           
        };
        
       

        this.getdata('/Advertisements/AllAds/');
       
        this.getuserinfo();

        

      
    }]);
    
})();