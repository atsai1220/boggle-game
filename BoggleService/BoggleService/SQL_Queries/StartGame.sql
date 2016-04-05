/*
public void StartGame(string player2Token, int player2TimeLimit, long startTime, string board)
            BoggleGame game = games[gameId];
            game.player2UserToken = player2Token;
            game.timeLimit = (game.timeLimit + player2TimeLimit) / 2;
            game.startTime = startTime;
            game.board = board;
*/
UPDATE Games
SET Player2 = '1234-1234-1234-1234',
 TimeLimit = (TimeLimit + @TimeLimit) / 2,
 StartTime = 14:00
 Board = 'abcdabcdabcdabcd'
WHERE 
todo boarrd and others