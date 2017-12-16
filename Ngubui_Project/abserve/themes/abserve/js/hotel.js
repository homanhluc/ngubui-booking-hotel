$(document).ready(function() {
		function getUrlVars()
	{
	    var vars = [], hash;
	    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	    for(var i = 0; i < hashes.length; i++)
	    {
	        hash = hashes[i].split('=');
	        vars.push(hash[0]);
	        vars[hash[0]] = hash[1];
	    }
	    return vars;
	}
	var first = getUrlVars()["inout"];

	if(first != 1)
	{
		$('.edit_').hide();

	}

		var id = $('#reviewer_id').val();
		var alrd = $('#already_reviewed').val();
		/*if(id == '' || id == '/')
		{	
			$('.review_btn').removeAttr( "data-target" );
		}*/

		if(alrd != '/' && alrd != '')
		{
			$('.review_btn').removeAttr( "data-target" );
		}

});

/*$(document).on('click','.bocme_host',function(){
	$user_id  = $('.di').val();
	$('#start_host').flash_message({
		text: 'Your request has been send to the admin..After getting access you can create your own rooms..',
		how: 'append'
	});
	$.ajax({
			url: base_url+'user/becomehost',
		    type: 'POST',
		    data: {
		    	di : $user_id
		    }
		});
});*/

$(document).on('click','.bocme_host',function(e){
	e.preventDefault();
	$user_id  = $('.di').val();
		$.ajax({
			url  : base_url+'user/becomehost',
			type : 'POST',
			data : {
				di : $user_id
			},
			success: function(data) {
				// alert(data);
				if(data == "success"){
					$('#start_host').addClass('alert-success').html('<strong>Success!!..</strong> Your request has been send to Admin..After getting access you can create your own rooms..').show().delay(4000).fadeOut('fast');
				} else if(data == "failed"){
					$('#start_host').addClass('alert-danger').html('<strong>Oops!!!</strong> Something went wrong').show().delay(4000).fadeOut('fast');
				} else {
					$('#start_host').addClass('alert-warning').html('<strong>Sorry!!!</strong> You already send the Host Request to admin..').show().delay(4000).fadeOut('fast');
				}
			}
	    });
});

$(document).on('click','.popup_abs',function(){
	$('.popup_abs').hide();	
	$('.alert').hide();	
});

$(document).on('click','.alert',function(){
	$('.popup_abs').hide();	
});

$(document).on('click','.cancl',function(){
	var room_id 	= $(this).closest(".cancl").find(".room_id").val();
	var status 		= $(this).closest(".cancl").find(".status").val();
	var transaction_id = $(this).closest(".cancl").find(".transaction_id").val();
	var status 		= $(this).closest(".cancl").find(".status").val();
	//$(this).closest(".cancl").find("tr").hide();If you cancel this the amount will be refunded as per cancellation policy
	if(status == 0){
		var result = confirm("Want to remove from your history?");
		if (result) {
			$(this).parents("tr").hide();	
			$.ajax({
				url: base_url+'ordercancel',
				type: 'POST',
				data: {
					room_id : room_id,
					status : status
				},
				success: function(data){
					if(data == "Success"){
						$('#history_display').addClass('alert-success').html('<strong>'+data+'!!!</strong> Room has been removed from your history ').show().delay(5000).fadeOut('fast');
					}
				}
			});
		}
	} else{
		var result = confirm("Really want to cancel the booking?");
		if (result) {
			$(this).parents("tr").hide();
			$.ajax({
				url: base_url+'ordercancel',
				type: 'POST',
				data: {
					room_id : room_id,
					status : status,
					transaction_id : transaction_id
				},
				success: function(data){
					if(data == "Success"){
						$('#history_display').addClass('alert-success').html('<strong>'+data+'!!!</strong> Amount has been tranfered Successfully').show().delay(5000).fadeOut('fast');
					} else{
						$('#history_display').addClass('alert-danger').html('<strong>Oops!!!</strong> '+data+'').show().delay(5000).fadeOut('fast');
					}

				}
			});
		}
	}
	
});
$(document).on('focusout','#city_tab',function(){

	var result = $('#city_tab').val().split(',');
	if(jQuery.type(result[2]) === "undefined" || jQuery.type(result[1]) === "undefined" || jQuery.type(result[0]) === "undefined" || jQuery.type(result[2]) === "" || jQuery.type(result[1]) === "" || jQuery.type(result[0]) === "" || result[2] == "" || result[1] == "" || result[0] == "" || result[2] == " " || result[1] == " " || result[0] == " "){
		$('#city_err').show();
	}
	else{
		$('#city_err').hide();
	}

});

