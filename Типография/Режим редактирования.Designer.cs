namespace Типография
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView = new DataGridView();
            listBoxTables = new ListBox();
            btnSave = new Button();
            menuItemNavigate = new ContextMenuStrip(components);
            перейтиToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            btnInRead = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            menuItemNavigate.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 12);
            dataGridView.Name = "dataGridView";
            dataGridView.RowTemplate.Height = 25;
            dataGridView.Size = new Size(880, 363);
            dataGridView.TabIndex = 0;
            dataGridView.MouseClick += dataGridView_MouseClick;
            // 
            // listBoxTables
            // 
            listBoxTables.FormattingEnabled = true;
            listBoxTables.ItemHeight = 15;
            listBoxTables.Location = new Point(12, 411);
            listBoxTables.Name = "listBoxTables";
            listBoxTables.Size = new Size(880, 169);
            listBoxTables.TabIndex = 1;
            listBoxTables.SelectedIndexChanged += listBoxTables_SelectedIndexChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(626, 381);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // menuItemNavigate
            // 
            menuItemNavigate.Items.AddRange(new ToolStripItem[] { перейтиToolStripMenuItem });
            menuItemNavigate.Name = "menuItemNavigate";
            menuItemNavigate.Size = new Size(122, 26);
            // 
            // перейтиToolStripMenuItem
            // 
            перейтиToolStripMenuItem.Name = "перейтиToolStripMenuItem";
            перейтиToolStripMenuItem.Size = new Size(121, 22);
            перейтиToolStripMenuItem.Text = "Перейти";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 391);
            label1.Name = "label1";
            label1.Size = new Size(120, 17);
            label1.TabIndex = 3;
            label1.Text = "Выберите таблицу:";
            // 
            // btnInRead
            // 
            btnInRead.Location = new Point(762, 381);
            btnInRead.Name = "btnInRead";
            btnInRead.Size = new Size(130, 23);
            btnInRead.TabIndex = 4;
            btnInRead.Text = "Режим чтения";
            btnInRead.UseVisualStyleBackColor = true;
            btnInRead.Click += btnInRead_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 590);
            Controls.Add(btnInRead);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(listBoxTables);
            Controls.Add(dataGridView);
            Name = "Form1";
            Text = "Режим редактирования БД";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            menuItemNavigate.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private ListBox listBoxTables;
        private Button btnSave;
        private ContextMenuStrip menuItemNavigate;
        private ToolStripMenuItem перейтиToolStripMenuItem;
        private Label label1;
        private Button btnInRead;
    }
}