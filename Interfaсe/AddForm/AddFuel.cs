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

namespace Interfaсe.AddForm
{
    public partial class AddFuel : Form
    {
        public MyDbContext context;
        public Fuel Fuel;
        public AddFuel(MyDbContext context)
        {
            InitializeComponent();
            this.context = context;
            UpdateBox();
        }

        public AddFuel(MyDbContext context,Fuel fuel):this(context)
        {
            this.Text = "Изменение";
            buttonAdd.Text = "Изменить";
            textBoxName.Text=fuel.Name;
            numericUpDownSell.Value = fuel.Price;
            comboBox1.SelectedItem = fuel.Provider;
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != ' ')
            {
                e.Handled = true;
            }

        }
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = !string.IsNullOrWhiteSpace(textBoxName.Text) && comboBox1.SelectedItem!=null;
        }

        private void UpdateBox()
        {
            comboBox1.DataSource = context.Providers.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var provider = (Provider)comboBox1.SelectedItem;

            if (context.Fuels.Where(x => x.Name.ToLower() == textBoxName.Text.ToLower() && 
                x.Price==numericUpDownSell.Value && x.ProviderId == provider.ProviderId).Count() > 0)
            {
                MessageBox.Show("Такое топлтво уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Fuel = new Fuel()
            {
                Name = textBoxName.Text,
                Price = numericUpDownSell.Value,
                ProviderId = provider.ProviderId,
                Provider = provider
            };
            this.DialogResult = DialogResult.OK;
        }
    }
}
