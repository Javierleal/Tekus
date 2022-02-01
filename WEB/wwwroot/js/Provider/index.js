var vm = new Vue({
    el: '#Providers',
    data: {
        ListProvider: [],
        selectProvider: { id: 0, nit: "", name: "", email: "" },
        Search: "",
        datatable: "#tableProvider",
        modalprovider: "#EditProviderModal",
        message: ""
    },
    methods: {
        NewProvider: function () {
            this.selectProvider.id = 0;
            this.selectProvider.nit = "";
            this.selectProvider.name = "";
            this.selectProvider.email = "";
            $(vm.modalprovider).modal('show');
        },
        DeleteProvider: function () {
            if (window.confirm("confirm you want to delete the provider: '" + this.selectProvider.name + "'?")) {
                $.ajax({
                    type: "Post",
                    url: '/Provider/DeleteProvider',
                    data: vm.selectProvider,
                    async: "false",
                    beforeSend: function () {

                    },
                    success: function (response) {
                        if (response.success) {
                            vm.GetListProvider();
                            vm.CloseModalProvider();
                        }
                        else {
                            vm.message = response.message;
                        }
                    }
                });
            }
        },
        SaveProvider: function () {
            $.ajax({
                type: "Post",
                url: '/Provider/SaveProvider',
                data: vm.selectProvider,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        vm.GetListProvider();
                        vm.CloseModalProvider();
                    } else {
                        vm.message = response.message;
                    }
                }
            });
        },
        CloseModalProvider: function () {
            $(vm.modalprovider).modal('hide');
        },
        GetListProvider: function () {
            $.ajax({
                type: "Post",
                url: '/Provider/GetProviderList',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if ($.fn.DataTable.isDataTable(vm.datatable))
                        $(vm.datatable).dataTable().fnDestroy();
                    var table = $(vm.datatable).DataTable({
                        data: response.providers,
                        columns: [
                            { data: "nit", title: "NIT" },
                            { data: "name", title: "Name" },
                            { data: "email", title: "Email" },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) { return '<button type="button" id="Edit" class="btn btn-info"> Edit </button>'; }
                            },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) { return '<button type="button" id="Details" class="btn btn-warning"> Details </button>'; }
                            }
                        ], search: {
                            return: true
                        }
                    });
                    //Evento de edicion de rows
                    $(vm.datatable + ' tbody').on('click', '#Edit', function () {
                        vm.selectProvider = table.row($(this).parents('tr')).data();
                        $(vm.modalprovider).modal('show');
                    });

                    $(vm.datatable + ' tbody').on('click', '#Details', function () {
                        vm.selectProvider = table.row($(this).parents('tr')).data();
                        window.location.href = "/Provider/Details/" + vm.selectProvider.id;
                    });
                }
            });
        }
    },
    mounted: function () {
        this.GetListProvider();
    }
});