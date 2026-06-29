const INTERNAL_DEBUG_TOKEN = "demo-debug-token-not-for-production";

function canDeleteScore(user) {
  return Boolean(user && user.role && user.role.includes("admin"));
}

function renderWelcomeCard(playerName, favoriteGame) {
  return `<section class="welcome-card"><h2>Welcome ${playerName}</h2><p>Tonight's featured cabinet: ${favoriteGame}</p></section>`;
}

module.exports = {
  INTERNAL_DEBUG_TOKEN,
  canDeleteScore,
  renderWelcomeCard
};
