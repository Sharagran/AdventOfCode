const fs = require("node:fs");

type PathStep = {
    position: Cord;
    direction: Cord;
};

class Cord {
    x: number;
    y: number;

    constructor(x: number, y: number) {
        this.x = x;
        this.y = y;
    }

    static add(cord1: Cord, cord2: Cord) {
        return new Cord(cord1.x + cord2.x, cord1.y + cord2.y);
    }

    add(cord: Cord) {
        this.x += cord.x
        this.y += cord.y
    }

    isEqual(cord: Cord) {
        return this.x === cord.x && this.y === cord.y;
    }

    rotate() {
        if (this.x !== 0) {
            switch (this.x) {
                // if right go down
                case 1:
                    this.x = 0;
                    this.y = 1;
                    break;

                // if left go up
                case -1:
                    this.x = 0;
                    this.y = -1;
                    break;
            }
        } else {
            switch (this.y) {
                // if down go left
                case 1:
                    this.x = -1;
                    this.y = 0;
                    break;

                // if up go right
                case -1:
                    this.x = 1;
                    this.y = 0;
                    break;
            }
        }
    }

    outsideOfBounds() {
        return (
            this.x < 0 ||
            this.x >= gameField[0].length ||
            this.y < 0 ||
            this.y >= gameField.length
        );
    }
}

class StepHistory {
    history: Array<PathStep>;
    constructor(initialArray = []) {
        this.history = initialArray;
    }

    isIn(step: PathStep) {
        const result = this.history.find(
            (hi) =>
                hi.position.isEqual(step.position) &&
                hi.direction.isEqual(step.direction)
        );

        if(result) {
            return true
        } else {
            return false
        }
    }

    push(step) {
        this.history.push(step)
    }
}

class GameField {
    gf: Array<string[]>;
    guardPosition: Cord

    constructor() {
        this.gf = [];
    }

    push(data) {
        this.gf.push(data)
    }

    copy(): GameField {
        return JSON.parse(JSON.stringify(this))
    }

    moveGuard(cord: Cord) {
        this.guardPosition.add(cord)
    }

    setGuard(cord: Cord) {
        this.guardPosition = cord
    }

    print() {
        this.gf.forEach((row) => {
            let line = "";
            row.forEach((char) => {
                line += char;
            });
            console.log(line);
        });
    }
}

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const gameField = []
    let guardPosition: Cord | null = null;

    data.split("\n").forEach((line: string, y) => {
        const row: string[] = [];
        for (let x = 0; x < line.length; x++) {
            const char = line[x];

            if(char === '\r') continue

            if (char === "^") {
                guardPosition = new Cord(x, y);
            }
            row.push(char);
        }

        if(row.length > 0) gameField.push(row);
    });

    return { gameField, guardPosition };
}

var { gameField, guardPosition } = readData("./input.txt");

const instance = new GameField()
const ogInstance = instance.copy()
instance.gf = gameField
instance.guardPosition = guardPosition

function calculatePath() {
    let direction = new Cord(0, -1); // 1: right/down | -1: left,up
    let position = instance.guardPosition
    const path = new StepHistory([{
        direction,
        position: instance.guardPosition
    }])

    let newTile;
    do {
        let newPos = Cord.add(position, direction)
        newTile = gameField[newPos.y]?.[newPos.x];

        if (newTile === "." || newTile === "^") {
            path.push({
                position: newPos,
                direction: new Cord(direction.x, direction.y),
            });
            position = newPos;
        } else if (newTile === "#") {
            direction.rotate();
        }
    } while (newTile);

    return path;
}

const path = calculatePath();

// render path
path.history.forEach((step) => {
    const cord = step.position;
    // const char = step.direction.x !== 0 ? "-" : "|"
    const char = "X";
    gameField[cord.y][cord.x] = char;
});

let distinctLocations = 0;
gameField.forEach((row) => {
    row.forEach((tile) => {
        if (tile === "X") {
            distinctLocations++;
        }
    });
});

instance.print()
console.log(distinctLocations);

// all possible loops

// check if a path >= 2 is parallel to current position, if so place obstacle in front

const loopObstacles: Cord[] = [];
path.history.forEach((step, stepIndex) => {
    const subInstance = instance.copy();
    const direction = new Cord(step.direction.x, step.direction.y);
    let position = step.position;
    const subPath = new StepHistory([
        {
            direction,
            position,
        },
    ]);

    const potentialObstacleLocation = Cord.add(position, direction)

    if(potentialObstacleLocation.outsideOfBounds()) {
        return
    }

    if(subInstance.gf[potentialObstacleLocation.y][potentialObstacleLocation.x] === "#") {
        return
    }


    subInstance.gf[potentialObstacleLocation.y][potentialObstacleLocation.x] = "#"
    direction.rotate();

    let newTile;
    do {
        let newPos = Cord.add(position, direction)
        const currentStep: PathStep = {
            position: newPos,
            direction: new Cord(direction.x, direction.y)
        }
        newTile = subInstance.gf[newPos.y]?.[newPos.x];

        if (subPath.isIn(currentStep)) {
            loopObstacles.push(potentialObstacleLocation);
            return;
        }

        if (newTile === "." || newTile === "^" || newTile === "X") {
            subPath.push(currentStep);
            position = newPos;
        } else if (newTile === "#") {
            direction.rotate();
        }
    } while (newTile);

    // subInstance.print()
});


loopObstacles.forEach((obstacle) => {
    gameField[obstacle.y][obstacle.x] = "O";
});

instance.print()

console.log(loopObstacles.length);
// 458 too low
// 1915 too high (prob close)
