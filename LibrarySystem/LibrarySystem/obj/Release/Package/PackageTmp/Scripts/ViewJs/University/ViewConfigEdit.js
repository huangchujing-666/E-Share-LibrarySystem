$(function () {
    $('#btnSave').click(function () {
        var ViewConfigId = $("#ViewConfigId"),
           UniversityId = $('#UniversityId option:selected'),
         ViewName = $("#ViewName"),
        Title = $('#Title'),
        Isbn = $('#Isbn'),
        Author = $('#Author'),
        Category = $('#Category'),
        Count = $('#Count'),
        PublicDate = $("#PublicDate"),
         //
        Available = $('#Available');
       
        //校验
        var reg=/^[A-Za-z]+$/;
        if (UniversityId.val() == '0') {
            swal("提示", "请选择正确的数据源");
            UniversityId.focus();
            return false;
        }
        if (ViewName.val() == null || ViewName.val() == '' || !reg.test(ViewName.val())) {
            swal("提示", "请输入由小于20个英文字母组成的视图名称");
            ViewName.focus();
            return false;
        }
        if (Title.val() == null || Title.val() == '' || !reg.test(Title.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书名称字段");
            Title.focus();
            return false;
        }
        if (Isbn.val() == null || Isbn.val() == '' || !reg.test(Isbn.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书ISBN字段");
            Isbn.focus();
            return false;
        }
        if (Author.val() == null || Author.val() == '' || !reg.test(Author.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书ISBN字段");
            Isbn.focus();
            return false;
        }
        if (Category.val() == null || Category.val() == '' || !reg.test(Category.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书类别字段");
            Category.focus();
            return false;
        }
        if (PublicDate.val() == null || PublicDate.val() == '' || !reg.test(PublicDate.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书出版日期字段");
            PublicDate.focus();
            return false;
        }
        if (Count.val() == null || Count.val() == '' || !reg.test(Count.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书库存数量字段");
            Count.focus();
            return false;
        }

        if (Available.val() == null || Available.val() == '' || !reg.test(Available.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书可预借数量字段");
            Available.focus();
            return false;
        }
        var arry = new Array(ViewName.val(), Title.val(), Isbn.val(), Author.val(), Category.val(), Count.val(), PublicDate.val(), Available.val());
        arr = arry.sort();
        for (var i = 0; i < arr.length; i++)
        {
            if (arr[i] == arr[i + 1])
            {
                swal("提示", "字段名称不能重复");
                Title.focus();
                return false;
            }
        }
        var dataArr = {
            ViewConfigId: ViewConfigId.val(),
            UniversityId: UniversityId.val(),
            ViewName: ViewName.val(),
            Title: Title.val(),
            Isbn: Isbn.val(),
            Author: Author.val(),
            Category: Category.val(),
            Count: Count.val(),
            PublicDate: PublicDate.val(),
            Available: Available.val()
        };
        window.parent.showModal();
        //提交
        $.post('/University/UpdateStart', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/University/UpdateStart';
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
    $('#btnStart').click(function () {
        var UniversityId = $("#UniversityId option:selected"),
            UpdateCount = $('#UpdateCount');
            //校验
            if (UniversityId.val() == 0) {
                swal("提示", "请选择正确的数据源");
                UniversityId.focus();
                return false;
            }
            var dataArr = {
                UniversityId: UniversityId.val(),
                UpdateCount: UpdateCount.val()
            };
            window.parent.showModal();
        //提交
            $.post('/University/Start', dataArr, function (data) {
                if (data.Status == 200) {
                    swal("提示", "操作成功");
                    setTimeout(function () {
                        window.location.href = '/University/UpdateStart';
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
    $('#btnSaveOnly').click(function () {
         var ViewConfigId = $("#ViewConfigId"),
         ViewName = $("#ViewName"),
        Title = $('#Title'),
        Isbn = $('#Isbn'),
        Author = $('#Author'),
        Category = $('#Category'),
        Count = $('#Count'),
        PublicDate = $("#PublicDate"),
        UpdateCount = $('#UpdateCount'),
        Available = $('#Available');
        //校验
        var reg=/^[A-Za-z]+$/;
        if (ViewName.val() == null || ViewName.val() == '' || !reg.test(ViewName.val())) {
            swal("提示", "请输入由小于20个英文字母组成的视图名称");
            ViewName.focus();
            return false;
        }
        if (Title.val() == null || Title.val() == '' || !reg.test(Title.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书名称字段");
            Title.focus();
            return false;
        }
        if (Isbn.val() == null || Isbn.val() == '' || !reg.test(Isbn.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书ISBN字段");
            Isbn.focus();
            return false;
        }
        if (Author.val() == null || Author.val() == '' || !reg.test(Author.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书ISBN字段");
            Isbn.focus();
            return false;
        }
        if (Category.val() == null || Category.val() == '' || !reg.test(Category.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书类别字段");
            Category.focus();
            return false;
        }
        if (PublicDate.val() == null || PublicDate.val() == '' || !reg.test(PublicDate.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书出版日期字段");
            PublicDate.focus();
            return false;
        }
        if (Count.val() == null || Count.val() == '' || !reg.test(Count.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书库存数量字段");
            Count.focus();
            return false;
        }

        if (Available.val() == null || Available.val() == '' || !reg.test(Available.val())) {
            swal("提示", "请输入由小于20个英文字母组成的图书可预借数量字段");
            Available.focus();
            return false;
        }
        var arry = new Array(ViewName.val(), Title.val(), Isbn.val(), Author.val(), Category.val(), Count.val(), PublicDate.val(), Available.val());
        arr = arry.sort();
        for (var i = 0; i < arr.length; i++)
        {
            if (arr[i] == arr[i + 1])
            {
                swal("提示", "字段名称不能重复");
                Title.focus();
                return false;
            }
        }
        var dataArr = {
            ViewConfigId: ViewConfigId.val(),
            ViewName: ViewName.val(),
            Title: Title.val(),
            Isbn: Isbn.val(),
            Author: Author.val(),
            Category: Category.val(),
            Count: Count.val(),
            UpdateCount: UpdateCount.val(),
            PublicDate: PublicDate.val(),
            Available: Available.val()
        };
        window.parent.showModal();
        //提交
        $.post('/University/UpdateStart', dataArr, function (data) {
            if (data.Status == 200) {
                swal("提示", "操作成功");
                setTimeout(function () {
                    window.location.href = '/University/UpdateStart';
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