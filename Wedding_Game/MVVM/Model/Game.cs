namespace MVVM.Model
{
    public static class Game
    {
        public static int Rounds { get; private set; }
        public static int ActualRound { get; private set; }

        public static bool IsOver(int points = 0)
        {
            if (Rounds < ActualRound) { return true; }
            else if (points >= WinCondition()) { return true; }
            return false;
        }

        public static int WinCondition()
        {
            int result = 0;
            for (int i = 1; i <= Rounds; i++)
            {
                result += i;
            }
            return (int)(result / 2) + 1;
        }

        /// <summary>
        /// Ends the current round and adds points to the winner.
        /// </summary>
        /// <param name="player">Player who won the current round.</param>
        /// <returns>Game is Over</returns>
        public static bool EndRound(Player player)
        {
            player.AddVictory(ActualRound);
            ActualRound++;
            return IsOver(player.Points);
        }

        /// <summary>
        /// Determines the winner of the game and returns it.
        /// </summary>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        /// <returns>Winner of the Game</returns>
        /// <exception cref="ArgumentNullException">One or more players do not exist.</exception>
        /// <exception cref="Exception">No winner could be determined.</exception>
        public static Player ReturnWinner(Player player1, Player player2)
        {
            if (player1 == null || player2 == null)
            {
                throw new ArgumentNullException("A non-existent player!");
            }
            else if (player1 == player2)
            {
                throw new Exception("A game against yourself!");
            }
            else if (player1.Points == player2.Points)
            {
                throw new Exception("The game ended in a tie!");
            }
            else if (player1.Points > player2.Points)
            {
                return player1;
            }
            else if (player1.Points < player2.Points)
            {
                return player2;
            }
            else
            {
                throw new Exception("An unexpected error occurred!");
            }
        }

        /// <summary>
        /// Undoes the last round.
        /// </summary>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        public static void UndoRound(Player player1, Player player2)
        {
            ActualRound--;
            player1.RemoveVictory(ActualRound);
            player2.RemoveVictory(ActualRound);
        }

        /// <summary>
        /// Start a new Game.
        /// </summary>
        /// <param name="rounds">Number of rounds</param>
        public static void Start(int rounds)
        {
            Rounds = rounds;
            ActualRound = 1;
        }
    }
}