$(document).on('keyup','#city_tab',function(){

	var result = $('#city_tab').val().split(',');
	if(jQuery.type(result[2]) === "" || jQuery.type(result[1]) === "" || jQuery.type(result[0]) === "" || jQuery.type(result[2]) === "undefined" || jQuery.type(result[1]) === "undefined" || jQuery.type(result[0]) === "undefined" || result[2] == "" || result[1] == "" || result[0] == "" || result[2] == " " || result[1] == " " || result[0] == " "){
		$('#city_err').show();
	}
});
$(document).on('submit','#search_form',function(e){
	var result = $('#city_tab').val().split(',');
	if(jQuery.type(result[2]) === "undefined" || jQuery.type(result[1]) === "undefined" || jQuery.type(result[0]) === "undefined" || jQuery.type(result[2]) === "" || jQuery.type(result[1]) === "" || jQuery.type(result[0]) === "" || result[2] == "" || result[1] == "" || result[0] == "" || result[2] == " " || result[1] == " " || result[0] == " "){
		$('#city_err').show();
		e.preventDefault();
	}
});

$(document).on('submit','#Book_avail',function(e){

	var From_date = new Date($("#srt_ck").val());
	var To_date = new Date($("#ed_ck").val());
	var diff_date =  To_date - From_date;
	 
	/*var years = Math.floor(diff_date/31536000000);
	var months = Math.floor((diff_date % 31536000000)/2628000000);*/
	var days = Math.floor(((diff_date % 31536000000) % 2628000000)/86400000);
	if( days >= 0 )
	{
		/*$('#area').flash_message({
	        text: 'Success',
	        how: 'append'
	    });*/
	}
	if( days < 0 )
	{
		// alert(days);
		alert("End date must be greater than Start date");
		e.preventDefault();
	}
	
});

$(document).on('click','#strt',function(){
	$('#strt').hide();
	$('#strt1').show();
});
$(document).on('click','#ed',function(){
	$('#ed').hide();
	$('#ed1').show();
});

$(document).on('click','.availability',function()
{
	$.load();
}); 

    $("#end1").on("change",function(){
        var selected = $('#end1').val();
        $('#ed_ck').val(selected);
    });
    $("#start1").on("change",function(){
        var selected = $('#start1').val();
        $('#srt_ck').val(selected);
    });

$(document).on('click','.avail',function()
{
	function getUrlVars()
	{
	    var vars = [], hash;
	    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	    for(var i = 0; i < hashes.length; i++)
	    {
	        hash = hashes[i].split('=');
	        vars.push(hash[0]);
	        vars[hash[0]] = hash[1];
	    }
	    return vars;
	}
	var first = getUrlVars()["inout"];

});

/*$(document).on('click','.book_now',function()
{
	// alert($(this).data('amt'));
	var room_count 		= $(this).data('room');
	var member_count 	= $(this).data('member');
	var start 			= $(this).data('start');
	var end 			= $(this).data('end');
	var address 		= $(this).data('area');
	var hot_id 			= $(this).data('hotel');
	var min_amt 		= $(this).data('min');
	var max_amt 		= $(this).data('max');
	var inout 			= 1;
	window.open(base_url+'hotel/detailed/'+hot_id+'?rm_no='+room_count+'&mem_count='+member_count+'&check_in='+start+'&check_out='+end+'&search_city='+address+'&min_amt='+min_amt+'&max_amt='+max_amt+'&inout='+inout);
});
*/

$(document).on('click','.book_room',function()
{
	// alert($(this).data('amt'));
	var roomcount 		= $(this).data('roomcount');
	var member 			= $(this).data('member');
	var start 			= $(this).data('start');
	var end 			= $(this).data('end');
	var address 		= $(this).data('area');
	var room_id 		= $(this).data('room');
	var min_amt 		= $(this).data('min');
	var max_amt 		= $(this).data('max');
	var inout 			= 1;
	window.open(base_url+'roomsedit/detailedroom/'+room_id+'?mem_count='+member+'&check_in='+start+'&check_out='+end+'&search_city='+address+'&min_amt='+min_amt+'&max_amt='+max_amt+'&inout='+inout);
});


$(document).on('click','.blog_cls',function()
{
	// alert($(this).data('amt'));
	var id 		= $(this).data('id');
	
	window.open(base_url+'blogrev'+id);
});



