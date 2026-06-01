# Inskolningsapplikation Navet

Det här projektet utgör mitt självständiga arbete för Webbutvecklingsprogrammet, Mittuniversitetet, under vårterminen 2026. Projektet utgörs av en webbaserad inskolningsapplikation utvecklad för simhallen Navet i Umeå och har syftat till att digitalisera och förbättra deras nuvarande inskolningsprocess. 

## Funktionalitet

### Generell funktionalitet
* Inloggning med ASP.NET Core Identity.
* Rollbaserad behörighet för "Employee" och "Admin".
* Responsiv design. 

### Gränssnitt för nyanställd personal 
* Personlig dashboard med översikt av progression och innehåll. 
* Digitaliserad checklista med kategoriserade inskolningsmoment. 
* Möjlighet att markera checklistemoment som påbörjade eller avklarade. 
* Personliga anteckningar kopplade till checklistan. 
* Sökfunktion för att hitta innehåll. 
* Modulbaserad utbildningsmaterial. 
* Kunskapstester kopplade till utbildningsmoduler. 
* Lösenordshantering. 

### Gränssnitt arbetsledning
* Särskild dashboard med översikt av innehållet för en admin-användare. 
* Hantering av användare (full CRUD). 
* Hantering av utbildningsmoduler med tillhörande sektioner och kunskapstest (full CRUD för samtliga tre delar). 
* Filuppladdning av bilder för sektionerna och möjlighet att bädda in YouTube-filmer. 
* Hantering av checklistans föremål (full CRUD).
* Översikt av användarnas progression. 
* Notifikationer. 

## Tekniker

Projektet är utvecklat med: 
- Blazor Web App (.NET)
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- C#
- HTML
- CSS
- JavaScript
- Quill Rich Text Editor

## Författare och utvecklare
Julia Gustafsson