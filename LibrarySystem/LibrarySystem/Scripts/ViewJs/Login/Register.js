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
            Type:1
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
        //var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
        //if (MobilePhone.val() == null || MobilePhone.val() == '') {
        //    swal("提示", "请输入手机号码");
        //    MobilePhone.focus();
        //    return false;
        //} else if (!myreg.test(MobilePhone.val())) {
        //    swal("提示", "手机号格式不正确");
        //    MobilePhone.focus();
        //    return false;
        //}
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
        if (Code.val() == null || Code.val() == '')
        {
            swal("提示", "请输入验证码");
            Code.focus();
            return false;
        }

        var dataArr = {
            UniversityId: UniversityId.val(),
            Account: Account.val(),
            PassWord: PassWord.val(),
            Code: Code.val(),
            Email:Email.val()
            //MobilePhone: MobilePhone.val()
        };
        // window.parent.showModal();

        //$.ajax({
        //    type: "post",
        //    url: "/Login/Register",
        //    data: {
        //        Account: Account.val().trim(), PassWord: PassWord.val().trim(), UniversityId: UniversityId.val(), MobilePhone: MobilePhone.val()
        //    },
        //    dataType: "json",
        //    success: function (data) {
        //        alert(data.Status);
        //        if (data.Status === 200) {
        //            //动画效果
        //            //$(".m-t").css("transform", "translate(0px,-260px)");
        //            //$(".m-t").css("opacity", "22");
        //            //$(".login h3").css("transform", "translate(0px,130px)");
        //            //$(".login h3 span").css("opacity", "1");
        //            //$(".login h3 span i").css("animation-play-state", "running");
         
        //            setTimeout(function () {
        //                window.location.href = '/Login/Login';
        //            }, 1500);
        //        }
        //        else if (data.Status === 202) {
        //            swal("提示", "操作失败");
        //        }
        //        else if (data.Status === 203) {
        //            swal("提示", "账号已存在");
        //        }
        //                    }
        //});
        //提交
        $.post('/Login/Register', dataArr, function (data) {
            if (data.Status === 200) {
                swal("提示", "注册成功");
                //return false;
                setTimeout(function () {
                    window.location.href = '/Login/Login';
                }, 1500);
            }
            if (data.Status === 202) {
                swal("提示", "操作失败，请检查验证码");
               // return false;
            }
            if (data.Status === 203) {
                swal("提示", "账号或邮箱已注册");
                //return false;
            }
            //.complete(function () { window.parent.hideModal(); })
        });
    });
});