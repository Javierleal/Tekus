var vm = new Vue({
    el: '#ProviderDetails',
    data: {
        provider: {},
        selectproviderservice: { id: 0, idProvider: 0, idService: 0, priceHour: 0, countryISO: "" },
        selectproviderdetail: { id: 0, idProvider: 0, rowName: "", rowValue: "" },
        Search: "",
        datatable: "#tableProviderService",
        modaledit: "#EditProviderServiceModal",
        listServices: [],
        listCountries: [],
        message: "",
        datatabledetail: "#tableProviderDetail",
        modaleditdetail: "#EditProviderDetailModal",
    },
    methods: {
        GetServiceAndCountries: function() {
            $.ajax({
                type: "Post",
                url: '/Service/GetService',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    vm.listServices = response.services;
                }
            });
            $.ajax({
                type: "Get",
                url: 'https://restcountries.com/v3.1/all',
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    vm.listCountries = response;
                }
            });
        },
        GetIdProvider: function () {
            $.ajax({
                type: "Post",
                url: '/Provider/GetProvider',
                data: null,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    vm.provider = response;
                    vm.GetListProviderService();
                    vm.GetListProviderDetails();
                }
            });
        },
        NewProviderService: function () {
            this.message = "";
            this.selectproviderservice.id = 0;
            this.selectproviderservice.idprovider = this.provider.id;
            this.selectproviderservice.idservice = 0;
            this.selectproviderservice.pricehour = 0;
            this.selectproviderservice.countryiso = "";
            $(vm.modaledit).modal('show');
        },
        DeleteProviderService: function () {
            if (window.confirm("confirm you want to delete the service provider: '" + this.selectproviderservice.serviceName + " Country: " + this.selectproviderservice.countryName + "'?")) {
                $.ajax({
                    type: "Post",
                    url: '/Provider/DeleteProviderService',
                    data: vm.selectproviderservice,
                    async: "false",
                    beforeSend: function () {

                    },
                    success: function (response) {
                        if (response.success) {
                            vm.GetListProviderService();
                            vm.CloseModalProviderDetail();
                        } else {
                            vm.message = response.message;
                        }
                    }
                });
            }
        },
        SaveProviderService: function () {
            this.message = "";
            $.ajax({
                type: "Post",
                url: '/Provider/SaveProviderService',
                data: vm.selectproviderservice,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        vm.GetListProviderService();
                        vm.CloseModalProviderDetail();
                    } else {
                        vm.message = response.message;
                    }
                }
            });
        },
        CloseModalProviderDetail: function () {
            $(vm.modaledit).modal('hide');
            $(vm.modaleditdetail).modal('hide');
        },
        GetListProviderService: function () {
            $.ajax({
                type: "Post",
                url: '/Provider/GetProviderServices',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if ($.fn.DataTable.isDataTable(vm.datatable))
                        $(vm.datatable).dataTable().fnDestroy();
                    var table = $(vm.datatable).DataTable({
                        data: response.providerServices,
                        columns: [
                            { data: "serviceName", title: "Name Service" },
                            { data: "priceHour", title: "Price/H ($)" },
                            { data: "countryName", title: "Country" },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) { return '<button type="button" id="Edit" class="btn btn-info"> Edit </button>'; }
                            }
                        ], search: {
                            return: true
                        }
                    });
                    //Evento de edicion de rows
                    $(vm.datatable + ' tbody').on('click', '#Edit', function () {
                        vm.selectproviderservice = table.row($(this).parents('tr')).data();
                        $(vm.modaledit).modal('show');
                    });
                }
            });
        },
        NewProviderDetail: function () {
            this.message = "";
            this.selectproviderdetail.id = 0;
            this.selectproviderdetail.idprovider = this.provider.id;
            this.selectproviderdetail.rowName = "";
            this.selectproviderdetail.rowValue = "";
            $(vm.modaleditdetail).modal('show');
        },
        DeleteProviderDetail: function () {
            if (window.confirm("confirm you want to delete the Detail provider: '" + this.selectproviderdetail.rowName + "'?")) {
                $.ajax({
                    type: "Post",
                    url: '/Provider/DeleteProviderDetail',
                    data: vm.selectproviderservice,
                    async: "false",
                    beforeSend: function () {

                    },
                    success: function (response) {
                        if (response.success) {
                            vm.GetListProviderDetails();
                            vm.CloseModalProviderDetail();
                        } else {
                            vm.message = response.message;
                        }
                    }
                });
            }
        },
        SaveProviderDetail: function () {
            this.message = "";
            $.ajax({
                type: "Post",
                url: '/Provider/SaveProviderDatails',
                data: vm.selectproviderdetail,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        vm.GetListProviderDetails();
                        vm.CloseModalProviderDetail();
                    } else {
                        vm.message = response.message;
                    }
                }
            });
        },
        GetListProviderDetails: function () {
            $.ajax({
                type: "Post",
                url: '/Provider/GetProviderDetails',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if ($.fn.DataTable.isDataTable(vm.datatabledetail))
                        $(vm.datatabledetail).dataTable().fnDestroy();
                    var table = $(vm.datatabledetail).DataTable({
                        data: response.providerDetails,
                        columns: [
                            { data: "rowName", title: "Name" },
                            { data: "rowValue", title: "Value" },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) { return '<button type="button" id="Edit" class="btn btn-info"> Edit </button>'; }
                            }
                        ], search: {
                            return: true
                        }
                    });
                    //Evento de edicion de rows
                    $(vm.datatabledetail + ' tbody').on('click', '#Edit', function () {
                        vm.selectproviderdetail = table.row($(this).parents('tr')).data();
                        $(vm.modaleditdetail).modal('show');
                    });
                }
            });
        },
    },
    mounted: function () {
        this.GetIdProvider();
        this.GetServiceAndCountries();
    }
});