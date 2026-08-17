# HZ1 – Nutzungsanalyse

## 1. Ausgangslage und Methode

Die fiktive TechShop AG verkauft Elektronik über einen Online-Shop. Viele Supportanfragen betreffen wiederkehrende Themen: Bestellstatus, Retouren und Produktkompatibilität. Der SupportBot AI soll diese Fragen schnell beantworten, ohne vorzutäuschen, ein Mensch zu sein. Bei Unsicherheit oder Unzufriedenheit muss jederzeit eine Übergabe an einen Mitarbeitenden möglich sein.

Für die Analyse wurden drei komplementäre Personas, typische Nutzungssituationen, Aufgaben und Risiken betrachtet. Die Anforderungen wurden anschliessend als überprüfbare User Stories formuliert und mit **Must**, **Should** oder **Could** priorisiert.

## 2. Nutzungsumfeld

| Dimension | Beobachtung | Konsequenz für die UI |
|---|---|---|
| Ort | zuhause, unterwegs, Arbeitsplatz | responsive Oberfläche, kurze verständliche Texte |
| Geräte | Notebook, Desktop, Tablet, Smartphone | Browserlösung, flexible Spalten, grosse Interaktionsflächen |
| Situation | oft parallel zur Suche nach Bestellung oder Rechnung | Bestellnummern erklären, Chatverlauf erhalten, Unterbrechungen tolerieren |
| Zeitdruck | verspätete Lieferung oder Rückgabefrist | Status sichtbar, schnelle Eskalation, keine Sackgassen |
| Erfahrung | von digital versiert bis wenig geübt | klare Begriffe, Schnellfragen, konsistente Navigation |
| KI-Vertrauen | reicht von neugierig bis skeptisch | KI immer textlich kennzeichnen, Unsicherheit und Grenzen offenlegen |
| Einschränkungen | Screenreader, eingeschränkte Sicht oder Motorik | Tastaturbedienung, `aria-live`, Kontrast, Textgrösse, Spracheingabe |
| Datenschutz | Chattext wird an einen externen LLM-Dienst gesendet | Warnung vor Passwörtern/Zahlungsdaten, Datenminimierung, keine versteckte Übermittlung |

## 3. Personas

### Persona A – Lea Meier, KI-skeptische Power-Userin

- **Alter / Rolle:** 34, Projektleiterin
- **Geräte:** Notebook und Smartphone, sehr technikerfahren
- **Situation:** Ihre Bestellung ist verspätet. Sie will rasch eine belastbare Information und hinterfragt automatisch erzeugte Aussagen.
- **Ziele:** Herkunft der Antwort erkennen, konkrete Bestelldaten erhalten, falsche Aussagen melden, bei Unsicherheit direkt zu einem Menschen wechseln.
- **Frustrationen:** Bots, die menschlich wirken wollen; unklare Quellen; absolute Aussagen trotz Unsicherheit; versteckte Eskalation.
- **Zitat:** „Ich nutze KI gern – solange klar ist, was sie weiss und was nicht.“
- **Abgeleitete Bedürfnisse:** sichtbare KI-Kennzeichnung, Kontextangaben, Feedback pro Antwort, kontrollierbarer Retry/Abbruch, dauerhafte Eskalationsoption.

### Persona B – Peter Keller, wenig geübter Nutzer

- **Alter / Rolle:** 61, Verkauf
- **Geräte:** älterer Laptop, gelegentlich Tablet
- **Situation:** Möchte Kopfhörer retournieren und kennt Chatbots kaum.
- **Ziele:** ohne Anleitung verstehen, wo er schreibt, einfache Antwort erhalten, keinen bisherigen Text verlieren.
- **Frustrationen:** Fachbegriffe, zu viele gleichwertige Aktionen, Icons ohne Text, unklare Fehlermeldungen.
- **Zitat:** „Sag mir einfach, was ich als Nächstes tun muss.“
- **Abgeleitete Bedürfnisse:** selbsterklärende Schaltflächen, Schnellfragen, lineare Hauptaktion, Formatbeispiele, Bestätigungen und konkrete Fehlermeldungen.

### Persona C – Nora Frei, blinde Screenreader-Nutzerin

- **Alter / Rolle:** 29, Juristin
- **Geräte:** Notebook mit Chrome, Screenreader und Tastatur
- **Situation:** Prüft den Lieferstatus während einer Arbeitsunterbrechung. Längere Texte gibt sie bevorzugt per Sprache ein.
- **Ziele:** neue Nachrichten automatisch hören, alle Funktionen per Tastatur erreichen, KI- und eigene Nachrichten unterscheiden, Spracheingabe nutzen.
- **Frustrationen:** Fokusverlust nach dynamischen Updates; Status nur über Farbe; unbeschriftete Symbole; visuell sichtbare, aber nicht angekündigte Ladezustände.
- **Zitat:** „Wenn etwas neu erscheint, muss mein Screenreader wissen, dass es da ist.“
- **Abgeleitete Bedürfnisse:** semantische Regionen, `aria-live`, Fokus auf neue Antwort, Textlabel „KI-generiert“, sichtbarer Fokus, Skip-Link, Spracheingabe und Sprachausgabe.

## 4. Hauptaufgaben

1. Eine neue Supportanfrage starten.
2. Eine Frage frei eingeben oder eine Schnellfrage wählen.
3. Während der Generierung erkennen, dass die App arbeitet.
4. KI-Antwort und deren Grenzen verstehen.
5. Antwort bewerten oder erneut versuchen.
6. Bei fehlender Lösung den Chat an den menschlichen Support übergeben.
7. Einen früheren Verlauf öffnen oder fortsetzen.
8. Darstellung und alternative Eingabemethoden anpassen.

