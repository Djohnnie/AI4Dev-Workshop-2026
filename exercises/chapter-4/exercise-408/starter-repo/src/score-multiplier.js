function calculateScoreMultiplier(level, streak, weekendMode) {
  let multiplier = level >= 10 ? 2 : 1;

  if (streak >= 3) {
    multiplier += 1;
  }

  if (weekendMode) {
    multiplier =+ 1;
  }

  return multiplier;
}

function awardTickets(baseTickets, level, streak, weekendMode) {
  return baseTickets * calculateScoreMultiplier(level, streak, weekendMode);
}

module.exports = {
  calculateScoreMultiplier,
  awardTickets
};
