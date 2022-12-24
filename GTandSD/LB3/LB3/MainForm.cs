namespace LB3
{
    public partial class MainForm : Form
    {
        public TaskModel Model { get; init; } = new TaskModel();

        public MainForm()
        {
            InitializeComponent();

            UpdateGridControl();
        }

        void UpdateGridControl()
        {
            var showBest = m_bestStrategyBox.Checked;
            var stratA = Model.StrategiesA;
            var stratB = Model.StrategiesB;

            m_reportView.BeginUpdate();

            m_reportView.Items.Clear();
            m_reportView.Columns.Clear();

            m_reportView.Columns.Add(new ColumnHeader() { Text = string.Empty });

            for (var i = 0; i < Model.N; i++)
            {
                m_reportView.Columns.Add(new ColumnHeader() { Text = $"B{i}" });
            }

            if (showBest)
            {
                m_reportView.Columns.Add(new ColumnHeader() { Text = "A mins" });
            }

            for (var j = 0; j < Model.M; j++)
            {
                var item = new ListViewItem() 
                {
                    Text = $"A{j}",
                    UseItemStyleForSubItems = false
                };

                var highliteItem = showBest &&
                                   stratA.BestNums.Any(b => b == j);

                for (var i = 0; i < Model.N; i++)
                {
                    var subItem = new ListViewItem.ListViewSubItem() 
                    {
                        Text = $"{Model.Matrix[j, i]}" 
                    };
                    var highliteSubItem = showBest &&
                                          stratB.BestNums.Any(b => b == i);
                    if (highliteItem)
                    {
                        subItem.BackColor = Color.LightPink;
                    }
                    else if (highliteSubItem)
                    {
                        subItem.BackColor = Color.LightBlue;
                    }

                    item.SubItems.Add(subItem);
                }

                if (showBest)
                {
                    var subItem = new ListViewItem.ListViewSubItem()
                    {
                        Text = $"{stratA.Values[j]}"
                    };

                    if (highliteItem)
                    {
                        subItem.BackColor = Color.LightPink;
                    }

                    item.SubItems.Add(subItem);
                }

                m_reportView.Items.Add(item);
            }

            if (showBest)
            {
                var item = new ListViewItem()
                {
                    Text = "B maxs",
                    UseItemStyleForSubItems = false
                };

                for (var i = 0; i < Model.N; i++)
                {
                    var subItem = new ListViewItem.ListViewSubItem()
                    {
                        Text = $"{stratB.Values[i]}"
                    };
                    var highliteSubItem = showBest &&
                                          stratB.BestNums.Any(b => b == i);
                    if (highliteSubItem)
                    {
                        subItem.BackColor = Color.LightBlue;
                    }

                    item.SubItems.Add(subItem);
                }

                m_reportView.Items.Add(item);
            }

            m_reportView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            m_reportView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            m_reportView.EndUpdate();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            m_fingersTextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.TotalFingers)}");

            m_fingersATextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.FingersA)}");
            m_numbersATextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.NumbersA)}");

            m_fingersBTextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.FingersB)}");
            m_numbersBTextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.NumbersB)}");

            m_scoresATextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.ScoresA)}");
            m_scoresBTextBox.DataBindings.Add("Text", this, $"{nameof(Model)}.{nameof(Model.ScoresB)}");
        }

        private void OnFingersChanged(object sender, EventArgs e)
        {
            UpdateGridControl();
        }

        private void OnFingersKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Validate();
            }
        }

        private void OnShowStrategy(object sender, EventArgs e)
        {
            UpdateGridControl();
        }

        private void OnFingersAValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var text = (sender as TextBox)!.Text;
            var valid = int.TryParse(text, out int value) &&
                       (value >= 1 && value <= Model.TotalFingers);
            if (!valid)
            {
                e.Cancel = true;
                (sender as TextBox)!.SelectAll();

                MessageBox.Show($"Значення повинні бути у діапазоні {1}...{Model.TotalFingers}");
            }
        }

        private void OnStep(object sender, EventArgs e)
        {
            Model.Step();

            m_fingersBTextBox.DataBindings["Text"].ReadValue();
            m_numbersBTextBox.DataBindings["Text"].ReadValue();

            m_scoresATextBox.DataBindings["Text"].ReadValue();
            m_scoresBTextBox.DataBindings["Text"].ReadValue();
        }
    }
}