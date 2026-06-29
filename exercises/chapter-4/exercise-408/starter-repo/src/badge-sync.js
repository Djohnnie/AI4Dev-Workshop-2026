async function syncBadge(playerId, badgeName, httpClient) {
  try {
    const response = await httpClient.post("/badges", {
      playerId,
      badgeName
    });

    return response.status === 201;
  } catch (error) {
    console.log("badge sync failed", error.message);
    return true;
  }
}

module.exports = {
  syncBadge
};
