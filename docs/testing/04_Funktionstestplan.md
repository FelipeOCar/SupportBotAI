# Funktionstestplan

**Umgebung:** Windows, Visual Studio 2022, .NET 8, Chrome, gültiger Gemini-Key  
**Tester/in:** [eintragen] · **Datum:** [eintragen]

| ID | Bereich | Schritte | Soll-Ergebnis | Ist / Status |
|---|---|---|---|---|
| FT-01 | Start | App starten | Übersicht ohne Fehler, zwei Seed-Chats sichtbar | [ ] |
| FT-02 | Chat | neue Frage senden | Nutzertext gespeichert, Ladezustand, echte gestreamte Antwort | [ ] |
| FT-03 | Leere Eingabe | ohne Text senden | Senden deaktiviert | [ ] |
| FT-04 | Verlauf | Browser neu laden | neue Konversation bleibt erhalten | [ ] |
| FT-05 | Retry | Timeout simulieren, Retry | verständlicher Fehler, danach neuer API-Versuch | [ ] |
| FT-06 | Abbruch | laufende Antwort abbrechen | UI reagiert, kein Absturz, Frage bleibt | [ ] |
| FT-07 | Konfiguration | Key temporär entfernen | verständlicher Konfigurationsfehler, kein Secret sichtbar | [ ] |
| FT-08 | Feedback | Ja/Nein und Kommentar speichern | Bestätigung erscheint | [ ] |
| FT-09 | Eskalation ungültig | Formular leer absenden | Pflichtfeldfehler erscheinen | [ ] |
| FT-10 | Bestellformat | `123` eingeben | Formatmeldung `TS-12345` | [ ] |
| FT-11 | Eskalation gültig | gültige Daten absenden | Ticketnummer, Status eskaliert, Snapshot gespeichert | [ ] |
| FT-12 | Suche/Filter | Verlauf suchen und filtern | passende Karten, Empty-State bei 0 Treffern | [ ] |
| FT-13 | Löschen | Verlauf löschen, abbrechen/bestätigen | Abbruch erhält Chat; Bestätigung löscht ihn | [ ] |
| FT-14 | Einstellungen | Textgrösse/Kontrast speichern | Darstellung ändert sich und bleibt nach Reload | [ ] |
| FT-15 | Spracheingabe | Mikrofon in Chrome starten | Transkript erscheint im Eingabefeld | [ ] |
| FT-16 | Sprachausgabe | Testausgabe starten | deutscher Beispielsatz wird vorgelesen | [ ] |
| FT-17 | Tastatur | nur Tab/Shift+Tab/Enter nutzen | alle Funktionen logisch erreichbar, Fokus sichtbar | [ ] |
| FT-18 | Responsive | Fenster auf ca. 390 px verkleinern | Kernchat einspaltig, keine horizontale Pflichtnavigation | [ ] |

