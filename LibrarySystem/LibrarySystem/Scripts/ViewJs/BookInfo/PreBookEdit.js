$(function () {

    var TrafficType = $('#TrafficType option:selected');
    switch (parseInt(TrafficType.val())) {
        case 1://EnumHelp.TrafficType.自取
            $(".traffic-address").css("display", "none");
            break;
        case 2://EnumHelp.TrafficType.邮寄
            $(".traffic-address").css("display", "block");
            break;
        default:
            $(".traffic-address").css("display", "none");
    }

    $('#btnSave').click(function () {
        //获取对象元素
        var BookInfoId = $('#BookInfoId'),
        SysAccountId = $('#SysAccountId'),
        TrafficType = $('#TrafficType option:selected'),
        BookOrderId = $('#BookOrderId'),
        Email = $('#Email'),
        MobilePhone = $('#MobilePhone'),
        Address = $('#Address');
        //校验


        //校验

        //var myemail = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
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
        var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
        if (MobilePhone.val() == null || MobilePhone.val() == '') {
            swal("提示", "请输入手机号码");
            mobilePhone.focus();
            return false;
        }
        else if (!myreg.test(MobilePhone.val())) {
            swal("提示", "手机号格式不正确");
            MobilePhone.focus();
            return false;
        }

        if (TrafficType.val() == 0) {
            swal("提示", "请选择正确的运输方式");
            TrafficType.focus();
            return false;
        }
        switch (parseInt(TrafficType.val())) {
            case 2://EnumHelp.TrafficType.邮寄
                if (Address.val() == null || Address.val() == '') {
                    swal("提示", "请输入正确的联系地址");
                    Address.focus();
                    return false;
                }
                break;
            default:
        }
        var dataArr = {
            BookInfoId: BookInfoId.val(),
            SysAccountId: SysAccountId.val(),
            BookOrderId:BookOrderId.val(),
            TrafficType: TrafficType.val(),
            Email: Email.val(),
            Address: Address.val(),
            MobilePhone: MobilePhone.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BookInfo/PreBook', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BookInfo/MyList';
                }, 1500);
            }
            if (data.Status == 202) {
                swal("提示", "操作失败");
            }
            if (data.Status == 203) {
                swal("提示", "数据重复");
            }
        }).complete(function () { window.parent.hideModal(); });
    });
});