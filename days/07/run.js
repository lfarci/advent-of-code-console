const aoc = require("../../helpers/aoc");
const { readFileContent } = require("../../helpers/input");
const { readNumbers, min, max } = require("../../helpers/util");

const incrementalCost = (distance) => (distance / 2) * (1 + distance);
const constantRateCost = (distance, costByStep = 1) => costByStep * distance;

const getFuelCostsByPositions = (crabSubmarinePositions, getFuelCostForDistance) => {
    const costs = new Map();
    const firstCandidate = min(crabSubmarinePositions);
    const lastCandidate = max(crabSubmarinePositions);
    for (let c = firstCandidate; c <= lastCandidate; c++) {
        let fuelCost = 0;
        crabSubmarinePositions.forEach(p => {
            let distance = Math.abs(c - p);
            fuelCost += getFuelCostForDistance(distance);
        });
        costs.set(c, fuelCost);
    };
    return costs;
};

aoc.day(7, "The Treachery of Whales", (inputFile) => {
    readFileContent(inputFile, (content) => {
        const positions = readNumbers(content, ',');
        const constantRateCosts = getFuelCostsByPositions(positions, constantRateCost);
        const incrementalCosts = getFuelCostsByPositions(positions, incrementalCost);
        console.log("- Constant cost (first part):", min(constantRateCosts.values()));
        console.log("- Incremental cost (second part):", min(incrementalCosts.values()));
    });
});