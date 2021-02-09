$(function () {
    $('#SendCode').click(function () {
        var Email = $('#Email');
        //校验
        var myemail = /^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
        if (Email.val() == null || Email.val() == '') {
            swal("提示", "请输入电子邮箱");
            Email.focus();
            return false;
        }
        else if (!myemail.test(Email.val())) {
            swal("提示", "电子邮箱格式不正确");
            Email.focus();
            return false;
        }
        this.value = "重新发送";

        var dataArr = {
            Email: Email.val(),
            Type: 11
        };
        //提交
        $.post('/Login/SendCode', dataArr, function (data) {
            if (data.Status === 200) {

                swal("提示", "发送成功");
                //Email.value = "重新发送";
            }
            if (data.Status === 202) {
                swal("提示", "发送失败");
                // return false;
            }
        });
    });

    $('#btnRegister').click(function () {
        var UniversityId = $('#UniversityId option:selected'),
        Account = $('#Account'),
        PassWord = $('#PassWord'),
        Email = $('#Email'),
        Code = $('#Code');
        //MobilePhone = $('#MobilePhone')
        //校验
        if (UniversityId.val() == 0) {
            swal("提示", "请选择正确的学校");
            UniversityId.focus();
            return false;
        }
        if (Account.val() == null || Account.val() == '') {
            swal("提示", "请输入账号");
            Account.focus();
            return false;
        }
        if (PassWord.val() == null || PassWord.val() == '') {
            swal("提示", "请输入密码");
            PassWord.focus();
            return false;
        }
        var myemail = /^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
        if (Email.val() == null || Email.val() == '') {
            swal("提示", "请输入电子邮箱");
            Email.focus();
            return false;
        }
        else if (!myemail.test(Email.val())) {
            swal("提示", "电子邮箱格式不正确");
            Email.focus();
            return false;
        }
        if (Code.val() == null || Code.val() == '') {
            swal("提示", "请输入验证码");
            Code.focus();
            return false;
        }
        var dataArr = {
            UniversityId: UniversityId.val(),
            Account: Account.val(),
            PassWord: PassWord.val(),
            Code: Code.val(),
            Email: Email.val()
        };

        //提交
        $.post('/Login/ResetPassword', dataArr, function (data) {
            if (data.Status === 200) {
                swal("提示", "密码重置成功");
                //return false;
                setTimeout(function () {
                    window.location.href = '/Login/Login';
                }, 1500);
            }
            if (data.Status === 202) {
                swal("提示", "密码重置失败");
                // return false;
            }
            if (data.Status === 203) {
                swal("提示", "密码重置失败，请检查输入是否有误");
                //return false;
            }
            //.complete(function () { window.parent.hideModal(); })
        });
    });
});