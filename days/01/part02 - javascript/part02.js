

exports.__esModule = true;
const puzzleInput_1 = require('./puzzleInput');

function calculateFuel(total, amount) {
    const neededFuel = amount / 3n - 2n;

    if (neededFuel <= 0) return total;

    return calculateFuel(total + neededFuel, neededFuel);
}

function getModuleFuelAmount() {
    return puzzleInput_1.MODULES.reduce(calculateFuel, 0n);
}

console.log(getModuleFuelAmount());
