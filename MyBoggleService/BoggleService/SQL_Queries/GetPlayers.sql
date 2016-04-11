/*
Get the user tokens of both players given GameId
*/
SELECT Player1, Player2
FROM Games
/*
WHERE GameId = @GameId
*/
WHERE GameId = '2'