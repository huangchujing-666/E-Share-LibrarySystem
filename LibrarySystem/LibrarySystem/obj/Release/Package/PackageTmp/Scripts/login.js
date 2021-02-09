//轮播图
var i = -1;
var timer = 0;
$(document).ready(function() {

    var len = $(".hc-banne .banner").children;
	move();
	timer = setInterval("move()", 5000);
});

var screenWidth = window.screen.width;
function move(){
    i++;
    if(i > 2) {
        i = 0;
    }
    if(i == 0){
        $('.hc-banner ul li').eq(i).fadeIn(100).siblings().fadeOut();
	}
    if(i == 1){
        $('.hc-banner ul li').eq(i).fadeIn(100).siblings().fadeOut();
    }
    if(i == 2){
        $('.hc-banner ul li').eq(i).fadeIn(100).siblings().fadeOut();
    }
}
//轮播图结束