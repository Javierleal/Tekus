var vm = new Vue({
    el: '#Dashboard',
    data: {
        dataDashboard: { providerCount: 0, serviceCount: 0, providerServiceCount: 0, providerDetailCount: 0 }
    },
    methods: {
        GetDataDashboard: function () {
            $.ajax({
                type: "Post",
                url: '/Home/GetDashboard',
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    vm.dataDashboard = response;
                }
            });
        }
    },
    mounted: function () {
        this.GetDataDashboard();
    }
});