﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using AtelierContracts.ViewModels;
using System.Windows.Forms;

namespace AtelierView
{
    public partial class FormCreateOrder : Form
    {
#pragma warning disable CS0246 // Не удалось найти тип или имя пространства имен "IDressLogic" (возможно, отсутствует директива using или ссылка на сборку).
        private readonly IDressLogic _logicP;
#pragma warning restore CS0246 // Не удалось найти тип или имя пространства имен "IDressLogic" (возможно, отсутствует директива using или ссылка на сборку).

        private readonly IOrderLogic _logicO;

#pragma warning disable CS0246 // Не удалось найти тип или имя пространства имен "IDressLogic" (возможно, отсутствует директива using или ссылка на сборку).
        public FormCreateOrder(IDressLogic logicP, IOrderLogic logicO)
#pragma warning restore CS0246 // Не удалось найти тип или имя пространства имен "IDressLogic" (возможно, отсутствует директива using или ссылка на сборку).
        {
            InitializeComponent();
            _logicP = logicP;
            _logicO = logicO;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            List<DressViewModel> list = _logicP.Read(null);
            if (list != null)
            {
                comboBoxDress.DisplayMember = "DressName";
                comboBoxDress.ValueMember = "Id";
                comboBoxDress.DataSource = list;
                comboBoxDress.SelectedItem = null;
            }
        }
        private void CalcSum()
        {
            if (comboBoxDress.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxDress.SelectedValue);
                    DressViewModel dress = _logicP.Read(new DressBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * dress?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxDress_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDress.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    DressId = Convert.ToInt32(comboBoxDress.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