$(document).on('click','.book_direct',function(e)
{
	// alert($(location).attr('href'));
	// e.preventDefault();
	var room_count 		= $(this).data('roomcount');
	var room_id 		= $(this).data('room');
	var member_count 	= $(this).data('member');
	var start 			= $(this).data('start');
	var end 			= $(this).data('end');
	var address 		= $(this).data('area');
	var hot_id 			= $(this).data('hotel');
	var min_amt 		= $(this).data('min');
	var max_amt 		= $(this).data('max');
	var inout 			= 0;
	window.open(base_url+'roomsedit/detailedroom/'+room_id+'?mem_count='+member_count+'&check_in='+start+'&check_out='+end+'&search_city='+address+'&min_amt='+min_amt+'&max_amt='+max_amt+'&inout='+inout);
	//window.open(base_url+'hotel/detailed/'+hot_id+'?rm_no='+room_count+'&mem_count='+member_count+'&search_city='+address+'&min_amt='+min_amt+'&max_amt='+max_amt);
});

(function($) {
    $.fn.flash_message = function(options) {
      
      options = $.extend({
        text: 'Done',
        time: 5000,
        how: 'before',
        class_name: ''
      }, options);
      
      return $(this).each(function() {
        if( $(this).parent().find('.flash_message').get(0) )
          return;
        
        var message = $('<span />', {
          'class': 'flash_message ' + options.class_name,
          text: options.text
        }).hide().fadeIn('fast');
        
        $(this)[options.how](message);
        
        message.delay(options.time).fadeOut('normal', function() {
          $(this).remove();
        });
        
      });
    };
})(jQuery);

function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

$(document).on('submit','#prfle_frm',function(e){
	$mail 		= $('.email').val();
	$first_name = $('.first_name').val();
	$last_name 	= $('.last_name').val();
	$username 	= $('#username').val();

	var sEmail = $('.email').val();
        
        if(!(validateEmail(sEmail))) {
           	$('#status_error_area').flash_message({
		        text: 'Profile has been not been saved successfully,Since you having following errors!',
		        how: 'append'
		    });
            e.preventDefault();
        }

	if($mail == ''){
		$('#empty_mail').text("Your Email Address can't be empty");
		$('#status').text("");
	}
	else{
		$('#empty_mail').text("");
	}
	if($username.length == 0){
		$('#uname').text("This field can't be empty");
	}
	else{
		$('#uname').text("");
	}
	$country = $('.country').val();
	if($country.length == 0){
		$('#error_msg_country').text("This field can't be empty");
	}
	else{
		$('#error_msg_country').text("");
	}
	$pin = $('.pin').val();
	if($pin.length == 0){
		$('#error_msg_pin').text("This field can't be empty");
	}
	else{
		$('#error_msg_pin').text("");
	}
	$state = $('.state').val();
	if($state.length == 0){
		$('#error_msg_state').text("This field can't be empty");
	}
	else{
		$('#error_msg_state').text("");
	}
	$city = $('.city').val();
	if($city.length == 0){
		$('#error_msg_city').text("This field can't be empty");
	}
	else{
		$('#error_msg_city').text("");
	}
	$address = $('.address').val();
	if($address.length == 0){
		$('#error_msg_address').text("This field can't be empty");
	}
	else{
		$('#error_msg_address').text("");
	}
	$phno = $('.phno').val();
	if($phno.length == 0){
		$('#error_msg_phno').text("Phone number field can't be empty");
	}

	var phno 		= $phno;
	var address 	= $address;
	var city 		= $city;
	var state 		= $state;
	var country 	= $country;
	var email 		= $mail;
	var pin 		= $pin;
	var last_name 	= $last_name;
	var first_name 	= $first_name;

	if($phno.length != 0 && $address.length != 0 && $city.length != 0 && $state.length != 0 && $pin.length != 0 && $country.length != 0 && email.length != 0 && $username != 0){
		$.ajax({
			/*url: 'userupdate',
		    type: 'POST',
		    data: {
		        phno : phno,
		        address : address,
		        city : city,
		        state : state,
		        pin : pin,
		        country : country,
		        email:email,
		        first_name : first_name,
		        last_name : last_name
		    },*/
		    success: function(data){
			    // location.reload(); // then reload the page.(3) 
			}
		});
		$('#status-area').flash_message({
	        text: 'Profile has been updated Successfully!',
	        how: 'append'
	    });
	}
	else{
		e.preventDefault();
	}
});

