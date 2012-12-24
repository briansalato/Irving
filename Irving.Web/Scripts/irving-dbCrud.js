/*! Irving.DbCrud*/
/// <reference path="jquery-1.6.2.min.js" />

var Irving = Irving || {};
Irving.DbCRUD = Irving.DbCRUD || {};

Irving.DbCRUD.showConfirmDelete = function () {
    $('#confirmDelete').slideDown();
};
Irving.DbCRUD.hideConfirmDelete = function () {
    $('#confirmDelete').slideUp();
};


Irving.AssetType = Irving.AssetType || {};
Irving.AssetType.addProperty = function () {
    var number = $('#numProperties').val();
    $('#propertiesHolder').append('<div><input type="text" name="Properties[' + number + ']" /><</div>');
};

$(function () {
    $(".date").datepicker();
});