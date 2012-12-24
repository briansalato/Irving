/*! Irving.Asset */
/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.9.0.js" />

var Irving = Irving || {};
Irving.Asset = Irving.Asset || {};

$(function () {
    $('#addNoteDialog').dialog({
        buttons: {
            "Add": Irving.Asset.addNote,
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

Irving.Asset.showAddNote = function () {
    $('#addNoteDialog input, #addNoteDialog textarea').val('');
    $('#addNoteDialog').dialog('open');
};

Irving.Asset.addNote = function () {
    $.ajax({
        url: '/Asset/AddNote/',
        type: 'POST',
        data: {
            title: $('#addNoteDialog #Title').val(),
            text: $('#addNoteDialog #Text').val(),
            date: $('#addNoteDialog #Date').val(),
            assetId: $('#Id').val()
        },
        success: function (html) {
            $('#noteList').append(html);
        }
    });
    $('#addNoteDialog').dialog('close');
};