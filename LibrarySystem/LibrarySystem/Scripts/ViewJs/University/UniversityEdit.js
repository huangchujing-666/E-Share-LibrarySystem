$(function () {
    $('#btnSave').click(function () {
        var UniversityId = $('#UniversityId'),
       IsUpdate = $('#IsUpdate option:selected'),
        Name = $('#Name'),
        Service = $('#Service'),
        DataBase = $('#DataBase');
        UserId = $('#UserId');
        UserPwd = $('#UserPwd');//
        TimeStart = $('#TimeStart');
        //校验
        if (Name.val() == null || Name.val() == '') {
            swal("提示", "请输入学校名称");
            Name.focus();
            return false;
        }
        if (Service.val() == null || Service.val() == '') {
            swal("提示", "请输入数据库服务");
            Service.focus();
            return false;
        }
        if (DataBase.val() == null || DataBase.val() == '') {
            swal("提示", "请输入数据库名称");
            DataBase.focus();
            return false;
        }
        if (UserId.val() == null || UserId.val() == '') {
            swal("提示", "请输入用户ID");
            UserId.focus();
            return false;
        }
        if (UserPwd.val() == null || UserPwd.val() == '') {
            swal("提示", "请输入用户密码");
            UserPwd.focus();
            return false;
        }
        if (TimeStart.val() == null || TimeStart.val() == '') {
            swal("提示", "请输入时间间隔");
            TimeStart.focus();
            return false;
        }
        var dataArr = {
            UniversityId: UniversityId.val(),
            Name: Name.val(),
            IsUpdate: IsUpdate.val(),
            Service: Service.val(),
            DataBase: DataBase.val(),
            UserId: UserId.val(),
            UserPwd: UserPwd.val(),
            TimeStart: TimeStart.val()
        };
        window.parent.showModal();
        //提交
        $.post('/University/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/University/List';
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