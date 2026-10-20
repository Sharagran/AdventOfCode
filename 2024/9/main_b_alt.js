const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const diskMap = [];

    const line = data.split("\r\n")[0];
    line.split("").forEach((char) => {
        if(char.match(/\d/))
            diskMap.push(parseInt(char));
    });

    return diskMap;
}

let data = readData('test.txt')

data = data.map((x, i) => {
    return {
        size: x,
        fileId: i % 2 === 0 ? i / 2 : -1
    }
})

// even = file, uneven = free space
const lastFileIndex = data[data.length-1].fileId !== -1 ? data.length - 1 : data.length - 2
for (let i = lastFileIndex; i >= 0; i-=2) {
    const fileSize = data[i].size;
    
    for (let j = 1; j < i; j+=2) {
        const freeSpaceSize = data[j].size

        if(freeSpaceSize >= fileSize) {
            // move file to free space
            data[j] = data[i]
            // delete file at old location
            data[i] = 0
            // increment free space indactor before deleted file
            data[i-1] += data[i]
        }
    }

}

console.log(data);

let checkSum = 0
for (let i = 0; i < data.length; i++) {
    if(i % 2 !== 0) continue // free space
    for (let j = 0; j < data[i]; j++) {
        checkSum += j * data[i]
    }
}

console.log(checkSum);
