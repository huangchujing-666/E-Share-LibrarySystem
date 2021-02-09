$(function () {
    var TrafficType = $('#TrafficType option:selected');//求书人
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
            //default:
            //    $(".traffic-address").css("display", "block");
    }

    var ShareTrafficType = $('#ShareTrafficType option:selected');//出书人
    switch (parseInt(ShareTrafficType.val())) {
        // 自行送至漂流书屋 = 1,
        //工作人员上门取书 = 2
        case 1:
            $(".traffic-address").css("display", "none");
            break;
        case 2:
            $(".traffic-address").css("display", "block");
            break;
            //default:
            //    $(".traffic-address").css("display", "block");
    }

    $('#btnSave').click(function () {
        //获取对象元素
        var ResearchAsideBookId = $('#ResearchAsideBookId'),
        TrafficType = $('#TrafficType option:selected'),
        ShareTrafficType = $('#ShareTrafficType option:selected'),
        ShareSysAccountId = $('#ShareSysAccountId'),
        SysAccountId = $('#SysAccountId'),
        Email = $('#Email'),
        MobilePhone = $('#MobilePhone'),
        Address = $('#Address'),
        ResearchStatus = $('#ResearchStatus');
        //CustomerInfo = $('#CustomerInfo');
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
        //if (Address.val() == null || Address.val() == '') {
        //    swal("提示", "请输入地址");
        //    Address.focus();
        //    return false;
        //}
        if (TrafficType.val() == 0) {
            swal("提示", "请选择正确的运输方式");
            TrafficType.focus();
            return false;
        }
        switch (parseInt(TrafficType.val())) {
            case 2://邮寄= 2
                if (Address.val() == null || Address.val() == '') {
                    swal("提示", "请输入正确的联系方式与地址");
                    Address.focus();
                    return false;
                }
                break;
            default:
        }
        if (ShareTrafficType.val() == 0) {
            swal("提示", "请选择正确的运输方式");
            TrafficType.focus();
            return false;
        }
        switch (parseInt(ShareTrafficType.val())) {
            case 2://邮寄= 2
                if (Address.val() == null || Address.val() == '') {
                    swal("提示", "请输入正确的联系方式与地址");
                    Address.focus();
                    return false;
                }
                break;
            default:
        }
        var tp = parseInt(ShareSysAccountId.val()) > 0 ? ShareTrafficType.val() : TrafficType.val();
        var dataArr = {
            ResearchAsideBookId: ResearchAsideBookId.val(),
            TrafficType: tp,
            ShareSysAccountId: ShareSysAccountId.val(),
            ResearchStatus: ResearchStatus.val(),
            SysAccountId: SysAccountId.val(),
            Address:Address.val(),
            Email: Email.val(),
            MobilePhone: MobilePhone.val(),

            //CustomerInfo: CustomerInfo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ResearchAsideBook/EditCustomerInfo', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/ResearchAsideBook/MyResearchList';
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