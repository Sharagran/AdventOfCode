const fs = require("node:fs");

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

const data = readData("test.txt");
