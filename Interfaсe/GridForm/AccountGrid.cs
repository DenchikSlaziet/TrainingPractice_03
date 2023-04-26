using Interfaсe.AddForm;
using Logic.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Interfaсe.GridForm
{
    public partial class AccountGrid : Form
    {
        private MyDbContext context;
        public AccountGrid()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            context = new MyDbContext();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var aa = new AddAccount(context);
            if(aa.ShowDialog() == DialogResult.OK)
            {
                context.Accounts.Add(aa.Account);
                context.SaveChanges();
                UpdateDB();
                MessageBox.Show($"Добавлен отчет по топливу ({aa.Account.Fuel})", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateDB()
        {
            dataGridView1.DataSource = context.Accounts.ToList();
            toolStripStatusLabelCountAll.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = (Account)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            if (data != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить {data.Fuel} ?", "Удаление Записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (context.Accounts.SingleOrDefault(x => x.AccountId == data.AccountId) is Account account)
                    {
                        context.Accounts.Remove(account);
                        context.SaveChanges();
                        UpdateDB();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выбирите запись!");
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            var data = (Account)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            var ap = new AddAccount(context, data);

            if (ap.ShowDialog(this) == DialogResult.OK)
            {
                data.Fuel = ap.Account.Fuel;
                data.FuelId = ap.Account.FuelId;
                data.VolumeNew = ap.Account.VolumeNew;
                data.VolumeSell = ap.Account.VolumeSell;
                context.SaveChanges();
                UpdateDB();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Экспортировать все?", "Справка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        var xlApp = GetExcel();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                            {
                                xlApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        xlApp.Visible = true;
                        break;
                    };

                case DialogResult.No:
                    {
                        var xlApp = GetExcel();
                        for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                            {
                                xlApp.Cells[i + 2, j + 1] = dataGridView1.SelectedRows[i].Cells[j].Value.ToString();
                            }
                        }
                        xlApp.Visible = true;
                        break;
                    };

                case DialogResult.Cancel:
                    return;
            }
        }

        private Excel.Application GetExcel()
        {
            Excel.Application xlApp;
            Worksheet xlSheet;
            xlApp = new Excel.Application();
            Excel.Workbook wBook;
            wBook = xlApp.Workbooks.Add();
            xlApp.Columns.ColumnWidth = 15;
            xlSheet = wBook.Sheets[1];
            xlSheet.Name = "Чеки";
            xlSheet.Cells.HorizontalAlignment = 3;
            for (int j = 1; j < dataGridView1.Columns.Count; j++)
            {
                xlApp.Cells[1, j] = dataGridView1.Columns[j - 1].HeaderText;
            }
            return xlApp;
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Топливо":
                    if (radioButtonUp.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.Fuel.Name).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.Fuel.Name).ToList();
                    break;

                case "Цена топлива":
                    if (radioButtonDown.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.Fuel.Price).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.Fuel.Price).ToList();
                    break;

                case "Производитель топлива":
                    if (radioButtonDown.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.Fuel.Provider.Name).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.Fuel.Provider.Name).ToList();
                    break;

                case "Дата":
                    if (radioButtonDown.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.Date).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.Date).ToList();
                    break;

                case "Начальный объем":
                    if (radioButtonDown.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.VolumeNew).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.VolumeNew).ToList();
                    break;

                case "Конечный объем":
                    if (radioButtonDown.Checked)
                        dataGridView1.DataSource = context.Accounts.OrderByDescending(x => x.VolumeSell).ToList();
                    else
                        dataGridView1.DataSource = context.Accounts.OrderBy(x => x.VolumeSell).ToList();
                    break;

                default:
                    {
                        MessageBox.Show("Не выбран столбец!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private async void AccountGrid_Load(object sender, EventArgs e)
        {
            List<Account> accounts = null;

            await Task.Run(() =>
            {
                accounts = new List<Account>(context.Accounts.ToList());
            });

            dataGridView1.DataSource = accounts;
            toolStripStatusLabelCountAll.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
            UpdateButton();
        }

        private void UpdateButton()
        {
            foreach(var item in this.Controls)
            {
                if(item is System.Windows.Forms.Button button)
                {
                    button.Enabled = true;
                }
            }

            foreach (var item in groupBox2.Controls)
            {
                if (item is System.Windows.Forms.Button button)
                {
                    button.Enabled = true;
                }
            }
        }
    }
}
