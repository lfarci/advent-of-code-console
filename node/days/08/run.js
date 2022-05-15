const aoc = require("../../helpers/aoc");
const { processLineByLine } = require("../../helpers/input");
const { mostCommon, hasValue } = require("../../helpers/util");

const SEGMENTS = 'abcdefg';
const TEST_PATTERN = 'deafgbc';

const SIGNAL_PATTERNS = ['abcefg', 'cf', 'acdeg', 'acdfg', 'bcdf', 'abdfg', 'abdefg', 'acf', 'abcdefg', 'abcdfg'];
const TEST_SAMPLE = ['cagedb', 'ab', 'gcdfa', 'fbcad', 'eafb', 'cdfbe', 'cdfgeb', 'dab', 'acedgfb', 'cefabd'];

const parseSignalPatterns = (line) => line.trim().split(" ");

const isUniqueSignalPattern = (s) => {
    return SIGNAL_PATTERNS.filter(c => c.length === s.length).length === 1;
};

const findUniqueSignalPatternNumber = (s) => {
    return SIGNAL_PATTERNS.findIndex(c => c.length === s.length);
};

const findUniqueSignalPattern = (s) => {
    const number = findUniqueSignalPatternNumber(s);
    return SIGNAL_PATTERNS[number];
};

const findAllSignalPatternByLength = (s) => {
    return SIGNAL_PATTERNS.filter(c => c.length === s.length);
};

const parseSnapshot = (line) => {
    const sections = line.split("|");
    return {
        signalPatterns: parseSignalPatterns(sections[0]),
        outputValue: parseSignalPatterns(sections[1])
    };
};

const countUniqueSignalPatterns = (snapshots) => {
    return snapshots
        .map(s => s.outputValue)
        .flat()
        .filter(isUniqueSignalPattern)
        .length;
};

const decodeSignalPattern = (s, currentState) => {

    // check knowledge

    /*
        1. Filter possible patterns based on size
        2. map each signal to its expected value
    */


    let options = findAllSignalPatternByLength(s);

    const expected = TEST_SAMPLE.findIndex(c => c === s);
    console.log("Actual:", s, "Expected number:", expected, "Expected signals:", SIGNAL_PATTERNS[expected]);

    [...s].forEach((signal, i) => {

        if (!currentState.decoded.has(signal) && currentState.combinations.has(signal)) {

            const combinations = currentState.combinations.get(signal);

            if (combinations.length > 2) {
                const mostProbable = mostCommon(combinations);
                options = options.filter(o => o.includes(mostProbable))


                console.log("\t", signal, combinations, mostProbable, "[MOST PROPABLE]");
            } else {
                console.log("\t", signal, combinations);
            }

        }

    });
    console.log("\tOptions:", options);
};

const decodeRemainingSignalPatterns = (signalPatterns, currentState) => {
    signalPatterns.forEach(s => decodeSignalPattern(s, currentState));
};


const decodeUniqueSignalPattern = (uniqueSignals, combinations) => {
    const signalsPattern = findUniqueSignalPattern(uniqueSignals);
    const signals = [...uniqueSignals].filter(s => !combinations.has(s));
    const remaining = [...signalsPattern].filter(s => !hasValue(combinations, s));
    signals.forEach(signal => combinations.set(signal, remaining));
};

const byLength  = (a, b) => a.length - b.length;

const decodeUniqueSignalPatterns = (uniqueSignalPatterns) => {
    const combinations = new Map();
    uniqueSignalPatterns.sort(byLength).forEach(s => {
        decodeUniqueSignalPattern(s, combinations);
    });
    return combinations;
}


/*
  0:              2:      3:
 aaaa            aaaa    aaaa
b    c          .    c  .    c
b    c          .    c  .    c
 ....            dddd    dddd
e    f          e    .  .    f
e    f          e    .  .    f
 gggg            gggg    gggg

  5:      6:                      9:
 aaaa    aaaa                    aaaa
b    .  b    .                  b    c
b    .  b    .                  b    c
 dddd    dddd                    dddd
.    f  e    f                  .    f
.    f  e    f                  .    f
 gggg    gggg                    gggg
*/

// deux groupes :
// - 7 - 2 segments: 2, 3, 5
// - 7 - 1 segments: 0, 6, 9

// le seul qui a pas a c et f est 6, le 6 nous dit lequel de a ou b est c ou f
// pareil 2 n'a pas de f, 3 et 5 en ont

const decodeAll = (signalPatterns) => {
    const uniqueSignalPatterns = signalPatterns.filter(isUniqueSignalPattern);
    const combinations = decodeUniqueSignalPatterns(uniqueSignalPatterns);
    const finalCombinations = new Map(combinations);

    const remainingSignalPatterns = signalPatterns.filter(s => !isUniqueSignalPattern(s));

    const allSegments = Array.from(combinations.entries());
    const rightSegments = allSegments.filter(e => e[1].includes('c') && e[1].includes('f'));
    const middleSegments = allSegments.filter(e => e[1].includes('d'));
    const bottomLeftSegment = allSegments.filter(e => e[1].includes('e'));

    remainingSignalPatterns.forEach(signalPattern => {

        const signalPatternCombinations = allSegments.filter(e => signalPattern.includes(e[0]));

        if (signalPattern.length === SEGMENTS.length - 1) {

            const currentSignalRightSegments = signalPatternCombinations.filter(e => e[1].includes('c') && e[1].includes('f'));
            const currentSignalMiddleSegment = signalPatternCombinations.filter(e => e[1].includes('d'));

            if (currentSignalRightSegments.length < 2) {
                const bottomRightSegmentSignal = currentSignalRightSegments[0][0];
                const topRightSegment = rightSegments.filter(s => s[0] !== bottomRightSegmentSignal);
                const topRightSegmentSignal = topRightSegment[0][0];

                finalCombinations.set(topRightSegmentSignal, ['c'])
                finalCombinations.set(bottomRightSegmentSignal, ['f'])
            }

            if (currentSignalMiddleSegment.length == 1) {
                // only two matches for middle, must be zero!
                const opponentSignal = currentSignalMiddleSegment[0][0];
                const middleSegment = middleSegments.filter(s => s[0] !== opponentSignal);
                const middleSegmentSignal = middleSegment[0][0];

                finalCombinations.set(middleSegmentSignal, ['d']);
                finalCombinations.set(opponentSignal, ['b']);

            }
        }

        if (signalPattern.length === SEGMENTS.length - 2) {

            const currentSignalBottomLeftSegments = signalPatternCombinations.filter(e => e[1].includes('e'));

            console.log(signalPattern, "Current:", currentSignalBottomLeftSegments, "All:", bottomLeftSegment);


            if (currentSignalBottomLeftSegments.length > 1) {

                const candidate1

                // console.log(signalPattern, "Current:", currentSignalBottomLeftSegments, "All:", bottomLeftSegment);
                

            }

        }
    });

    console.log(finalCombinations)
};


aoc.day(8, "Seven Segment Search", (inputFile, testFile) => {
    const snapshots = [];
    processLineByLine(inputFile, line => snapshots.push(parseSnapshot(line)), () => {
        console.log("Occurences of digits 1, 4, 7, or 8:", countUniqueSignalPatterns(snapshots));

        const sample = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
        const snapshot = parseSnapshot(sample);

        decodeAll(snapshot.signalPatterns);
    });
});