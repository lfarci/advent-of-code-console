const input = require("../../helpers/input");

const move = (position, line) => {
    const tokens = line.split(" ");
    const value = parseInt(tokens[1]);
    switch (tokens[0]) {
        case "up":
            position.aim -= value;
            break;
        case "down":
            position.aim += value;
            break;
        case "forward":
            position.horizontal += value;
            position.depth += position.aim * value;
            break;
        default:
            console.error("Not a known token:", tokens[0]);
    }
    return position;
};

input.startDailyChallengeWith((inputFileName) => {
    const position = {aim: 0, depth: 0, horizontal: 0};
    input.processLineByLine(inputFileName, (line) => {
        move(position, line);
    }, () => {
        const result = position.depth * position.horizontal;
        console.log("Depth:", position.depth, "Horizontal:", position.horizontal, "Result:", result);
    });
});