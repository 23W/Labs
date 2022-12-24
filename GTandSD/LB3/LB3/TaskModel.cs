namespace LB3
{
    public class TaskModel
    {
        #region Nested classes

        public struct Strategies
        {
            public List<int> Values { get; set; }
            public List<int> BestNums { get; set; }

        }

        #endregion

        #region Properties

        public int TotalFingers
        {
            get => m_fingers;
            set { m_fingers = value; InitMatrix(); }
        }

        public int[,] Matrix { get; private set; } = { };

        public int M => Matrix.GetLength(0);

        public int N => Matrix.GetLength(1);

        public Strategies StrategiesA => CalcMaxMinStrategyA();

        public Strategies StrategiesB => CalcMinMaxStrategyB();

        public int ScoresA { get; private set; } = 0;

        public int ScoresB { get; private set; } = 0;

        public int FingersA { get; set; } = 1;

        public int NumbersA { get; set; } = 1;

        public int FingersB { get; private set; } = 0;

        public int NumbersB { get; private set; } = 0;

        #endregion

        #region Construction

        public TaskModel()
        {
            m_fingers = 2;

            InitMatrix();
        }

        #endregion

        #region Methods

        public void Step()
        {
            int fingerA = FingersA;
            int numberA = NumbersA;
            var fingerB = m_randomizer.Next(1, m_fingers + 1);
            var numberB = m_randomizer.Next(1, m_fingers + 1);
            FingersB = fingerB;
            NumbersB = numberB;

            var winA = numberA == fingerB;
            var winB = numberB == fingerA;

            if ((winA && winB) || (!winA && !winB))
            {
                // actually nothing
                ScoresA += 0;
                ScoresB += 0;
            }
            else if (winA)
            {
                ScoresA += fingerA + fingerB;
            }
            else
            {
                ScoresB += fingerA + fingerB;
            }
        }

        #endregion

        #region Helper Methods

        void InitMatrix()
        {
            ScoresA = 0;
            ScoresB = 0;
            FingersA = 1;
            NumbersA = 1;
            FingersB = 0;
            NumbersB = 0;

            var size = m_fingers * m_fingers;

            Matrix = new int[size, size];

            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < M; j++)
                {
                    var fingerA = (j / m_fingers) + 1;
                    var numberA = (j % m_fingers) + 1;

                    var fingerB = (i / m_fingers) + 1;
                    var numberB = (i % m_fingers) + 1;

                    var winA = numberA == fingerB;
                    var winB = numberB == fingerA;

                    if ((winA && winB) || (!winA && !winB))
                    {
                        Matrix[j, i] = 0;
                    }
                    else if (winA)
                    {
                        Matrix[j, i] = fingerA + fingerB;
                    }
                    else
                    {
                        Matrix[j, i] = -(fingerA + fingerB);
                    }
                }
            }
        }

        Strategies CalcMaxMinStrategyA()
        {
            Strategies res = new Strategies()
            {
                Values = new List<int>(),
                BestNums = new List<int>()
            };

            for (var j = 0; j < M; j++)
            {
                var min = Matrix[j, 0];

                for (var i = 0; i < N; i++)
                {
                    if (min > Matrix[j, i])
                    {
                        min = Matrix[j, i];
                    }
                }

                res.Values.Add(min);
            }

            var bestValue = res.Values.Max();
            for (var i = 0; i < res.Values.Count; i++)
            {
                if (res.Values[i] == bestValue)
                {
                    res.BestNums.Add(i);
                }
            }

            return res;
        }

        Strategies CalcMinMaxStrategyB()
        {
            Strategies res = new Strategies()
            {
                Values = new List<int>(),
                BestNums = new List<int>()
            };

            for (var i = 0; i < N; i++)
            {
                var max = Matrix[0, i];

                for (var j = 0; j < M; j++)
                {
                    if (max < Matrix[j, i])
                    {
                        max = Matrix[j, i];
                    }
                }

                res.Values.Add(max);
            }

            var bestValue = res.Values.Min();
            for (var i = 0; i < res.Values.Count; i++)
            {
                if (res.Values[i] == bestValue)
                {
                    res.BestNums.Add(i);
                }
            }

            return res;
        }

        #endregion

        #region Members

        Random m_randomizer = new Random();

        int m_fingers = 2;

        #endregion
    }
}
