var vm = new Vue({
    el: '#Services',
    data: {
        ListService: [],
        selectservice: { id: 0, name: "", description:"" },
        Search: "",
        datatable: "#tableService",
        modalservice: "#EditServiceModal",
        message: ""
    },
    methods: {
        NewService: function () {
            this.selectservice.id = 0;
            this.selectservice.name = "";
            this.selectservice.description = "";
            $(vm.modalservice).modal('show');
        },
        DeleteService: function () {
            if (window.confirm("confirm you want to delete the service: '" + this.selectservice.name +"'?")) {
                $.ajax({
                    type: "Post",
                    url: '/Service/DeleteService',
                    data: vm.selectservice,
                    async: "false",
                    beforeSend: function () {

                    },
                    success: function (response) {
                        if (response.success) {
                            vm.GetListService();
                            $(vm.modalservice).modal('hide');
                        } else {
                            vm.message = response.message;
                        }
                    }
                });
            }
        },
        SaveService: function () {
            $.ajax({
                type: "Post",
                url: '/Service/SaveService',
                data: vm.selectservice,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        vm.GetListService();
                        $(vm.modalservice).modal('hide');
                    } else {
                        vm.message = response.message;
                    }
                }
            });
        },
        CloseModalService: function () {
            $(vm.modalservice).modal('hide');
        },
        GetListService: function () {
            $.ajax({
                type: "Post",
                url: '/Service/GetService',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if ($.fn.DataTable.isDataTable(vm.datatable))
                        $(vm.datatable).dataTable().fnDestroy();
                    var table = $(vm.datatable).DataTable({
                        data: response.services,
                        columns: [
                            { data: "name", title: "Name" },
                            { data: "description", title: "Description" },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) { return '<button type="button" id="Edit" class="btn btn-info"> Edit </button>'; }
                            }
                        ], search: {
                            return: true
                        }
                    });
                    //Evento de seleccion de rows
                    $(vm.datatable +' tbody').on('click', '#Edit', function () {
                        vm.selectservice = table.row($(this).parents('tr')).data();
                        $(vm.modalservice).modal('show');
                    });
                }
            });
        }
    },
    mounted: function () {
        this.GetListService();
    }
});