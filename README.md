# Chatbot

Ein einfacher Chatbot, der mit WPF erstellt wurde. Der Bot kann einfache Konversationen führen und das aktuelle Wetter für eine angegebene Stadt mithilfe der OpenWeatherMap API anzeigen.

## Anforderungen

- .NET 7.0 SDK
- Visual Studio 2022 oder ein aequivalenter Editor

## Installation

### 1. Projekt klonen

Das Repository sollte auf den lokalen Computer geklont werden.

```console
git clone <URL des Repositories>
```

### 2. NuGet-Pakete installieren

Das Projekt sollte in Visual Studio geoeffnet und alle NuGet-Pakete sollten installiert werden:

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- Newtonsoft.Json

### 3. OpenWeatherMap API Key

Besuche OpenWeatherMap [https://openweathermap.org](https://openweathermap.org) und erstelle einen kostenlosen Account, um einen API-Schluessel zu erhalten.
Im Wurzelverzeichnis des Projekts sollte eine Datei namens `appsettings.json` erstellt werden.
Der API-Schluessel sollte wie folgt eingegeben werden:

```JSON
{
  "OpenWeatherMapApiKey": "IHR_API_KEY"
}
```

Dann sollte man die `appsettings.json` Datei im Solution Explorer von Visual Studio rechtsklicken.
Die **Properties** sollten ausgewaehlt werden.
Es sollte sichergestellt werden, dass die **Copy to Output Directory**-Eigenschaft auf **Copy if newer** eingestellt ist.
Ausserdem sollte sichergestellt werden, dass `appsettings.json` in der `.gitignore`-Datei enthalten ist.

### 4. Projekt ausfuehren

Das Projekt sollte in Visual Studio gebaut und gestartet werden.

# Nutzung

Nach dem Starten der Anwendung kann der Benutzer mit dem Bot kommunizieren, indem er Nachrichten im Textfeld eingibt.

## Moegliche Benutzereingaben:

- Was kannst du alles machen?
- Begruessung: "hallo"
- Abschied: "tschuess"
- Uhrzeit: "uhrzeit"
- Datum: "datum"
- Wetterinformationen: "wetter", "Wie ist das Wetter heute?"

Der Bot wird den Benutzer nach dem Ort fragen, fuer den er das Wetter wissen moechte. Der Benutzer sollte einfach den Namen der Stadt eingeben.
