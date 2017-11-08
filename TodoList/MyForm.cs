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
        // class variables
        public static List<Todo> todoList = new List<Todo> { };
        private static TableLayoutPanel table;
        private static TableLayoutPanel todoListTable;
        private static TextBox todoInput;
        private static Label itemCount;
        private static Panel statusBar;
        private static Button removeButton;
        public MyForm()
        {
            // Table layout
            table = new TableLayoutPanel
            {
                BackColor = Color.Lavender,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.MinimumSize = new Size(500, 500);
            //todo title
            table.Controls.Add(new Label
            {
                Text = "todos",
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("consolas", 50),
                ForeColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill

            });
            //Textbox for input
            todoInput = new TextBox
            {
                Font = new Font("consolas", 20),
                ForeColor = Color.CornflowerBlue,
                BackColor = Color.Lavender,
                BorderStyle = BorderStyle.None,
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
            //statusbar panel and label
            statusBar = new Panel
            {
                BackColor = Color.Lavender,
                Width = 350,
                Height = 30,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(statusBar);
            itemCount = new Label
            {
                BackColor = Color.Lavender,
                //Text = "0",
                Location = new Point(0, 0),
                Width = 100,
                Height = 30

            };
            statusBar.Controls.Add(itemCount);
        }
        // update display, create panels
        private static void UpdateTodoListDisplay()
        {
            todoListTable.Controls.Clear();
            foreach (Todo todo in todoList)
            {
                Panel todoPanel = new Panel
                {
                    BackColor = Color.Lavender,
                    Width = 350,
                    Height = 30,
                    Tag = todoList.IndexOf(todo)
                };
                todoListTable.Controls.Add(todoPanel);
                CheckBox checkBox = new CheckBox
                {
                    BackColor = Color.Lavender,
                    Width = 20,
                    Height = 30,
                    Location = new Point(0, 0),
                    Anchor = AnchorStyles.Top,
                    Tag = todoList.IndexOf(todo)
                };
                if (todo.IsDone == true)
                {
                    checkBox.Checked = true;
                }
                else
                {
                    checkBox.Checked = false;
                }
                todoPanel.Controls.Add(checkBox);
                checkBox.Click += ClickedCheckBoxEventHandler;
                Label todoText = new Label
                {
                    Text = todo.Information,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(20, 0),
                    BackColor = Color.Lavender,
                    ForeColor = Color.CornflowerBlue,
                    Font = new Font("consolas", 20),
                    Width = 300,
                    Height = 30,
                    Anchor = AnchorStyles.Left,
                    Tag = todoList.IndexOf(todo)
                };
                
                if (todo.IsDone == true)
                {
                    todoText.Font = new Font("consolas", 20, FontStyle.Strikeout);
                }
                todoPanel.Controls.Add(todoText);

                Button removeButton = new Button
                {
                    Text = "X",
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("consolas", 14),
                    FlatStyle = FlatStyle.Flat,
                    Width = 20,
                    Height = 30,
                    Location = new Point(330, 0),
                    Tag = todoList.IndexOf(todo)
                };
                //removeButton.Hide();
                todoPanel.Controls.Add(removeButton);
                removeButton.FlatAppearance.BorderSize = 0;
                removeButton.Click += RemoveButtonEventHandler;
                todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            }
        }

        private static void TodoInputEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string itemOrItems = " items left";
                TextBox textBox = (TextBox)sender;
                todoListTable.Controls.Clear();
                todoList.Add(new Todo
                {
                    Information = textBox.Text
                });
                UpdateTodoListDisplay();
                if (todoList.Count < 2)
                {
                    itemOrItems = " item left";
                }
                itemCount.Text = todoList.Count.ToString() + itemOrItems;
                textBox.Clear();
            }
            else
            {

            }
        }
        private static void RemoveButtonEventHandler(object sender, EventArgs e)
        {
            string itemOrItems = " items left";
            Button info = (Button)sender;
            int todoIndex = (int)info.Tag;
            todoList.RemoveAt(todoIndex);
            UpdateTodoListDisplay();
            if (todoList.Count < 2)
            {
                itemOrItems = " item left";
            }
            itemCount.Text = todoList.Count.ToString() + itemOrItems;

        }
        private static void ClickedCheckBoxEventHandler(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = (int)checkBox.Tag;
            if (todoList[index].IsDone == true)
            {
                todoList[index].IsDone = false;
            }
            else
            {
                todoList[index].IsDone = true;
            }
            UpdateTodoListDisplay();
        }


    }

}


