# Advent Of Code 2021 for .NET 6

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