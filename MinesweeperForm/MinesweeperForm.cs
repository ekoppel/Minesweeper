using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using MinesweeperLib;

namespace MinesweeperForm
{
    public partial class MinesweeperForm : Form
    {
        MinesweeperModel model = new MinesweeperModel(90);
        private readonly int ROW = MinesweeperModel.ROW, COL = MinesweeperModel.COL;
        Button[,] buttons;
        System.Timers.Timer timer;

        public MinesweeperForm()
        {
            InitializeComponent2(ROW, COL);
        }

        private void InitializeComponent2(int rowCount, int columnCount)
        {
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.button1 = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            this.SuspendLayout();

            this.tableLayoutPanel1.ColumnCount = columnCount;

            for (int i = 0; i < columnCount; i++)
                this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / columnCount));

            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";

            this.tableLayoutPanel1.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / rowCount));

            buttons = new Button[rowCount, columnCount];
            Button button;
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++)
                {
                    buttons[i, j] = button = new Button();
                    button.Tag = new MinesweeperModel.Cell(i, j, model.GetBoard());
                    button.Dock = DockStyle.Fill;
                    button.Click += new EventHandler(button1_Click);
                    this.tableLayoutPanel1.Controls.Add(button, i, j);
                }

            this.tableLayoutPanel1.Size = new Size(800, 900);
            this.tableLayoutPanel1.TabIndex = 0;

            this.button1.Dock = DockStyle.Fill;
            this.button1.Location = new Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(200, 300);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(button1_Click);

            this.AutoScaleDimensions = new SizeF(4F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(421, 507);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

            timer = new System.Timers.Timer(30000);
            timer.Elapsed += timerElapsedEvent;
            timer.Start();
        }

        private void timerElapsedEvent(object sender, EventArgs e)
        {
            Random random = new Random();
            int row = random.Next() % ROW;
            int col = random.Next() % COL;

            button1_Click(buttons[row, col], new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
            Button button = sender as Button;
            MinesweeperModel.Cell cell = (MinesweeperModel.Cell)button.Tag;
            MinesweeperModel.Cell[,] board = cell.GetBoard();
            if (model.Inspect(cell.ROW, cell.COL) == null)
            {
                for (int i = 0; i < ROW; i++)
                    for (int j = 0; j < COL; j++)
                        if (board[i, j].IsBomb)
                            board[i, j].Visible = true;

                Application.Restart();
            }

            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                    if (board[i, j].Visible)
                        (tableLayoutPanel1.GetControlFromPosition(i, j) as Button).Text = board[i, j].Display.ToString();
        }
    }
}