const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const map = [];

    const lines = data.split("\n")
    lines.forEach(line => {
        if(line.length === 0) return
        const row = []
        line.split("").forEach(char => {
            row.push(parseInt(char))
        })
        map.push(row)
    })

    return map;
}

const data = readData('input.txt')

console.log(data);

let totalScore = 0
for (let i = 0; i < data.length; i++) {
    const row = data[i];
    for (let j = 0; j < row.length; j++) {
        const element = row[j];
        if(element === 0) {
            const score = traversePath(0, i, j)
            totalScore += score
        }
    }
}

console.log(totalScore);



function getNeighbours(rowIndex, colIndex) {
    let n = []

    n.push({
        value: data[rowIndex-1]?.[colIndex],
        rowIndex: rowIndex-1,
        colIndex: colIndex,
    })
    n.push({
        value: data[rowIndex+1]?.[colIndex],
        rowIndex: rowIndex+1,
        colIndex: colIndex,
    })
    n.push({
        value: data[rowIndex]?.[colIndex-1],
        rowIndex: rowIndex,
        colIndex: colIndex-1,
    })
    n.push({
        value: data[rowIndex]?.[colIndex+1],
        rowIndex: rowIndex,
        colIndex: colIndex+1,
    })

    n = n.filter(x => x !== undefined)

    return n
}

// recusrive function: find paths which lead to 9 and return the amount of those paths
function traversePath(number, rowIndex, colIndex) {
    // check neighbors

    // Currently calculates diffrent paths to reach number

    let score = 0
    getNeighbours(rowIndex, colIndex).forEach(n => {
        if(n.value === number+1) {
            if(n.value === 9) {
                score += 1
            } else {
                score += traversePath(n.value, n.rowIndex, n.colIndex)
            }
        } else {
            score += 0
        }
    })

    return score
}
