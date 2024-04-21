using Core;

namespace MVVM.Model
{
    public class Player : ObservableObject
    {
        public string Name { get; set; }

        private int _points = 0;
        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                OnPropertyChanged();
            }
        }

        public List<int> Victories { get; set; }

        public Player() { }
        public Player(string name)
        {
            Name = name;
            Victories = new List<int>();
        }

        public void AddVictory(int round)
        {
            Victories.Add(round);
            Points = CalculatePoints();
        }

        public void RemoveVictory(int round)
        {
            Victories.Remove(round);
            Points = CalculatePoints();
        }

        private int CalculatePoints()
        {
            int result = 0;
            if (Victories != null && Victories.Count > 0)
            {

                foreach (int i in Victories)
                {
                    result += i;
                }
            }
            return result;
        }
    }
}
