// b

// # anti-nodes can overlap antennas

const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const map = [];
    const antennas = {};

    data.split("\r\n").forEach((line, y) => {
        if (line.length === 0) return;

        const row = [];
        line.split("").forEach((char, x) => {
            row.push(char);

            if (char !== ".") {
                if (antennas[char]) {
                    antennas[char].push(new Vector2D(x, y));
                } else {
                    antennas[char] = [new Vector2D(x, y)];
                }
            }
        });

        map.push(row);
    });

    return { map, antennas };
}

class Vector2D {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    add(vec) {
        this.x += vec.x;
        this.y += vec.y;
    }

    sub(vec) {
        this.x -= vec.x;
        this.y -= vec.y;
    }

    insideBounds() {
        const upperBound = this.x < map[0].length && this.y < map.length;
        const lowerBound = this.x >= 0 && this.y >= 0;
        return upperBound && lowerBound;
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

const { map, antennas } = readData("./input.txt");

// calc vector between all points of one freqency
// apply vector to 2nd/other/greater point, invert vec and apply to 1st/smaller point

const antiNodes = [];
for (const freq in antennas) {
    const freqAntennas = antennas[freq];

    for (let i = 0; i < freqAntennas.length; i++) {
        const a = freqAntennas[i];

        if(freqAntennas.length > 1) {
            antiNodes.push(new Vector2D(a.x, a.y))
        }

        for (let j = 0; j < freqAntennas.length; j++) {
            const b = freqAntennas[j];

            if (i === j) {
                continue;
            }

            const diff = Vector2D.sub(a, b);


            const aVec = Vector2D.add(a, diff)
            do {
                antiNodes.push(new Vector2D(aVec.x, aVec.y))
                aVec.add(diff)
            } while (aVec.insideBounds());

            
            const bVec = Vector2D.sub(b, diff)
            do {
                antiNodes.push(new Vector2D(bVec.x, bVec.y))
                bVec.sub(diff)
            } while (bVec.insideBounds());

        }
    }
}

const antiNodesWithinBounds = antiNodes.filter((node) => node.insideBounds());

const antiNodesUnique = [];
antiNodesWithinBounds.forEach((node) => {
    // is already inside
    if (!antiNodesUnique.some((n) => n.x === node.x && n.y === node.y)) {
        antiNodesUnique.push(node);
    }
});

console.log(antiNodesUnique.length); // 807 too low

// update map
for (let y = 0; y < map.length; y++) {
    const row = map[y];
    for (let x = 0; x < row.length; x++) {
        if (antiNodesUnique.find((node) => node.x === x && node.y === y)) {
            // dont print over antenna
            if (map[y][x] === ".") {
                map[y][x] = "#";
            }
        }
    }
}

// print map
// map.forEach(row => {
//     let s = ""
//     row.forEach(element => {
//         s += element
//     })
//     console.log(s)
// })
