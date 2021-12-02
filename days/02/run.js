const input = require("../../helpers/input");

const toInstruction = (tokens) => {
    const position = {depth: 0, horizontal: 0};
    const value = parseInt(tokens[1]);
    switch (tokens[0]) {
        case "up":
            position.depth -= value;
            break;
        case "down":
            position.depth += value;
            break;
        case "forward":
            position.horizontal += value;
            break;
        default:
            console.error("Not a known token:", tokens[0]);
    }
    return position;
};

const parseInstruction = (line) => {
    const tokens = line.split(" ");
    if (tokens.length === 2) {
        return toInstruction(tokens);
    } else {
        console.error("Not a valid instruction:", line);
    }
};

input.startDailyChallengeWith((inputFileName) => {
    const position = {depth: 0, horizontal: 0}
    input.processLineByLine(inputFileName, (line) => {
        const instruction = parseInstruction(line);
        position.depth += instruction.depth;
        position.horizontal += instruction.horizontal;
    }, () => {
        const result = position.depth * position.horizontal;
        console.log("Depth:", position.depth, "Horizontal:", position.horizontal, "Result:", result);
    });
});