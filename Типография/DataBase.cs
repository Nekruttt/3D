using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Типография
{
    class DataBase
    {
        public SqlConnection connectString = new SqlConnection(@"Server=DESKTOP-0HK31G7\SQLEXPRESS01;Database=3D;Integrated Security=True;Encrypt=False;");

        public void openConnection()
        {
            if (connectString.State == System.Data.ConnectionState.Closed)
            {
                connectString.Open();
            }
        }

        public void closeConnection()
        {
            if (connectString.State == System.Data.ConnectionState.Open)
            {
                connectString.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return connectString;
        }

        public List<string> ReadTables()
        {
            {
                connectString.Open();
                SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME != 'sysdiagrams'", getConnection());

                SqlDataReader reader = command.ExecuteReader();

                List<string> TablesList = new List<string>();

                while (reader.Read())
                {
                    TablesList.Add(reader["TABLE_NAME"].ToString());
                }

                connectString.Close();

                return TablesList;
            }
        }

        public string GetTableNameFromColumn(string columnName)
        {
            switch (columnName)
            {
                case "ClientID":
                    return "Clients";
                case "SupplierID":
                    return "Suppliers";
                case "MaterialTypeID":
                    return "MaterialTypes";
                case "EquipmentTypeID":
                    return "EquipmentTypes";
                case "EquipmentStatusID":
                    return "EquipmentStatuses";
                case "OrderID":
                    return "Orders";
                case "PositionID":
                    return "Positions";
                case "EmployeeID":
                    return "Employees";
                case "MaterialID":
                    return "Materials";
                case "EquipmentID":
                    return "Equipment";
                default:
                    return null;
            }
        }

        public string GetSqlFromName(string SqlName)
        {
            switch (SqlName)
            {
                case "Детали по заказу":
                    return 
                  @"SELECT od.OrderDetailID, o.OrderID, mt.TypeName AS MaterialType, et.TypeName AS EquipmentType, od.Quantity
                  FROM OrderDetails od
                  JOIN Orders o ON od.OrderID = o.OrderID
                  JOIN Materials m ON od.MaterialID = m.MaterialID
                  JOIN MaterialTypes mt ON m.MaterialTypeID = mt.MaterialTypeID
                  JOIN Equipment e ON od.EquipmentID = e.EquipmentID 
                  JOIN EquipmentTypes et ON e.EquipmentTypeID = et.EquipmentTypeID;";

                case "Заказы на сотрудниках":
                    return
                  @"SELECT eo.EmployeeOrderID, e.Name AS EmployeeName, o.OrderID
                  FROM EmployeeOrders eo
                  JOIN Employees e ON eo.EmployeeID = e.EmployeeID
                  JOIN Orders o ON eo.OrderID = o.OrderID;";

                case "Заказы":
                    return
                  @"SELECT o.OrderID, c.ContactInfo, o.OrderDate, o.DueDate
                  FROM Orders o
                  JOIN Clients c ON o.ClientID = c.ClientID;";

                case "Материалы на складе":
                    return
                  @"SELECT i.InventoryID, mt.TypeName AS MaterialType, i.Quantity
                  FROM Inventory i
                  JOIN Materials m ON i.MaterialID = m.MaterialID
                  JOIN MaterialTypes mt ON m.MaterialTypeID = mt.MaterialTypeID;";

                case "Обслуживание оборудования":
                    return
                  @"SELECT m.MaintenanceID, et.TypeName AS EquipmentType, m.MaintenanceDate, m.Description
                  FROM Maintenance m
                  JOIN Equipment e ON m.EquipmentID = e.EquipmentID
                  JOIN EquipmentTypes et ON e.EquipmentTypeID = et.EquipmentTypeID;";

                case "Поставки материалов":
                    return
                  @"SELECT m.MaterialID, mt.TypeName, m.Cost, s.Name AS SupplierName
                  FROM Materials m
                  JOIN Suppliers s ON m.SupplierID = s.SupplierID
                  JOIN MaterialTypes mt ON m.MaterialTypeID = mt.MaterialTypeID;";

                case "Финансы":
                    return
                  @"SELECT f.FinanceID, o.OrderID, f.TransactionType, f.Amount, f.TransactionDate
                  FROM Finance f
                  JOIN Orders o ON f.OrderID = o.OrderID;";

                default:
                    return null;
            }
        }
    }

    public enum QueryType
    {
        Simple,
        ByID,
        ByDateRange
    }

    public class QueryInfo
    {
        public string Name { get; set; }
        public QueryType Type { get; set; }
        public string SqlQuery { get; set; }

         public static QueryInfo ExecuteQuery(string queryName)
         {
            List<QueryInfo> queries = new List<QueryInfo>
         {

            // Простые запросы 
            new QueryInfo { Name = "Отчет о принтерах и задачах со сроками", Type = QueryType.Simple, SqlQuery =
                @"SELECT pr.Model, p.ProjectName, pj.StartTime, pj.EndTime
                 FROM PrintingJobs pj
                JOIN Printers pr ON pj.PrinterID = pr.PrinterID
                JOIN Projects p ON pj.ProjectID = p.ProjectID
                ORDER BY pr.Model, pj.StartTime;" },

            new QueryInfo { Name = "Отчет о длительности задач и их менеджерах", Type = QueryType.Simple, SqlQuery =
                @"SELECT p.ProjectName, e.Name AS Manager, DATEDIFF(day, p.StartDate, p.EndDate) AS DurationInDays
                FROM Projects p
                JOIN Employees e ON p.ManagerID = e.EmployeeID;" },

            new QueryInfo { Name = "Отчет по сотрудникам и их проектам + суммарный срок исполнения", Type = QueryType.Simple, SqlQuery =
                @"SELECT e.Name, COUNT(p.ProjectID) AS ManagedProjects, SUM(DATEDIFF(day, p.StartDate, p.EndDate)) AS TotalDurationDays
                FROM Employees e
                JOIN Projects p ON e.EmployeeID = p.ManagerID
                GROUP BY e.Name;" },

            new QueryInfo { Name = "Запрос по типам материалов и количеству их использований", Type = QueryType.Simple, SqlQuery =
                @"SELECT m.MaterialName, COUNT(pm.MaterialID) AS UsageCount
                FROM Materials m
                LEFT JOIN ProjectMaterials pm ON m.MaterialID = pm.MaterialID
                GROUP BY m.MaterialName
                ORDER BY UsageCount DESC;" },

            
            //Запросы с выбором ID
            new QueryInfo { Name = "Отчет с информацией по конкретному проекту", Type = QueryType.ByID, SqlQuery =
                @"SELECT p.ProjectName, p.StartDate, p.EndDate, e.Name AS Manager
                FROM Projects p
                JOIN Employees e ON p.ManagerID = e.EmployeeID
                WHERE p.ProjectID = @id;" },

            new QueryInfo { Name = "Отчет с информацией по сотрудникам на конкретной должности", Type = QueryType.ByID, SqlQuery =
                @"SELECT e.EmployeeID, e.Name, e.Email, e.Phone
                FROM Employees e
                WHERE e.PositionID = @id;" },


            new QueryInfo { Name = "Отчет с типами материалов, используемых в конкретном проекте и их кол-во", Type = QueryType.ByID, SqlQuery =
                @"SELECT cp.ClientID, p.ProjectID, p.ProjectName, pj.JobID, pj.StartTime, pj.EndTime
                FROM ClientProjects cp
                JOIN Projects p ON cp.ProjectID = p.ProjectID
                JOIN PrintingJobs pj ON p.ProjectID = pj.ProjectID
                WHERE cp.ClientID = @id;" },

            new QueryInfo { Name = "Отчет обо всех проектах конкретного сотрудника", Type = QueryType.ByID, SqlQuery =
                @"SELECT p.ProjectID, p.ProjectName, p.StartDate, p.EndDate
                FROM Projects p
                WHERE p.ManagerID = @id;" },

             new QueryInfo { Name = "Отчет с деталями всех проектов по клиенту", Type = QueryType.ByID, SqlQuery =
                @"SELECT cp.ClientID, p.ProjectID, p.ProjectName, pj.JobID, pj.StartTime, pj.EndTime
                FROM ClientProjects cp
                JOIN Projects p ON cp.ProjectID = p.ProjectID
                JOIN PrintingJobs pj ON p.ProjectID = pj.ProjectID
                WHERE cp.ClientID = @id;
                " },

            //Запросы с выбором временного промежутка
            new QueryInfo { Name = "Отчет о всех исполняемых проектах за указанный период", Type = QueryType.ByDateRange, SqlQuery =
                @"SELECT ProjectID, ProjectName, StartDate, EndDate
                FROM Projects
                WHERE StartDate BETWEEN @startDate AND @endDate;" },

            new QueryInfo { Name = "Отчет о материалах, заказанных за указанный период", Type = QueryType.ByDateRange, SqlQuery =
                @"SELECT m.MaterialID, m.MaterialName, SUM(pm.Quantity) AS TotalOrdered
                FROM Materials m
                JOIN ProjectMaterials pm ON m.MaterialID = pm.MaterialID
                JOIN Projects p ON pm.ProjectID = p.ProjectID
                WHERE p.StartDate BETWEEN @startDate AND @endDate
                GROUP BY m.MaterialID, m.MaterialName;" },

            //СППР отчеты
            new QueryInfo { Name = "Приоритетные принтеры для закупки", Type = QueryType.Simple, SqlQuery =
                @"SELECT TOP 5 pr.PrinterID, pr.Model, COUNT(pj.JobID) AS NumberOfCompletedJobs
                FROM Printers pr
                JOIN PrintingJobs pj ON pr.PrinterID = pj.PrinterID
                WHERE pj.Status = 'Завершено'
                GROUP BY pr.PrinterID, pr.Model
                ORDER BY NumberOfCompletedJobs DESC;" },
            new QueryInfo { Name = "3 самых прибыльных направления деятельности", Type = QueryType.Simple, SqlQuery =
                @"SELECT TOP 3 p.ProjectID, p.ProjectName, DATEDIFF(day, p.StartDate, p.EndDate) AS DurationInDays, pos.PositionName
                FROM Projects p
                JOIN Employees e ON p.ManagerID = e.EmployeeID
                JOIN Positions pos ON e.PositionID = pos.PositionID
                WHERE p.StartDate IS NOT NULL AND p.EndDate IS NOT NULL
                ORDER BY DurationInDays DESC;" },
            new QueryInfo { Name = "Открытые проекты", Type = QueryType.Simple, SqlQuery =
                @"SELECT pj.JobID, p.ProjectName, e.Name AS ManagerName, DATEDIFF(day, pj.StartTime, GETDATE()) AS DaysSinceStart
                FROM PrintingJobs pj
                JOIN Projects p ON pj.ProjectID = p.ProjectID
                JOIN Employees e ON p.ManagerID = e.EmployeeID
                WHERE pj.Status <> 'Завершено' AND pj.StartTime IS NOT NULL;" },
         };
            
            var queryInfo = queries.FirstOrDefault(q => q.Name == queryName);
            return queryInfo;
         }
    }
}
