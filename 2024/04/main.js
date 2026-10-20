const fs = require("node:fs")

function readData() {
    let data = fs.readFileSync("./input.txt", "utf8")

    data = data.split('\n')
    // remove last empty line
    data = data.slice(0, data.length-1)

    return data
}

var data = readData()

let xmasCount = 0
let combo = 0
// horizontal
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        const element = data[i][j];
        
        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}

// ABC -> CBA
// ABC
// BCA
// CAB
// ->
// CAB

// horizontal reverse
for (let i = 0; i < data.length; i++) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        const element = data[i][j];
        
        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}

// vertical
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        const element = data[j][i];
        
        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}

// vertical reverse
for (let i = 0; i < data.length; i++) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        const element = data[j][i];
        
        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}

// diagonal
// major
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        if(i+j >= data.length) {
            continue
        }

        const element = data[i+j][j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
// minor
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        if(j+i >= data[i].length) {
            continue
        }

        const element = data[i][j+i];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
//#region other diagonal (yellow) v1 old
// for (let i = 0; i < data.length; i++) {
//     for (let j = 0; j < data[i].length; j++) {
//         if(j-i < 0) {
//             continue
//         }

//         const element = data[i][i-j];
        

//         if((combo == 0 || combo == 1) && element == 'X') {
//             combo = 1
//         } else if (combo == 1 && element == "M") {
//             combo = 2
//         } else if (combo == 2 && element == 'A') {
//             combo = 3
//         } else if (combo == 3 && element == "S") {
//             xmasCount++
//             combo = 0
//         } else {
//             combo = 0
//         }
//     }
//     combo = 0
// }
// // reverse?
// for (let i = 0; i < data.length; i++) {
//     for (let j = 0; j < data[i].length; j++) {
//         if(i-j < 0) {
//             continue
//         }

//         const element = data[i-j][j];
        

//         if((combo == 0 || combo == 1) && element == 'X') {
//             combo = 1
//         } else if (combo == 1 && element == "M") {
//             combo = 2
//         } else if (combo == 2 && element == 'A') {
//             combo = 3
//         } else if (combo == 3 && element == "S") {
//             xmasCount++
//             combo = 0
//         } else {
//             combo = 0
//         }
//     }
//     combo = 0
// }
//#endregion

//#region other diagonal (yellow) v2
// major
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        if (i+j >= data.length) {
            continue
        }
        const element = data[i+j][data.length-j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
// minor
for (let i = 0; i < data.length; i++) {
    for (let j = 0; j < data[i].length; j++) {
        if (data.length-j >= data.length) {
            continue
        }
        const element = data[data.length-j][i+j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
//#endregion

//#region diagonal reverse
// red
for (let i = data.length-1; i >= 0 ; i--) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        if(i+j >= data.length) {
            continue
        }

        const element = data[i+j][j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
for (let i = data.length-1; i >= 0 ; i--) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        if(j+i >= data[i].length) {
            continue
        }

        const element = data[i][j+i];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}

// yellow
for (let i = data.length-1; i >= 0 ; i--) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        if(i+j >= data.length) {
            continue
        }

        const element = data[i+j][data.length-j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
for (let i = data.length-1; i >= 0 ; i--) {
    for (let j = data[i].length-1; j >= 0 ; j--) {
        if (data.length-j >= data.length) {
            continue
        }

        const element = data[data.length-j][i+j];

        if((combo == 0 || combo == 1) && element == 'X') {
            combo = 1
        } else if (combo == 1 && element == "M") {
            combo = 2
        } else if (combo == 2 && element == 'A') {
            combo = 3
        } else if (combo == 3 && element == "S") {
            xmasCount++
            combo = 0
        } else {
            combo = 0
        }
    }
    combo = 0
}
//#endregion



console.log(xmasCount);
// 1405 too low
// 2104 too low
