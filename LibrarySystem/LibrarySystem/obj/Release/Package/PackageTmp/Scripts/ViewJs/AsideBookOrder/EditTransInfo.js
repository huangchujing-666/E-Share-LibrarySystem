$(function () {
    var TrafficType = $('#TrafficType option:selected');
    switch (parseInt(TrafficType.val())) {
        //自取 = 1,
        //邮寄 = 2,
        //顺路送书 = 3
        case 1:
            $(".traffic-address").css("display", "none");
            break;
        case 2:
            $(".traffic-address").css("display", "block");
            break;
        default:
            $(".traffic-address").css("display", "block");
    }
    $(".senderinfo").css("display", "none");
    var SysAccount = $('#SysAccountId');
    var TrafficAccount = $('#TrafficAccountId');
    if (parseInt(SysAccount.val()) > 0) {
        $(".traffic-address").css("display", "block");
    }
    else if (parseInt(TrafficAccount.val()) > 0) {
        $(".senderinfo").css("display", "block");
    }
    $('#btnSave').click(function () {
        //获取对象元素
        var AsideBookOrderId = $('#AsideBookOrderId'),
        TrafficType = $('#TrafficType option:selected'),
        SysAccountId = $('#SysAccountId'),
        TrafficAccountId = $('#TrafficAccountId'),
        TrafficFee = $('#TrafficFee'),
        Email = $('#Email'),
        MobilePhone = $('#MobilePhone'),
        SenderAddress=$('#SenderAddress'),
        Address = $('#Address');
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


        //自取 = 1, 邮寄 = 2, 顺路送书 = 3

        if (TrafficType.val() == 0) {
            swal("提示", "请选择正确的运输方式");
            TrafficType.focus();
            return false;
        }
        if (parseInt(SysAccountId.val()) > 0) {
            switch (parseInt(TrafficType.val())) {
                case 2://邮寄= 2
                    if (Address.val() == null || Address.val() == '') {
                        swal("提示", "请输入正确的联系地址");
                        Address.focus();
                        return false;
                    }
                case 3://邮寄= 2
                    if (Address.val() == null || Address.val() == '') {
                        swal("提示", "请输入正确的联系地址");
                        Address.focus();
                        return false;
                    }
                    break;
                default:
            }
        }
        if (parseInt(SysAccountId.val()) > 0) {
            if (Address.val() == null || Address.val() == '') {
                swal("提示", "请输入正确的联系方式与地址");
                Address.focus();
                return false;
            }
        }
        if (parseInt(TrafficAccountId.val()) > 0) {
            if (SenderAddress.val() == null || SenderAddress.val() == '') {
                swal("提示", "请输入正确的联系方式与地址");
                SenderAddress.focus();
                return false;
            }
        }
        var dataArr = {
            AsideBookOrderId: AsideBookOrderId.val(),
            TrafficType: TrafficType.val(),
            SysAccountId: SysAccountId.val(),
            TrafficAccountId: TrafficAccountId.val(),
            TrafficFee: TrafficFee.val(),
            Email: Email.val(),
            SenderAddress:SenderAddress.val(),
            Address: Address.val(),
            MobilePhone: MobilePhone.val()
        };
        window.parent.showModal();
        //提交
        $.post('/AsideBookOrder/EditTransInfo', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/AsideBookOrder/MyTransRecord';
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