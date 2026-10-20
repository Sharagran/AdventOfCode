const fs = require("node:fs");

function readData(path) {
    let data = fs.readFileSync(path, "utf8");

    const keys = [];
    const locks = []

    const entries = data.split(/\n\s*\n/);
    entries.forEach((entry) => {
        const lines = entry.split('\r\n').filter(line => line.length > 0)
        const isKey = lines[0].includes(".")

        const heights = new Array(lines.length-2).fill(0)

        let heightLines = lines.filter((_, i) => i !== 0 && i !== lines.length-1)

        heightLines.forEach(line => {
            line.split("").forEach((el, j) => {
                if(el === '#') {
                    heights[j] += 1
                } else {
                    return
                }
            })
        })
        
        if(isKey) {
            keys.push(heights)
        } else {
            locks.push(heights)
        }
    });

    return [keys, locks];
}

const [keys, locks] = readData("input.txt");

let pairs = 0
locks.forEach(lock => {
    keys.forEach(key => {
        for (let i = 0; i < lock.length; i++) {
            if(lock[i] + key[i] > 5) {
                return
            }
        }
        pairs++
    })
})

console.log(pairs);
