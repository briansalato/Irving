/// <reference path="jquery-1.6.2.min.js" />

var Irving = Irving || {};
Irving.CollectionManager = Irving.CollectionManager || {};

Irving.CollectionManager.addItem = function (collectionId) {
    var collection = $('#' + collectionId);
    var lastRow = collection.children().last();

    var htmlToBuild = '<div>';
    var elements = lastRow.children();
    var firstItemName = elements.first().attr('name');
    var startIndex = Irving.CollectionManager.startIndex(firstItemName);
    var number = Irving.CollectionManager.number(firstItemName);

    $.each(elements, function () {
        var endIndex = elements.first().attr('name');
        var postFix = Irving.CollectionManager.postfix(name);
    });

    htmlToBuild += '</div>';
};

Irving.CollectionManager.startIndex = function (name) {
    return name.indexOf('[');
}

Irving.CollectionManager.endIndex = function(name) {
    return name.indexOf(']');
}

Irving.CollectionManager.number = function(name) {
    var startIndex = name.indexOf('[') + 1;
    var endIndex = name.indexOf(']');
    return name.substring(startIndex, endIndex);
}