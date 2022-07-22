[![Build Status](https://dev.azure.com/lfarci/Advent%20of%20Code%20Kit/_apis/build/status/lfarci.advent-of-code-kit?branchName=main)](https://dev.azure.com/lfarci/Advent%20of%20Code%20Kit/_build/latest?definitionId=1&branchName=main)
# Advent Of Code Kit
The kit gathers some tools that can help .NET developers during the Advent Of Code.

## Client

### Environment variables

- `AOC_HOST` (Optional): server host name running the targeted Advent Of Code instance. Its default value is set to `adventofcode.com`.
- `AOC_SESSION_ID` (Required): the user session id. It is required to fetch resources from the Advent Of Code website for authentication. You can find this token by navigating to the website using your browser and investigate cookies using the developer tools. Its default value is an empty string.