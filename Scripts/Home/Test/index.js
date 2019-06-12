//;(function() {
	
//	var elems = $('.row [class*="item"]');
//	var setHeight = function(elem) { 
//			$(elem).height($(elem).width());
//	};
//	$(window).resize(function() {
//		$.each(elems, function(key, value) {
//			setHeight(elems[key]);
//		});
//	});
//	var i = 0;
//	x = true;
//	$('button').click(function() {
//		if (i === -1 || i == elems.length) {
//			animate();
//			x = !x;
//		}
//	});
//	var animate = function() {
//		setTimeout(function() {
//			$(elems[i]).toggleClass('shown');
//			setHeight(elems[i]);
//			(x) ? i++ : i--;
//			console.log(i);
//			if (i < elems.length && i >= 0) {
//				animate();
//			};
//		}, 60);
//	}; animate();
	
//})();