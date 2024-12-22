
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantTableManager
{
    public partial class MainForm : Form
    {
        private List<Table> tables;

        public MainForm()
        {
            InitializeComponent();
            InitializeTables();
            RenderTables();
        }

        private void InitializeTables()
        {
            tables = new List<Table>();
            for (int i = 1; i <= 10; i++)
            {
                tables.Add(new Table { Id = i, Capacity = 4, Status = "Dostępny", Dishes = new List<Dish>() });
            }
        }

        private void RenderTables()
        {
            flowLayoutPanelTables.Controls.Clear();

            foreach (var table in tables)
            {
                var tableButton = new Button
                {
                    Text = $"Stolik {table.Id}\n{table.Status}\nKwota: {table.GetTotalCost()} zł",
                    Width = 100,
                    Height = 100,
                    BackColor = table.Status == "Dostępny" ? Color.LightGreen : Color.LightCoral,
                    Tag = table.Id
                };

                tableButton.Click += TableButton_Click;
                flowLayoutPanelTables.Controls.Add(tableButton);
            }
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            int tableId = (int)button.Tag;
            var table = tables.First(t => t.Id == tableId);

            using (var tableForm = new TableForm(table))
            {
                if (tableForm.ShowDialog() == DialogResult.OK)
                {
                    RenderTables();
                }
            }
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanelTables = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelTables
            // 
            this.flowLayoutPanelTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTables.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTables.Name = "flowLayoutPanelTables";
            this.flowLayoutPanelTables.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanelTables.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanelTables);
            this.Name = "MainForm";
            this.Text = "Zarządzanie stolikami w restauracji";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTables;
    }

    public class Table
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
        public List<Dish> Dishes { get; set; }

        public decimal GetTotalCost()
        {
            return Dishes.Sum(d => d.Price);
        }
    }

    public class Dish
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price} zł";
        }
    }
}
