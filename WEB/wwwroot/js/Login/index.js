var vm = new Vue({
	el: '#login',
	data: {
		username: "",
		password: "",
		Message: ""
	},
	methods: {
		Submit: function () {
			var isvalid = $(".form-validate").valid();
			if (isvalid) {
				vm.Message = 'Espere...';
				$.ajax({
					type: "Post",
					url: '/Login/Login',
					data: { user: this.username, pass: this.password },
					async: "false",
					beforeSend: function () {
						
					},
					success: function (response) {
						if (response.success) {
							window.location.href = "/Home/Index";
						}
						else {
							vm.password = '';
							vm.Message = response.message;
						}
					},
					complete: function (da) {


					},
					error: function (a, b, c) {


					}
				});
			}
		}
	},
	mounted: function () {
		//this.FaceLog($("#fc").data("fc"));
	}
});