$(document).on('keypress','.phno',function (e) {
     //if the letter is not digit then display error and don't type anything
	if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
	}
});

$(document).on('keyup','.email',function(){
        var v = $('.email').val();
    var regex = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
    $('#status1').hide();
    $('#error').hide();
    $('#status').text(regex.test(v) ? "" : "Enter valid email Address");
});
$(document).on('focusout','.email',function(){
        var v = $('.email').val();
    var regex = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
    $('#status1').hide();
    $('#error').hide();
	$('#status').text(regex.test(v) ? "" : "Enter valid email Address");
});

$(".wishlist1 span.tooltip-content-flip .tooltip-back").append('Remove from wishlist');
$(".wishlist span.tooltip-content-flip .tooltip-back, .wish_log span.tooltip-content-flip .tooltip-back").append('Add to wishlist');
/*$('.wishlist_close').on('click', function(c){
	$(this).parent().parent().fadeOut('slow', function(c){
	});
});*/
$(document).on('click','.wishlist_close',function(e){
	e.preventDefault();
	var wish_id = $(this).closest(".wishlist_close").find(".wish_id").val();
	// alert(wish_id);
	$.ajax({
		url: 'removewish',
	    type: 'POST',
	    data: {
	        wish_id : wish_id
	          }/*,
	    success: function(data) {
	    	// alert('Hotel Removed from wishlist')
	    }*/
	});
	$(this).parent().parent().parent().parent().fadeOut('slow', function(c){
		});
    // $(this).closest('.booking-item-wishlist-title').remove();
});

$(document).on('click','.wishlist1',function(e){
	e.preventDefault();
	$(this).find('span.tooltip-content-flip .tooltip-back').text('').append('Add to wishlist');
	$(this).removeClass('wishlist1');
	$(this).addClass('wishlist');
	$('.btn_full_outline').html("<i class='icon-heart-empty'></i> Add to wishlist");
	var user_id = $(".user_id").val();
	// var room_id = $(this).closest(".wishlist").find(".room_id").val();
	var room_id		= $(this).find(".room_id").val();
	if(room_id == '' || room_id == undefined) {
		var room_id	= $(".room_id1").val();
	}

	$.ajax({
		url: base_url+'roomsedit/removewish',
	    type: 'POST',
	    // dataType:'json',
	    data: 
		{
			room_id : room_id,
			user_id : user_id
		},
	    success: function(data) {
	    	if(data == "success"){
		    	$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> Room Remove from wishlist</span>');
		    } else {
		    	$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> Already removed from wishlist</span>');
		    }
	    }
	});
});

$(document).on('click','.newVerificationreset',function(e)
{
	$('#newVerification')[0].reset();
});

$(document).on('click','.wishlist',function(e){
	e.preventDefault();
	var user_id = $('.user_id').val();
	
	if(user_id === "" || jQuery.type(user_id) === "undefined") {
		$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> You should be logged in</span>');
		$('#wish_add1').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> You should be logged in</span>');
	} else {
		var obj = $(this);
		$(this).find('span.tooltip-content-flip .tooltip-back').text('').append('Remove from wishlist');
		obj.removeClass('wishlist');
		obj.addClass('wishlist1');
		$('.btn_full_outline').html("<i class='icon-heart'></i> Remove from wishlist");
		var d = new Date();

		var month = d.getMonth()+1;
		var day = d.getDate();

		var created_at = d.getFullYear() + '/' +
		    ((''+month).length<2 ? '0' : '') + month + '/' +
		    ((''+day).length<2 ? '0' : '') + day;

		//var hotl_id = $(this).closest(".wishlist").find(".hotl_id").val();
		var room_id		= $(this).find(".room_id").val();

		if(room_id == '' || room_id == undefined) {
			var room_id	= $(".room_id1").val();
		}

		$.ajax({
			url: base_url+'roomsedit/wishlist',
		    type: 'POST',
		    // dataType:'json',
		    data: 
		    {
				room_id : room_id,
				user_id:user_id,
				created_at:created_at
	        },
		    success: function(data) {
		    	if(data == "success"){
		    		$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert alert-success"><strong>Success!</strong> Room Added to wishlist</span>');
		    	} if(data == "already_added") {
		    		$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> Wishlist Already Added</span>');
		    	}  if(data == "failed") {
		    		$('#wish_add').show().delay(3000).fadeOut('slow').html('<span class="alert-info alert"><strong>Info!</strong> You should Login First to add this room as wishlist</span>');
		    	}
		    }
		});
	}
});

