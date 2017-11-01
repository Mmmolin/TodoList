using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TodoList
{

    class MyForm : Form
    {
        public static List<Todo> todoList = new List<Todo> { };
        private static TableLayoutPanel table;
        private static TableLayoutPanel todoListTable;
        private static TextBox todoInput;
        private static Label itemCount;
        public MyForm()
        {
            table = new TableLayoutPanel
            {
                BackColor = Color.Lavender,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.MinimumSize = new Size(500, 500);
            table.Controls.Add(new Label
            {
                Text = "todos",
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("consolas", 50),
                ForeColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill

            });
            todoInput = new TextBox
            {
                Font = new Font("consolas", 20),
                ForeColor = Color.CornflowerBlue,
                BackColor = Color.Lavender,
                Width = 350,
                Height = 50,
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top
            };
            todoInput.KeyDown += TodoInputEventHandler;
            //todoInput.Multiline = true;
            Controls.Add(table);
            table.Controls.Add(todoInput);

            todoListTable = new TableLayoutPanel
            {
                RowCount = 5,
                Dock = DockStyle.Fill,
                BackColor = Color.Lavender,
                Width = 350,
                Anchor = AnchorStyles.Top,
                AutoSize = true
            };
            todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.Controls.Add(todoListTable);

            TableLayoutPanel statusBar = new TableLayoutPanel
            {
                ColumnCount = 2,
                BackColor = Color.Lavender,
                Width = 350,
                Height = 30,
                Dock = DockStyle.Top,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(statusBar);
            itemCount = new Label
            {
                Text = "0"
            };
            statusBar.Controls.Add(itemCount);

        }
        private static void TodoInputEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = (TextBox)sender;
                todoListTable.Controls.Clear();
                todoList.Add(new Todo
                {
                    Information = textBox.Text
                });

                foreach (Todo todo in todoList)
                {
                    todoListTable.Controls.Add(new Label
                    {
                        Text = todo.Information,
                        TextAlign = ContentAlignment.MiddleLeft,
                        BackColor = Color.Lavender,
                        ForeColor = Color.CornflowerBlue,
                        Font = new Font("consolas", 20),
                        Width = 350,
                        Height = 30,
                        Anchor = AnchorStyles.Top
                    });
                    todoListTable.RowCount = todoListTable.RowCount + 1;
                    todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    
                }                
                itemCount.Text = int.Parse(itemCount.Text) + 1 + "";
                textBox.Clear();
            }
            else
            {

            }
        }
    }

}


