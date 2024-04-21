using Core;
using MVVM.Model;

namespace Wedding_Game.MVVM.ViewModel
{
    public class Round : ObservableObject
    {
        public int Value { get; set; }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
    }

    class GameViewModel : ObservableObject
    {
        public List<Round> Rounds { get; set; }

        private double _progressPlayer1;
        public double ProgressPlayer1
        {
            get { return _progressPlayer1; }
            set
            {
                _progressPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private double _progressPlayer2;
        public double ProgressPlayer2
        {
            get { return _progressPlayer2; }
            set
            {
                _progressPlayer2 = value;
                OnPropertyChanged();
            }
        }

        public bool IsOver { get; set; } = false;

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        #region Commands

        public RelayCommand UndoCommand { get; set; }
        public RelayCommand Player1WinnerCommand { get; set; }

        public RelayCommand Player2WinnerCommand { get; set; }
        #endregion

        public GameViewModel(string player1, string player2, int rounds)
        {
            Player1 = new Player(player1);
            Player2 = new Player(player2);
            Game.Start(rounds);
            CreateRounds();

            UndoCommand = new RelayCommand(o =>
            {
                Game.UndoRound(Player1, Player2);
                IsOver = false;
                UpdateRounds();
            });

            Player1WinnerCommand = new RelayCommand(o =>
            {
                IsOver = Game.EndRound(Player1);
                UpdateRounds();
            });

            Player2WinnerCommand = new RelayCommand(o =>
            {
                IsOver = Game.EndRound(Player2);
                UpdateRounds();
            });
        }

        private void CreateRounds()
        {
            Rounds = new List<Round>();
            for (int i = 1; i <= Game.Rounds; i++)
            {
                Rounds.Add(new Round()
                {
                    Value = i,
                    IsVisible = true,
                });
            }
        }

        private void UpdateRounds()
        {
            for (int i = 0; i < Rounds.Count; i++)
            {
                if (i < Game.ActualRound - 1)
                {
                    Rounds[i].IsVisible = false;
                }
                else
                {
                    Rounds[i].IsVisible = true;
                }
            }
            ProgressPlayer1 = UpdateProgress(Player1);
            ProgressPlayer2 = UpdateProgress(Player2);
            OnPropertyChanged();
        }

        private double UpdateProgress(Player player)
        {
            return (100 / Game.WinCondition()) * player.Points;
        }
    }
}
