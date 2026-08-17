# HZ3 – Implementierung und kritische Komponente

## 1. Technologiewahl

Umgesetzt wurde eine **Blazor Web App mit .NET 8 und Interactive Server Rendering**. Diese Wahl passt besser als .NET MAUI zum Auftrag:

- plattformübergreifender Zugriff ohne Installation
- semantisches HTML, `aria-live` und Browser-Screenreader direkt nutzbar
- Web Speech API über kleine JavaScript-Interop-Schicht
- Streaming lässt sich mit `IAsyncEnumerable`, SignalR und `StateHasChanged()` unmittelbar darstellen
- einfache Live-Demo in Chrome
- zentrale, geschützte Ablage des API-Schlüssels auf dem Server

Der Nachteil ist die dauerhafte Serververbindung. Für die lokale Modul-Demo ist das vertretbar. Bei einer produktiven Lösung müssten Skalierung, Authentifizierung und Datenschutz zusätzlich behandelt werden.

## 2. Architektur

| Ebene | Verantwortung | Beispiele |
|---|---|---|
| Razor UI | Darstellung, Interaktion, Barrierefreiheit | `Chat.razor`, `Escalation.razor`, `Settings.razor` |
| Models | serialisierbarer Zustand und Validierungsregeln | `Conversation`, `FeedbackEntry`, `EscalationRequest` |
| Services | KI-Zugriff, Persistenz, TechShop-Kontext | `GeminiChatService`, `JsonAppDataStore`, `TechShopContext` |
| JavaScript-Interop | Browserfunktionen | Speech Recognition, Speech Synthesis, Fokus, localStorage |
| Konfiguration | Modell, Timeout, Outputlimit, Secret | `appsettings.json`, Visual Studio User Secrets |

## 3. Echte KI-Anbindung

Die App verwendet das offizielle NuGet-Paket `Google.GenAI` und das Modell `gemini-2.5-flash`. Der API-Key wird aus User Secrets oder der Umgebungsvariable `GEMINI_API_KEY` gelesen und nie im Repository gespeichert. Der System-Prompt enthält ausschliesslich fiktive Produkte, Bestellungen und Richtlinien der TechShop AG.

Die Modellregeln verlangen kurze deutsche Antworten, verbieten erfundene Fakten und fordern bei fehlendem Kontext eine ehrliche Einschränkung mit Eskalation. Das reduziert Halluzinationsrisiken, ersetzt aber keine fachliche Prüfung.

## 4. Kritische Komponente: Streaming und Zustandsverwaltung

### Fragestellung

Kann eine variable, potenziell langsame LLM-Antwort so dargestellt werden, dass der Chat konsistent bleibt, der Nutzer jederzeit Feedback erhält und Timeout, Abbruch oder API-Ausfall keine Nachricht verlieren?

### Lösung

1. Nutzertext wird vor dem API-Aufruf als Chatnachricht gespeichert.
2. Eine KI-Nachricht mit `IsStreaming=true` wird angelegt.
3. `GeminiChatService.StreamReplyAsync()` liefert Textteile als `IAsyncEnumerable<string>`.
4. Jeder Teil wird an dieselbe Nachrichteninstanz angehängt; `StateHasChanged()` aktualisiert die UI.
5. Ein verknüpftes `CancellationTokenSource` unterscheidet Nutzerabbruch und Zeitüberschreitung.
6. `WaitAsync(timeoutToken)` erzwingt das UI-Timeout auch dann, wenn ein SDK den Token nicht bis zum HTTP-Aufruf durchreicht.
7. Im Fehlerfall erhält dieselbe KI-Nachricht einen Fehlercode und eine verständliche Meldung.
8. Im `finally`-Block werden Busy-State, Ressourcen und Persistenz in jedem Fall abgeschlossen.

### Zustandsinvarianten

