//
var awaitCalcle = function (url, skipUrl, id, statusId) {
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
            window.parent.showModal();//顺风送 取消顺风送调用   求书更新状态调用
            $.post(url, { ResearchAsideBookId: id, ResearchStatus: statusId, AsideBookOrderId: id, OrderStatus: statusId }, function (data) {
                if (data.Status == 200) {
                    swal("取消成功！", "您已经完成此订单的取消服务。", "success");
                    setTimeout(function () {
                        //window.location.href = skipUrl;
                        window.location.href = location.href;
                    }, 1500);
                } else {
                    swal("提示", "取消失败");
                }
            }).complete(function () { window.parent.hideModal(); });
        } else {
            swal("已取消", "您取消了此次操作！", "error");
        }
    });
};

//确认图书领取
var orderConfirm = function (url, skipUrl, id, statusId) {
    swal({
        title: "您确定收书人本人已收到图书吗",
        text: "确认后将无法修改，请谨慎操作！",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "是的，我确认！",
        cancelButtonText: "让我再考虑一下…",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            window.parent.showModal();
            $.post(url, { AsideBookOrderId: id, OrderStatus: statusId }, function (data) {
                if (data.Status == 200) {
                    swal("确认成功！", "您已经完成此订单服务。", "success");
                    setTimeout(function () {
                        //window.location.href = skipUrl;
                        window.location.href = location.href;
                    }, 1500);
                } else {
                    swal("提示", "确认失败");
                }
            }).complete(function () { window.parent.hideModal(); });
        } else {
            swal("已取消", "您取消了确认操作！", "error");
        }
    });
};