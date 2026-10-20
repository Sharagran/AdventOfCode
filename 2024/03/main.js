const fs = require("node:fs")

function readData() {
    let data = fs.readFileSync("./input.txt", "utf8")

    let matches = data.matchAll(/(mul\(\d{1,3},\d{1,3}\))|do\(\)|don't\(\)/g)

    let validInstructions = []

    let match = matches.next()
    var enabled = true
    while (!match.done) {
        if (match.value[0] === 'do()') {
            enabled = true
        } else if (match.value[0] === "don't()") {
            enabled = false
        } else if (enabled) {
            validInstructions.push(match.value[0])
        }

        match = matches.next()
    }

    return validInstructions
}

const mul = (x, y) => x * y 

var instructions = readData()

var sum = 0
instructions.forEach(instruction => {
    sum += eval(instruction)
});

console.log(sum);
