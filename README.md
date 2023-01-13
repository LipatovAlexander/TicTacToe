# Tic Tac Toe

## Интерфейсы

### API
    
Общая модель для каждого response:

```
{
    "isSuccessful": true | false,
    "data" { } | null,
    "errors": [ "string" ]
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

```
{
    "mark": "crosses" | "noughts"
}
```

#### POST /join:
 
```
{
    "gameId": 1
}
```

```
{
    "mark": "crosses" | "noughts"
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