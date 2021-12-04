const aoc = require("../../helpers/aoc");
const input = require("../../helpers/input");
const { findCoordinates, readNumbers } = require("../../helpers/util");

const parseSubmarineBingoSubsystem = (file, onParsed) => {
    let drawnNumbers = [];
    let currentBoardIndex = 0;
    let currentBoard;
    const boards = [];
    const whitespace = /\s+/;
    input.processLineByLine(file, (line, index) => {
        if (index == 0) {
            drawnNumbers = readNumbers(line, ',');
        } else {
            if (line === '') {
                if (currentBoard) boards.push(currentBoard);
                currentBoard = new BingoBoard(currentBoardIndex++);
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

function BingoBoard(order = null) {

    this.grid = [];
    this.marks = [];
    this.order = order;
    this.won = false;
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
            this.won = this.isWinningHit(c);
            if (this.won) {
                this.finalScore = this.getFinalScore(number);
            }
            return this.won;
        }
        return false;
    };
};

const play = (bingo) => {
    let currentDrawIndex = 0;
    let winners = [];
    while (currentDrawIndex < bingo.draws.length && winners.length < bingo.boards.length) {
        const currentDraw = bingo.draws[currentDrawIndex];
        bingo.boards.filter(b => !b.won).forEach((b) => {
            if (b.play(currentDraw)) {
                winners.push(b);
            }
        });
        currentDrawIndex++;
    }
    return winners;
};

aoc.day(4, "Giant Squid", (inputFile) => {
    parseSubmarineBingoSubsystem(inputFile, (bingo) => {
        const winners = play(bingo);
        console.log('\nWinning boards (in order):');
        winners.forEach((w, i) => {
            console.log(`\t${i + 1}. Board ${w.order + 1} - score: ${w.finalScore}`);
        });
    });
});