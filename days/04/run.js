const aoc = require("../../helpers/aoc");
const input = require("../../helpers/input");
const { findCoordinates, readNumbers } = require("../../helpers/util");

const parseSubmarineBingoSubsystem = (file, onParsed) => {
    let drawnNumbers = [];
    let currentBoard;
    const boards = [];
    const whitespace = /\s+/;
    input.processLineByLine(file, (line, index) => {
        if (index == 0) {
            drawnNumbers = readNumbers(line, ',');
        } else {
            if (line === '') {
                if (currentBoard) boards.push(currentBoard);
                currentBoard = new BingoBoard();
            } else {
                currentBoard.addRow(readNumbers(line, whitespace));
            }
        }
    }, () => {
        boards.push(currentBoard);
        onParsed({
            draws: drawnNumbers,
            boards: boards
        });
    });
};

function BingoBoard() {

    this.grid = [];
    this.marks = [];
    this.finalScore = null;

    this.addRow = (numbers) => this.grid.push(numbers);
    this.hasAFullyMarkedRowAfter = (hit) => this.marks.filter(m => m.row === hit.row).length === this.grid.length;
    this.hasAFullyMarkedColumnAfter = (hit) => this.marks.filter(m => m.column === hit.column).length === this.grid.length;

    this.isWinningHit = (hit) => {
        return this.marks.length >= this.grid.length
            && (this.hasAFullyMarkedRowAfter(hit) || this.hasAFullyMarkedColumnAfter(hit));
    };

    this.isMarked = (coordinates) => {
        return this.marks.some(m => m.row === coordinates.row && m.column === coordinates.column);
    };

    this.getFinalScore = (number) => {
        let sum = 0;
        this.grid.flat().forEach(n => {
            const c = findCoordinates(this.grid, n);
            if (c && !this.isMarked(c)) {
                sum += n;
            }
        });
        return sum * number;
    }

    this.play = (number) => {
        const c = findCoordinates(this.grid, number);
        if (c) {
            this.marks.push(c);
            this.finalScore = this.getFinalScore(number);
            return this.isWinningHit(c);
        }
        return false;
    };
};

const play = (bingo) => {
    let currentDrawIndex = 0;
    let winner = null;
    while (currentDrawIndex < bingo.draws.length && !winner) {
        const currentDraw = bingo.draws[currentDrawIndex];
        bingo.boards.forEach((b) => {
            if (b.play(currentDraw)) winner = b;
        });
        currentDrawIndex++;
    }
    return winner;
};

aoc.day(4, "Giant Squid", (inputFile) => {
    parseSubmarineBingoSubsystem(inputFile, (bingo) => {
        const winner = play(bingo);
        if (winner) {
            console.log("Winning board final score:", winner.finalScore);
        } else {
            console.log("No winner with given input.");
        }
    });
});