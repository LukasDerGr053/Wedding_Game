using Core;
using MVVM.Model;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Wedding_Game.MVVM.ViewModel
{
    class WinningViewModel : ObservableObject
    {
        private const string _priceCovered = "PriceCovered.png";
        private const string _price0 = "Price0.png";
        private const string _price1 = "Price1.png";
        private const string _price2 = "Price2.png";

        public bool GoBack { get; set; } = false;

        public Player Winner { get; set; }

        private string _massage = string.Empty;
        public string Massage
        {
            get { return _massage; }
            set
            {
                _massage = value;
                OnPropertyChanged();
            }
        }

        public Image Price0 { get; set; }

        public Image Price1 { get; set; }

        public Image Price2 { get; set; }

        #region Commands
        public RelayCommand BackCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand ChangeImage0Command { get; set; }

        public RelayCommand ChangeImage1Command { get; set; }

        public RelayCommand ChangeImage2Command { get; set; }
        #endregion

        public WinningViewModel(Player player1, Player player2)
        {
            try
            {
                Price0 = new Image();
                Price1 = new Image();
                Price2 = new Image();

                Winner = Game.ReturnWinner(player1, player2);

                Price0.Source = new BitmapImage(new Uri(GetImagePath(_priceCovered)));
                Price1.Source = new BitmapImage(new Uri(GetImagePath(_priceCovered)));
                Price2.Source = new BitmapImage(new Uri(GetImagePath(_priceCovered)));
            }
            catch (Exception e)
            {
                if (Winner == null)
                {
                    Winner = new Player("Nobody");
                }
                Massage = e.Message;
            }

            #region Commands

            BackCommand = new RelayCommand(o =>
            {
                GoBack = true;
                OnPropertyChanged();
            });

            CloseCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

            ChangeImage0Command = new RelayCommand(o =>
            {
                try
                {
                    Price0.Source = new BitmapImage(new Uri(GetImagePath(_price0)));
                }
                catch
                {
                    Massage = "Unfortunately there is no price. Nite";
                }
            });

            ChangeImage1Command = new RelayCommand(o =>
            {
                try
                {
                    Price1.Source = new BitmapImage(new Uri(GetImagePath(_price1)));
                }
                catch
                {
                    Massage = "Unfortunately there is no price. Nite";
                }
            });

            ChangeImage2Command = new RelayCommand(o =>
            {
                try
                {
                    Price2.Source = new BitmapImage(new Uri(GetImagePath(_price2)));
                }
                catch
                {
                    Massage = "Unfortunately there is no price. Nite";
                }
            });
            #endregion
        }


        private string GetImagePath(string imageName)
        {
            string installDirectory = AppContext.BaseDirectory;
            return Path.Combine(installDirectory, imageName);
        }
    }
}
