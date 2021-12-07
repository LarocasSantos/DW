/* $(document).ready(function(){

    // link action
    $(".action").click(function(e){
        e.preventDefault();

        $(".slide").removeClass("active");
        var slide = $(this).closest(".slide");
        slide.addClass("active");
    });

});

/*
window.onload = function(){
    var hideMe = document.getElementById('slide');
    document.onclick = function(e){
       if(e.target.id !== 'slide'){
          //hideMe.style.display = 'none';
          $(".slide").removeClass("active");
       }
       else{
        var slide = $(this).closest(".slide");
        slide.addClass("active");
       }
    };
 };
*/

/* // checkWidth
checkWidth = function(){
    var windowsize = $(window).width();
    if (windowsize > 480) {
        var slideWidth = $('.active').width();
        $('.slide-content').css({
            "width" : slideWidth+"px"
        });
    }
    
}

$(window).resize(function() {
    // onresize
    checkWidth();

    // finish resize
    clearTimeout(window.resizedFinished);
    window.resizedFinished = setTimeout( checkWidth , 500);
}); */ 