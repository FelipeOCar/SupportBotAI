(function () {
    let recognition = null;

    function announce(message) {
        const region = document.getElementById("global-announcer");
        if (!region) return;
        region.textContent = "";
        window.setTimeout(() => region.textContent = message, 30);
    }

    function applySettings(settings) {
        const root = document.documentElement;
        const textSize = settings?.textSize || "standard";
        const highContrast = Boolean(settings?.highContrast);
        root.dataset.textSize = textSize;
        root.dataset.highContrast = highContrast ? "true" : "false";
    }

    window.supportBotSettings = {
        initialise: function () {
            try {
                const stored = localStorage.getItem("supportbot-accessibility");
                if (stored) {
                    applySettings(JSON.parse(stored));
                }
            } catch {
                applySettings(null);
            }
        },

        load: function () {
            try {
                const stored = localStorage.getItem("supportbot-accessibility");
                return stored ? JSON.parse(stored) : null;
            } catch {
                return null;
            }
        },

        save: function (settings) {
            localStorage.setItem("supportbot-accessibility", JSON.stringify(settings));
            applySettings(settings);
            announce("Einstellungen wurden gespeichert.");
        },

        announce: announce,

        focus: function (id) {
            window.setTimeout(() => document.getElementById(id)?.focus(), 30);
        },

        confirmDelete: function (title) {
            return window.confirm(`Möchtest du „${title}“ wirklich löschen? Diese Aktion kann nicht rückgängig gemacht werden.`);
        },

        copyText: async function (text) {
            await navigator.clipboard.writeText(text);
            announce("Chatverlauf wurde kopiert.");
        },

        startSpeechRecognition: function (dotNetReference) {
            const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
            if (!SpeechRecognition) return false;

            if (recognition) recognition.abort();
            recognition = new SpeechRecognition();
            recognition.lang = "de-CH";
            recognition.continuous = false;
            recognition.interimResults = true;

            recognition.onresult = function (event) {
                let transcript = "";
                let isFinal = false;
                for (let index = event.resultIndex; index < event.results.length; index++) {
                    transcript += event.results[index][0].transcript;
                    isFinal = event.results[index].isFinal;
                }
                dotNetReference.invokeMethodAsync("ReceiveSpeechResult", transcript, isFinal);
            };

            recognition.onerror = function (event) {
                dotNetReference.invokeMethodAsync("ReceiveSpeechError", event.error || "unknown");
            };

            recognition.onend = function () {
                dotNetReference.invokeMethodAsync("SpeechRecognitionEnded");
                recognition = null;
            };

            recognition.start();
            return true;
        },

        stopSpeechRecognition: function () {
            if (recognition) recognition.stop();
        },

        speak: function (text, rate) {
            if (!("speechSynthesis" in window)) return false;
            window.speechSynthesis.cancel();
            const utterance = new SpeechSynthesisUtterance(text);
            utterance.lang = "de-CH";
            utterance.rate = Number(rate || 1);
            window.speechSynthesis.speak(utterance);
            return true;
        }
    };
})();
