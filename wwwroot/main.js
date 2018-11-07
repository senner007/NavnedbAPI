// https://stackoverflow.com/questions/10344498/best-way-to-iterate-over-an-array-without-blocking-the-ui/10344560#10344560

const processLargeArray = (function () {

    var clearSetTimeout = undefined;
    var ul = document.querySelector('ul');
    var chunk = 500;

    function doChunk(array, index, textFormat) {
        var cnt = chunk;
        var fragment = document.createDocumentFragment();
        while (cnt-- && index < array.length) {

            if (array[index].navn.startsWith(textFormat)) {
                var el = document.createElement('li');
                el.classList.add('list-group-item');
                el.innerText = array[index].navn + " køn: " + array[index].køn;
                fragment.appendChild(el);
            }
            ++index;
        }

        ul.appendChild(fragment);

        if (index < array.length) {
            clearSetTimeout = setTimeout(() => doChunk(array, index, textFormat), 1);
        }
    }

    return {
        reset: function () {
            if (clearSetTimeout) clearTimeout(clearSetTimeout);
            ul.innerHTML = "";
        },
        process: function (array, input) {
            this.reset();
            var textFormat = input.value != "" ? input.value[0].toUpperCase() + input.value.toLowerCase().substring(1, input.value.length) : "";
            // TODO : Check for hyphen - Improve me!
            if (textFormat.indexOf('-') != -1 && textFormat[textFormat.indexOf('-') + 1]) {
                textFormat = textFormat.substring(0, textFormat.indexOf('-') + 1) +
                    textFormat.charAt(textFormat.indexOf('-') + 1).toUpperCase() +
                    textFormat.substring(textFormat.indexOf('-') + 2, textFormat.length).toLowerCase();
            }
            doChunk(array, 0, textFormat);
        }
    }

}());


;(function setHandlers() {

    var cachedArray = undefined;

    var textInput = document.querySelector("#textId");

    function processStart(arr) {
        cachedArray = arr;
        processLargeArray.process(arr, textInput);
    } 

    document.querySelector("#textId").addEventListener('keyup', function (e) {

        if (textInput.value == "") {
            processLargeArray.reset();
            return;
        }

        if (cachedArray && cachedArray.length > 0 && cachedArray[0].navn[0].toLowerCase() == textInput.value[0].toLowerCase()) {
            console.log('from cached');
            processStart(cachedArray);
        } else if (!callApi.isPending) {
            console.log('from fetch');
            callApi.call(textInput).then(arr => processStart(arr));
        }

    });

    document.querySelector("#inputSex").addEventListener('change', function (e) {
        
        callApi.call(textInput).then(arr => processStart(arr));

    });


}());

const callApi = (function () {
    return {
        call: async function callApi(textInput) {
            this.isPending = true;
            const response = await fetch('api/navne?startsWith=' + textInput.value[0] + (checkSex() ? '&sex=' + checkSex() : ""));
            const toJson = await response.json();
            this.isPending = false;
            return toJson;
        },
        isPending: false
    }
}());


const checkSex = (function IIFE() {
    // close over inputSex var
    const inputSex = document.querySelector('#inputSex');
    return () => inputSex[inputSex.selectedIndex].dataset.sex;
}());