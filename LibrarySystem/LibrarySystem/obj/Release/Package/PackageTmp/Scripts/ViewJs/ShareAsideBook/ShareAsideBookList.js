//确认
var confirm = function (url, skipUrl, id, universityId, isbn, ResearchAsideBookId) {
    swal({
        title: "您确定此书无误吗",
        text: "确定后将无法恢复，请谨慎操作！",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "是的，我已确认！",
        cancelButtonText: "让我再考虑一下…",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            window.parent.showModal();
            $.post(url, { ShareAsideBookId: id, UniversityId: universityId, Isbn: isbn, ResearchAsideBookId: ResearchAsideBookId }, function (data) {
                if (data.Status == 200) {
                    swal("确认成功！", "您已经将此书入库。", "success");
                    setTimeout(function () {
                        if (ResearchAsideBookId > 0) {
                            window.location.href = "/ResearchAsideBook/List?ResearchAsideBookId="+ResearchAsideBookId;
                        }
                        else {
                            window.location.href = location.href;
                        }
                        //window.location.href = skipUrl;
                    }, 1500);
                } else {
                    swal("提示", "入库失败");
                }
            }).complete(function () { window.parent.hideModal(); });
        } else {
            swal("已取消", "您取消了入库操作！", "error");
        }
    });
};

//取消
var cancle = function (url, skipUrl, id) {
    swal({
        title: "您确定取消此书吗",
        text: "取消后将无法恢复，请谨慎操作！",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "是的，我已确认！",
        cancelButtonText: "让我再考虑一下…",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            window.parent.showModal();
            $.post(url, { ShareAsideBookId: id, ResearchAsideBookId:0 }, function (data) {
                if (data.Status == 200) {
                    swal("取消成功！", "您已经取消此书入库。", "success");
                    setTimeout(function () {
                        //window.location.href = skipUrl;
                        window.location.href = location.href;
                    }, 1500);
                } else {
                    swal("提示", "取消失败");
                }
            }).complete(function () { window.parent.hideModal(); });
        } else {
            swal("已取消", "您取消了本次操作！", "error");
        }
    });
};