$(".currency > li").click(function() {
    $("#currencytitle").text($(this).text()); 
    var currency = $(this).data('value');
});
$(".language > li").click(function() {
    $("#language_title").text($(this).text()); 
});

$(document).on('keyup','#n_email',function(){
    var v = $('#n_email').val();
var regex = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
$('#n_status').text(regex.test(v) ? "" : "Enter valid email Address");
});

$(document).on('focusout','#n_email',function(){
        var v = $('#n_email').val();
    var regex = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
	$('#n_status').text(regex.test(v) ? "" : "Enter valid email Address");
});

$(document).on('click','.but_sucss',function(e){

    e.preventDefault();
    var old_password   =  $(this).closest("#change_pass").find('.old_password').val();
    var new_password   =  $(this).closest("#change_pass").find('.new_password').val();

          $.ajax({
            url: base_url+'user/changepassword',
            type: 'POST',
            data: {
                old_password : old_password,
                new_password : new_password/*,
                ajx:'kik'*/
            },
            success: function(data) {
                if(data == 'Invalid'){
                	$('#error_area').flash_message({
				        text: 'Your Current password is either null or mismatch',
				        how: 'append'
				    });
                }
            else{
            		$('#pass_area').flash_message({
				        text: 'Password has been saved!',
				        how: 'append'
				    });
                }
            }
          });
});

$(document).on('click','.news_letter',function(e){
	e.preventDefault();
	var nEmail = $('#n_email').val();
        if(validateEmail(nEmail)){
        	// $('#n_status').text("Your Request has been send");
        	$.ajax({
		        url  : base_url+'roomsedit/newsletter',
		        type : 'POST',
		        data : {
		        	email_id : nEmail
		        },
				success: function(data) {
					// alert(data);
					if(data == "success"){
						$('#n_status').flash_message({
							text: 'Your Request has been sent',
							how: 'append'
						});
					} else {
						$('#n_status').flash_message({
							text: 'You have already sent the newletter Request!',
							how: 'append'
						});
					}
				}
		    });
        }
        if(!(validateEmail(nEmail))) {
           	$('#n_status').text("Enter valid email Address");
            e.preventDefault();
        }

	if(nEmail == ''){
		$('#n_status').text("Your Email Address can't be empty");
	}
});

	$(document).on('click','.review_btn',function(){
		var id = $('#reviewer_id').val();
		var alrd = $('#already_reviewed').val();
		/*if(id == '' || id == '/')
		{	
			$('#rev_emsg').addClass('alert-info').html('<strong>Sorry!</strong> You Should login to add a review!').show().delay(1500).fadeOut('fast');
			return false;
		}

		else */if(alrd != '/' && alrd != '')
		{
			$('#rev_emsg').addClass('alert-warning').html('<strong>Sorry!!!</strong> You can not review more than one time for a Room').show().delay(2000).fadeOut('fast');
			return false;
		}
		// $(this).closest(".text-right").find(".review_box").slideToggle();
	});

	$(".revw_desc").focusout(function(){
		var revw_desc		=	$(this).closest("#myForm").find(".revw_desc").val();

		if( revw_desc.length <= 0){
			$('.desc_error').fadeIn();
		}
		if(revw_desc.length > 1){
			$('.desc_error').fadeOut();
		}
	});

	$(".revw_tle").focusout(function(){
		var revw_tle		=	$(this).closest("#myForm").find(".revw_tle").val();

		if( revw_tle.length <= 0){
			$('.tle_error').fadeIn();
		}
		if(revw_tle.length > 1){
			$('.tle_error').fadeOut();
		}
	});

