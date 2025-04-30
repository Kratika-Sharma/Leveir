// Admin Login Form Validation
const loginValidate = document.getElementById("adminLoginForm");
if (loginValidate) {
    loginValidate.addEventListener("submit", function (e) {
        let isValid = true;

        document.getElementById("emailError").classList.add("d-none");
        document.getElementById("passwordError").classList.add("d-none");

        const email = document.getElementById("emailaddress").value.trim();
        const password = document.getElementById("password").value.trim();

        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

        if (!email || !emailPattern.test(email)) {
            document.getElementById("emailError").classList.remove("d-none");
            isValid = false;
        }

        if (!password) {
            document.getElementById("passwordError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

// Add Jewelry Type Form Validation
const jewelryTypeValidator = document.getElementById("addJewelryForm");

if (jewelryTypeValidator) {
    jewelryTypeValidator.addEventListener("submit", function (e) {
        let isValid = true;

        document.getElementById("nameError").classList.add("d-none");
        document.getElementById("descriptionError").classList.add("d-none");
        document.getElementById("imageError").classList.add("d-none");

        const name = document.getElementById("JewelryName").value.trim();
        const description = document.getElementById("Description").value.trim();
        const image = document.getElementById("JewelryImg").files.length;
        const jewelryTypeId = document.querySelector('input[name="JewelryTypeId"]').value;
        const jewelryImgName = document.querySelector('input[name="JewelryImgName"]')?.value ?? "";

        if (!name) {
            document.getElementById("nameError").classList.remove("d-none");
            isValid = false;
        }

        if (!description) {
            document.getElementById("descriptionError").classList.remove("d-none");
            isValid = false;
        }
        if ((jewelryTypeId == "" || jewelryTypeId == "0") && image === 0) {
            document.getElementById("imageError").classList.remove("d-none");
            isValid = false;
        }
        else if (jewelryTypeId > 0 && image === 0 && jewelryImgName === "") {
            document.getElementById("imageError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

//Add Setting Style Validation
const settingStyleValidator = document.getElementById("addSettingStyleForm");

if (settingStyleValidator) {
    settingStyleValidator.addEventListener("submit", function (e) {
        let isValid = true;

        document.getElementById("jewelryTypeError").classList.add("d-none");
        document.getElementById("styleNameError").classList.add("d-none");
        document.getElementById("descriptionError").classList.add("d-none");
        document.getElementById("styleQuantityError").classList.add("d-none");
        document.getElementById("stylePriceError").classList.add("d-none");
        document.getElementById("styleImageError").classList.add("d-none");

        const jewelryType = document.getElementById("JewelryTypeId").value;
        const styleName = document.getElementById("StyleName").value.trim();
        const description = document.getElementById("Description").value.trim();
        const styleQuantity = document.getElementById("StyleQuantity").value.trim();
        const stylePrice = document.getElementById("StylePrice").value.trim();
        const imageFile = document.getElementById("StyleImg").files.length;
        const styleId = document.querySelector("input[name='SettingStyleId']").value || 0;
        const existingImage = document.querySelector("input[name='StyleImg']").value || "";

        if (jewelryType === "" || jewelryType === "0") {
            document.getElementById("jewelryTypeError").classList.remove("d-none");
            isValid = false;
        }

        if (!styleName) {
            document.getElementById("styleNameError").classList.remove("d-none");
            isValid = false;
        }

        if (!description) {
            document.getElementById("descriptionError").classList.remove("d-none");
            isValid = false;
        }

        if (!styleQuantity) {
            document.getElementById("styleQuantityError").classList.remove("d-none");
            isValid = false;
        }

        if (!stylePrice) {
            document.getElementById("stylePriceError").classList.remove("d-none");
            isValid = false;
        }

        if (styleId == 0 && imageFile === 0) {
            document.getElementById("styleImageError").classList.remove("d-none");
            isValid = false;
        } else if (styleId > 0 && imageFile === 0 && !existingImage) {
            document.getElementById("styleImageError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const jewelryTypeDropdown = document.getElementById("JewelryTypeId");
    if (jewelryTypeDropdown) {
        jewelryTypeDropdown.options[0].disabled = true;
    }
});

//Add Metal Validation
const AddMetalFormValidator = document.getElementById("AddMetalForm");

if (AddMetalFormValidator) {
    AddMetalFormValidator.addEventListener("submit", function (e) {
        let isValid = true;

        document.getElementById("jewelryTypeError").classList.add("d-none");
        document.getElementById("metalNameError").classList.add("d-none");
        document.getElementById("metalPriceError").classList.add("d-none");
        document.getElementById("metalQuantityError").classList.add("d-none");
        document.getElementById("metalCaratError").classList.add("d-none");
        document.getElementById("descriptionError").classList.add("d-none");

        const jewelryType = document.getElementById("JewelryTypeId").value;
        const MetalName = document.getElementById("MetalName").value.trim();
        const MetalPrice = document.getElementById("MetalPrice").value.trim();
        const MetalQuantity = document.getElementById("MetalQuantity").value.trim();
        const MetalCarat = document.getElementById("MetalCarat").value.trim();
        const Description = document.getElementById("Description").value.trim();
        const styleId = document.querySelector("input[name='SettingStyleId']").value || 0;
        const existingImage = document.querySelector("input[name='SettingStyle.StyleImg']").value || "";

        if (jewelryType === "" || jewelryType === "0") {
            document.getElementById("jewelryTypeError").classList.remove("d-none");
            isValid = false;
        }

        if (!MetalName) {
            document.getElementById("metalNameError").classList.remove("d-none");
            isValid = false;
        }

        if (!MetalPrice) {
            document.getElementById("metalPriceError").classList.remove("d-none");
            isValid = false;
        }

        if (!MetalQuantity) {
            document.getElementById("metalQuantityError").classList.remove("d-none");
            isValid = false;
        }

        if (!MetalCarat) {
            document.getElementById("metalCaratError").classList.remove("d-none");
            isValid = false;
        }

        if (!Description) {
            document.getElementById("descriptionError").classList.remove("d-none");
            isValid = false;
        }

        if (styleId == 0 && imageFile === 0) {
            document.getElementById("styleImageError").classList.remove("d-none");
            isValid = false;
        } else if (styleId > 0 && imageFile === 0 && !existingImage) {
            document.getElementById("styleImageError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const jewelryTypeDropdown = document.getElementById("JewelryTypeId");
    if (jewelryTypeDropdown) {
        jewelryTypeDropdown.options[0].disabled = true;
    }
});

