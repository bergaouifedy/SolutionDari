function terms_changed1(termsCheckBox) {
    //If the checkbox has been checked
    if (termsCheckBox.checked) {
        //Set the disabled property to FALSE and enable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = false;
        document.getElementById("name").disabled = true;
        document.getElementById("type").disabled = true;
        document.getElementById("box1").checked = true;
        document.getElementById("box2").checked = false;
        document.getElementById("box3").checked = false;

    } else {
        //Otherwise, disable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = true;
        document.getElementById("name").disabled = true;
        document.getElementById("type").disabled = true;
        document.getElementById("box1").checked = false;
        document.getElementById("box2").checked = false;
        document.getElementById("box3").checked = false;

    }
}

function terms_changed2(termsCheckBox) {
    //If the checkbox has been checked
    if (termsCheckBox.checked) {
        //Set the disabled property to FALSE and enable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = true;
        document.getElementById("name").disabled = false;
        document.getElementById("type").disabled = true;
        document.getElementById("box1").checked = false;
        document.getElementById("box2").checked = true;
        document.getElementById("box3").checked = false;

    } else {
        //Otherwise, disable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = true;
        document.getElementById("name").disabled = true;
        document.getElementById("type").disabled = true;
        document.getElementById("box1").checked = false;
        document.getElementById("box2").checked = false;
        document.getElementById("box3").checked = false;

    }
}

function terms_changed3(termsCheckBox) {
    //If the checkbox has been checked
    if (termsCheckBox.checked) {
        //Set the disabled property to FALSE and enable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = true;
        document.getElementById("name").disabled = true;
        document.getElementById("type").disabled = false;
        document.getElementById("box1").checked = false;
        document.getElementById("box2").checked = false;
        document.getElementById("box3").checked = true;

    } else {
        //Otherwise, disable the text inputs.
        //   document.getElementById("minprice").disabled = true;
        //   document.getElementById("maxprice").disabled = true;
        document.getElementById("price").disabled = true;
        document.getElementById("name").disabled = true;
        document.getElementById("type").disabled = true;
        document.getElementById("box1").checked = false;
        document.getElementById("box2").checked = false;
        document.getElementById("box3").checked = false;

    }
}




function GetState() {
    var state = document.getElementById("state").value;
}