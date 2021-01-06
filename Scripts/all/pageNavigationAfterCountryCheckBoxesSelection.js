// page navigation logic
//document.getElementById("continueButton").value = "Close Curtain"; document.getElementById("continueButton").innerHTML  = "Close Curtain";
$('#continueButton').click(function () {
    console.log("inside continueButton onclick method");
    document.getElementById("continueButton").value = "Fetching data...";

    var checkboxes = document.getElementsByClassName('countryCheckInput');;
    if (checkboxes.length == 0) {
        alert("Please select the countries first!");
    }
    else {
        countryAppender = ""; // variable declared in main html file
        var itr = 0;
        for (i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                if (itr == 0) {
                    countryAppender = "?";
                }
                else {
                    countryAppender = countryAppender + "&";
                }
                countryAppender = countryAppender + "countries[" + itr.toString() + "]=" + checkboxes[i].name;
                itr = itr + 1;
                console.log(checkboxes[i].name);
            }
        }
        navigate(); // call navigate on main html file
        
    }

});

if (window.performance && window.performance.navigation.type === window.performance.navigation.TYPE_BACK_FORWARD) {
    console.log("came back using back button");
    document.getElementById("continueButton").value = "Continue";
}