var vm = new Vue({
    el: '#Provider',
    data: {
        ListProvider: [],
        selectprovider: {},
        Search: ""
    },
    methods: {
        SaveProvider: function () {
            alert(vm.SelectProvider.nit);
        },
        ModalProvider: function () {
            $('#EditProviderModal').modal('show');
        },
        GetListPrivider: function () {
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
                    $('#tableProvider tbody').on('click', '#Edit', function () {
                        vm.selectprovider = table.row($(this).parents('tr')).data();
                        vm.ModalProvider();
                    });
                },
                complete: function (da) {


                },
                error: function (a, b, c) {


                }
            });
        }
    },
    mounted: function () {
        this.GetListPrivider();
    }
});