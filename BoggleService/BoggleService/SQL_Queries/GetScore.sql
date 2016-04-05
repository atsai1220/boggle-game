/*
Returns the score of a player given gameId and usertoken
*/
SELECT SUM(Score)
FROM Words
WHERE GameID = '2' AND Player = '1234-1234-1234'