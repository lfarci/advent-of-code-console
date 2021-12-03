const fs = require("fs");

function isEmpty(path) {
    return fs.readdirSync(path).length === 0;
}

const buildDailyChallengeHeading = (dayIndex, title) => {
    let heading = "🎄 Advent Of Code 2021 - Day ";
    if (dayIndex === undefined) {
        throw "AOC: A day index is required.";
    } else {
        heading += dayIndex;
        if (title) {
            heading += `: ${title}`;
        }
        heading += ` (https://adventofcode.com/2021/day/${dayIndex})`;
    }
    return heading;
};

const buildInputFolderPath = (dayIndex) => {
    const dayIndexString = dayIndex < 10 ? `0${dayIndex}` : dayIndex;
    return `./days/${dayIndexString}/input`;
};

const day = (dayIndex, title = undefined, showSolutions = undefined) => {
    const heading = buildDailyChallengeHeading(dayIndex, title);
    console.log(heading);
    const inputFolder = buildInputFolderPath(dayIndex);
    if (fs.existsSync(inputFolder)) {
        if (isEmpty(inputFolder)) {
            console.log(`Add input files to the ${inputFolder} folder before starting your challenge.`);
        } else {
            if (showSolutions) {
                console.log("\nSolutions:");
                showSolutions(`${inputFolder}/input.txt`, `${inputFolder}/test.txt`);
            } else {
                console.log("Provide an implementation.");
            }
        }
    } else {
        console.log(`Create the ${inputFolder} folder before starting your challenge.`);
    }
};

module.exports = {
    day: day
};