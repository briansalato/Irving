/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.9.0.js" />

var Irving = Irving || {};
Irving.Asset = Irving.Asset || {};

Irving.Asset.showAddNote = function () {
    $('#addNoteDialog').dialog('open');
};

Irving.Asset.hideAddNote = function () {
    $('#addNoteDialog').dialog('close');
};

Irving.Asset.addNote = function () {
    $.ajax({
        url: '/Asset/AddNote/',
        type: 'POST',
        data: {
            title: $('#addNoteDialog #Title').val(),
            text: $('#addNoteDialog #Text').val(),
            assetId: $('#Asset_Id').val()
        },
        success: function (html) {
            $('#noteList').append(html);
        }
    });
    $('#addNoteDialog').dialog('close');
};