const fs = require("node:fs");

let startPosid = 0
class Vector2D {
    constructor(x, y) {
        this.x = x;
        this.y = y;
        this.id = startPosid
        startPosid++
    }

    add(vec) {
        this.x += vec.x;
        this.y += vec.y;
    }

    sub(vec) {
        this.x -= vec.x;
        this.y -= vec.y;
    }

    static add(vec1, vec2) {
        return new Vector2D(vec1.x + vec2.x, vec1.y + vec2.y);
    }

    static sub(vec1, vec2) {
        return new Vector2D(vec1.x - vec2.x, vec1.y - vec2.y);
    }

    invertSign() {
        this.x *= -1;
        this.y *= -1;
    }
}

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

//trailhead = 0
// score = 9's welche von einer 0 erreichbar sind
// total score = summe alle trailhead scores

const reachable = {}
for (let i = 0; i < data.length; i++) {
    const row = data[i];
    for (let j = 0; j < row.length; j++) {
        const element = row[j];
        if(element === 0) {
            traversePath(0, i, j, new Vector2D(j, i))
        }
    }
}

// const unique = [];
let score = 0
for (const startPosid in reachable) {
    const element = reachable[startPosid];

    let unique = []
    element.forEach((node) => {
        // is already inside
        if (!unique.some((n) => n.x === node.x && n.y === node.y)) {
            unique.push(node);
        }
    });
    score += unique.length
}

console.log(score);


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
function traversePath(number, rowIndex, colIndex, startPos) {
    // check neighbors

    getNeighbours(rowIndex, colIndex).forEach(n => {
        if(n.value === number+1) {
            if(n.value === 9) {
                if(reachable[startPos.id]) {
                    reachable[startPos.id].push({x: n.colIndex, y: n.rowIndex})
                } else {
                    reachable[startPos.id] = [{x: n.colIndex, y: n.rowIndex}]
                }
            } else {
                traversePath(n.value, n.rowIndex, n.colIndex, startPos)
            }
        }
    })
}
