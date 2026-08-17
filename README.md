# SupportBot AI

Blazor-Web-App für das Modul 322: ein transparenter und barrierearmer KI-Kundenservice der fiktiven TechShop AG. Die App verwendet **Gemini 2.5 Flash**, streamt Antworten, speichert Daten lokal als JSON und bietet einen klaren Übergang zu menschlichem Support.

## Funktionsumfang

- Übersicht und filterbare Konversationshistorie
- echter KI-Chat mit sichtbarem Lade-/Streaming-Zustand
- textliche Kennzeichnung jeder KI-Antwort
- verständliche Fehler-, Abbruch-, Retry- und Timeout-Zustände
- kontrollierter Timeout-Schalter für die Live-Demo
- Eskalationsformular mit Validierung und Chat-Snapshot
- Inline-Feedback nach jeder KI-Antwort
- JSON-Persistenz für Chats, Feedback und Eskalationen
- Spracheingabe über die Web Speech API sowie Sprachausgabe
- Textgrösse, Kontrastmodus, Tastaturfokus und `aria-live`
- 15 importierbare Balsamiq-Boards mit zwei Chatvarianten und Zustandsflows

## Voraussetzungen

- Visual Studio 2022 mit Workload **ASP.NET und Webentwicklung**
- .NET 8 SDK
- Google-Chrome für die Spracheingabe-Demo
- kostenloser Gemini-API-Key aus [Google AI Studio](https://aistudio.google.com/app/apikey)

## Einrichten

1. `SupportBotAI.sln` in Visual Studio öffnen.
2. Im Solution Explorer Rechtsklick auf das Projekt **SupportBotAI** → **Manage User Secrets**.
3. Den Inhalt von `secrets.json` so setzen:

   ```json
   {
     "Gemini": {
       "ApiKey": "DEIN_ECHTER_API_KEY"
     }
   }
   ```

4. Speichern. Der Schlüssel gehört **nie** in `appsettings.json`, Screenshots oder Git.
5. **Build → Rebuild Solution** ausführen.
6. Mit dem grünen HTTPS-Startknopf starten und die lokale Adresse in Chrome öffnen.

Alternativ in einem Terminal im Projektordner:

```powershell
dotnet user-secrets set "Gemini:ApiKey" "DEIN_ECHTER_API_KEY"
dotnet restore
dotnet run
```

## Empfohlene erste Prüfung

1. Auf **Neuer Chat** klicken.
2. `Wo ist meine Bestellung TS-10482?` senden.
3. Prüfen, ob die Antwort sichtbar gestreamt und als **KI-GENERIERT** bezeichnet wird.
4. Ein Feedback speichern.
5. Den Chat über **An Menschen übergeben** eskalieren.
6. Unter **Demo-Werkzeuge** den Timeout aktivieren und **Erneut versuchen** testen.
7. Unter **Einstellungen** Textgrösse, Kontrast und Sprache aktivieren.

Der vollständige Ablauf für Setup, Peer-Tests, Audit, Live-Demo und GitHub steht in [`docs/04_Live_Demo_und_Abgabe.md`](docs/04_Live_Demo_und_Abgabe.md).

## Fiktive Testdaten

| Bestellnummer | Inhalt | Zustand |
|---|---|---|
| TS-10482 | NovaBook Air 14, VisionDock 4K | versendet, Lieferung morgen 08:00–12:00 |
| TS-10273 | SoundPulse Pro | zugestellt am 12.08.2026, Retoure bis 11.09.2026 |
| TS-10511 | PixelView 27Q | Zahlung wird geprüft, noch kein Liefertermin |

Die Produkt-, Bestell- und Firmendaten sind vollständig fiktiv und nur für die Modulprüfung bestimmt.

## Projektstruktur

```text
Components/Pages/       sechs geforderte Screens plus Übersichtsseiten
Models/                 Chat-, Feedback- und Eskalationsmodelle
Services/               Gemini-Streaming, TechShop-Kontext, JSON-Speicher
wwwroot/                 Styles, App-Icon, Speech-/Accessibility-JavaScript
docs/mockups/            importierbares Balsamiq-Projekt
docs/                    Analyse, Entwurf, Test- und Techniknachweise
App_Data/                lokale Laufzeitdaten; JSON-Datei wird nicht committed
```

## Datenschutz und Grenzen

Nutzereingaben werden für die Antwort an die externe Gemini API übermittelt. Deshalb fordert die Oberfläche ausdrücklich dazu auf, keine Passwörter oder vollständigen Zahlungsdaten einzugeben. Das Modell darf nur den fiktiven TechShop-Kontext verwenden und muss bei fehlendem Wissen Unsicherheit nennen und eskalieren. Eine produktive App bräuchte zusätzlich eine Datenschutzprüfung, Aufbewahrungsregeln, Zugriffskontrolle und serverseitige Schutzmechanismen gegen Missbrauch.

## Abgabe-Check

- [ ] Name und Datum in der Dokumentation ersetzt
- [ ] App in Visual Studio erfolgreich kompiliert
- [ ] echte Gemini-Antwort getestet
- [ ] zwei echte Peer-Tests durchgeführt und SUS-Scores eingetragen
- [ ] Lighthouse-/axe-Audit in Chrome durchgeführt und Screenshot ergänzt
- [ ] Screenshots der laufenden App ergänzt
- [ ] Präsentation in 15–20 Minuten geprobt
- [ ] Repository zu GitHub gepusht und Link geprüft

## Quellen

- [Google Gen AI .NET SDK](https://googleapis.github.io/dotnet-genai/)
- [Gemini-Modelle](https://ai.google.dev/gemini-api/docs/models)
- [Gemini API – Preise und kostenloses Kontingent](https://ai.google.dev/gemini-api/docs/pricing)
- [WCAG 2.1](https://www.w3.org/TR/WCAG21/)
- [System Usability Scale](https://www.usability.gov/how-to-and-tools/methods/system-usability-scale.html)
