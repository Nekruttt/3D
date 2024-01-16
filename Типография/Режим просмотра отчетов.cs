using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Типография
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private SqlDataAdapter dataAdapter;

        private DataBase dataBase = new DataBase();

        private void btnInWrite_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(Open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void Open()
        {
            Application.Run(new Form1());
        }
        private void dataGripOption()
        {
            int sumWid = 60;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                sumWid += column.Width;
            }
            dataGridView.ReadOnly = true;
            dataGridView.Width = sumWid;
            this.Width = sumWid + 460;

        }

        private void listBoxSQL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSQL.SelectedItem != null)
            {
                QueryInfo query = QueryInfo.ExecuteQuery(listBoxSQL.SelectedItem.ToString());

                switch (query.Type)
                {
                    case QueryType.Simple:
                        ExecuteSimpleQuery(query.SqlQuery);
                        break;
                    case QueryType.ByID:
                        int selectedId = SelectIdFromTable(query.Name);
                        ExecuteParameterizedQuery(query.SqlQuery, selectedId.ToString());
                        break;
                    case QueryType.ByDateRange:
                        (DateTime start, DateTime end) = PromptForDateRange();
                        ExecuteDateRangeQuery(query.SqlQuery, start, end);
                        break;
                }

                int sumWid = 60;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    sumWid += column.Width;
                }
                dataGridView.ReadOnly = true;
                //dataGridView.Width = sumWid;
                //this.Width = sumWid + 460;

                // Блокирование редактирования
                dataGridView.ReadOnly = true;
            }
        }


        private (DateTime, DateTime) PromptForDateRange()
        {
            using (var form = new DateRangeForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return (form.StartDate, form.EndDate);
                }
                else
                {
                    return (DateTime.MinValue, DateTime.MinValue); // Возвращаем минимальные значения, если пользователь нажал "Отмена"
                }
            }
        }

        private void ExecuteSimpleQuery(string sqlQuery)
        {
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, dataBase.connectString);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
        }

        private void ExecuteParameterizedQuery(string sqlQuery, string id)
        {
            {
                SqlCommand command = new SqlCommand(sqlQuery, dataBase.connectString);
                command.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
        }

        private int SelectIdFromTable(string tableName)
        {
            using (var form = new SelectionForm())
            {
                form.LoadData(tableName);
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return form.SelectedId;
                }
            }
            return -1;
        }

        private void ExecuteDateRangeQuery(string sqlQuery, DateTime start, DateTime end)
        {
            {
                SqlCommand command = new SqlCommand(sqlQuery, dataBase.connectString);
                command.Parameters.AddWithValue("@startDate", start);
                command.Parameters.AddWithValue("@endDate", end);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                QueryInfo query = QueryInfo.ExecuteQuery(listBox1.SelectedItem.ToString());

                switch (query.Type)
                {
                    case QueryType.Simple:
                        ExecuteSimpleQuery(query.SqlQuery);
                        break;
                    case QueryType.ByID:
                        int selectedId = SelectIdFromTable(query.Name);
                        ExecuteParameterizedQuery(query.SqlQuery, selectedId.ToString());
                        break;
                    case QueryType.ByDateRange:
                        (DateTime start, DateTime end) = PromptForDateRange();
                        ExecuteDateRangeQuery(query.SqlQuery, start, end);
                        break;
                }

                int sumWid = 60;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    sumWid += column.Width;
                }
                dataGridView.ReadOnly = true;
                //dataGridView.Width = sumWid;
                //this.Width = sumWid + 460;

                // Блокирование редактирования
                dataGridView.ReadOnly = true;
            }
        }
    }
}
