# Courses

## Instalacja

***API***
Upewnij się, że masz zainstalowane .NET Core 5.0 SDK. Możesz znaleźć je [tutaj](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

Aplikacja posiada bazę umieszczoną w localDB, w appSettings.json powstał defaultowy connectionString(upewnij się, że posiadasz bazę o takiej samej nazwie w SQL Server Object Explorer). Projekt zawiera już migracje, wystarczy (np w Package Menager Console) uruchomić:

```
Update-Database
```
Następnie uruchamiamy aplikacje jako IIS Express lub poprzez kliknięcie klawisza F5. 

***Angular***: 

otwórz projekt CoursesWeb w visual studio code lub w cmd przejdz do:

```
cd [YourDirectory]../Courses/coursesWeb
```

uruchom dwie komendy:
```
npm install
ng serve
```

## Stack technologiczny:

***Użyte technologie***
+ Angular 13
+ Angular material
+ .Net Core 5.0

***Dodatkowe informacje:***
+ Po odpaleniu API wyświetli nam się dokumentacja swaggera. 

### Dodatkowe info:
Autor:
Kamil Krawczyk

Giganci Programowania Zadanie Rekrutacyjne
