$(function () {
    $('#btnSave').click(function () {
        var id = $('#id'),
            imgInfoId = $('#imgInfoId'),
            oldImgInfoId = $('#oldImgInfoId'),
            account = $('#account'),
            passWord = $('#passWord'),
            nickName = $('#nickName'),
            mobilePhone = $('#mobilePhone'),
            Email = $('#Email'),
            Address = $('#Address'),
            remarks = $('#remarks');


        //if (roleId.val() == 2 && businessInfoId.val()<=0) {
        //    swal("提示", "请输入帐号对应的商家ID");
        //    return false;
        //}

        if (id.val() == 0 && (passWord.val() == null || passWord.val() == '' || passWord.val().length < 6 || passWord.val().length > 18)) {
            swal("提示", "请输入6至18位数字或字母密码");
            passWord.focus();
            return false;
        }
        var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
        if (mobilePhone.val() == null || mobilePhone.val() == '')
        {
            swal("提示", "请输入手机号码");
            mobilePhone.focus();
            return false;
        }
        else if (!myreg.test(mobilePhone.val())) {
            swal("提示", "手机号格式不正确");
            mobilePhone.focus();
            return false;
        }
       
        var myemail =/^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
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
        if (Address.val() == null || Address.val() == '') {
            swal("提示", "请输入地址");
            Address.focus();
            return false;
        }

        //删除旧图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        var dataArr = {
            SysAccountId: id.val(),
            BaseImageId: imgInfoId.val(),
            Account: account.val(),
            PassWord: passWord.val(),
            NickName: nickName.val(),
            Address: Address.val(),
            Email:Email.val(),
            MobilePhone: mobilePhone.val(),
            Remarks: remarks.val()
        };
        window.parent.showModal();
        //提交
        $.post('/SysAccount/StudentEdit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/SysAccount/StudentEdit';
                }, 1500);
            }
            if (data.Status == 202) {
                swal("提示", "操作失败");
            }
            if (data.Status == 203) {
                swal("提示", "该账号已存在，请勿重复添加。");
            }
        }).complete(function () { window.parent.hideModal(); });
    });
});