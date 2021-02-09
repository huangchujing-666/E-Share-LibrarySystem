$(function () {
    $('#btnSave').click(function () {
        var BookInfoId = $('#BookInfoId'),
        UniversityId = $('#UniversityId option:selected'),
        Title = $('#Title'),
        Isbn = $('#Isbn'),
        Author = $('#Author'),
        Category = $('#Category'),
        Count = $('#Count'),
        PublicDate = $("#PublicDate"),
        Available = $('#Available');
        //校验
        if (UniversityId.val() == 0)
        {
            swal("提示", "请选择正确的学校");
            UniversityId.focus();
            return false;
        }
        if (Title.val() == null || Title.val() =='') {
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
        if (PublicDate.val() == null || PublicDate.val()=='')
        {
            swal("提示", "请输入出版日期");
            PublicDate.focus();
            return false;
        }
        if (Count.val() == null || Count.val() == 0) {
            swal("提示", "请输入图书数量");
            Count.focus();
            return false;
        }
        if (Available.val() == null || Available.val() < 0) {
            swal("提示", "请输入可借数量");
            Available.focus();
            return false;
        }
        if ( parseInt( Count.val()) <parseInt( Available.val())) {
            swal("提示", "可以预借数量必须小于图书数量");
            Count.focus();
            return false;
        }

        var dataArr = {
            BookInfoId: BookInfoId.val(),
            UniversityId: UniversityId.val(),
            Category: Category.val(),
            Title: Title.val(),
            Isbn: Isbn.val(),
            Author: Author.val(),
            PublicDate:PublicDate.val(),
            Count: Count.val(),
            Available: Available.val()
        };
        window.parent.showModal();
        //提交
        $.post('/BookInfo/Edit', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/BookInfo/List';
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