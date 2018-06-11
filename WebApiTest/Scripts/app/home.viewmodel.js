function HomeViewModelFactory(app, dataModel) {

    var self = this;

    self.myHometown = ko.observable("");

    Sammy(function () {


        this.get('#home', function () {
        //    // Make a call to the protected Web API by passing in a Bearer Authorization Header
        //    $.ajax({
        //        method: 'get',
        //        url: app.dataModel.userInfoUrl,
        //        contentType: "application/json; charset=utf-8",
        //        headers: {
        //            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
        //        },
        //        success: function (data) {
        //            self.myHometown('Your Hometown is : ' + data.hometown);
        //        }
        //    });

            var dt = $('#assets-data-table').DataTable({
                serverSide: true,
                processing: true,
                ajax: {
                    "url": "api/Me2"
                },
                "columns": [
                    { "title": "FIO", "data": "FIO", "searchable": true },
                    { "title": "AssetDate", "data": "AssetDate", "searchable": true },
                    { "title": "AssetNumber", "data": "AssetNumber", "searchable": true },
                    { "title": "OrgName", "data": "OrgName", "searchable": true },
                    { "title": "Position", "data": "Position", "searchable": true },
                    { "title": "EMail", "data": "EMail", "searchable": true }
                ],
                "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
            });

        });
        this.get('/', function () { this.app.runRoute('get', '#home'); });
    });

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModelFactory
});
