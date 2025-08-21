document.addEventListener("DOMContentLoaded", function () {
    const editBtn = document.getElementById("editBtn");
    const saveBtn = document.getElementById("saveBtn");
    const inputs = document.querySelectorAll(".editable");

    editBtn.addEventListener("click", function () {
        inputs.forEach(input => {
            input.removeAttribute("readonly");
        });
        saveBtn.removeAttribute("disabled");
        editBtn.setAttribute("disabled", "true");
    });
});
