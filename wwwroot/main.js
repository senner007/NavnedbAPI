
// https://stackoverflow.com/questions/10344498/best-way-to-iterate-over-an-array-without-blocking-the-ui/10344560#10344560
function processLargeArray(array, input) {
    reset();

    var textFormat = input.value != "" ? input.value[0].toUpperCase() + input.value.toLowerCase().substring(1, input.value.length) : "";
        // TODO : Check for hyphen - Improve me!
    if (textFormat.indexOf('-') != -1 && textFormat[textFormat.indexOf('-') +1]) {
        textFormat = textFormat.substring(0, textFormat.indexOf('-') + 1) + 
        textFormat.charAt(textFormat.indexOf('-') +1).toUpperCase() +
        textFormat.substring(textFormat.indexOf('-') +2, textFormat.length).toLowerCase();
    }

    // set this to whatever number of items you can process at once
    var chunk = 500; 
    var index = 0;

    function doChunk() {
        var cnt = chunk;
        // TODO : Improve me!
        var fragment = document.createDocumentFragment();
        while (cnt-- && index < array.length) {
            // console.log(cnt)
            if (array[index].navn.startsWith(textFormat)) {
                var el = document.createElement('li');
                el.classList.add('list-group-item'); 
                el.innerText = array[index].navn + " køn: " + array[index].køn;
                fragment.appendChild(el);
            }
            ++index;
        }
        document.querySelector('ul').appendChild(fragment);
        
        if (index < array.length) {
            GLOBAL.clearSetTimeout = setTimeout(doChunk, 1);
        }
    }
    doChunk();
}

var GLOBAL = {
    clearSetTimeout : false,
    waitForPromise: false
}


function reset () {
    if (GLOBAL.clearSetTimeout) clearTimeout(GLOBAL.clearSetTimeout);
    document.querySelector('ul').innerHTML = "";
}

;(function setHandlers () {

    var cachedArray = undefined;

    var textInput = document.querySelector("#textId");

    document.querySelector("#textId").addEventListener('keyup', function (e) {
        
        if (textInput.value == "") {
            reset();
            return;
        }

        if (cachedArray && cachedArray.length > 0 && cachedArray[0].navn[0].toLowerCase() == textInput.value[0].toLowerCase()) {
            console.log('from cached');
            processLargeArray(cachedArray, textInput);
        } else if(!GLOBAL.waitForPromise) {
            console.log('from fetch');
            calltheApi();
        }
    
    });

    document.querySelector("#inputSex").addEventListener('change', function (e) {
    
        cachedArray = undefined; 
        if (textId.value == "") {
            reset();
            return;
        }

        calltheApi();
    });

    function calltheApi () {
        callApi(textInput).then(arr => {
            cachedArray = arr; 
            processLargeArray(arr, textId)
        });
    }

}());


// waitTrigger will remain true when Promise pending

async function callApi (textInput) {
    GLOBAL.waitForPromise = true;
    const response = await fetch('api/navne?startsWith=' + textInput.value[0] + (checkSex() ? '&sex=' + checkSex(): "")); 
    const toJson = await response.json();
    GLOBAL.waitForPromise = false;
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


