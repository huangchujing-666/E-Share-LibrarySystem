$(function () {
    $('#btnSave').click(function () {
        //获取对象元素
        var AsideBookOrderId = $('#AsideBookOrderId'),
        ExpressId = $('#ExpressId'),
        ExpressName = $('#ExpressName'),
        TrafficFee = $('#TrafficFee'),
        ExpressNo = $('#ExpressNo');
        //校验

        if (ExpressName.val() == null || ExpressName.val() == '') {
            swal("提示", "请输入快递名称");
            ExpressName.focus();
            return false;
        }
        if (ExpressNo.val() == null || ExpressNo.val() == '') {
            swal("提示", "请输入快递编号");
            ExpressNo.focus();
            return false;
        }
        var dataArr = {
            AsideBookOrderId: AsideBookOrderId.val(),
            ExpressId: ExpressId.val(),
            ExpressName: ExpressName.val(),
            TrafficFee:TrafficFee.val()==''?0:TrafficFee.val(),
            ExpressNo: ExpressNo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/AsideBookOrder/BookEmail', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/AsideBookOrder/List';
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