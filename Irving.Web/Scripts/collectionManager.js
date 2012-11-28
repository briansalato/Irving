/// <reference path="jquery-1.6.2.min.js" />

var Irving = Irving || {};
Irving.CollectionManager = Irving.CollectionManager || {};

Irving.CollectionManager.addItem = function (collectionId) {
    var collection = $('#' + collectionId);
    var lastRow = collection.children().last();
    if (lastRow.is(":hidden")) {
        lastRow.show();
    } else {
        var newRow = lastRow.clone(true);
        collection.append(newRow);
        var elements = newRow.children();
        var number = Irving.CollectionManager.number(elements.first().attr('name')) + 1;
        $.each(elements, function () {
            var tagName = this.tagName.toLowerCase();
            if (tagName == 'input' || tagName == 'select') {
                this.name = Irving.CollectionManager.namePrefix(this.name) +
                            number +
                            Irving.CollectionManager.namePostfix(this.name);

                this.id = Irving.CollectionManager.idPrefix(this.id) +
                         number +
                         Irving.CollectionManager.idPostfix(this.id);

                $(this).val(this.name.indexOf('.Id') > -1 ? '0' : '');
            }
            if (this.classList.contains('date')) {
                //recreate the datepicker so it isnt attached to the other input
                $(this).datepicker('destroy').datepicker();
            }
        });
    }
};

Irving.CollectionManager.deleteItem = function (deleteButton) {
    var rowToRemove = $(deleteButton).parent();
    var rowRemovedNumber = Irving.CollectionManager.number(rowToRemove.children().first().attr('name'));
    var collection = rowToRemove.parent();
    rowToRemove.remove();
    var rows = collection.children();
    for (var i = 0; i < rows.length; i++) {
        if (i >= rowRemovedNumber) {
            $.each($(rows[i]).children(), function () {
                var tagName = this.tagName.toLowerCase();
                if (tagName == 'input' || tagName == 'select') {
                    var newNumber = Irving.CollectionManager.number(this.name) - 1;
                    this.name = Irving.CollectionManager.namePrefix(this.name) +
                                newNumber +
                                Irving.CollectionManager.namePostfix(this.name);

                    this.id = Irving.CollectionManager.idPrefix(this.id) +
                              newNumber +
                              Irving.CollectionManager.idPostfix(this.id);
                }
            });
        }
    }

}

Irving.CollectionManager.idPrefix = function (name) {
    return name.substring(0, Irving.CollectionManager.idStartIndex(name));
}

Irving.CollectionManager.idPostfix = function (name) {
    return name.substring(Irving.CollectionManager.idEndIndex(name));
}

Irving.CollectionManager.idStartIndex = function (name) {
    return name.indexOf('_') + 1;
}

Irving.CollectionManager.idEndIndex = function (name) {
    return name.indexOf('__');
}

Irving.CollectionManager.namePrefix = function (name) {
    return name.substring(0, Irving.CollectionManager.nameStartIndex(name));
}

Irving.CollectionManager.namePostfix = function (name) {
    return name.substring(Irving.CollectionManager.nameEndIndex(name));
}

Irving.CollectionManager.nameStartIndex = function (name) {
    return name.indexOf('[') + 1;
}

Irving.CollectionManager.nameEndIndex = function (name) {
    return name.indexOf(']');
}

Irving.CollectionManager.number = function(name) {
    return parseInt(name.substring(Irving.CollectionManager.nameStartIndex(name), Irving.CollectionManager.nameEndIndex(name)));
}