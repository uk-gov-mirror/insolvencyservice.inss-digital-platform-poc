// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", function () {
    var formCompleted = document.getElementById("analytics-form-completed");
    if (formCompleted) {
        if (formCompleted.dataset.companyType) {
            plausible("Form: Completed", { props: { companyType: formCompleted.dataset.companyType } });
        }
        if (formCompleted.dataset.majorShareholder) {
            plausible("Form: Completed", { props: { majorShareholder: formCompleted.dataset.majorShareholder } });
            plausible("Form: Completed", { props: { sharePercentage: formCompleted.dataset.sharePercentage } });
        }
    }
});

