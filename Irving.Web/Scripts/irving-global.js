/*! Irving Global Functions*/
/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.9.0.js" />

$(function () {
    $('.modal').dialog({
        modal: true,
        autoOpen: false,
        width: 'auto'
    });

    $('.date').datepicker();
});