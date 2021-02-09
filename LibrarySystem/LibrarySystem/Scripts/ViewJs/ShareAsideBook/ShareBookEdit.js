$(function () {

    var TrafficType = $('#TrafficType option:selected');
    switch (parseInt(TrafficType.val())) {
        //自行送至漂流书屋 = 1,
        // 等待工作人员上门 = 2
        case 1:
            $(".traffic-address").css("display", "none");
            break;
        case 2:
            $(".traffic-address").css("display", "block");
            break;
        default:
            $(".traffic-address").css("display", "none");
    }

    $('#btnSave').click(function () {
        //获取对象元素
        var ShareAsideBookId = $('#ShareAsideBookId'),
        TrafficType = $('#TrafficType option:selected'),
        SysAccountId = $('#SysAccountId'),
        ResearchAsideBookId = $('#ResearchAsideBookId'),
        Email = $('#Email'),
        MobilePhone = $('#MobilePhone')
        Address = $('#Address');

        var myemail =/^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
        if (Email.val() == null || Email.val() == '') {
            swal("提示", "请输入电子邮箱");
            Email.focus();
            return false;
        }
            //.match(/^(((13[0-9]{1})|159|153)+\d{8})$/)
         else if (!myemail.test(Email.val())) {
        //else if (Email.val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
            swal("提示", "电子邮箱格式不正确");
            Email.focus();
            return false;
        }
        var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
        if (MobilePhone.val() == null || MobilePhone.val() == '') {
            swal("提示", "请输入手机号码");
            MobilePhone.focus();
            return false;
        }
        else if (!myreg.test(MobilePhone.val())) {
       // else if (MobilePhone.val().match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
            swal("提示", "手机号格式不正确");
            MobilePhone.focus();
            return false;
        }

        //校验
        if (TrafficType.val() == 0) {
            swal("提示", "请选择正确的运输方式");
            TrafficType.focus();
            return false;
        }
        switch (parseInt(TrafficType.val())) {
            case 2://等待工作人员上门 = 2
                if (Address.val() == null || Address.val() == '') {
                    swal("提示", "请输入正确的联系地址");
                    Address.focus();
                    return false;
                }
                break;
            default:
        }
        var dataArr = {
            ShareAsideBookId: ShareAsideBookId.val(),
            TrafficType: TrafficType.val(),
            ResearchAsideBookId: ResearchAsideBookId.val(),
            SysAccountId: SysAccountId.val(),
            Email: Email.val(),
            Address: Address.val(),
            MobilePhone: MobilePhone.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ShareAsideBook/ShareBook', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/ShareAsideBook/MyShareList';
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