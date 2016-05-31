namespace NFC
{
    partial class Attendance
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("attendance", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RegNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Department");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("College");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DateTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CourseId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Level");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Attendance));
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.Attendance_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.attendanceUltraGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.attendanceBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.studentprofileDataSet6 = new NFC.studentprofileDataSet6();
            this.attendanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studentprofileDataSet2 = new NFC.studentprofileDataSet2();
            this._Attendance_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._Attendance_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._Attendance_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._Attendance_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.attendanceTableAdapter = new NFC.studentprofileDataSet6TableAdapters.attendanceTableAdapter();
            this.tableAdapterManager = new NFC.studentprofileDataSet6TableAdapters.TableAdapterManager();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.attendanceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.attendanceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.attendanceTableAdapter1 = new NFC.studentprofileDataSet6TableAdapters.attendanceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.Attendance_Fill_Panel.ClientArea.SuspendLayout();
            this.Attendance_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceUltraGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentprofileDataSet6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentprofileDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingNavigator)).BeginInit();
            this.attendanceBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // Attendance_Fill_Panel
            // 
            // 
            // Attendance_Fill_Panel.ClientArea
            // 
            this.Attendance_Fill_Panel.ClientArea.Controls.Add(this.attendanceUltraGrid);
            this.Attendance_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.Attendance_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Attendance_Fill_Panel.Location = new System.Drawing.Point(8, 57);
            this.Attendance_Fill_Panel.Name = "Attendance_Fill_Panel";
            this.Attendance_Fill_Panel.Size = new System.Drawing.Size(1203, 526);
            this.Attendance_Fill_Panel.TabIndex = 0;
            // 
            // attendanceUltraGrid
            // 
            this.attendanceUltraGrid.DataSource = this.attendanceBindingSource1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 48;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 254;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 120;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 147;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 226;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 147;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 45;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 78;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 114;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            this.attendanceUltraGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.attendanceUltraGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attendanceUltraGrid.Location = new System.Drawing.Point(0, 0);
            this.attendanceUltraGrid.Name = "attendanceUltraGrid";
            this.attendanceUltraGrid.Size = new System.Drawing.Size(1203, 526);
            this.attendanceUltraGrid.TabIndex = 0;
            this.attendanceUltraGrid.Text = "ultraGrid1";
            this.attendanceUltraGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.attendanceUltraGrid_InitializeLayout);
            // 
            // attendanceBindingSource1
            // 
            this.attendanceBindingSource1.DataMember = "attendance";
            this.attendanceBindingSource1.DataSource = this.studentprofileDataSet6;
            // 
            // studentprofileDataSet6
            // 
            this.studentprofileDataSet6.DataSetName = "studentprofileDataSet6";
            this.studentprofileDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // attendanceBindingSource
            // 
            this.attendanceBindingSource.DataMember = "attendance";
            this.attendanceBindingSource.DataSource = this.studentprofileDataSet2;
            // 
            // studentprofileDataSet2
            // 
            this.studentprofileDataSet2.DataSetName = "studentprofileDataSet2";
            this.studentprofileDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _Attendance_UltraFormManager_Dock_Area_Left
            // 
            this._Attendance_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._Attendance_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._Attendance_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._Attendance_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Attendance_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._Attendance_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 8;
            this._Attendance_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 32);
            this._Attendance_UltraFormManager_Dock_Area_Left.Name = "_Attendance_UltraFormManager_Dock_Area_Left";
            this._Attendance_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(8, 551);
            // 
            // _Attendance_UltraFormManager_Dock_Area_Right
            // 
            this._Attendance_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._Attendance_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._Attendance_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._Attendance_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Attendance_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._Attendance_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 8;
            this._Attendance_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(1211, 32);
            this._Attendance_UltraFormManager_Dock_Area_Right.Name = "_Attendance_UltraFormManager_Dock_Area_Right";
            this._Attendance_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(8, 551);
            // 
            // _Attendance_UltraFormManager_Dock_Area_Top
            // 
            this._Attendance_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._Attendance_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._Attendance_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._Attendance_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Attendance_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._Attendance_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._Attendance_UltraFormManager_Dock_Area_Top.Name = "_Attendance_UltraFormManager_Dock_Area_Top";
            this._Attendance_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(1219, 32);
            // 
            // _Attendance_UltraFormManager_Dock_Area_Bottom
            // 
            this._Attendance_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._Attendance_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._Attendance_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._Attendance_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Attendance_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._Attendance_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 8;
            this._Attendance_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 583);
            this._Attendance_UltraFormManager_Dock_Area_Bottom.Name = "_Attendance_UltraFormManager_Dock_Area_Bottom";
            this._Attendance_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(1219, 8);
            // 
            // attendanceTableAdapter
            // 
            this.attendanceTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.attendanceTableAdapter = this.attendanceTableAdapter;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.UpdateOrder = NFC.studentprofileDataSet6TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // attendanceBindingNavigatorSaveItem
            // 
            this.attendanceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.attendanceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("attendanceBindingNavigatorSaveItem.Image")));
            this.attendanceBindingNavigatorSaveItem.Name = "attendanceBindingNavigatorSaveItem";
            this.attendanceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.attendanceBindingNavigatorSaveItem.Text = "Save Data";
            this.attendanceBindingNavigatorSaveItem.Click += new System.EventHandler(this.attendanceBindingNavigatorSaveItem_Click);
            // 
            // attendanceBindingNavigator
            // 
            this.attendanceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.attendanceBindingNavigator.BindingSource = this.attendanceBindingSource;
            this.attendanceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.attendanceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.attendanceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.attendanceBindingNavigatorSaveItem});
            this.attendanceBindingNavigator.Location = new System.Drawing.Point(8, 32);
            this.attendanceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.attendanceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.attendanceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.attendanceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.attendanceBindingNavigator.Name = "attendanceBindingNavigator";
            this.attendanceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.attendanceBindingNavigator.Size = new System.Drawing.Size(1203, 25);
            this.attendanceBindingNavigator.TabIndex = 5;
            this.attendanceBindingNavigator.Text = "bindingNavigator1";
            // 
            // attendanceTableAdapter1
            // 
            this.attendanceTableAdapter1.ClearBeforeFill = true;
            // 
            // Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 591);
            this.Controls.Add(this.Attendance_Fill_Panel);
            this.Controls.Add(this.attendanceBindingNavigator);
            this.Controls.Add(this._Attendance_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._Attendance_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._Attendance_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._Attendance_UltraFormManager_Dock_Area_Bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Attendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Attendance_FormClosing);
            this.Load += new System.EventHandler(this.Attendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.Attendance_Fill_Panel.ClientArea.ResumeLayout(false);
            this.Attendance_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attendanceUltraGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentprofileDataSet6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentprofileDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingNavigator)).EndInit();
            this.attendanceBindingNavigator.ResumeLayout(false);
            this.attendanceBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel Attendance_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _Attendance_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _Attendance_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _Attendance_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _Attendance_UltraFormManager_Dock_Area_Bottom;
        private System.Windows.Forms.BindingSource attendanceBindingSource;
        private studentprofileDataSet2 studentprofileDataSet2;
        private studentprofileDataSet6TableAdapters.attendanceTableAdapter attendanceTableAdapter;
        private studentprofileDataSet6TableAdapters.TableAdapterManager tableAdapterManager;
        private System.IO.Ports.SerialPort serialPort;
        private Infragistics.Win.UltraWinGrid.UltraGrid attendanceUltraGrid;
        private System.Windows.Forms.BindingNavigator attendanceBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton attendanceBindingNavigatorSaveItem;
        private studentprofileDataSet6 studentprofileDataSet6;
        private System.Windows.Forms.BindingSource attendanceBindingSource1;
        private studentprofileDataSet6TableAdapters.attendanceTableAdapter attendanceTableAdapter1;
    }
}