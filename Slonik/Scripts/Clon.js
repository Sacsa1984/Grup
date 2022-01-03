Cl.addEventListener("click", function () {
    let parent = document.getElementById('divClon');
    let elem3 = parent.querySelector('#divAdd');
    let clone3 = elem3.cloneNode(true);
    parent.appendChild(clone3);
})