## 5. Priorisierte Anforderungen

| ID | User Story / Anforderung | Persona | Priorität | Akzeptanzkriterium |
|---|---|---|---|---|
| R01 | Als Kundin möchte ich eine Frage frei eingeben und absenden. | alle | Must | leere Eingabe ist blockiert; bis 2000 Zeichen; Strg+Enter funktioniert |
| R02 | Als Nutzer möchte ich sofort sehen, dass die KI arbeitet. | alle | Must | Ladeindikator und `aria-busy` erscheinen bis zum ersten bzw. letzten Stream-Teil |
| R03 | Als Skeptikerin möchte ich KI-Inhalte eindeutig erkennen. | Lea | Must | jede KI-Nachricht trägt das Textlabel **KI-GENERIERT** |
| R04 | Als Kundin möchte ich Unsicherheit und Grenzen erkennen. | Lea | Must | Systemanweisung verbietet erfundene Fakten und fordert Eskalation bei fehlendem Kontext |
| R05 | Als Nutzer möchte ich nach einem Fehler weiterkommen. | Peter | Must | verständliche Meldung, **Erneut versuchen** und **An Support übergeben** sichtbar |
| R06 | Als Kundin möchte ich jederzeit menschliche Hilfe verlangen. | alle | Must | Eskalationsaktion im Chat und im Fehlerzustand erreichbar |
| R07 | Als Supportmitarbeitender brauche ich vollständige Pflichtangaben. | intern | Must | Name, Bestellnummer, Anliegen und Dringlichkeit werden validiert |
| R08 | Als Kundin möchte ich den bisherigen Verlauf übergeben. | alle | Must | Opt-in ist sichtbar; gespeicherter Snapshot ist unveränderlich vom späteren Chat |
| R09 | Als Kundin möchte ich jede KI-Antwort bewerten. | Lea | Must | hilfreich/nicht hilfreich sowie optionaler Kommentar pro Antwort |
| R10 | Als blinde Nutzerin möchte ich neue Inhalte angekündigt erhalten. | Nora | Must | Chatlog besitzt `role=log` und konfigurierbares `aria-live`; Fokus wechselt zur Antwort |
| R11 | Als Tastaturnutzerin möchte ich jede Aktion ohne Maus bedienen. | Nora | Must | logische DOM-Reihenfolge, Skip-Link und gut sichtbarer Fokus |
| R12 | Als blinde Nutzerin möchte ich sprechen statt tippen. | Nora | Must | Mikrofontaste startet Chrome Web Speech API und überträgt das Transkript ins Eingabefeld |
| R13 | Als Nutzerin mit Sehschwäche möchte ich Darstellung anpassen. | Nora | Must | Textgrösse und hoher Kontrast werden lokal gespeichert und sofort angewendet |
| R14 | Als wenig geübter Nutzer möchte ich Beispiele auswählen. | Peter | Should | drei klar beschriftete Schnellfragen befüllen oder starten den Chat |
| R15 | Als wiederkehrende Kundin möchte ich frühere Chats finden. | alle | Should | Liste, Suche, Statusfilter und Fortsetzen sind vorhanden |
| R16 | Als Nutzer möchte ich eine Antwort abbrechen können. | alle | Should | Abbruch beendet den UI-Zustand kontrolliert und erhält die Nutzerfrage |
| R17 | Als Prüfer möchte ich den Fehlerfall zuverlässig demonstrieren. | Prüfung | Should | expliziter Demo-Schalter erzeugt einmalig einen kontrollierten Timeout |
| R18 | Als Nutzer möchte ich Inhalte vorlesen lassen. | Nora | Could | optionale Sprachausgabe liest eine vollständige KI-Antwort vor |

## 6. Vertrauen und Transparenz

Vertrauen wird nicht durch eine menschenähnliche Darstellung erzeugt, sondern durch **Vorhersagbarkeit, Ehrlichkeit und Kontrolle**:

- App und Nachrichten nennen KI ausdrücklich im Text.
- Eine permanente Infobox erklärt, dass Antworten Fehler enthalten können.
- Der Modellname und der Streaming-Zustand sind sichtbar.
- Der verwendete fiktive Kontext wird als Produktkatalog, Testbestellungen und Richtlinien beschrieben.
- Bei fehlendem Kontext soll die KI keine Daten erfinden, sondern Unsicherheit nennen.
- Feedback, Retry, Abbruch und Eskalation geben Nutzenden Kontrolle.
- Eine Datenschutzwarnung nennt, welche Inhalte nicht eingegeben werden dürfen.

## 7. Ergonomische Leitlinien

Für das Projekt sind besonders sechs Grundsätze der DIN EN ISO 9241-110 relevant:

| Grundsatz | Umsetzung in SupportBot AI |
|---|---|
| Aufgabenangemessenheit | drei Hauptthemen, Schnellfragen, keine fachfremden Funktionen |
| Selbstbeschreibungsfähigkeit | ausgeschriebene Aktionen, Feldhilfen, Status- und Bestätigungstexte |
| Steuerbarkeit | Abbruch, Retry, Chat fortsetzen, Eskalation, Filter und Einstellungen |
| Erwartungskonformität | konsistente Navigation, bekannte Chatmetapher, Mülleimer nur mit Bestätigung |
| Fehlertoleranz | Validierung, leere Nachricht blockiert, Timeout ohne Datenverlust |
| Individualisierbarkeit | Textgrösse, Kontrast, Sprach-Ein/-Ausgabe und Ankündigungen |

Lernförderlichkeit wird durch Beispiele, Schnellfragen und wiederkehrende Positionen unterstützt. Ein umfangreiches Tutorial ist für die kleine, fokussierte App nicht notwendig.

