// Update visual elements after the ajax backend change

function _checkRequiredFields() {
    _checkTextRequired();
    _checkNumberRequired();
    _checkDateTimeRequired();
    _checkSelectRequired();
}

function _checkTextRequired() {
    var elements = document.getElementsByClassName("text-required");
    var labels = document.getElementsByClassName("text-required-label");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].value == "") {
            labels[i].classList.add("text-danger");
            Ybq.alert("Error | Missing required fields", false, null, 3000);
            throw new Error("Stopping the function!");
        }
    }
}

function _checkNumberRequired() {
    var elements = document.getElementsByClassName("number-required");
    var labels = document.getElementsByClassName("number-required-label");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].value == "") {
            labels[i].classList.add("text-danger");
            Ybq.alert("Error | Missing required fields", false, null, 3000);
            throw new Error("Stopping the function!");
        }
    }
}

function _checkDateTimeRequired() {
    var elements = document.getElementsByClassName("datetime-required");
    var labels = document.getElementsByClassName("datetime-required-label");
    for (var i = 0; i < elements.length; i++) {
        if (!elements[i].value) {
            labels[i].classList.add("text-danger");
            Ybq.alert("Error | Missing required fields", false, null, 3000);
            throw new Error("Stopping the function!");
        }
    }
}

function _checkSelectRequired() {
    var elements = document.getElementsByClassName("select-required");
    var labels = document.getElementsByClassName("select-required-label");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].value == "") {
            labels[i].classList.add("text-danger");
            Ybq.alert("Error | Missing required fields", false, null, 3000);
            throw new Error("Stopping the function!");
        }
    }
}