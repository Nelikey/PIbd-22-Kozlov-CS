﻿using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AtelierView
{
    public partial class FormMain : Form
    {
        private readonly IReportLogic _reportLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IImplementerLogic _implementerLogic;
        private readonly IWorkProcess _workProcces;
        private readonly IBackUpLogic _backUpLogic;
        public FormMain(IOrderLogic orderLogic, IReportLogic reportLogic, 
            IWorkProcess workProcess, IImplementerLogic implementerLogic,
            IBackUpLogic backUpLogic)
        {
            InitializeComponent();
            _orderLogic = orderLogic;
            _reportLogic = reportLogic;
            _implementerLogic = implementerLogic;
            _workProcces = workProcess;
            _backUpLogic = backUpLogic;
        }

        private void toolStripMenuItemComponent_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormComponents>();
            form.ShowDialog();
        }

        private void toolStripMenuItemDresses_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormDresses>();
            form.ShowDialog();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(_orderLogic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonIssuedOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value); try
                {
                    _orderLogic.DeliveryOrder(new ChangeStatusBindingModel
                    {
                        OrderId = id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripMenuItemDressesList_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _reportLogic.SaveDressesToWordFile(new ReportBindingModel
                {
                    FileName = dialog.FileName
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItemDressComponent_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportDressComponents>();
            form.ShowDialog();
        }

        private void toolStripMenuItemOrdersList_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportOrders>();
            form.ShowDialog();
        }

        private void toolStripMenuItemClients_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void toolStripMenuItemStartWorks_Click(object sender, EventArgs e)
        {
            _workProcces.DoWork(_implementerLogic,_orderLogic);
            LoadData();
        }

        private void toolStripMenuItemImplementers_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormImplementers>();
            form.ShowDialog();
        }

        private void toolStripMenuItemMessages_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormMessages>();
            form.ShowDialog();
        }

        private void toolStripMenuItemCreateBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (_backUpLogic != null)
                {
                    var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        _backUpLogic.CreateBackUp(new
                        BackUpSaveBindingModel
                        { FolderName = fbd.SelectedPath });
                        MessageBox.Show("Бекап создан", "Сообщение",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
