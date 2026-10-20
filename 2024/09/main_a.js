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

    if(element === '.') continue

    for (let j = 0; j < i; j++) {
        const position = parsed[j];
        
        if(position === '.') {
            parsed[j] = parsed[i]
            parsed[i] = '.'
            break
        }
    }
    
}


let checkSum = 0
for (let i = 0; i < parsed.length; i++) {
    if(parsed[i] === '.') continue
    const fileID = parseInt(parsed[i]);
    checkSum += fileID * i
}

console.log(checkSum);


