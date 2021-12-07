const aoc = require("../../helpers/aoc");
const { readFileContent } = require("../../helpers/input");

// I got stuck on the second pard. I found some solution here:
// https://github.com/Im-Beast/advent_of_code/blob/main/2021/day_6.ts   
const efficientlySimulate = (lanternfish, days) => {
  const timers = Array(9).fill(0);
  for (const fishTimer of lanternfish) {
    timers[fishTimer]++;
  }
  while (--days >= 0) {
    const zeros = timers.shift();
    timers[6] += zeros;
    timers[8] ||= zeros;
  }
  return timers.reduce((a, b) => a + b);
}

const naivelySimulate = (lanternfish, days = 10) => {
    let day = 0;
    const history = [];
    while (day < days) {
        const newLanternFish = [];
        for (let i = 0; i < lanternfish.length; i++) {
            if (lanternfish[i]-- === 0) {
                lanternfish[i] = 6;
                newLanternFish.push(8);
            }
        }
        if (newLanternFish.length > 0)
            lanternfish = lanternfish.concat(newLanternFish);
        history.push([...lanternfish]);
        day++;
    }
    return history.at(-1).length;
}


aoc.day(6, "Lanternfish", (inputFile) => {
    readFileContent(inputFile, (content) => {
        const lanternfish = content.split(",");
        const firstPart = naivelySimulate([...lanternfish], 18);
        const secondPart = efficientlySimulate([...lanternfish], 256);

        console.log(`there are a total of ${firstPart} fish.`);
        console.log(`there are a total of ${secondPart} fish.`);
    });
});