const readNumbers = (line, separator) => {
    return line.trim().split(separator).map(c => Number(c));
};

const findCoordinates = (numbersGrid, number) => {
    let coordinates = null;
    const rowIndex = numbersGrid.findIndex(r => r.includes(number));
    if (rowIndex >= 0) {
        const columnIndex = numbersGrid[rowIndex].indexOf(number);
        if (columnIndex >= 0) {
            coordinates = {row: rowIndex, column: columnIndex};
        }
    }
    return coordinates;
};

module.exports = {
    readNumbers: readNumbers,
    findCoordinates: findCoordinates
};