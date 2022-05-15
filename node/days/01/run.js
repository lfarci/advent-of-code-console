const aoc = require("../../helpers/aoc");
const input = require("../../helpers/input");

const add = (accumulator, a) => accumulator + a;

const countDepthMeasurementIncreases = (inputFileName, onEndOfFile) => {

    let depthMeasurementIncreases = 0;
    let lastDepthMeasurement = null;

    const incrementMeasurementWindowsIncreases = (line) => {
        const currentDepthMeasurement = parseInt(line);
        if (lastDepthMeasurement !== null && lastDepthMeasurement < currentDepthMeasurement) {
            depthMeasurementIncreases++;
        }
        lastDepthMeasurement = currentDepthMeasurement;
    };

    input.processLineByLine(inputFileName, incrementMeasurementWindowsIncreases, () => {
        onEndOfFile(depthMeasurementIncreases);
    });
};

const countDepthMeasurementWindowsIncreases = (inputFileName, windowSize, onEndOfFile) => {

    const windows = [];
    let windowIndexToBeRemoved = null;
    let previousValue = null;
    let currentValue = null;
    let count = 0;

    const isPushable = (window) => window.length > 0 && window.length < windowSize;

    const checkShouldBePerformedAt = (windowIndex) => {
        window = windows[windowIndex];
        previousWindow = windows[windowIndex - 1];
        return window.length === windowSize && previousWindow && previousWindow.length === windowSize;
    };

    const incrementDepthMeasurementWindowsIncreases = (line) => {
        const currentDepthMeasurement = parseInt(line);
        windows.forEach((window, windowIndex) => {
            if (isPushable(window)) {
                window.push(currentDepthMeasurement);
            }
            if (checkShouldBePerformedAt(windowIndex)) {
                previousValue = windows[windowIndex - 1].reduce(add, 0);
                currentValue = window.reduce(add, 0);
                windowIndexToBeRemoved = windowIndex - 1;
            }
        });
        if (previousValue < currentValue) {
            count++;
        }
        if (windowIndexToBeRemoved !== null) {
            windows.splice(windowIndexToBeRemoved, 1);
        }
        windows.push([currentDepthMeasurement]);
    };

    input.processLineByLine(inputFileName, incrementDepthMeasurementWindowsIncreases, () => {
        onEndOfFile(count);
    });
};

aoc.day(1, "Sonar Sweep", (inputFile) => {
    countDepthMeasurementIncreases(inputFile, (result) => {
        console.log("Number of depth measurement increases:", result)
    });
    countDepthMeasurementWindowsIncreases(inputFile, 3, (result) => {
        console.log("Number of depth measurement windows increases:", result);
    });
});