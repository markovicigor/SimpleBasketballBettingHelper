export function getMaxBetGames() {
    return fetch('https://localhost:7059/getMaxBetGames')
      .then(data => data.json())
  }
  export function getAwayTeam(home) {
    return fetch(`https://localhost:7059/getAwayTeam?homeTeam=${home}`)
      .then(data => data.json())
  }
  export function getPercents() {
    return fetch('https://localhost:7059/getPointsByTeam')
      .then(data => data.json())
  }
