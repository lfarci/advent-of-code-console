const input = require("../../helpers/input");

const parseIntFromByte = (byte) => {
    return parseInt(byte.join(""), 2);
};

const countOnes = (bytes) => {
    const onesCounts = [];
    bytes.forEach(byte => {
        [...byte].forEach((bit, i) => {
            onesCounts[i] = Number(bit) + (onesCounts[i] ? onesCounts[i] : 0);
        });
    });
    return onesCounts;
};

const mostCommonBit = (o, z) => o >= z ? '1' : '0';
const leastCommonBit = (o, z) => o >= z ? '0' : '1';

const findByteByJoiningSelectedBits = (bytes, getBitCriteria) => {
    const onesCounts = countOnes(bytes);
    const byte = [];
    onesCounts.forEach((onesCount, index) => {
        const zerosCount = bytes.length - onesCount;
        byte[index] = getBitCriteria(onesCount, zerosCount);
    });
    return byte;
};


const findByteByFilteringBytes = (bytes, getBitCriteria) => {
    let remainingBytes = [...bytes];
    let position = 0;
    while (remainingBytes.length > 1) {
        const onesCounts = countOnes(remainingBytes);
        const onesCount = onesCounts[position];
        const zerosCount = remainingBytes.length - onesCount;
        const bitCriteria = getBitCriteria(onesCount, zerosCount);
        remainingBytes = remainingBytes.filter(byte => byte[position] === bitCriteria);
        position++;
    }
    return remainingBytes[0];
};


const findPowerConsumption = (bytes) => {
    const gammaRateByte = findByteByJoiningSelectedBits(bytes, mostCommonBit);
    const epsilonRateByte = findByteByJoiningSelectedBits(bytes, leastCommonBit);
    const gammaRate = parseIntFromByte(gammaRateByte);
    const epsilonRate = parseIntFromByte(epsilonRateByte);
    return gammaRate * epsilonRate;
};

const findLifeSupportRating = (bytes) => {
    const oxygenGeneratorRateByte = findByteByFilteringBytes(bytes, mostCommonBit);
    const CO2ScrubberRateByte = findByteByFilteringBytes(bytes, leastCommonBit);
    const oxygenGeneratorRate = parseIntFromByte(oxygenGeneratorRateByte);
    const CO2ScrubberRate = parseIntFromByte(CO2ScrubberRateByte);
    return oxygenGeneratorRate * CO2ScrubberRate;
};

input.startDailyChallengeWith((inputFileName) => {
    let bytes = [];
    input.processLineByLine(inputFileName, (byte) => {
        bytes.push([...byte]);
    }, () => {
        console.log("Power consumption:", findPowerConsumption(bytes));
        console.log("Life support rating:", findLifeSupportRating(bytes));
    });
});