/// <reference path="jquery-1.8.2.min.js" />

var Irving = Irving || {};
Irving.Routing = Irving.Routing || {};

Irving.Routing.LoadPage = function (url) {
    $.ajax({
        url: url,
        success: function (newHtml) {
            $('#ContentHolder').html(newHtml);
        }
    });
};