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
        private static IEnumerable<Todo> sortList = todoList;
        private static string sortChoice { get; set; }

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
            Controls.Add(table);

            table.Controls.Add(new Label
            {
                Text = "todos",
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("consolas", 50),
                ForeColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill
            });

            Panel sortButtonsPanel = new Panel
            {
                BackColor = Color.Lavender,
                Width = 350,
                Height = 20,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(sortButtonsPanel);
            Button allButton = new Button
            {
                Text = "All",
                Location = new Point(50, 0),
                Width = 40,
                Height = 20
            };
            Button activeButton = new Button
            {
                Text = "Active",
                Location = new Point(100, 0),
                Width = 50,
                Height = 20
            };
            sortButtonsPanel.Controls.Add(activeButton);
            activeButton.Click += SortButtonClickEventHandler;
            sortButtonsPanel.Controls.Add(allButton);
            Button completedButton = new Button
            {
                Text = "Completed",
                Location = new Point(160, 0),
                Width = 60,
                Height = 20
            };
            sortButtonsPanel.Controls.Add(completedButton);
            completedButton.Click += SortButtonClickEventHandler;

            allButton.Click += SortButtonClickEventHandler;
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
            table.Controls.Add(todoInput);
            table.AutoScroll = true;
            todoInput.KeyDown += TodoInputEventHandler;

            todoListTable = new TableLayoutPanel
            {
                RowCount = 5,
                Dock = DockStyle.Fill,
                BackColor = Color.Lavender,
                Width = 350,
                Anchor = AnchorStyles.Top,
                AutoSize = true
            };
            table.Controls.Add(todoListTable);
            todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            

        }

        private static void CreateTodoListDisplay()
        {
            todoListTable.Controls.Clear();
            foreach (Todo todo in sortList)
            {
                Panel todoPanel = new Panel
                {
                    BackColor = Color.Lavender,
                    Width = 350,
                    Height = 30,
                    Tag = todoList.IndexOf(todo)
                };
                CheckBox checkBox = new CheckBox
                {
                    BackColor = Color.Lavender,
                    Width = 20,
                    Height = 30,
                    Location = new Point(0, 0),
                    Anchor = AnchorStyles.Top,
                    Tag = todoList.IndexOf(todo)
                };
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
                Button removeButton = new Button
                {
                    Text = "X",
                    Name = "RemoveButton " + todoList.IndexOf(todo),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("consolas", 14),
                    FlatStyle = FlatStyle.Flat,
                    Width = 20,
                    Height = 30,
                    Location = new Point(330, 0),
                    Tag = todoList.IndexOf(todo)
                };

                TodoIsDone(todo, checkBox);
                TodoStrikeOut(todo, todoText);

                todoListTable.Controls.Add(todoPanel);
                todoPanel.Controls.Add(checkBox);
                todoPanel.Controls.Add(todoText);
                todoPanel.Controls.Add(removeButton);
                checkBox.Click += ClickedCheckBoxEventHandler;
                removeButton.FlatAppearance.BorderSize = 0;
                removeButton.Click += RemoveButtonEventHandler;
                todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        private static void TodoInputEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                TextBox textBox = (TextBox)sender;
                if (textBox.Text != "")
                {
                    todoListTable.Controls.Clear();
                    todoList.Add(new Todo
                    {
                        Information = textBox.Text
                    });
                }
                textBox.Clear();
                CreateTodoListDisplay();
            }
            else { }
        }
        private static void RemoveButtonEventHandler(object sender, EventArgs e)
        {
            Button info = (Button)sender;
            int todoIndex = (int)info.Tag;
            todoList.RemoveAt(todoIndex);
            CreateTodoListDisplay();
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
            CreateTodoListDisplay();
        }
        private static bool TodoIsDone(Todo todo, CheckBox checkBox)
        {
            if (todo.IsDone == true)
            {
                return checkBox.Checked = true;
            }
            else
            {
                return checkBox.Checked = false;
            }
        }
        private static void TodoStrikeOut(Todo todo, Label label)
        {
            if (todo.IsDone == true)
            {
                label.Font = new Font("consolas", 20, FontStyle.Strikeout);
                label.ForeColor = Color.Gray;
            }
        }
        private static void SortButtonClickEventHandler(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            sortChoice = button.Text;
            if (sortChoice == "All")
            {
                sortList = todoList;
            }
            else if (sortChoice == "Completed")
            {
                sortList = todoList.Where(t => t.IsDone == true);
            }
            else if (sortChoice == "Active")
            {
                sortList = todoList.Where(t => t.IsDone == false);
            }
            CreateTodoListDisplay();
        }
    }
}


