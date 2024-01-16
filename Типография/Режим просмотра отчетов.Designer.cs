namespace Типография
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnInWrite = new Button();
            listBoxSQL = new ListBox();
            dataGridView = new DataGridView();
            label1 = new Label();
            listBox1 = new ListBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnInWrite
            // 
            btnInWrite.Location = new Point(12, 609);
            btnInWrite.Name = "btnInWrite";
            btnInWrite.Size = new Size(409, 23);
            btnInWrite.TabIndex = 0;
            btnInWrite.Text = "Режим редактирования";
            btnInWrite.UseVisualStyleBackColor = true;
            btnInWrite.Click += btnInWrite_Click;
            // 
            // listBoxSQL
            // 
            listBoxSQL.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            listBoxSQL.FormattingEnabled = true;
            listBoxSQL.HorizontalScrollbar = true;
            listBoxSQL.ItemHeight = 15;
            listBoxSQL.Items.AddRange(new object[] { "Отчет о принтерах и задачах со сроками", "Отчет о длительности задач и их менеджерах", "Отчет по сотрудникам и их проектам + суммарный срок исполнения", "Запрос по типам материалов и количеству их использований", "Отчет с информацией по конкретному проекту", "Отчет с информацией по сотрудникам на конкретной должности", "Отчет с типами материалов, используемых в конкретном проекте и их кол-во", "Отчет с деталями всех проектов по клиенту", "Отчет обо всех проектах конкретного сотрудника", "Отчет о всех исполняемых проектах за указанный период", "Отчет о материалах, заказанных за указанный период" });
            listBoxSQL.Location = new Point(437, 463);
            listBoxSQL.Name = "listBoxSQL";
            listBoxSQL.RightToLeft = RightToLeft.No;
            listBoxSQL.Size = new Size(453, 169);
            listBoxSQL.TabIndex = 1;
            listBoxSQL.SelectedIndexChanged += listBoxSQL_SelectedIndexChanged;
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 10);
            dataGridView.Name = "dataGridView";
            dataGridView.RowTemplate.Height = 25;
            dataGridView.Size = new Size(878, 432);
            dataGridView.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(437, 445);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 3;
            label1.Text = "Отчеты:";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Items.AddRange(new object[] { "Приоритетные принтеры для закупки", "3 самых прибыльных направления деятельности", "Открытые проекты" });
            listBox1.Location = new Point(12, 464);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(409, 139);
            listBox1.TabIndex = 4;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 445);
            label2.Name = "label2";
            label2.Size = new Size(217, 15);
            label2.TabIndex = 5;
            label2.Text = "Система помощи принятия решений:";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 644);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(dataGridView);
            Controls.Add(listBoxSQL);
            Controls.Add(btnInWrite);
            Name = "Form2";
            Text = "Режим просмотра отчетов";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnInWrite;
        private ListBox listBoxSQL;
        private DataGridView dataGridView;
        private Label label1;
        private ListBox listBox1;
        private Label label2;
    }
}