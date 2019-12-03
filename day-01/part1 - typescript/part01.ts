import { MODULES } from './puzzleInput';


function calculateFuel(total: number, amount: number): number {
    return total + Math.floor(amount / 3) - 2;
}

function getModuleFuelAmount(): number {
    return MODULES.reduce(calculateFuel, 0);
}

console.log(getModuleFuelAmount());
