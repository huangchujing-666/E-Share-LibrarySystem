$(function () {
    $('#btnSave').click(function () {
        //获取对象元素
        var ResearchAsideBookId = $('#ResearchAsideBookId'),
        UniversityId = $('#UniversityId option:selected'),
        TrafficType = $('#TrafficType option:selected'),
        PayType = $('#PayType option:selected'),
        PayMoney = $('#PayMoney'),
        imgInfoId = $('#imgInfoId'),
        oldImgInfoId = $('#oldImgInfoId'),
        SysAccountId = $('#SysAccountId');
        Title = $('#Title'),
        Isbn = $('#Isbn'),
        Author = $('#Author'),
        MobilePhone = $('#MobilePhone'),
        Email = $('#Email'),
        Address = $('#Address'),
        Category = $('#Category'),
        Remark = $("#Remark"),
        PublicDate = $("#PublicDate");
        //校验
        if (imgInfoId.val() == null || imgInfoId.val() == 0) {
            swal("提示", "请上传图书图片");
            return false;
        }
        //删除旧图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        if (UniversityId.val() == 0) {
            swal("提示", "请选择正确的学校");
            UniversityId.focus();
            return false;
        }
        if (Title.val() == null || Title.val() == '') {
            swal("提示", "请输入图书名称");
            Title.focus();
            return false;
        }
        if (Isbn.val() == null || Isbn.val() == '') {
            swal("提示", "请输入图书ISBN");
            Isbn.focus();
            return false;
        }
        if (Category.val() == null || Category.val() == '') {
            swal("提示", "请输入图书类别");
            Category.focus();
            return false;
        }
        if (Author.val() == null || Author.val() == '') {
            swal("提示", "请输入作者名称");
            Author.focus();
            return false;
        }
        if (PublicDate.val() == null) {
            swal("提示", "请输入作者名称");
            PublicDate.focus();
            return false;
        }
        if (PayType.val() == 0)//有偿
        {
            swal("提示", "请输入寻书方式");
            PayType.focus();
            return false;
        }
        if (PayType.val() == 1)//有偿
        {
            if (PayMoney.val() <= 0) {
                swal("提示", "请输入有偿金额");
                PayMoney.focus();
                return false;
            }
        }
        if (TrafficType.val() == 0) {
            swal("提示", "请选择正确的运书方式");
            TrafficType.focus();
            return false;
        }
        var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
        if (MobilePhone.val() == null || MobilePhone.val() == '') {
            swal("提示", "请输入手机号码");
            MobilePhone.focus();
            return false;
        }
        else if (!myreg.test(MobilePhone.val())) {
            swal("提示", "手机号格式不正确");
            MobilePhone.focus();
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
        if (Address.val() == null || Address.val() == '') {
            swal("提示", "请输入地址");
            Address.focus();
            return false;
        }
        var dataArr = {
            ResearchAsideBookId: ResearchAsideBookId.val(),
            UniversityId: UniversityId.val(),
            Category: Category.val(),
            Title: Title.val(),
            Isbn: Isbn.val(),
            Author: Author.val(),
            PublicDate: PublicDate.val(),
            Remark: Remark.val(),
            BaseImageId: imgInfoId.val(),
            Address: Address.val(),
            Email: Email.val(),
            MobilePhone:MobilePhone.val(),
            PayType: PayType.val(),
            TrafficType: TrafficType.val(),
            PayMoney: PayMoney.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ResearchAsideBook/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/ResearchAsideBook/MyResearchList';
                   // window.history.back(-1);
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

//确认图书领取
var orderCalcle = function (url, skipUrl, id, statusId) {
    swal({
        title: "您确定是取消此订单吗",
        text: "取消后将无法修改，请谨慎操作！",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "是的，确定取消！",
        cancelButtonText: "让我再考虑一下…",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            window.parent.showModal();
            $.post(url, { AsideBookOrderId: id, OrderStatus: statusId }, function (data) {
                if (data.Status == 200) {
                    swal("取消成功！", "您已经完成此订单的去取消服务。", "success");
                    setTimeout(function () {
                        //window.location.href = skipUrl;
                        window.location.href = location.href;
                    }, 1500);
                } else {
                    swal("提示", "领取失败");
                }
            }).complete(function () { window.parent.hideModal(); });
        } else {
            swal("已取消", "您取消了此次操作！", "error");
        }
    });
};