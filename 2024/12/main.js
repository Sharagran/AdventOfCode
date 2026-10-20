const { assert } = require("node:console");
const fs = require("node:fs");

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

    length() {
        return Math.abs(this.x) + Math.abs(this.y)
    }

    // insideBounds() {
    //     const upperBound = this.x < map[0].length && this.y < map.length;
    //     const lowerBound = this.x >= 0 && this.y >= 0;
    //     return upperBound && lowerBound;
    // }

    static add(vec1, vec2) {
        return new Vector2D(vec1.x + vec2.x, vec1.y + vec2.y);
    }

    static sub(vec1, vec2) {
        return new Vector2D(vec1.x - vec2.x, vec1.y - vec2.y);
    }

    isEqual(vec) {
        return this.x === vec.x && this.y === vec.y;
    }
}

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const map = [];

    const lines = data.split("\r\n");
    lines.forEach((line) => {
        if (line.length === 0) return;
        const row = [];
        line.split("").forEach((char) => {
            row.push(char);
        });
        map.push(row);
    });

    return map;
}

const data = readData("test2.txt");

function detectArea(pos, foundTiles = []) {
    foundTiles.push(pos)

    const type = data[pos.y][pos.x];

    const right = new Vector2D(pos.x + 1, pos.y);
    if (
        !foundTiles.some((t) => t.isEqual(right)) &&
        data[right.y]?.[right.x] == type
    ) {
        detectArea(right, foundTiles);
    }

    const left = new Vector2D(pos.x - 1, pos.y);
    if (
        !foundTiles.some((t) => t.isEqual(left)) &&
        data[left.y]?.[left.x] == type
    ) {
        detectArea(left, foundTiles);
    }

    const down = new Vector2D(pos.x, pos.y + 1);
    if (
        !foundTiles.some((t) => t.isEqual(down)) &&
        data[down.y]?.[down.x] == type
    ) {
        detectArea(down, foundTiles);
    }

    const up = new Vector2D(pos.x, pos.y - 1);
    if (!foundTiles.some((t) => t.isEqual(up)) && data[up.y]?.[up.x] == type) {
        detectArea(up, foundTiles);
    }

    return foundTiles;
}

const detectedAreas = [];
data.forEach((row, y) => {
    row.forEach((plot, x) => {
        const startPos = new Vector2D(x, y);
        if (
            detectedAreas.some((areas) =>
                areas.some((vec) => vec.isEqual(startPos))
            )
        ) {
            return;
        }
        const detectedArea = detectArea(startPos);
        detectedAreas.push(detectedArea);
    });
});

function calculatePerimeter(area) {
    let fences = 0
    area.forEach((pos, aIndex) => {
        let neighbours = 0;
        area.forEach((pos2, bIndex) => {
            if (aIndex === bIndex) return;

            if (Math.abs(pos.x - pos2.x) + Math.abs(pos.y - pos2.y) <= 1) {
                neighbours++;
            }
        });

        fences += 4 - neighbours;
    });

    return fences;
}

// b
function calculatePerimeter2(area) {
    let fences = []
    area.forEach((pos, aIndex) => {
        const right = new Vector2D(pos.x+1, pos.y)
        const left = new Vector2D(pos.x-1, pos.y)
        const down = new Vector2D(pos.x, pos.y+1)
        const up = new Vector2D(pos.x, pos.y-1)

        const possibleFences = [right, left, down, up]
        possibleFences.forEach(pf => {
            // fence not on top of plot
            if(!area.some(plot => plot.isEqual(pf))) {
                fences.push(pf)
            }
        })
    });

    return fences;
}

function calculateSides(area) {
    const perimeter = calculatePerimeter2(area)
    console.log(perimeter);

    // sortby x, y
    perimeter.sort(
        function(a, b) {          
           if (a.x === b.x) {
              // Price is only important when cities are the same
              return a.y - b.y;
           }
           return a.x > b.x ? 1 : -1;
        });
    
    let sides = 0
    let prev
    perimeter.forEach(current => {
        if(!prev) {
            prev = current
            return
        }

        if(prev)


        prev = current
    })

    console.log(sides);
    return sides
}

function calculatePrice() {
    let totalPrice = 0
    detectedAreas.forEach(area => {
        calculateSides(area)

        const perimeter = calculatePerimeter(area)
        const price = area.length * perimeter
        totalPrice += price
    })

    return totalPrice
}

const price = calculatePrice()
console.log(price);
// assert(price === 436)

// 347272 too low
