﻿document.onkeydown = function (event) {
    ee = event ? event : (window.event ? window.event : null);
    if (ee.keyCode == 13) {
        document.getElementById("btnLogin").click();
    }
}
if (window.top !== window.self) {
    window.top.location = window.location;
}
$(function () {
    $('#account').focus();

    $('#btnLogin').click(function () {
        var _account = $('#account'),
        _password = $('#password'),
        _role = $('#RoleId option:selected'),
    _university = $('#UniversityId option:selected');
        if (_role.val() == 0) {
            swal("提示", "请选择角色");
            return false;
        }
        if (_role.val() != 1000)
        {
             if (_university.val() == 0) {
            swal("提示", "请选择学校");
            return false;
        }
        }
        if (_account.val() == '' || _account.val() == null) {
            _account.focus();
            return false;
        }
        if (_password.val() == '' || _password.val() == null) {
            _password.focus();
            return false;
        }

        $.ajax({
            type: "post",
            url: "/Login/Login",
            data: {
                Account: _account.val().trim(), Password: _password.val().trim(), UniversityId: _university.val(), RoleId: _role.val()      
            },
            dataType: "json",
            success: function (data) {
                if (data.Status === 1) {
                    //动画效果
                    $(".m-t").css("transform", "translate(0px,-260px)");
                    $(".m-t").css("opacity", "22");
                    $(".login h3").css("transform", "translate(0px,130px)");
                    $(".login h3 span").css("opacity", "1");
                    $(".login h3 span i").css("animation-play-state", "running");

                    setTimeout(function () {
                        window.location.href = '/Home/Index';
                    }, 1000);
                }
                if (data.Status === -1) {
                    swal("提示", "请选择角色");
                }
                if (data.Status === -3) {
                    swal("提示", "请选择学校");
                }
                if (data.Status === -1) {
                    swal("提示", "账号或密码错误");
                }
                if (data.Status === -2) {
                    swal("提示", "账号已被禁用");
                }
                
            }
        });
    });
});