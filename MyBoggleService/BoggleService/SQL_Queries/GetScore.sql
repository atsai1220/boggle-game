/*
Returns the score of a player given gameId and usertoken
*/
SELECT SUM(Score)
FROM Words
WHERE GameID = '18' AND Player = '48204bcd-5841-42fa-9750-1ff14b1decea'