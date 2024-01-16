using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Типография
{
    public partial class SelectionForm : Form
    {
        public int SelectedId { get; private set; }

        public SelectionForm()
        {
            InitializeComponent();
            SelectedId = -1;
        }

        DataBase DataBase = new DataBase();

        public void LoadData(string queryName)
        {
            string tableName;
            switch (queryName)
            {
                case "Отчет с информацией по конкретному проекту":
                    tableName = "Projects";
                    break;
                case "Отчет с информацией по сотрудникам на конкретной должности":
                    tableName = "Employees";
                    break;
                case "Отчет с типами материалов, используемых в конкретном проекте и их кол-во":
                    tableName = "ClientProjects";
                    break;
                case "Отчет с деталями всех проектов по клиенту":
                    tableName = "ClientProjects";
                    break;
                case "Отчет обо всех проектах конкретного сотрудника":
                    tableName = "Projects";
                    break;
                default: tableName = queryName; break;
            }

            string query = $"SELECT * FROM [{tableName}]"; // Запрос на выборку всех данных из указанной таблицы

            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                // Загружаем данные в DataTable
                adapter.Fill(dataTable);

                // Устанавливаем DataTable как источник данных для DataGridView
                dataGridView.DataSource = dataTable;

                // Настройка DataGridView
                dataGridView.ReadOnly = true;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        private void dataGripOption()
        {
            int sumWid = 60;

            foreach (DataGridViewColumn column in dataGridView.SelectedRows)
            {
                sumWid += column.Width;
            }
            //dataGridView.ReadOnly = true;
            //dataGridView.Width = sumWid;
            //btnOK.Width = sumWid;
            //this.Width = sumWid + 40;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                SelectedId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value); // Предполагаем, что ID находится в первом столбце
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Выберите запись");
            }
        }
    }
}
