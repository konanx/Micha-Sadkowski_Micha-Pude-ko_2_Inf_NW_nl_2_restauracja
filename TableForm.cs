
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantTableManager
{
    public partial class TableForm : Form
    {
        private Table table;
        private ComboBox statusComboBox;
        private ListBox dishListBox;
        private TextBox newDishNameTextBox;
        private NumericUpDown newDishPriceNumericUpDown;
        private Button addDishButton;
        private Button saveButton;

        public TableForm(Table table)
        {
            this.table = table;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.statusComboBox = new ComboBox();
            this.dishListBox = new ListBox();
            this.newDishNameTextBox = new TextBox();
            this.newDishPriceNumericUpDown = new NumericUpDown();
            this.addDishButton = new Button();
            this.saveButton = new Button();

            // 
            // statusComboBox
            // 
            this.statusComboBox.Items.AddRange(new object[] { "Dostępny", "Zajęty", "Zarezerwowany" });
            this.statusComboBox.SelectedItem = table.Status;
            this.statusComboBox.Location = new Point(20, 20);
            this.statusComboBox.Width = 200;

            // 
            // dishListBox
            // 
            this.dishListBox.Items.AddRange(table.Dishes.ToArray());
            this.dishListBox.Location = new Point(20, 60);
            this.dishListBox.Size = new Size(200, 100);

            // 
            // newDishNameTextBox
            // 
            this.newDishNameTextBox.PlaceholderText = "Nazwa potrawy";
            this.newDishNameTextBox.Location = new Point(20, 170);
            this.newDishNameTextBox.Width = 150;

            // 
            // newDishPriceNumericUpDown
            // 
            this.newDishPriceNumericUpDown.DecimalPlaces = 2;
            this.newDishPriceNumericUpDown.Location = new Point(180, 170);
            this.newDishPriceNumericUpDown.Width = 80;

            // 
            // addDishButton
            // 
            this.addDishButton.Text = "Dodaj potrawę";
            this.addDishButton.Location = new Point(20, 210);
            this.addDishButton.Click += AddDishButton_Click;

            // 
            // saveButton
            // 
            this.saveButton.Text = "Zapisz";
            this.saveButton.Location = new Point(20, 250);
            this.saveButton.Click += SaveButton_Click;

            // 
            // TableForm
            // 
            this.ClientSize = new Size(300, 300);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.dishListBox);
            this.Controls.Add(this.newDishNameTextBox);
            this.Controls.Add(this.newDishPriceNumericUpDown);
            this.Controls.Add(this.addDishButton);
            this.Controls.Add(this.saveButton);
        }

        private void AddDishButton_Click(object sender, EventArgs e)
        {
            var newDishName = newDishNameTextBox.Text.Trim();
            var newDishPrice = newDishPriceNumericUpDown.Value;

            if (!string.IsNullOrEmpty(newDishName) && newDishPrice > 0)
            {
                var newDish = new Dish { Name = newDishName, Price = newDishPrice };
                table.Dishes.Add(newDish);
                dishListBox.Items.Add(newDish);
                newDishNameTextBox.Clear();
                newDishPriceNumericUpDown.Value = 0;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            table.Status = statusComboBox.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
