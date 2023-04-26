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

namespace Interfaсe.AddForm
{
    public partial class AddAccount : Form
    {
        private MyDbContext context;
        public Account Account { get; set; }
        public AddAccount(MyDbContext context)
        {
            InitializeComponent();
            this.context = context;
            UpdateBox();
            
        }

        public AddAccount(MyDbContext context, Account account):this(context)
        {
            this.Text = "Изменение";
            buttonAdd.Text = "Изменить";
            comboBox1.SelectedItem = account.Fuel;
            numericUpDownSell.Value = account.VolumeNew;
            numericUpDown1.Value = account.VolumeSell;
        }

        private void UpdateBox()
        {
            comboBox1.DataSource = context.Fuels.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (numericUpDownSell.Value >= numericUpDown1.Value)
            {
                var date = DateTime.Now.Date;

                if (context.Accounts.Where(x => x.FuelId == ((Fuel)comboBox1.SelectedItem).FuelId && x.Date == date &&
                 x.VolumeNew == numericUpDownSell.Value && x.VolumeSell == numericUpDown1.Value).Count() == 0)
                {
                    var fuel = (Fuel)comboBox1.SelectedItem;

                    Account = new Account()
                    {
                        Date = date,
                        VolumeNew = numericUpDownSell.Value,
                        VolumeSell = numericUpDown1.Value,
                        Fuel = fuel,
                        FuelId = fuel.FuelId,
                    };

                    this.DialogResult = DialogResult.OK;
                    return;
                }
                MessageBox.Show("Такой отчет уже присутствует!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Начальный объем не может быть меньше конечного!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled =  comboBox1.SelectedItem != null;
        }
    }
}
