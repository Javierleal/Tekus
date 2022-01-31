var vm = new Vue({
    el: '#Provider',
    data: {
        ListProvider: [],
        selectprovider: { id: 0, nit: "", name: "", email: "" },
        Search: "",
        datatable: "#tableProvider",
        modalprovider: "#EditProviderModal"
    },
    methods: {
        NewProvider: function () {
            this.selectprovider.id = 0;
            this.selectprovider.nit = "";
            this.selectprovider.name = "";
            this.selectprovider.email = "";
            $(vm.modalprovider).modal('show');
        },
        DeleteProvider: function () {
            if (window.confirm("confirm you want to delete the provider: '" + this.selectprovider.name + "'?")) {
                $.ajax({
                    type: "Post",
                    url: '/Privider/DeleteProvider',
                    data: vm.selectservice,
                    async: "false",
                    beforeSend: function () {

                    },
                    success: function (response) {
                        if (response.success) {
                            vm.GetListProvider();
                            vm.CloseModalProvider();
                        }
                    }
                });
            }
        },
        SaveProvider: function () {
            $.ajax({
                type: "Post",
                url: '/Service/SaveService',
                data: vm.selectservice,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        vm.GetListProvider();
                        vm.CloseModalProvider();
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
                url: '/Provider/GetProvider',
                data: { search: "", page: 1, pagesize : 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    var table = $('#tableProvider').DataTable({
                        data: response.providers,
                        columns: [
                            { data: "nit", title: "NIT" },
                            { data: "name", title: "Name" },
                            { data: "email", title: "Email" },
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
                        vm.selectprovider = table.row($(this).parents('tr')).data();
                        $(vm.modalprovider).modal('show');
                    });
                }
            });
        }
    },
    mounted: function () {
        this.GetListProvider();
    }
});