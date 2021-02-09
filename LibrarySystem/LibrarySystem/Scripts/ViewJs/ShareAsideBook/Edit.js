$(function () {
    $('#btnSave').click(function () {
        //获取对象元素
        var ShareAsideBookId = $('#ShareAsideBookId'),
        imgInfoId = $('#imgInfoId'),
        oldImgInfoId = $('#oldImgInfoId'),
        UniversityId = $('#UniversityId option:selected'),
        Category = $('#Category'),
        Title = $('#Title'),
        Isbn = $('#Isbn'),
        Author = $('#Author'),
        PublicDate = $("#PublicDate"),
        Count = $("#Count"),
        PayType = $('#PayType option:selected'),
        PayMoney = $("#PayMoney"),
        TrafficType = $('#TrafficType option:selected'),
        Email = $("#Email"),
        MobilePhone = $("#MobilePhone"),
        Address = $("#Address");
        //校验
        if (imgInfoId.val() == null || imgInfoId.val() == 0) {
            swal("提示", "请上传图书图片");
            return false;
        }
        //删除旧图片
        if (imgInfoId.val() != oldImgInfoId.val()) {
            $.post("/Uploader/DeleteFile", { Id: oldImgInfoId.val() }, function (data) { });
        }
        if (Category.val() == null || Category.val() == '') {
            swal("提示", "请输入图书类别");
            Category.focus();
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
        if (Author.val() == null || Author.val() == '') {
            swal("提示", "请输入作者名称");
            Author.focus();
            return false;
        }
        if (PublicDate.val() == null || PublicDate.val() == '') {
            swal("提示", "请输入出版日期");
            PublicDate.focus();
            return false;
        }
        if (Count.val() <= 0) {
            swal("提示", "请输入数量");
            Count.focus();
            return false;
        }
        if (PayType.val() == 0)//有偿
        {
            swal("提示", "请选择共享方式");
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
            MobilePhone.focus();
            return false;
        }
        else if (!myreg.test(MobilePhone.val())) {
            swal("提示", "手机号格式不正确");
            MobilePhone.focus();
            return false;
        }

        if (TrafficType.val() == 0)//有偿
        {
            swal("提示", "请选择图书运输类型");
            TrafficType.focus();
            return false;
        }
        if (TrafficType.val() == 2)//有偿   自行送至漂流书屋 = 1,            等待工作人员上门 = 2
        {
            if (Address.val() == "" || Address.val() == null) {
                swal("提示", "请输入联系地址");
                Address.focus();
                return false;
            }
        }
        var dataArr = {
            ShareAsideBookId: ShareAsideBookId.val(),
            UniversityId: UniversityId.val(),
            Category: Category.val(),
            Title: Title.val(),
            Isbn: Isbn.val(),
            Author: Author.val(),
            PublicDate: PublicDate.val(),
            Count: Count.val(),
            BaseImageId: imgInfoId.val(),
            PayType: PayType.val(),
            PayMoney: PayMoney.val(),
            TrafficType: TrafficType.val(),
            Email: Email.val(),
            MobilePhone: MobilePhone.val(),
            Address: Address.val()
            //ShareCustomerInfo: ShareCustomerInfo.val()
        };
        window.parent.showModal();
        //提交
        $.post('/ShareAsideBook/Edit', dataArr, function (data) {
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