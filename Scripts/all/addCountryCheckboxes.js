// create checkbox for each country


countrySelectionDiv = document.getElementById("countrySelectionDiv"); 

for (var i = 0; i < countriesList.length; i++) { //countriesList variable declared in main html file
    console.log(countriesList[i]);

    var labelValue = document.createElement('label');
    labelValue.innerHTML = countriesList[i];
    var inputValue = document.createElement('input');
    inputValue.className = "countryCheckInput";
    inputValue.type = "checkbox";
    inputValue.name = countriesList[i];

    var inputDiv = document.createElement('div');
    inputDiv.style = "display:inline-block; ";
    inputDiv.appendChild(inputValue);
    inputDiv.appendChild(labelValue);
    countrySelectionDiv.appendChild(inputDiv);

}



