const test = require("node:test");
const assert = require("node:assert/strict");

const {
  awardTickets
} = require("../src/score-multiplier");

test("awards double tickets for experienced players", () => {
  assert.equal(awardTickets(100, 10, 0, false), 200);
});
