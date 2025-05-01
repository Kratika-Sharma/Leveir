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

// Add Setting Style Validation
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

// Add Metal Validation
const AddMetalFormValidator = document.getElementById("AddMetalForm");

if (AddMetalFormValidator) {
   AddMetalFormValidator.addEventListener("submit", function (e) {
        let isValid = true;

      document.getElementById("jewelryTypeError").classList.add("d-none");
       document.getElementById("SettingStyleIdError").classList.add("d-none");
       document.getElementById("metalNameError").classList.add("d-none");
       document.getElementById("metalPriceError").classList.add("d-none");
       document.getElementById("metalQuantityError").classList.add("d-none");
       document.getElementById("metalCaratError").classList.add("d-none");
        document.getElementById("descriptionError").classList.add("d-none");

        const jewelryType = document.getElementById("JewelryTypeId").value;
       const SettingStyleId = document.getElementById("SettingStyleId").value;
       const metalName = document.getElementById("MetalName").value.trim();
       const metalPrice = document.getElementById("MetalPrice").value.trim();
       const metalQuantity = document.getElementById("MetalQuantity").value.trim();
       const metalCarat = document.getElementById("MetalCarat").value.trim();
       const description = document.getElementById("Description").value.trim();

        if (jewelryType === "" || jewelryType === "0") {
            document.getElementById("jewelryTypeError").classList.remove("d-none");
            isValid = false;
       }
       if (SettingStyleId === "" || SettingStyleId === "0") {
           document.getElementById("SettingStyleIdError").classList.remove("d-none");
           isValid = false;
       }

       if (!metalName) {
            document.getElementById("metalNameError").classList.remove("d-none");
            isValid = false;
        }
       if (!metalPrice) {
           document.getElementById("metalPriceError").classList.remove("d-none");
           isValid = false;
       }
       if (!metalQuantity) {
           document.getElementById("metalQuantityError").classList.remove("d-none");
           isValid = false;
       }
       if (!metalCarat) {
           document.getElementById("metalCaratError").classList.remove("d-none");
           isValid = false;
       }
        if (!description) {
            document.getElementById("descriptionError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

// Add Diamond Type Validation 
const diamondTypeForm = document.getElementById("addDiamondTypeForm");

if (diamondTypeForm) {
    diamondTypeForm.addEventListener("submit", function (e) {
        let isValid = true;
        const diamondTypeInput = document.getElementById("DiamondTypeName");
        const diamondTypeError = document.getElementById("diamondTypeError");

        diamondTypeError.classList.add("d-none");

        if (!diamondTypeInput.value.trim()) {
            diamondTypeError.classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });
}

// Add/Edit Diamond Form Validation
const diamondForm = document.getElementById("diamondForm");
if (diamondForm) {
    diamondForm.addEventListener("submit", function (e) {
        let isValid = true;

        // Reset error messages
        document.querySelectorAll('#diamondForm .text-danger').forEach(el => el.classList.add('d-none'));

        // Validate Diamond Type
        const diamondTypeId = document.getElementById("DiamondTypeId");
        if (!diamondTypeId.value) {
            document.getElementById("DiamondTypeIdError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Shape
        const shape = document.getElementById("Shape");
        if (!shape.value.trim()) {
            document.getElementById("ShapeError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Color
        const color = document.getElementById("Color");
        if (!color.value.trim()) {
            document.getElementById("ColorError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Clarity
        const clarity = document.getElementById("Clarity");
        if (!clarity.value.trim()) {
            document.getElementById("ClarityError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Carat
        const carat = document.getElementById("Carat");
        if (!carat.value.trim()) {
            document.getElementById("CaratError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Cut
        const cut = document.getElementById("Cut");
        if (!cut.value.trim()) {
            document.getElementById("CutError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Image (only for new entries or when updating and no existing image)
        const diamondImg = document.getElementById("DiamondImg");
        const diamondImgName = document.querySelector('input[name="DiamondImgName"]').value;
        const isUpdate = document.querySelector('input[name="DiamondId"]').value > 0;

        if (!isUpdate && diamondImg.files.length === 0) {
            // New entry requires an image
            document.getElementById("DiamondImgError").classList.remove("d-none");
            isValid = false;
        } else if (isUpdate && diamondImg.files.length === 0 && !diamondImgName) {
            // Update with no existing image and no new image selected
            document.getElementById("DiamondImgError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Price
        const price = document.getElementById("Price");
        if (!price.value || isNaN(price.value) || parseFloat(price.value) <= 0) {
            document.getElementById("PriceError").classList.remove("d-none");
            isValid = false;
        }

        // Validate Stock Quantity
        const stockQuantity = document.getElementById("StockQuantity");
        if (!stockQuantity.value || isNaN(stockQuantity.value) || parseInt(stockQuantity.value) < 0) {
            document.getElementById("StockQuantityError").classList.remove("d-none");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
        }
    });

    // Add event listeners to clear error messages when user starts typing/selecting
    diamondForm.querySelectorAll('input, select').forEach(element => {
        element.addEventListener('input', function () {
            const errorElement = document.getElementById(this.id + 'Error');
            if (errorElement) {
                errorElement.classList.add('d-none');
            }
        });
    });
}

// Common DOM Ready Handler
document.addEventListener("DOMContentLoaded", function () {
    const jewelryTypeDropdown = document.getElementById("JewelryTypeId");
    if (jewelryTypeDropdown) {
        jewelryTypeDropdown.options[0].disabled = true;
    }
});