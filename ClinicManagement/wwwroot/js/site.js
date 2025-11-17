function hasPermission(required) {
    const perms = window.userPermissions || [];
    return perms.includes(required);
}

function applyPermissionVisibility() {
    const elems = document.querySelectorAll("[data-permission]");

    elems.forEach(el => {
        const required = el.getAttribute("data-permission");
        if (!hasPermission(required)) {
            el.style.display = "none";
        }
    });
}

document.addEventListener("DOMContentLoaded", applyPermissionVisibility);
