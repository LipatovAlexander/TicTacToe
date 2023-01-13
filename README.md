# Tic Tac Toe

## Интерфейсы

### API
    
Общая модель для каждого response:

```
{
    "isSuccessful": true | false,
    "data" { } | null,
    "errors": [ "string" ] | null
}
```


#### GET /games:

```
[
    {
        "id": 1,
        "createdAt": "date",
        "hostUsername": "string"
    }
]
```

#### POST /create:

```
{
  "id": 1
}
```

#### POST /game/join:
 
```
{
  "gameId": 1
}
```

#### GET /currentGame

```
{
  "board": [
    [crosses, null, crosses],
    [crosses, crosses, crosses],
    [crosses, crosses, crosses]
  ],
  "mark": "crosses" | "noughts",
  "opponentUsername": "string",
  "state": "notStarted" | "inProgress" | "noughtsWon" | "crossesWon" | "draw",
  "nextMove": "crosses" | "noughts"
}
```

### HUB:

- OnConnected
- OnDisconnedted
- Move(x, y)

### CLIENT:

- OnGameStarted(string opponentUsername)
- OnMoved(x, y, mark, state)
  
  state: "InProgress" | "noughtsWon" | "crossesWon" | "draw"

рейтинг 
комната 
просмотр 
пагинация
