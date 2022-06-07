# Advent Of Code 2021 for .NET 6

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=lfarci_advent-of-code-2021&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=lfarci_advent-of-code-2021)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=lfarci_advent-of-code-2021&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=lfarci_advent-of-code-2021)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=lfarci_advent-of-code-2021&metric=coverage)](https://sonarcloud.io/summary/new_code?id=lfarci_advent-of-code-2021)

## Launch settings
I ignored the `launchSettings.json` file because my Advent Of Code 2021 session id was stored in it. Here is an example of a helpful configuration.

```json
{
  "profiles": {
    "Challenge 01: Sonar Sweep": {
      "commandName": "Project",
      "commandLineArgs": "01",
      "environmentVariables": {
        "AOC_SESSION_ID": "<get your session id from adventofcode.com>"
      }
    }
  }
}
```