- Es existiert höchstens eine aktive KI-Generierung pro Chatkomponente.
- Die Nutzerfrage wird vor Beginn der Generierung persistent gespeichert.
- Nach Erfolg, Fehler oder Abbruch ist `IsStreaming=false`.
- Der Einmal-Demo-Schalter wird nach jedem Versuch zurückgesetzt.
- Fehlertexte enthalten keine Secrets oder internen Stacktraces.
- Retry entfernt nur die letzte fehlgeschlagene KI-Nachricht, nicht die Nutzerfrage.

### Prüffälle

| ID | Ausgangslage | Aktion | Erwartung |
|---|---|---|---|
| ST-01 | gültiger Key, Netzwerk verfügbar | Testfrage senden | Ladeindikator, Teiltexte, fertige KI-Antwort, Speicherung |
| ST-02 | Demo-Timeout aktiviert | Testfrage senden | Timeoutmeldung, Frage bleibt, Retry und Eskalation sichtbar |
| ST-03 | kein API-Key | Testfrage senden | Konfigurationsmeldung ohne Absturz oder Schlüsselleck |
| ST-04 | Antwort läuft | **Antwort abbrechen** | verständliche Abbruchmeldung; UI wieder bedienbar |
| ST-05 | fehlgeschlagene Antwort | **Erneut versuchen** | Fehlerkarte verschwindet; genau ein neuer Versuch startet |
| ST-06 | Browser neu laden | Verlauf öffnen | bisher gespeicherte Nachrichten aus JSON vorhanden |

### Ergebnis der Machbarkeitsprüfung

Der Aufbau ist technisch geeignet, weil Darstellung und persistenter Domänenzustand getrennt bleiben. Der kontrollierte Timeout macht den schwierigen Fehlerfall reproduzierbar. Vor der Abgabe müssen ST-01 bis ST-06 einmal in Visual Studio protokolliert werden; besonders ST-01 benötigt den persönlichen API-Key und kann nicht sinnvoll durch statischen Code ersetzt werden.

## 5. Fehlerbehandlung

| Fehlerklasse | Nutzerfeedback | Wiederherstellung |
|---|---|---|
| fehlender Key | Key über User Secrets einrichten | Konfiguration ergänzen, Retry |
| Timeout | KI antwortete nicht rechtzeitig; Nachricht bleibt | Retry oder menschlicher Support |
| Nutzerabbruch | Antwort wurde abgebrochen | erneut senden oder weiterchatten |
| API/Netzwerk | Dienst momentan nicht erreichbar | Retry oder Eskalation |
| ungültiges Formular | feldnahe Meldung und Summary | Eingabe direkt korrigieren |
| beschädigte JSON-Datei | Datei wird gesichert, Seed-Daten werden neu angelegt | Backup mit Zeitstempel bleibt lokal |

## 6. Persistenz

`JsonAppDataStore` serialisiert Chats, Feedback und Eskalationen in `App_Data/supportbot-data.json`. Ein `SemaphoreSlim` verhindert konkurrierende Schreibzugriffe. Geschrieben wird zuerst in eine temporäre Datei und anschliessend per Move ersetzt. Laufzeitdaten und Secrets sind durch `.gitignore` vom Repository ausgeschlossen.

## 7. Fiktiver Datenkontext

Die TechShop-Daten sind bewusst klein und prüfbar: fünf Produkte, drei Bestellungen und vier Richtlinien. So kann im Live-Chat gezeigt werden, ob das Modell vorhandene Informationen korrekt nutzt und bei unbekannten Bestellnummern seine Grenze nennt.

## 8. Quellen

- [Google Gen AI .NET SDK](https://googleapis.github.io/dotnet-genai/)
- [Gemini API – Modelle](https://ai.google.dev/gemini-api/docs/models)
- [Gemini API – Preise](https://ai.google.dev/gemini-api/docs/pricing)
- [ASP.NET Core Blazor](https://learn.microsoft.com/aspnet/core/blazor/)

