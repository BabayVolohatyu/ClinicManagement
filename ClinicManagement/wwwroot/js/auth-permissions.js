function parseJwt(token) {
    try {
        const payload = token.split('.')[1];
        return JSON.parse(atob(payload));
    } catch {
        return null;
    }
}

function hasPermission(token, permission) {
    const payload = parseJwt(token);
    if (!payload || !payload.permission) return false;

    const perms = Array.isArray(payload.permission) ? payload.permission : [payload.permission];
    return perms.includes(permission);
}

document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem('jwt');
    if (!token) return;

    document.querySelectorAll("[data-permission]").forEach(el => {
        const perm = el.getAttribute("data-permission");
        if (!hasPermission(token, perm)) {
            el.style.display = "none";
        }
    });
});

