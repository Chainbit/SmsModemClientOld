namespace WindowsFormsApplication2
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComPortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.loadComsButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectedPortLabel = new System.Windows.Forms.Label();
            this.selectedPortName = new System.Windows.Forms.Label();
            this.request = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.response = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.commandsTab = new System.Windows.Forms.TabPage();
            this.SMSListTab = new System.Windows.Forms.TabPage();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.SMStext = new System.Windows.Forms.TextBox();
            this.SMStime = new System.Windows.Forms.TextBox();
            this.SMSsender = new System.Windows.Forms.TextBox();
            this.SMSList = new System.Windows.Forms.DataGridView();
            this.getSMSListButton = new System.Windows.Forms.Button();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.commandsTab.SuspendLayout();
            this.SMSListTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.ComPortName,
            this.isOpen,
            this.Select});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(585, 244);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // number
            // 
            this.number.FillWeight = 10F;
            this.number.HeaderText = "№";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            // 
            // ComPortName
            // 
            this.ComPortName.HeaderText = "ComPortName";
            this.ComPortName.Name = "ComPortName";
            this.ComPortName.ReadOnly = true;
            // 
            // isOpen
            // 
            this.isOpen.FillWeight = 20F;
            this.isOpen.HeaderText = "isOpen";
            this.isOpen.Name = "isOpen";
            this.isOpen.ReadOnly = true;
            // 
            // Select
            // 
            this.Select.FillWeight = 20F;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Text = "Select";
            this.Select.UseColumnTextForButtonValue = true;
            // 
            // loadComsButton
            // 
            this.loadComsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadComsButton.Enabled = false;
            this.loadComsButton.Location = new System.Drawing.Point(0, 242);
            this.loadComsButton.Name = "loadComsButton";
            this.loadComsButton.Size = new System.Drawing.Size(585, 31);
            this.loadComsButton.TabIndex = 1;
            this.loadComsButton.Text = "Получить список портов";
            this.loadComsButton.UseVisualStyleBackColor = true;
            this.loadComsButton.Click += new System.EventHandler(this.loadComsButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.loadComsButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 273);
            this.panel1.TabIndex = 2;
            // 
            // selectedPortLabel
            // 
            this.selectedPortLabel.AutoSize = true;
            this.selectedPortLabel.Location = new System.Drawing.Point(13, 291);
            this.selectedPortLabel.Name = "selectedPortLabel";
            this.selectedPortLabel.Size = new System.Drawing.Size(122, 13);
            this.selectedPortLabel.TabIndex = 3;
            this.selectedPortLabel.Text = "Выбранный COM-порт:";
            // 
            // selectedPortName
            // 
            this.selectedPortName.AutoSize = true;
            this.selectedPortName.Location = new System.Drawing.Point(132, 291);
            this.selectedPortName.Name = "selectedPortName";
            this.selectedPortName.Size = new System.Drawing.Size(16, 13);
            this.selectedPortName.TabIndex = 4;
            this.selectedPortName.Text = "...";
            // 
            // request
            // 
            this.request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.request.Location = new System.Drawing.Point(8, 13);
            this.request.Name = "request";
            this.request.Size = new System.Drawing.Size(500, 20);
            this.request.TabIndex = 5;
            this.request.KeyDown += new System.Windows.Forms.KeyEventHandler(this.request_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(522, 6);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(71, 33);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // response
            // 
            this.response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.response.Location = new System.Drawing.Point(6, 48);
            this.response.Multiline = true;
            this.response.Name = "response";
            this.response.ReadOnly = true;
            this.response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.response.Size = new System.Drawing.Size(592, 159);
            this.response.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.commandsTab);
            this.tabControl1.Controls.Add(this.SMSListTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 308);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(612, 239);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // commandsTab
            // 
            this.commandsTab.Controls.Add(this.response);
            this.commandsTab.Controls.Add(this.sendButton);
            this.commandsTab.Controls.Add(this.request);
            this.commandsTab.Location = new System.Drawing.Point(4, 22);
            this.commandsTab.Name = "commandsTab";
            this.commandsTab.Padding = new System.Windows.Forms.Padding(3);
            this.commandsTab.Size = new System.Drawing.Size(604, 213);
            this.commandsTab.TabIndex = 0;
            this.commandsTab.Text = "Команды";
            this.commandsTab.UseVisualStyleBackColor = true;
            // 
            // SMSListTab
            // 
            this.SMSListTab.Controls.Add(this.DecodeButton);
            this.SMSListTab.Controls.Add(this.SMStext);
            this.SMSListTab.Controls.Add(this.SMStime);
            this.SMSListTab.Controls.Add(this.SMSsender);
            this.SMSListTab.Controls.Add(this.SMSList);
            this.SMSListTab.Controls.Add(this.getSMSListButton);
            this.SMSListTab.Location = new System.Drawing.Point(4, 22);
            this.SMSListTab.Name = "SMSListTab";
            this.SMSListTab.Padding = new System.Windows.Forms.Padding(3);
            this.SMSListTab.Size = new System.Drawing.Size(604, 213);
            this.SMSListTab.TabIndex = 1;
            this.SMSListTab.Text = "Получение СМС";
            this.SMSListTab.UseVisualStyleBackColor = true;
            // 
            // DecodeButton
            // 
            this.DecodeButton.Location = new System.Drawing.Point(518, 6);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(75, 22);
            this.DecodeButton.TabIndex = 5;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // SMStext
            // 
            this.SMStext.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.SMStext.Location = new System.Drawing.Point(190, 32);
            this.SMStext.Multiline = true;
            this.SMStext.Name = "SMStext";
            this.SMStext.ReadOnly = true;
            this.SMStext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SMStext.Size = new System.Drawing.Size(403, 124);
            this.SMStext.TabIndex = 4;
            // 
            // SMStime
            // 
            this.SMStime.Location = new System.Drawing.Point(369, 7);
            this.SMStime.Name = "SMStime";
            this.SMStime.ReadOnly = true;
            this.SMStime.Size = new System.Drawing.Size(143, 20);
            this.SMStime.TabIndex = 3;
            // 
            // SMSsender
            // 
            this.SMSsender.Location = new System.Drawing.Point(190, 7);
            this.SMSsender.Name = "SMSsender";
            this.SMSsender.ReadOnly = true;
            this.SMSsender.Size = new System.Drawing.Size(173, 20);
            this.SMSsender.TabIndex = 2;
            // 
            // SMSList
            // 
            this.SMSList.AllowUserToAddRows = false;
            this.SMSList.AllowUserToDeleteRows = false;
            this.SMSList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SMSList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.sender,
            this.timeStamp,
            this.text});
            this.SMSList.Location = new System.Drawing.Point(8, 6);
            this.SMSList.Name = "SMSList";
            this.SMSList.ReadOnly = true;
            this.SMSList.RowHeadersVisible = false;
            this.SMSList.Size = new System.Drawing.Size(175, 150);
            this.SMSList.TabIndex = 1;
            this.SMSList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SMSList_CellContentClick);
            // 
            // getSMSListButton
            // 
            this.getSMSListButton.Location = new System.Drawing.Point(51, 162);
            this.getSMSListButton.Name = "getSMSListButton";
            this.getSMSListButton.Size = new System.Drawing.Size(132, 23);
            this.getSMSListButton.TabIndex = 0;
            this.getSMSListButton.Text = "Получить список СМС";
            this.getSMSListButton.UseVisualStyleBackColor = true;
            this.getSMSListButton.Click += new System.EventHandler(this.getSMSListButton_Click);
            // 
            // index
            // 
            this.index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.index.FillWeight = 30F;
            this.index.HeaderText = "№";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            // 
            // sender
            // 
            this.sender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sender.HeaderText = "Отправитель";
            this.sender.Name = "sender";
            this.sender.ReadOnly = true;
            // 
            // timeStamp
            // 
            this.timeStamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.timeStamp.FillWeight = 70F;
            this.timeStamp.HeaderText = "Время";
            this.timeStamp.Name = "timeStamp";
            this.timeStamp.ReadOnly = true;
            // 
            // text
            // 
            this.text.HeaderText = "Текст";
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 547);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.selectedPortName);
            this.Controls.Add(this.selectedPortLabel);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "SerialPortListener";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.commandsTab.ResumeLayout(false);
            this.commandsTab.PerformLayout();
            this.SMSListTab.ResumeLayout(false);
            this.SMSListTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button loadComsButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComPortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn isOpen;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
        private System.Windows.Forms.Label selectedPortLabel;
        private System.Windows.Forms.Label selectedPortName;
        private System.Windows.Forms.TextBox request;
        private System.Windows.Forms.Button sendButton;
        public System.Windows.Forms.TextBox response;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage commandsTab;
        private System.Windows.Forms.TabPage SMSListTab;
        private System.Windows.Forms.Button getSMSListButton;
        private System.Windows.Forms.DataGridView SMSList;
        private System.Windows.Forms.TextBox SMStext;
        private System.Windows.Forms.TextBox SMStime;
        private System.Windows.Forms.TextBox SMSsender;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
    }
}

