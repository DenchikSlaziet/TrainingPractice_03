using Interfaсe.AddForm;
using Interfaсe.GridForm;
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

namespace Interfaсe
{
    public partial class Main : Form
    {
        public MyDbContext context;
        public Main()
        {
            InitializeComponent();
            context = new MyDbContext();
        }

        private void справочникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pg = new ProviderGrid();
            pg.Show();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ap = new AddProvider();
            if(ap.ShowDialog() == DialogResult.OK)
            {
                context.Providers.Add(ap.Provider);
                context.SaveChanges();

                MessageBox.Show($"Добавлен поставщик: {ap.Provider.Name}", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void сущностиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var FG = new FuelGrid();
            FG.Show();
        }

        private void учетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var AG = new AccountGrid();
            AG.Show();
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var af = new AddFuel(context);
            if(af.ShowDialog() == DialogResult.OK)
            {
                context.Fuels.Add(af.Fuel);
                context.SaveChanges();
                MessageBox.Show($"Добавлено топливо: {af.Fuel.Name}", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var aa = new AddAccount(context);
            if(aa.ShowDialog() == DialogResult.OK)
            {
                context.Accounts.Add(aa.Account);
                context.SaveChanges();
                MessageBox.Show($"Добавлен отчет по топливу ({aa.Account.Fuel})", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}
