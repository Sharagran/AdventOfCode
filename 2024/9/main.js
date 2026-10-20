// b

const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const diskMap = [];

    const line = data.split("\r\n")[0];
    line.split("").forEach((char) => {
        if(char.match(/\d/))
            diskMap.push(char);
    });

    return diskMap;
}

const data = readData('input.txt')

let file = true
let parsed = []
for (let i = 0; i < data.length; i++) {
    const element = parseInt(data[i]);

    for (let j = 0; j < element; j++) {
        if(file) {
            parsed.push(i/2)
        } else {
            parsed.push('.')
        }
    }


    file = !file
}

console.log(parsed);


for (let i = parsed.length-1; i >= 0; i--) {
    const element = parsed[i];
    const elementSize = parsed.filter(x => x === element).length

    if(element === '.') continue

    let foundFreeSpace = 0
    for (let j = 0; j < i; j++) {
        const position = parsed[j];
        
        if(position === '.') {
            foundFreeSpace++
        } else {
            foundFreeSpace = 0
        }

        if(foundFreeSpace >= elementSize) {
            for (let k = 0; k < elementSize; k++) {
                parsed[j-k] = parsed[i-k]
                parsed[i-k] = '.'
            }
            break
        }
    }
}

console.log(parsed);



let checkSum = 0
for (let i = 0; i < parsed.length; i++) {
    if(parsed[i] === '.') continue
    const fileID = parseInt(parsed[i]);
    checkSum += fileID * i
}

console.log(checkSum);


