using Core;

namespace Wedding_Game.MVVM.ViewModel
{
    class StartViewModel : ObservableObject
    {
        public string Player1 { get; set; } = "Player 1";

        public string Player2 { get; set; } = "Player 2";

        public bool Start { get; set; } = false;

        private int _rounds = 6;
        public int Rounds
        {
            get { return _rounds; }
            set
            {
                if (value > 0)
                {
                    _rounds = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Commands

        public RelayCommand StartCommand { get; set; }

        public RelayCommand PlusCommand { get; set; }

        public RelayCommand MinusCommand { get; set; }
        #endregion

        public StartViewModel()
        {

            StartCommand = new RelayCommand(o =>
            {
                Start = true;
                OnPropertyChanged();
            });

            PlusCommand = new RelayCommand(o =>
            {
                Rounds++;
            });

            MinusCommand = new RelayCommand(o =>
            {
                if (Rounds > 1)
                {
                    Rounds--;
                }
            });

        }
    }
}
