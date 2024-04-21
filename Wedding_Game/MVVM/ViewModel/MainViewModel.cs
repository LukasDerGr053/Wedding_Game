using Core;
using MVVM.Model;
using Wedding_Game.MVVM.ViewModel;

namespace MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region Views

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public StartViewModel StartVM { get; set; }

        public GameViewModel GameVM { get; set; }

        public WinningViewModel WinningVM { get; set; }
        #endregion

        public MainViewModel()
        {
            StartVM = new StartViewModel();
            StartVM.PropertyChanged += PropertyChangedHandler;

            CurrentView = StartVM;
        }

        private void PropertyChangedHandler(object sender, EventArgs e)
        {
            if (sender == StartVM && StartVM.Start)
            {
                GameVM = new GameViewModel(StartVM.Player1, StartVM.Player2, StartVM.Rounds);
                GameVM.PropertyChanged += PropertyChangedHandler;
                CurrentView = GameVM;
            }
            if (sender == GameVM && GameVM.IsOver)
            {
                WinningVM = new WinningViewModel(GameVM.Player1, GameVM.Player2);
                WinningVM.PropertyChanged += PropertyChangedHandler;
                CurrentView = WinningVM;
                return;
            }
            if (sender == WinningVM && WinningVM.GoBack)
            {
                GameVM.UndoCommand.Execute(null);
                CurrentView = GameVM;
            }
        }
    }
}
