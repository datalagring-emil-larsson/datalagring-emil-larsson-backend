CourseManager - Backend API

CourseManager är ett backend system bygg med ASP.NET Core Minimal API där jag har använt Clean Architecture och en domändriven design(DDD).
Systemet hanterar kurser, kurstillfällen, deltagare, lärare, platser och kursregistreringar.

Projeket använder sig av Entity Framework Core med en Code first approach och en normaliserad relationsdatabas

Strukturern:

Min solution följer Clean Architecture med dessa lagerna:

"Presentation" - Minimal API Endpoints
"Application" - Affärslogik, services och DTOs.
"Domain" - Entiteter och domänregler.
"Infrastructure" - EF Core, repositories och databas konfiguration.
"Tests" - Enhetstest.

Hur du start det lokalt:

Se till att du har:

	.Net SDK 8.0 eller senare.
	SQL Server (LocalDb eller Full SQL server)
	Visual Studio

2. Klona repositoryt
	
	git clone <https://github.com/datalagring-emil-larsson/datalagring-emil-larsson-backend>

	cd CourserManager

3. Konfigurerar databasens connectionstring
	
	Öppna "appsettings.json" i "CourseManager.Presentation.API" och uppdatera connectionstring om det krävs.

4. Tillämpa databasmigrering

	I package Manager Console (View > Other windows > Package Manager Console) skriv följande:
	Update-Database

5. Kör API:et

	Se till att du ligger i CourseManager.Presentation.API och tryck på F5.
	Du kommer få en Https://localhost:<port>

6. Swagger

	För att använda Swagger så tar du httpslänken och lägger till "/swagger" i slutet.
	Exempel: Https://localhost:<port>/swagger

	Härifrån kommer du kunna testa alla API endpoints.

7. Tester

	Öppna "ParticipantServicetest.cs"
	Öppna test explorer (View > Test Explorer)
	Tryck på "Run all tests in view"

Detta projektet var utvecklat som en inlämnings för att demonstrera:

Clean Architecture Principer
Domain-driven design (DDD)
Relationsdatabas modellering
Entity Framework Core Code first
API utveckling med minimal api
Enhetstest

Status:

Backend klart
Databas implementerad
CRUD implementerat
Test implementerat

Utvecklad av: Emil Jonas Larsson

