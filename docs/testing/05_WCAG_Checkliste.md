# HZ5 – WCAG-2.1- und Accessibility-Checkliste

Statuslegende: **implementiert** = im Quellcode nachweisbar; **manuell prüfen** = muss in der laufenden App bestätigt werden.

| WCAG / Thema | Status | Umsetzung / Nachweis | manuelles Resultat |
|---|---|---|---|
| 1.1.1 Nicht-Text-Inhalt | implementiert | dekorative Zeichen `aria-hidden`; Funktionstasten mit `aria-label` | [ ] |
| 1.3.1 Info und Beziehungen | implementiert | Überschriften, `nav`, `main`, `aside`, `fieldset`, Labels | [ ] |
| 1.3.2 sinnvolle Reihenfolge | implementiert | DOM-Reihenfolge entspricht Navigation → Inhalt → Nebenaktionen | [ ] |
| 1.4.1 Farbe nicht allein | implementiert | KI-, Status-, Erfolg- und Fehlerzustände immer mit Text | [ ] |
| 1.4.3 Kontrast Minimum | manuell prüfen | definierte dunkle Schrift auf hellen Flächen; High-Contrast-Modus | [Kontrastwerte/Audit] |
| 1.4.4 Textgrösse | implementiert | lokale Stufen klein/standard/gross; responsives Layout | [Zoom 200 %] |
| 1.4.10 Reflow | manuell prüfen | Breakpoints bei 1220/900/640 px | [320 CSS px] |
| 2.1.1 Tastatur | implementiert | native Controls, Links, Buttons; Strg+Enter optional | [vollständiger Rundgang] |
| 2.1.2 keine Tastaturfalle | manuell prüfen | keine modalen Eigenkomponenten | [ ] |
| 2.4.1 Blöcke überspringen | implementiert | Skip-Link zu `#main-content` | [ ] |
| 2.4.3 Fokusreihenfolge | manuell prüfen | logische DOM-Reihenfolge, Fokus auf neue Antwort | [ ] |
| 2.4.6 Überschriften/Labels | implementiert | beschreibende Titel, Feldlabels, Hilfetexte | [ ] |
| 2.4.7 Fokus sichtbar | implementiert | globaler `:focus-visible`-Rahmen, im Kontrastmodus verstärkt | [ ] |
| 3.2.3 konsistente Navigation | implementiert | persistente Hauptnavigation und konsistente Aktionsstile | [ ] |
| 3.3.1 Fehlerkennzeichnung | implementiert | ValidationSummary, ValidationMessage, `role=alert` | [ ] |
| 3.3.2 Labels/Anweisungen | implementiert | Pflichtstern, Format `TS-12345`, Zeichenlimits | [ ] |
| 3.3.3 Fehlerkorrektur | implementiert | konkrete Korrekturtexte, Retry und Eskalation | [ ] |
| 4.1.2 Name/Rolle/Wert | implementiert | native Inputs, `role=switch/log/status/alert`, `aria-pressed` | [axe prüfen] |
| 4.1.3 Statusmeldungen | implementiert | `aria-live`, `role=status`, `aria-busy`, globaler Announcer | [Screenreader prüfen] |
| alternative Eingabe | implementiert | Web Speech API mit `de-CH`, Fehlerhinweis bei fehlendem Support | [Chrome + Mikrofon] |
| alternative Ausgabe | implementiert | optionale Speech Synthesis mit regelbarer Geschwindigkeit | [Chrome] |

## Automatisierter Audit

1. App in Chrome öffnen.
2. `F12` → **Lighthouse**.
3. Nur **Accessibility** aktivieren, Modus Desktop, Analyse starten.
4. Zusätzlich axe DevTools ausführen, falls verfügbar.
5. Screenshot und Resultate hier bzw. im Gesamtdokument ergänzen.

- Lighthouse Accessibility Score: `[__/100]`
- axe kritische Fehler: `[__]`
- Datum / Browser-Version: `[eintragen]`
- behobene Audit-Funde: `[eintragen]`
- bewusst offene Punkte und Begründung: `[eintragen]`

## Screenreader-Kurztest

Mit Windows Narrator oder NVDA und ausgeschaltetem Bildschirm/abgewandtem Blick:

- [ ] Skip-Link wird zuerst angeboten.
- [ ] Navigation und Seitenüberschrift sind verständlich.
- [ ] Eingabe und Mikrofontaste haben eindeutige Namen.
- [ ] „KI antwortet“ wird angekündigt.
- [ ] neue KI-Antwort wird angekündigt und als KI-generiert bezeichnet.
- [ ] Fehler und Bestätigungen werden ohne Fokussuche wahrgenommen.
- [ ] Eskalationsformular kann vollständig abgeschickt werden.

