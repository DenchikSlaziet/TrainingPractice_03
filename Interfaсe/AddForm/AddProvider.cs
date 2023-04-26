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
    public partial class AddProvider : Form
    {
        public Provider Provider;
        public AddProvider()
        {
            InitializeComponent();
        }

        public AddProvider(Provider provider):this()
        {
            this.Text = "Изменение";
            buttonAdd.Text = "Изменить";
            textBoxName.Text = provider.Name;
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' &&  l != ' ')
            {
                e.Handled = true;
            }

        }
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = !string.IsNullOrWhiteSpace(textBoxName.Text);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using(var context = new MyDbContext())
            {
                if(context.Providers.Where(x=>x.Name.ToLower() == textBoxName.Text.ToLower()).Count() > 0)
                {
                    MessageBox.Show("Такой поставщик уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Provider = new Provider()
                {
                    Name = textBoxName.Text
                };
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }
    }
}
