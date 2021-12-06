const aoc = require("../../helpers/aoc");
const { processLineByLine } = require("../../helpers/input");

function Point(x, y) {
    this.x = x;
    this.y = y;
    this.equal = (other) => {
        return this.x === other.x && this.y === other.y;
    };
};

function Segment(start, end) {
    this.start = start;
    this.end = end;
    this.equal = (other) => {
        return this.start.equal(other.start) && this.end.equal(other.end);
    };
};

const parseCoordinates = (line) => {
    const tokens = line.split(",");
    return new Point(Number(tokens[0]), Number(tokens[1]));
};

const parseLineOfVents = (line) => {
    const tokens = line.split(" ");
    return new Segment(parseCoordinates(tokens[0]), parseCoordinates(tokens[2]));
};

aoc.day(5, "Hydrothermal Venture", (inputFile, t) => {
    const hydrothermalVentsField = [];
    processLineByLine(t, (line) => {
        hydrothermalVentsField.push(parseLineOfVents(line));
    }, () => {
    });
});