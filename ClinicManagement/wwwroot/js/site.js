function hasPermission(required) {
    const perms = window.userPermissions || [];
    return perms.includes(required);
}

document.addEventListener("DOMContentLoaded", function () {
    applyPermissionVisibility();
    applyEntityActions();
    applyClickableRows();
});

function applyPermissionVisibility() {
    document.querySelectorAll("[data-permission]").forEach(el => {
        const required = el.getAttribute("data-permission");
        const hasPerm = hasPermission(required);
        el.style.display = hasPerm ? "" : "none";
    });
}



function applyEntityActions() {
    const pageInfo = document.getElementById("page-info");
    const actions = document.getElementById("entity-actions");
    if (!actions) return;

    const entity = pageInfo?.dataset.entity;
    const hasEntity = entity && entity.trim() !== "";

    const entityButtons = actions.querySelectorAll("[data-entity-action]");
    
    if (hasEntity) {
        entityButtons.forEach(btn => {
            const perm = btn.getAttribute("data-permission");

            if (!hasPermission(perm)) {
                btn.style.display = "none";
                return;
            }

            if (btn.tagName.toLowerCase() === "a") {
                if (btn.hasAttribute("href") && btn.getAttribute("href") !== "") {
                    btn.style.display = "inline-flex";
                    return;
                }

                const routes = {
                    create: "Create",
                    download_csv: "DownloadCsv",
                    view_promotions_list: "Promotions",
                    ask_promotion: "AskPromotion"
                };

                const action = routes[perm];
                if (action) {
                    btn.setAttribute("href", `/${entity}/${action}`);
                    btn.style.display = "inline-flex";
                } else {
                    btn.style.display = "none";
                }
            } else {
                btn.style.display = "inline-flex";
            }
        });
    } else {
        entityButtons.forEach(btn => {
            btn.style.display = "none";
        });
    }

    const allButtons = actions.querySelectorAll("a[data-permission], button[data-permission]");
    allButtons.forEach(btn => {
        if (!btn.hasAttribute("data-entity-action")) {
            const perm = btn.getAttribute("data-permission");
            if (hasPermission(perm)) {
                if (btn.tagName.toLowerCase() === "a") {
                    const route = btn.getAttribute("data-route");
                    if (route && !btn.hasAttribute("href")) {
                        btn.setAttribute("href", route);
                    }
                }
                btn.style.display = "inline-flex";
            } else {
                btn.style.display = "none";
            }
        }
    });

    const anyVisible = Array.from(allButtons).some(b => {
        const display = b.style.display;
        return display !== "none" && display !== "";
    });
    actions.style.display = anyVisible ? "flex" : "none";
}

function applyClickableRows() {
    document.querySelectorAll(".clickable-row").forEach(row => {
        row.addEventListener("click", () => {
            const url = row.dataset.href;
            if (url) window.location.href = url;
        });
    });
}

