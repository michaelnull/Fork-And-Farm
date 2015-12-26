(function () {
    var app = angular.module('forkandfarm', []);

    app.controller('DisplayController', ['$http', function ($http) {

        var display = this;
        this.all = [];
        this.role = '';

        this.submitoffer = function (id, info) {
            if(confirm('Confirm Offer \n Quantity: ' + info.Quantity +
                '\n Unit Price: ' + info.UnitPrice +
                '\n Payment Terms: ' + info.PaymentTerms +
                '\n Memo: ' + info.Memo +
                '\n Delivery Date: ' + info.Delivery
                )) {
                $http.post('/Deals/SubmitOffer', { OfferId: id, UnitPrice: info.UnitPrice, Quantity: info.Quantity, Delivery: info.Delivery, PaymentTerms: info.PaymentTerms, Memo: info.Memo })
           .then(function (response) {
               window.alert(response.data + response.statusText);
               display.userinfo.DealFromMeCount++;
               document.getElementById('offerform').reset();
               display.getuserinfo();
           });
            }
        };

        this.getuserinfo = function () {
            $http.get('/portalVM/getUserInfo').success(function (data) {
                display.userinfo = data;
                display.role = data.UserRole;
                console.log('get user function says: ' + display.role)
                if (display.role == 'Purchaser') {
                    display.all = display.getdata('/advertisements/SupplyList');
                }
                else if (display.role == 'Supplier') {
                    display.all = display.getdata('/advertisements/purchaselist');
                }
                else {
                    display.all = display.getdata('/advertisements/allads');
                }
            })};

        this.goto = function (page) {
            display.page = page;
            var start = (page - 1) * 25;
            var end = start + 25;
            display.show = display.all.slice(start, end);
            console.log(display.page);
            document.body.scrollTop = document.documentElement.scrollTop = 0;
        };

        this.search = function (string, terms) {
            $http.get(string + terms).success(function (data) {
                display.all = data;
               
                display.goto(1);
            });
        };

        this.create = function (ad, info) {
            $http.post('/advertisements/submitad',{Product: ad.Product, Quantity: ad.Quantity, Unit: ad.Unit, UnitPrice: ad.UnitPrice, PaymentTerms: ad.PaymentTerms, Invoice: ad.Invoice, Memo: ad.Memo, Delivery: ad.Delivery})
            .then(function (response) {
                $("#myModal").modal('hide');
                document.getElementById('createform').reset();
                window.alert(response.data);
                display.userinfo.AdCount++;
               
                display.getdata('/advertisements/myads');
                display.goto(1);
            });
        };
        
        this.getdata = function (string) {
            $http.get(string).success(function (data) {
                display.all = data;
                display.goto(1);
            });
        };

        this.getonead = function (id) {
            console.log('get one ad called for ' +id)
            $http.get('/advertisements/getonead/'+ id).success(function (data) {
                display.show = data;
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
                msg = 'delete this ad?--Posted on ' + display.getjsdate(response.CreatedOn) +
                    '\n Product: ' + response.Product +
                    '\n Units: ' + response.Unit +
                    '\n Quantity: ' + response.Quantity + 
                    '\n Unit Price: ' + response.UnitPrice +
                    '\n Delivery Date: ' + display.getjsdate(response.Delivery) +
                    '\n Memo: ' + response.Memo;
                if (confirm(msg)) {
                    $http.post('/advertisements/DeleteAd/' + id).then(function (response) {
                        window.alert(response.data);
                        display.userinfo.AdCount--;
                        display.getdata('/advertisements/MyAds');
                    });
                }
            });
           
        };

        this.deleteresponse = function (id) {
            var adid = '';
            $http.get('/deals/deleteresponse/' + id).success(function (response) {
                adid = response.OfferId;
                var msg = 'delete response? --created on: ' + display.getjsdate(response.CreatedOn) +
                    '\n Quantity: ' + response.Quantity +
                    '\n Unit Price: ' + response.UnitPrice +
                    '\n Total Price ' + response.ExtPrice + 
                    '\n Delivery Date ' + display.getjsdate(response.Delivery) +
                    '\n Memo: ' + response .Memo;
                if (confirm(msg)) {
                    $http.post('/deals/deleteresponse/' + id).then(function (answer) {
                        window.alert(answer.data);
                        display.userinfo.DealFromMeCount--;
                        display.getonead(adid);
                    })
                }
            });
        };
        
        this.resetcount = function () {
            $http.get('/deals/clearresponsecount').success(function (response) {
                console.log(response);
                display.userinfo.CountNewResponses = 0;
            });
        };

        this.setold = function (id) {
            $http.get('/deals/setold/' + id).success(function(response){
                console.log(response);
            });
        };

        this.getuserinfo();

      
    }]);
    
})();