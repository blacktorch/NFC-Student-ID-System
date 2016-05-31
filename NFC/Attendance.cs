using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Infragistics.Win.UltraWinGrid;

namespace NFC
{
    public partial class Attendance : Form
    {

        string bluetoothData;
        int rowCount;
        bool isSaved = false;
        bool isRecord = false;
        WeatId mainForm = (WeatId)Application.OpenForms["WeatId"];
        public Attendance()
        {
            InitializeComponent();
            bindingNavigatorAddNewItem.Enabled = false;
            bindingNavigatorDeleteItem.Enabled = false;
        }

        private void saveToSecondaryTable()
        {
            string query = "INSERT INTO allattendance (Name, RegNumber, Department, College, DateTime, CourseId, Level, Type)" +
                " SELECT Name, RegNumber, Department, College, DateTime, CourseId, Level, Type FROM attendance";
            Helper.OpenConnection();
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.cmd.ExecuteNonQuery();
            Helper.CloseConnection();
        }

        private void deletePrimaryData()
        {
            string query = "TRUNCATE TABLE attendance";
            Helper.OpenConnection();
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.cmd.ExecuteNonQuery();
            Helper.CloseConnection();
        }

        private void attendanceBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                return;
            }
            else
            {
                //this.attendanceUltraGrid.Rows[rowCount - 1].Delete();
                this.Validate();
                this.attendanceBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.studentprofileDataSet6);
                MethodInvoker action1 = () => this.attendanceUltraGrid.Rows[rowCount - 1].Update();
                attendanceUltraGrid.BeginInvoke(action1);
            }

            isSaved = true;
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentprofileDataSet6.attendance' table. You can move, or remove it, as needed.
            this.attendanceTableAdapter1.Fill(this.studentprofileDataSet6.attendance);
            // TODO: This line of code loads data into the 'studentprofileDataSet6.attendance' table. You can move, or remove it, as needed.
            this.attendanceTableAdapter1.Fill(this.studentprofileDataSet6.attendance);
            WeatId mainForm = (WeatId)Application.OpenForms["WeatId"];
            if (mainForm != null)
            {
                if (mainForm.GetRecordState() == false)
                {
                    serialPort.PortName = mainForm.GetPortName();
                    System.Threading.Thread.Sleep(200);
                    serialPort.Open();
                    //MethodInvoker action = () => this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();
                    //attendanceUltraGrid.BeginInvoke(action);
                    //System.Threading.Thread.Sleep(200);
                }
               
            }
            // TODO: This line of code loads data into the 'studentprofileDataSet2.attendance' table. You can move, or remove it, as needed.
            //this.attendanceTableAdapter.Fill(this.studentprofileDataSet2.attendance);
            attendanceUltraGrid.Text = mainForm.CourseName;
        }

        private void attendanceUltraGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
            this.attendanceUltraGrid.DisplayLayout.Bands[0].Columns[8].CellActivation = Activation.NoEdit;
        }

        private void Attendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm.GetRecordState() == false)
            {
                serialPort.Close();
                mainForm.OpenBluetooth();
                if (!isSaved)
                {
                    //this.attendanceUltraGrid.Rows[rowCount - 1].Delete();
                    this.Validate();
                    this.attendanceBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.studentprofileDataSet6);
                    MethodInvoker action1 = () => this.attendanceUltraGrid.Rows[rowCount - 1].Update();
                    attendanceUltraGrid.BeginInvoke(action1);
                }
                saveToSecondaryTable();
                deletePrimaryData();
            }
            else
            {
                deletePrimaryData();
            }

        }

        private void attendDataReceived()
        {
            bluetoothData = serialPort.ReadLine();
            //buildData.Clear();
            if (bluetoothData.Trim() == null || bluetoothData.Trim() == "")
            {
                MessageBox.Show("Invalid NFC wristband Identification");
                return;
            }
            else if (bluetoothData.Substring(0, 2) == "MO")
            {
                takeAttendance(bluetoothData);
            }
            else
            {
                MessageBox.Show("Invalid NFC wristband Identification");
                return;
            }
        }

        private void takeAttendance(string regNo)
        {
            
            string query = "SELECT Name, Department, College, RegNumber, Level FROM profile WHERE RegNumber = '" + regNo.Trim() + "'";
            Helper.OpenConnection();
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.da = new MySqlDataAdapter(Helper.cmd);
            DataTable dt = new DataTable();
            Helper.da.Fill(dt);
            rowCount = this.attendanceUltraGrid.DisplayLayout.Rows.Count;

            if (rowCount > 0)
            {
                UltraGridBand band0 = this.attendanceUltraGrid.DisplayLayout.Bands[0];
                foreach (UltraGridRow row in band0.GetRowEnumerator(GridRowType.DataRow))
                {
                    if (dt.Rows[0][3].ToString() == row.Cells["RegNumber"].Value.ToString())
                    {
                        Helper.CloseConnection();
                        return;
                    }
                    else if (row == attendanceUltraGrid.Rows[rowCount - 1])
                    {
                        break;
                    }
                 
                }

            }

                         
                 MethodInvoker action = () => this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();
                 attendanceUltraGrid.BeginInvoke(action);
                 System.Threading.Thread.Sleep(200);

                 rowCount = this.attendanceUltraGrid.DisplayLayout.Rows.Count;

                

                 this.attendanceUltraGrid.Rows[rowCount - 1].Cells["Name"].Value = dt.Rows[0][0].ToString();
                 this.attendanceUltraGrid.Rows[rowCount-1].Cells[2].Value = dt.Rows[0][3].ToString();
                 this.attendanceUltraGrid.Rows[rowCount-1].Cells[3].Value = dt.Rows[0][1].ToString();
                 this.attendanceUltraGrid.Rows[rowCount-1].Cells[4].Value = dt.Rows[0][2].ToString();
                 this.attendanceUltraGrid.Rows[rowCount - 1].Cells["Level"].Value = dt.Rows[0][4].ToString();
                 this.attendanceUltraGrid.Rows[rowCount-1].Cells[5].Value = DateTime.Now.ToShortDateString();
                 this.attendanceUltraGrid.Rows[rowCount-1].Cells["CourseId"].Value = mainForm.GetCourseId();
                 this.attendanceUltraGrid.Rows[rowCount - 1].Cells["Type"].Value = mainForm.GetAttendanceType();
                 MethodInvoker action1 = () => this.attendanceUltraGrid.Rows[rowCount - 1].Update();
                 // this.attendanceUltraGrid.ActiveCell = attendanceUltraGrid.Rows[rowCount - 1].Cells[0];
                 // MethodInvoker action1 = () => this.attendanceUltraGrid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                 //attendanceUltraGrid.BeginInvoke(action1);
                 //MethodInvoker action2 = () => this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();
                 attendanceUltraGrid.BeginInvoke(action1);
                 //System.Threading.Thread.Sleep(200);
                 isSaved = false;
            Helper.CloseConnection();
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (!isRecord)
            {
                attendDataReceived();
            }
            
        }
    }
}
