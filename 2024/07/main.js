const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const equations = []

    data.split("\n").forEach((line) => {
        if(line.length === 0) return

        const matches = line.matchAll(/\d+/g)

        let match = matches.next()
        const result = parseInt(match.value)
        match = matches.next()

        const numbers = []
        while (!match.done) {
            numbers.push(parseInt(match.value))
            match = matches.next()
        }

        equations.push({
            result,
            numbers
        })
    });

    return equations
}

const equations = readData('./input.txt')

function binNumberToOperatorSequence(number) {
    let seq = ""

    do {
        if(number & 1 === 1) {
            seq += "*"
        } else {
            seq += "+"
        }

        number = number >> 1
    } while (number > 0);
    return seq.split().reverse().join()
}

binNumberToOperatorSequence(3)

function test(expectedResult, numbers) {
    // 2 numbers = 2^2-1 = 3(dec) = 11(bin)
    let perm = Math.pow(2, numbers.length)-1


    for (let i = 0; i < perm; i++) {
        // all permutations
        let result = numbers[0]

        const operatorSequence = binNumberToOperatorSequence(i)
        //do stuff
        for (let j = 1; j < numbers.length; j++) {
            const number = numbers[j];
            const operator = operatorSequence[j-1]
            
            if(operator === "+") {
                result += number
            } else {
                result *= number
            }
        }

        if(result === expectedResult) {
            return true
        }
    }


    return false
}


let sum = 0
equations.forEach(eq => {
    const { result, numbers } = eq

    if(test(result, numbers)) {
        sum += result
    }
})
console.log(sum); // 3749

//#region b

function binNumberToOperatorSequence2(number) {
    let base3 = (number >>> 0).toString(3)

    base3 = base3.padStart(32, '0')

    base3 = base3.replaceAll('0', '+')
    base3 = base3.replaceAll('1', '*')
    base3 = base3.replaceAll('2', '|')

    return base3
}

function test2(expectedResult, numbers) {
    // 2 numbers = 2^2-1 = 3(dec) = 11(bin)
    let perm = Math.pow(3, numbers.length)


    for (let i = 0; i < perm; i++) {
        // all permutations
        let result = numbers[0]

        const operatorSequence = binNumberToOperatorSequence2(i)
        
        //do stuff
        for (let j = 1; j < numbers.length; j++) {
            const number = numbers[j];
            const operator = operatorSequence[operatorSequence.length - j]
            
            if(operator === "+") {
                result += number
            } else if (operator === '*') {
                result *= number
            } else {
                result = parseInt(result + `${number}`)
            }
        }

        if(result === expectedResult) {
            return true
        }
    }


    return false
}

let sum2 = 0
equations.forEach(eq => {
    const { result, numbers } = eq

    if(test2(result, numbers)) {
        sum2 += result
    }
})
console.log(sum2); // 3749


//#endregion
