var $jq = jQuery.noConflict();
$jq(document).on('ready', function () {
		$jq(".slick-list").slick({
				dots: false,
				infinite: false,
				speed: 300,
				slidesToShow: 1,
				slidesToScroll: 1,
				autoplay: true,
				autoplaySpeed: 2000,
		});

});
//$("#used-point").on('click', function(){
//		$("#used-point").addClass("active-tab");
//		$("#get-point").removeClass("active-tab");
//		$(".used-point").addClass("fade out");
//		$(".get-point").removeClass("fade out");
//});
//$("#get-point").on('click', function(){
//		$("#get-point").addClass("active-tab");
//		$("#used-point").removeClass("active-tab");
//		$(".get-point").addClass("fade out");
//		$(".used-point").removeClass("fade out");
//});