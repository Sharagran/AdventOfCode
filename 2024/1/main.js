const fs = require("node:fs")

function readData() {
    let data = fs.readFileSync("./input.txt", "utf8")
    let list1 = []
    let list2 = []
    data.split('\n').forEach(row => {
        const values = row.split(/\s+/)
        if(values.length > 1) {
            list1.push(parseInt(values[0]))
            list2.push(parseInt(values[1]))
        }
    })

    list1.sort()
    list2.sort()

    return [list1, list2]
}

let [list1, list2] = readData()

// puzzle 1
let totalDistance = 0
for (let i = 0; i < list1.length; i++) {
    totalDistance += Math.abs(list1[i] - list2[i])
}
console.log(totalDistance);

// puzzle 2
let similarities = {}
for (let i = 0; i < list1.length; i++) {
    const element = list1[i];
    
    if(similarities[element]) {
        similarities[element] += element * list2.filter(x => x === element).length
    } else {
        similarities[element] = element * list2.filter(x => x === element).length
    }
}

let similarityScore = Object.values(similarities).reduce((prev, current) => {
    return prev + current
}, 0);
console.log(similarityScore);
