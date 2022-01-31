var vm = new Vue({
    el: '#Services',
    data: {
        ListService: [],
        selectservice: {},
        Search: ""
    },
    methods: {
        SaveService: function () {
            $.ajax({
                type: "Post",
                url: '/Service/UpdateService',
                data: vm.selectservice,
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    if (response.success) {
                        $('#tableService').DataTable().clear();
                        vm.GetListPrivider();
                        $('#EditServiceModal').modal('close');
                    }
                },
                complete: function (da) {


                },
                error: function (a, b, c) {


                }
            });
        },
        ModalService: function () {
            $('#EditServiceModal').modal('show');
        },
        GetListPrivider: function () {
            
            $.ajax({
                type: "Post",
                url: '/Service/GetService',
                data: { search: "", page: 1, pagesize: 100 },
                async: "false",
                beforeSend: function () {

                },
                success: function (response) {
                    
                    var table = $('#tableService').DataTable({
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
                    //Evento de edicion de rows
                    $('#tableService tbody').on('click', '#Edit', function () {
                        vm.selectservice = table.row($(this).parents('tr')).data();
                        vm.ModalService();
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