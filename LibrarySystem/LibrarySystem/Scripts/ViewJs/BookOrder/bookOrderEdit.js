$(function () {
    $('#btnSave').click(function () {
        var BookOrderId = $('#BookOrderId'),
        UniversityId = $('#UniversityId option:selected'),
        Isbn = $('#Isbn'),
        Account = $('#Account'),
        Count = $('#Count');
        //校验
        if (UniversityId.val() == 0) {
            swal("提示", "请选择正确的学校");
            UniversityId.focus();
            return false;
        }
        if (Isbn.val() == null || Isbn.val() == '') {
            swal("提示", "请输入图书ISBN");
            Isbn.focus();
            return false;
        }
        if (Account.val() == null || Account.val() == '') {
            swal("提示", "请输入学生账户");
            Isbn.focus();
            return false;
        }
        if (Count.val() == null || Count.val() == 0) {
            swal("提示", "请输入图书数量");
            Count.focus();
            return false;
        }
        var dataArr = {
            BookOrderId: BookOrderId.val(),
            UniversityId: UniversityId.val(),
            Isbn: Isbn.val(),
            Account: Account.val(),
            Isbn: Isbn.val(),
            Count: Count.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BookOrder/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BookOrder/List';
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