using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading;

namespace Типография
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private SqlDataAdapter dataAdapter;

        private DataBase dataBase = new DataBase();

        private void LoadTables()
        {
            List<string> TablesList = dataBase.ReadTables();
            foreach (string table in TablesList)
            {
                listBoxTables.Items.Add(table);
            }
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem != null)
            {
                string tableName = listBoxTables.SelectedItem.ToString();
                LoadTableData(tableName);
            }
        }

        private void LoadTableData(string tableName)
        {
            {
                // Очистка существующих данных и столбцов
                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();

                dataAdapter = new SqlDataAdapter($"SELECT * FROM [{tableName}]", dataBase.getConnection());
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;

                // Настройка ширины столбцов
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

                // Блокирование редактирования ID
                dataGridView.Columns[0].ReadOnly = true;
                dataGridView.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
                dataBase.closeConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    dataBase.openConnection();

                    DataTable changes = ((DataTable)dataGridView.DataSource).GetChanges();
                    if (changes != null)
                    {
                        dataAdapter = new SqlDataAdapter($"SELECT * FROM [{listBoxTables.SelectedItem.ToString()}]", dataBase.getConnection());
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                        dataAdapter.Update(changes);
                        ((DataTable)dataGridView.DataSource).AcceptChanges();

                        // Перезагрузка данных из базы данных после сохранения изменений
                        string tableName = listBoxTables.SelectedItem.ToString();
                        LoadTableData(tableName);
                    }

                    dataBase.closeConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var hitTestInfo = dataGridView.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    int columnIndex = hitTestInfo.ColumnIndex;
                    int rowIndex = hitTestInfo.RowIndex;

                    string columnName = dataGridView.Columns[columnIndex].Name;
                    string tableName = dataBase.GetTableNameFromColumn(columnName);
                    string idValue = dataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString();

                    if (!string.IsNullOrEmpty(tableName))
                    {
                        // Загружаем данные соответствующей таблицы
                        LoadTableData(tableName);

                        // Ищем и выделяем строку с соответствующим ID
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (row.Cells[columnName].Value.ToString() == idValue)
                            {
                                row.Selected = true;
                                dataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btnInRead_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(Open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void Open()
        {
            Application.Run(new Form2());
        }

    }
}