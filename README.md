# [Deutsch](#deutsch-1)

Deutsche Erklärung des Programms

# [English](#english-1)

English explanation of the program

# Deutsch

## Chatbot

Ein einfacher Chatbot, der mit WPF erstellt wurde. Der Bot kann einfache Konversationen fuehren und das aktuelle Wetter fuer eine angegebene Stadt mithilfe der OpenWeatherMap API anzeigen.

### Anforderungen

- .NET 7.0 SDK
- Visual Studio 2022 oder ein äquivalenter Editor

### Installation

#### 1. Projekt klonen

Das Repository sollte auf den lokalen Computer geklont werden.

```console
git clone <URL des Repositories>
```

#### 2. NuGet-Pakete installieren

Das Projekt sollte in Visual Studio geoeffnet und alle NuGet-Pakete sollten installiert werden:

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- Newtonsoft.Json

#### 3. OpenWeatherMap API Key

Besuche OpenWeatherMap [https://openweathermap.org](https://openweathermap.org) und erstelle einen kostenlosen Account, um einen API-Schluessel zu erhalten.
Im Wurzelverzeichnis des Projekts sollte eine Datei namens `appsettings.json` erstellt werden.
Der API-Schluessel sollte wie folgt eingegeben werden:

```JSON
{
  "OpenWeatherMapApiKey": "IHR_API_KEY"
}
```

Dann sollte man die `appsettings.json` Datei im Solution Explorer von Visual Studio rechtsklicken.
Die **Properties** sollten ausgewählt werden.
Es sollte sichergestellt werden, dass die **Copy to Output Directory**-Eigenschaft auf **Copy if newer** eingestellt ist.
Ausserdem sollte sichergestellt werden, dass `appsettings.json` in der `.gitignore`-Datei enthalten ist.

#### 4. Projekt ausfuehren

Das Projekt sollte in Visual Studio gebaut und gestartet werden.

## Nutzung

Nach dem Starten der Anwendung kann der Benutzer mit dem Bot kommunizieren, indem er Nachrichten im Textfeld eingibt.

### Moegliche Benutzereingaben:

- Was kannst du alles machen?
- Begruessung: "hallo"
- Abschied: "tschuess"
- Uhrzeit: "uhrzeit"
- Datum: "datum"
- Wetterinformationen: "wetter", "Wie ist das Wetter heute?"

Der Bot wird den Benutzer nach dem Ort fragen, fuer den er das Wetter wissen moechte. Der Benutzer sollte einfach den Namen der Stadt eingeben.

# English

## Chatbot

A simple chatbot created with WPF. The bot can conduct simple conversations and display the current weather for a specified city using the OpenWeatherMap API.

### Requirements

- .NET 7.0 SDK
- Visual Studio 2022 or an equivalent editor

### Installation

#### 1. Clone project

The repository should be cloned to the local computer.

```console
git clone <URL of the repository>
```

#### 2. Install NuGet packages

The project should be opened in Visual Studio and all NuGet packages should be installed:

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- Newtonsoft.Json

#### 3. OpenWeatherMap API Key

Visit OpenWeatherMap [https://openweathermap.org](https://openweathermap.org) and create a free account to obtain an API key. In the root directory of the project, a file named appsettings.json should be created. The API key should be entered as follows:

```JSON
{ "OpenWeatherMapApiKey": "YOUR_API_KEY" }
```

Then one should right-click the `appsettings.json` file in the Solution Explorer of Visual Studio.
The **Properties** should be selected.
It should be ensured that the **Copy to Output Directory** property is set to **Copy if newer**.
Additionally, it should be ensured that `appsettings.json` is included in the `.gitignore` file.

#### 4. Execute project

The project should be built and started in Visual Studio.

## Usage

After launching the application, the user can communicate with the bot by entering messages in the text field.

### Possible user inputs:

- What can you do?
- Greeting: "hello"
- Farewell: "goodbye"
- Time: "time"
- Date: "date" Weather information: "weather", "What's the weather like today?"

The bot will ask the user for the location for which they want to know the weather. The user should simply enter the name of the city.
