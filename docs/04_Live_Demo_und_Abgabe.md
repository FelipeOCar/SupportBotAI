# Live-Demo- und Abgabeplan

Diese Reihenfolge minimiert das Risiko vor der Abgabe. Alle Produkt-, Bestell- und Kundendaten in der Demo sind fiktiv.

## 1. Projekt einrichten und bauen

1. `SupportBotAI.sln` in Visual Studio 2022 öffnen.
2. Prüfen, dass **SupportBotAI** als Startprojekt gesetzt ist.
3. Rechtsklick auf das Projekt → **Manage User Secrets**.
4. Folgenden Inhalt eintragen und `DEIN_ECHTER_API_KEY` ersetzen:

   ```json
   {
     "Gemini": {
       "ApiKey": "DEIN_ECHTER_API_KEY"
     }
   }
   ```

5. **Build → Rebuild Solution** ausführen.
6. Mit dem HTTPS-Profil starten und die App in Google Chrome öffnen.

Der API-Key gehört nie in `appsettings.json`, Screenshots, Dokumentation oder GitHub.

## 2. Fünf-Minuten-Smoke-Test

- [ ] Übersicht zeigt zwei vorbereitete Konversationen.
- [ ] Neuer Chat lässt sich öffnen.
- [ ] Frage `Wo ist meine Bestellung TS-10482?` erzeugt eine echte, gestreamte Antwort.
- [ ] Jede KI-Antwort trägt das Textlabel **KI-GENERIERT**.
- [ ] Feedback kann gespeichert werden.
- [ ] Eskalation mit Name, Bestellnummer, Anliegen und Dringlichkeit erzeugt eine Ticketnummer.
- [ ] Browser-Reload erhält Chat, Feedback und Eskalation.
- [ ] Demo-Timeout zeigt Retry und **An Support übergeben**.
- [ ] Kontrastmodus und Textgrösse bleiben nach Reload erhalten.
- [ ] Spracheingabe funktioniert in Chrome nach erteiltem Mikrofonzugriff.

## 3. Live-Demo in der Präsentation

Vor der Präsentation Chrome, Visual Studio und die App öffnen. Keine unnötigen Tabs und keine sichtbaren Secrets.

1. **Übersicht:** sechs Seiten und vorhandene Verläufe kurz zeigen.
2. **Echte KI:** neuen Chat öffnen und `Wo ist meine Bestellung TS-10482?` senden.
3. **Streaming:** Ladeindikator, schrittweise Antwort und KI-Label zeigen.
4. **Feedback:** Antwort als hilfreich oder nicht hilfreich bewerten.
5. **Grenze:** nach `Wo ist meine Bestellung TS-99999?` fragen und erklären, dass die KI nicht raten darf.
6. **Eskalation:** an menschlichen Support übergeben; Pflichtfeldfehler und danach gültige Bestätigung zeigen.
7. **Kontrollierter Fehler:** in den Demo-Werkzeugen den einmaligen Timeout aktivieren, Frage senden und anschliessend Retry zeigen.
8. **Barrierefreiheit:** grosse Schrift, Kontrastmodus und eine kurze Spracheingabe demonstrieren.

### Demo-Notfallplan

- API-Key und Internet vor Beginn testen.
- Testfrage und Bestellnummer als Text zum Kopieren bereithalten.
- Bei echtem API-Ausfall den verständlichen Fehlerzustand erklären und **Erneut versuchen** zeigen; die Live-Demo darf aber nicht durch ein Video ersetzt werden.
- Den Demo-Timeout nur einmal aktivieren; der Schalter setzt sich danach automatisch zurück.

## 4. Zwei echte Peer-Tests

1. `docs/testing/02_Peer_Testbogen.md` zweimal kopieren oder ausdrucken.
2. Zwei Drittpersonen nacheinander die sechs Aufgaben selbstständig durchführen lassen.
3. Nur helfen, wenn die Person sonst abbricht; Hilfe und Beobachtung protokollieren.
4. Alle zehn SUS-Antworten und die drei Trust-Fragen erfassen.
5. Ergebnisse in `docs/testing/03_Auswertung.md` übertragen.
6. Mindestens drei tatsächlich beobachtete Probleme im Code verbessern.
7. Jede Änderung mit Befund, Datei und kurzer Nachprüfung dokumentieren.

Keine Testwerte, Zitate oder Optimierungen erfinden. Wenn du mir die beiden ausgefüllten Testbögen schickst, kann ich die SUS-Werte berechnen und die drei Änderungen gezielt einbauen.

## 5. Lighthouse, axe und Screenreader

1. App in Chrome öffnen und `F12` drücken.
2. Lighthouse → nur **Accessibility** → Desktop → Analyse starten.
3. Score, Datum und Chrome-Version in `docs/testing/05_WCAG_Checkliste.md` eintragen.
4. Ergebnis als Screenshot speichern.
5. Falls verfügbar zusätzlich axe DevTools ausführen und kritische Fehler eintragen.
6. Mit Windows Narrator oder NVDA den Skip-Link, die Navigation, eine neue KI-Antwort, einen Fehler und das Eskalationsformular prüfen.

## 6. Dokumentation und Folien finalisieren

- [ ] Name, Klasse, Datum und späteren GitHub-Link auf dem Titelblatt ersetzen.
- [ ] Screenshots der tatsächlich laufenden App in Kapitel 3 ergänzen.
- [ ] Peer-Test-, SUS- und Trust-Ergebnisse auf den Seiten 14–15 eintragen.
- [ ] Drei echte Optimierungen mit Vorher/Nachher-Nachweis ergänzen.
- [ ] Lighthouse-/axe-Resultat und Screenshot auf Seite 17 ergänzen.
- [ ] Den Begriff **Entwurf** aus finalem Dokument- und Präsentationsdateinamen entfernen.
- [ ] Folie 12 nach den Tests durch echte Ergebnisse und wichtigste Optimierungen ersetzen.
- [ ] Gesamtdokument erneut als PDF exportieren und kontrollieren, dass es 15–20 Seiten umfasst.
- [ ] Präsentation als PDF exportieren und auf einem zweiten Gerät öffnen.

## 7. GitHub-Abgabe

1. In Visual Studio **Git Changes** öffnen.
2. Prüfen, dass keine Datei `supportbot-data.json`, kein `secrets.json`, kein API-Key und keine `bin`-/`obj`-Ordner vorgemerkt sind.
3. Sinnvolle Commit-Nachricht verwenden, z. B. `SupportBot AI implementiert und dokumentiert`.
4. Repository **SupportBotAI** erstellen bzw. auswählen und pushen.
5. GitHub-Link in einem privaten Browserfenster öffnen und kontrollieren.
6. README, Solution, Quellcode, Mockups, Gesamtdokumentation und Präsentations-PDF müssen sichtbar sein.

## 8. Zeitkontrolle der Präsentation

| Teil | Richtzeit |
|---|---:|
| Ausgangslage, Personas, Anforderungen | 2:30 min |
| Entwurf und Variantenwahl | 2:30 min |
| Architektur und kritische Komponente | 2:30 min |
| Live-Demo | 5:00 min |
| Usability/Trust und Optimierungen | 2:00 min |
| Barrierefreiheit und Reflexion | 2:30 min |
| **Total** | **17:00 min** |

Einmal mit Stoppuhr proben. Zielbereich: 15–20 Minuten; die Fragerunde kommt danach.

