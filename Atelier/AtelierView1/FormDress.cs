using System;
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
using Unity;

namespace AtelierView
{
    public partial class FormDress : Form
    {
        public int Id { set { id = value; } }

        private readonly IDressLogic _logic;

        private int? id;

        private Dictionary<int, (string, int)> dressComponents;

        public FormDress(IDressLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormDress_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DressViewModel view = _logic.Read(new DressBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.DressName;
                        textBoxPrice.Text = view.Price.ToString();
                        dressComponents = view.DressComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dressComponents = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (dressComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in dressComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormDressComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (dressComponents.ContainsKey(form.Id))
                {
                    dressComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    dressComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormDressComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = dressComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dressComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        dressComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxName.Text))
                {
                    MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(textBoxPrice.Text))
                {
                    MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dressComponents == null || dressComponents.Count == 0)
                {
                    MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    _logic.CreateOrUpdate(new DressBindingModel
                    {
                        Id = id,
                        DressName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        DressComponents = dressComponents
                    });
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
