using Interfaсe.AddForm;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaсe.GridForm
{
    public partial class FuelGrid : Form
    {
        public MyDbContext context;
        public FuelGrid()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            context = new MyDbContext();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var af = new AddFuel(context);
            if (af.ShowDialog() == DialogResult.OK)
            {
                context.Fuels.Add(af.Fuel);
                context.SaveChanges();
                UpdateDG();

                MessageBox.Show($"Добавлено топливо: {af.Fuel.Name}", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void UpdateDG()
        {
            dataGridView1.DataSource = context.Fuels.ToList();
            toolStripStatusLabelCount.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var data = (Fuel)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            if (MessageBox.Show($"Вы действительно хотите удалить {data.Name} ?\nПосле удаления топлива информация о заправке будет удалена!", "Удаление Записи",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (context.Fuels.SingleOrDefault(x => x.FuelId == data.FuelId) is Fuel fuel)
                {
                    context.Fuels.Remove(fuel);
                    context.SaveChanges();
                    UpdateDG();
                }
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            var data = (Fuel)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            var ap = new AddFuel(context, data);

            if (ap.ShowDialog(this) == DialogResult.OK)
            {
                data.Name = ap.Fuel.Name;
                data.Price = ap.Fuel.Price;
                data.ProviderId = ap.Fuel.ProviderId;
                data.Provider = ap.Fuel.Provider;
                context.SaveChanges();
                UpdateDG();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDG();
            numericUpDownSell.Value = numericUpDownSell.Minimum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (context.Fuels.Where(x => x.Price == numericUpDownSell.Value).Count() == 0)
            {
                MessageBox.Show("Ничего не найдено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridView1.DataSource = context.Fuels.Where(x => x.Price == numericUpDownSell.Value).ToList();
            toolStripStatusLabelCount.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
        }

        private async void FuelGrid_Load(object sender, EventArgs e)
        {
            List<Fuel> fuels = null;

            await Task.Run(() =>
            {
                fuels = new List<Fuel>(context.Fuels.ToList());
            });

            dataGridView1.DataSource = fuels;
            toolStripStatusLabelCount.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
            UpdateButton();
        }

        private void UpdateButton()
        {
            foreach (var item in this.Controls)
            {
                if (item is System.Windows.Forms.Button button)
                {
                    button.Enabled = true;
                }
            }
        }
    }
}
