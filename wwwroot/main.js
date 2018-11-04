
// https://stackoverflow.com/questions/10344498/best-way-to-iterate-over-an-array-without-blocking-the-ui/10344560#10344560


function processLargeArray(array) {
    reset();
    // set this to whatever number of items you can process at once
    var chunk = 500; // adjust the chunk size;
    var index = 0;

    function doChunk() {
        var cnt = chunk;
        var textIdFormat = textId.value != "" ? textId.value[0].toUpperCase() + textId.value.toLowerCase().substring(1, textId.value.length) : "";
        var fragment = document.createDocumentFragment();
        while (cnt-- && index < array.length) {
            // process array[index] here
            
            if (array[index].navn.startsWith(textIdFormat)) {
                var el = document.createElement('li');
                el.classList.add('list-group-item'); 
                el.innerText = array[index].navn + " køn: " + array[index].køn;
                fragment.appendChild(el);
            }
            ++index;
        }

        document.querySelector('ul').appendChild(fragment);
        if (index < array.length) {
            clearVar = setTimeout(doChunk, 1);
        }
    }
    doChunk();
}

var clearVar;
function reset () {
    if (clearVar) clearTimeout(clearVar);
    document.querySelector('ul').innerHTML = "";
}


var cachedArray = undefined;

const textId = document.querySelector("#textId");

document.querySelector("#textId").addEventListener('keyup', function (e) {
    
    if (textId.value == "") {
        reset();
        return;
    }

    if (cachedArray && cachedArray.length > 0 && cachedArray[0].navn[0].toLowerCase() == textId.value[0].toLowerCase()) {
        console.log('from cached')
        processLargeArray(cachedArray);
    } else if (!waitTrigger){
        console.log('from fetch')
        callApi().then(t => processLargeArray(t));
    }
  
})

document.querySelector("#inputSex").addEventListener('change', function (e) {
   
    cachedArray = undefined; 
    if (textId.value == "") {
        reset();
        return;
    }

    callApi().then(t => processLargeArray(t));
})


// waitTrigger will remain true when Promise pending
var waitTrigger;

async function callApi () {
    waitTrigger = true;
    const response = await fetch('api/navne?startsWith=' + textId.value[0] + (checkSex() ? '&sex=' + checkSex(): "")); 
    const toJson = await response.json();
    cachedArray = toJson; 
    waitTrigger = false;
    return toJson;
}

const checkSex = (function IIFE () {
    // close over inputSex var
    const inputSex = document.querySelector('#inputSex');
    return function  () {
        var selectedSex;
        switch(inputSex.selectedIndex) {
            case 0:
                selectedSex = "";
            break;
            case 1:
                selectedSex = "m";
            break;
            case 2:
                selectedSex = "k";
            break;
            case 3:
                selectedSex = "mk";
            break;
        }
        return selectedSex;
    }
}());


