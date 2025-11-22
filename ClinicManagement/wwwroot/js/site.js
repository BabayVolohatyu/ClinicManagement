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
    if (!pageInfo || !actions) return;

    const entity = pageInfo.dataset.entity;
    if (!entity || entity.trim() === "") {
        actions.style.display = "none";
        return;
    }

    const entityButtons = actions.querySelectorAll("[data-entity-action]");

    entityButtons.forEach(btn => {
        const perm = btn.getAttribute("data-permission");

        if (!hasPermission(perm)) {
            btn.style.display = "none";
            return;
        }

        if (btn.tagName.toLowerCase() === "a") {
            const routes = {
                create: "Create",
                download_csv: "DownloadCsv",
                execute_raw_queries: "QueryBuilder",
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

    const anyVisible = Array.from(entityButtons).some(b => b.style.display !== "none");
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

