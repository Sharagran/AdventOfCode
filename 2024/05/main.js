const fs = require("node:fs")

function readData() {
    let data = fs.readFileSync("./input.txt", "utf8")

    const ordering = []
    const updates = []

    data.split('\n').forEach(line => {
        if(line.includes('|')) {
            const numbers = line.split("|")
            ordering.push({
                before: parseInt(numbers[0]),
                after: parseInt(numbers[1])
            })
        } else if (line.includes(',')) {
            const update = []
            line.split(',').forEach(number => {
                update.push(parseInt(number))
            })
            updates.push(update)
        }
    })

    return {
        ordering,
        updates
    }
}

var { ordering, updates } = readData()

// dict=hashmap
// key = number, value = array of number not allowed before key
const orderingGrouped = {}
ordering.forEach(order => {
    if(orderingGrouped[order.before]) {
        orderingGrouped[order.before].push(order.after)
    } else {
        orderingGrouped[order.before] = [order.after]
    }
})

const verifiedUpdates = []

updates.forEach(update => {
    const seenValues = []
    let valid = true
    for (let i = 0; i < update.length; i++) {
        const page = update[i];

        // orderingGrouped[page] updates die nach update "page" kommen müssen
        // wenn ein wert bereits gesehen wurde, welcher danach kommen muss:
        if(seenValues.some((value) => orderingGrouped[page].includes(value))) {
            valid = false
            break
        }

        seenValues.push(page) // 1
    }

    verifiedUpdates.push({
        update,
        valid
    })
})

const validUpdates = verifiedUpdates.filter(update => update.valid).map(x => x.update)

let sum = 0
validUpdates.forEach(update => {
    let middleElement = update[Math.floor(update.length/2)]
    sum += middleElement
})

console.log(sum);

// part 2

const unvalidUpdates = verifiedUpdates.filter(update => !update.valid).map(x => x.update)

sum = 0
unvalidUpdates.forEach(update => {
    //order
    update.sort((a, b) => {
        if(orderingGrouped[a].includes(b)) {
            return -1
        } else {
            return 1
        }
    })

    // get middle element
    let middleElement = update[Math.floor(update.length/2)]
    sum += middleElement
})

console.log(sum);
