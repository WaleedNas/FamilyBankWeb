var INDEX = 0;
function onLoad(obj) {

    $("#chat-circle").click(function () {
        $("#chat-circle").toggle('scale');
        $(".chat-box").toggle('scale');
        obj.invokeMethodAsync("OnOpenChatbox");
    })

    $(".chat-box-toggle").click(function () {
        $("#chat-circle").toggle('scale');
        $(".chat-box").toggle('scale');
    })
}

function onSubmit(obj) {
    var msg = $("#chat-input").val();
    if (msg.trim() == '') {
        return false;
    }
    generate_message(msg, 'self');
    obj.invokeMethodAsync("SendToChatbot", msg);
}

function chatbotReply(msg) {
    generate_message(msg, 'user');
}

function generate_message(msg, type) {
    INDEX++;
    var str = "";
    str += "<div id='cm-msg-" + INDEX + "' class=\"chat-msg " + type + "\">";
    str += "          <div class=\"cm-msg-text\">";
    str += msg;
    str += "          <\/div>";
    str += "        <\/div>";
    $(".chat-logs").append(str);
    $("#cm-msg-" + INDEX).hide().fadeIn(300);
    if (type == 'self') {
        $("#chat-input").val('');
    }
    $(".chat-logs").stop().animate({ scrollTop: $(".chat-logs")[0].scrollHeight }, 1000);
}

//function generate_button_message(msg, buttons) {
//    /* Buttons should be object array
//        [
//        {
//            name: 'Existing User',
//            value: 'existing'
//        },
//        {
//            name: 'New User',
//            value: 'new'
//        }
//        ]
//    */
//    INDEX++;
//    var btn_obj = buttons.map(function (button) {
//        return "              <li class=\"button\"><a href=\"javascript:;\" class=\"btn btn-primary chat-btn\" chat-value=\"" + button.value + "\">" + button.name + "<\/a><\/li>";
//    }).join('');
//    var str = "";
//    str += "<div id='cm-msg-" + INDEX + "' class=\"chat-msg user\">";
//    str += "          <div class=\"cm-msg-text\">";
//    str += msg;
//    str += "          <\/div>";
//    str += "          <div class=\"cm-msg-button\">";
//    str += "            <ul>";
//    str += btn_obj;
//    str += "            <\/ul>";
//    str += "          <\/div>";
//    str += "        <\/div>";
//    $(".chat-logs").append(str);
//    $("#cm-msg-" + INDEX).hide().fadeIn(300);
//    $(".chat-logs").stop().animate({ scrollTop: $(".chat-logs")[0].scrollHeight }, 1000);
//    $("#chat-input").attr("disabled", true);
//}

//$(document).delegate(".chat-btn", "click", function () {
//    var value = $(this).attr("chat-value");
//    var name = $(this).html();
//    $("#chat-input").attr("disabled", false);
//    generate_message(name, 'self');
//})