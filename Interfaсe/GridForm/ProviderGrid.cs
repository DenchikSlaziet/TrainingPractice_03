using Interfaсe.AddForm;
using Logic;
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
    public partial class ProviderGrid : Form
    {
        public MyDbContext context;
        public ProviderGrid()
        {
            InitializeComponent();
            context = new MyDbContext();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var data = (Provider)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            if (MessageBox.Show($"Вы действительно хотите удалить {data.Name} ?\nПосле удаления поставщика информация о топливе будет удалена!", "Удаление Записи",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (context.Providers.SingleOrDefault(x => x.ProviderId == data.ProviderId) is Provider provider)
                {
                    context.Providers.Remove(provider);
                    context.SaveChanges();
                    UpdateDG();
                }
            }
        }

        private void UpdateDG()
        {
            dataGridView1.DataSource = context.Providers.ToList();
            toolStripStatusLabelCountAll.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var pf = new AddProvider();
            if (pf.ShowDialog() == DialogResult.OK)
            {
                context.Providers.Add(pf.Provider);
                context.SaveChanges();
                UpdateDG();

                MessageBox.Show($"Добавлен поставщик: {pf.Provider.Name}", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            var data = (Provider)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

            var ap = new AddProvider(data);

            if (ap.ShowDialog(this) == DialogResult.OK)
            {
                data.Name = ap.Provider.Name;
                context.SaveChanges();
                UpdateDG();
            }
        }

        private async void ProviderGrid_Load(object sender, EventArgs e)
        {
            List<Provider> providers = null;
            await Task.Run(() =>
            {
                providers = new List<Provider>(context.Providers.ToList());
            });

            dataGridView1.DataSource = providers;
            toolStripStatusLabelCountAll.Text = $"Кол-во элементов: {dataGridView1.RowCount}";
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
