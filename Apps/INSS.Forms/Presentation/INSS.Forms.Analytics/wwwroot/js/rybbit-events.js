console.log("rybbit-events.js loaded and running.");
let rybbitCompletedEventSent = false;
let rybbitFormSubmissionSent = new Set();

function sendRybbitCompletedEvent() {
    if (rybbitCompletedEventSent)
        return true; // Prevent duplicate sends

    var formCompleted = document.getElementById("analytics-form-completed");
    if (formCompleted && window.rybbit) {
        var eventData = {};
        if (formCompleted.dataset.companyType) {
            eventData.companyType = formCompleted.dataset.companyType;
        }
        if (formCompleted.dataset.majorShareholder) {
            eventData.majorShareholder = formCompleted.dataset.majorShareholder;
            eventData.sharePercentage = formCompleted.dataset.sharePercentage;
        }
        if (Object.keys(eventData).length > 0) {
            window.rybbit.event("Form: Completed", eventData);
            console.log("Rybbit event sent:", eventData);
            rybbitCompletedEventSent = true;
            return true;
        }
    }
    return false;
}

function sendRybbitFormSubmissionEvent(form) {
    // Use form id or action as unique key
    const key = form.id || form.action;
    if (rybbitFormSubmissionSent.has(key))
        return; // Prevent duplicate sends per form

    if (window.rybbit) {
        window.rybbit.event("Form: Submission", {
            formId: form.id || form.action,
            env: "Test"
        });
        console.log("Form submission event sent for:", form.id || form.action);
        rybbitFormSubmissionSent.add(key);
    }
}

function waitForRybbitAndSendEvent() {
    if (sendRybbitCompletedEvent()) {
        // Event sent, stop polling
        return;
    }
    // Poll every 100ms until window.rybbit is available
    var interval = setInterval(function () {
        if (sendRybbitCompletedEvent()) {
            clearInterval(interval);
        }
    }, 100);
}

function setupFormSubmissionEvents() {
    document.querySelectorAll("form").forEach(form => {
        form.addEventListener("submit", function () {
            sendRybbitFormSubmissionEvent(form);
        });
    });
}

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", function () {
        waitForRybbitAndSendEvent();
        setupFormSubmissionEvents();
    });
}
else {
    waitForRybbitAndSendEvent();
    setupFormSubmissionEvents();
}

window.addEventListener("scroll", () => {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        // User reached the end of the page
        if (window.rybbit) {
            window.rybbit.event("scroll_end", { page: window.location.pathname });
        }
    }
});