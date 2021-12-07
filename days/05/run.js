const aoc = require("../../helpers/aoc");
const { processLineByLine } = require("../../helpers/input");

function Point(x, y) {
    this.x = x;
    this.y = y;
    this.equal = (other) => {
        return this.x === other.x && this.y === other.y;
    };
};

function* verticalPoints(firstX, lastX, y) {
    for (let i = Math.min(firstX, lastX); i <= Math.max(firstX, lastX); i++) {
        yield new Point(i, y);
    };
};

function* horizontalPoints(firstY, lastY, x) {
    for (let i = Math.min(firstY, lastY); i <= Math.max(firstY, lastY); i++) {
        yield new Point(x, i);
    }
};

function* diagonalPoints(start, end) {
    const stepX = start.x < end.x ? 1 : -1;
    const stepY = start.y < end.y ? 1 : -1;
    let x = start.x;
    let y = start.y;
    while (x - stepX != end.x && y - stepY != end.y) {
        yield new Point(x, y);
        x += stepX;
        y += stepY;
    }
};

function Segment(start, end) {
    this.start = start;
    this.end = end;

    this.isVertical = () => this.start.y === this.end.y;
    this.isHorizontal = () => this.start.x === this.end.x;
    this.isDiagonal = () => {
        return Math.abs(this.start.x - this.end.x) == Math.abs(this.start.y - this.end.y);
    };

    this.points = function* () {
        if (this.isVertical()) {
            return yield* verticalPoints(this.start.x, this.end.x, this.start.y);
        } else if (this.isHorizontal()) {
            return yield* horizontalPoints(this.start.y, this.end.y, this.start.x);
        } else if (this.isDiagonal()) {
            return yield* diagonalPoints(this.start, this.end);
        } else {
            return null;
        }
    };

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
    const field = [];
    let overlaps = 0;

    processLineByLine(inputFile, (line) => {
        const lineOfVents = parseLineOfVents(line);

        for (let point of lineOfVents.points()) {
            if (field[point.x] && field[point.x][point.y]) {
                field[point.x][point.y]++;
                if (field[point.x][point.y] === 2) {
                    overlaps++;
                }
            }
            if (field[point.x] === undefined) {
                field[point.x] = [];
            }
            if (field[point.x][point.y] === undefined) {
                field[point.x][point.y] = 1;
            }
        }

    }, () => {
        console.log("- Overlaps (all):", overlaps);
    });
});