$(document).on('click','.review_submit',function(){

	// alert($(this).closest("#myForm").find(".overall .selected").length);
	var revw_tle		=	$(this).closest("#myForm").find('.revw_tle').val();
	var revw_desc		=	$(this).closest("#myForm").find('.revw_desc').val();
	var overall			=	$(this).closest("#myForm").find(".overall .selected").length;
	var sleep			=	$(this).closest("#myForm").find(".sleep .selected").length;
	var location		=	$(this).closest("#myForm").find(".location .selected").length;
	var service			=	$(this).closest("#myForm").find(".service .selected").length;
	var cleanliness		=	$(this).closest("#myForm").find(".cleanliness .selected").length;
	var rooms			=	$(this).closest("#myForm").find(".rooms .selected").length;
	var room_id			=	$(this).closest("#myForm").find(".room_id").val();
	var user_id			=	$(".user_id").val();
	var usr_img			=	$(".usr_img").val();
	var username		=	$(".username").val();

	if( revw_desc.length <= 0){
		$('.desc_error').fadeIn();
	}

	if(revw_desc.length > 0){
		$('.desc_error').fadeOut();
	}

	if(revw_tle.length <= 0){
		$('.tle_error').fadeIn();
	}

	if(revw_tle.length > 0 ){
		$('.tle_error').fadeOut();
	}
	if(overall >0){
		$('.over_all').fadeOut();
	}
	else{
		$('.over_all').fadeIn();
	}
	if(sleep >= 1 || location >= 1 || service >= 1 || cleanliness >= 1 || rooms >= 1){
		$('.ratng_error').fadeOut();
	}
	else{
		$('.ratng_error').fadeIn();
	}

	if(revw_tle.length > 0 && revw_desc.length > 0 ){
	if(sleep >= 1  || location >= 1 || service >= 1 || cleanliness >= 1 || rooms >= 1){
		if(overall > 0){

	$.ajax({
		url: base_url+'userreviews/review',
	    type: 'POST',
	    data: {
			revw_tle: revw_tle,
			revw_desc: revw_desc,
			overall:overall,
			sleep:sleep,
			location:location,
			service:service,
			cleanliness:cleanliness,
			rooms:rooms,
			room_id : room_id,
			user_id:user_id,
			usr_img:usr_img,
			username:username,
			ajx:'ajax'
				},
		success: function(data) {
			$("#myForm")[0].reset();
			$(".overall .selected").removeClass("selected");
			$(".sleep .selected").removeClass("selected");
			$(".location .selected").removeClass("selected");
			$(".service .selected").removeClass("selected");
			$(".rooms .selected").removeClass("selected");
			$(".cleanliness .selected").removeClass("selected");
			$('.review_list').html(data);
		}
	});
	if(user_id != ''){
		// message-review
		$('#message-review').addClass('alert-success').html('<strong>Success!</strong> Your review has been added Successfully!').show().delay(2000).fadeOut('fast');
		 window.location.reload();
	}
	/*else{
		$('#rev_emsg').addClass('alert-success').html('<strong>Sorry!</strong> You Should login to add a review!').show().delay(2000).fadeOut('fast');
	}*/
}
}
}
});
$(document).on('click','.next',function(){

	var pageNum 		=	(($(this).closest(".pagin").find(".active").val())+1);
	var room_id			=	$(".room_id").val();

		$.ajax({
			url: base_url+'roomsedit/reviews',
			type:'GET',
			data:{
				page:pageNum,
				room_id:room_id
			},
			success: function(data) {
				$('.review_view').html(data);
				$(".pagin li").removeClass('active');
				$('#'+pageNum).addClass('active');

			}
		});
});
$(document).on('click','.prev',function(){

	var pageNum			=	(($(this).closest(".pagin").find(".active").val())-1);
	var room_id			=	$(".room_id").val();

		$.ajax({
			url: base_url+'roomsedit/reviews',
			type:'GET',
			data:{
				page:pageNum,
				room_id:room_id
			},
			success: function(data) {
				$('.review_view').html(data);
				$(".pagin li").removeClass('active');
				$('#'+pageNum).addClass('active');

			}
		});
});
$(document).on('click','.last',function(){

	// var pageNum 		=	(($(this).closest(".pagin").find(".active").val())+1);
	var room_id			=	$(".room_id").val();
	var pageNum			=	$('.total_page').val();

		$.ajax({
			url: base_url+'roomsedit/reviews',
			type:'GET',
			data:{
				page:pageNum,
				room_id:room_id
			},
			success: function(data) {
				$('.review_view').html(data);
				$(".pagin li").removeClass('active');
				$('#'+pageNum).addClass('active');

			}
		});
});
$(document).on('click','.pagenum',function(e){

	var pageNum			= 	$(this).attr('id');
	var room_id			=	$(".room_id").val();

	$.ajax({
		url: base_url+'roomsedit/reviews',
		type:'GET',
		data:{
			page:pageNum,
			room_id:room_id
		},
		success: function(data) {
			$('.review_view').html(data);
			$(".pagin li").removeClass('active');
			$('#'+pageNum).addClass('active');

		}
	});
});
$(document).ready(function() {
$("div2").hide();
  $("#access_link").click(function() {
    $("#tab-forgot").hide();
    $("#div2").show();
  });
});
