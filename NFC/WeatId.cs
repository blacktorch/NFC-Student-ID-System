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
using System.IO;
using System.IO.Ports;
using Infragistics.Win.UltraWinGrid;
namespace NFC
{
    public partial class WeatId : Form
    {
        private bool btnState = false;
        bool isRecord;
        //string server, database, uid, password, 
        //MySqlConnection connection;
        string btData;
        //MySqlCommand cmd;
        //MySqlDataAdapter da;
        string imagename;
        bool IsTagScanned = false;
        string bluetoothData;
        char[] buffer = new char[14];
        int collegeId, courseId;
        //int rowCount;
       // StringBuilder buildData = new StringBuilder();
       
       // private bool regBtnState = false;
       // private bool scanBtnState = true;
       // private bool attendBtnState = false;
       // private bool editBtnState = false;
        Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
        delegate void SetTextCallback(string text);
        delegate void SetPanelVisible(Infragistics.Win.UltraWinTabControl.UltraTabPageControl tab);
        public string CourseName { get; set; }
        

        public WeatId()
        {
            InitializeComponent();
            tabControl.Tabs["tab2"].Selected = true;
            Helper.dbConnection();
            foreach (string s in SerialPort.GetPortNames())
            {
                ports.Items.Add(s);
            }
            makePanelInvisible();
            
        }

        public bool GetRecordState()
        {
            return isRecord;
        }

        public string GetAttendanceType()
        {
            return attendTypeCombo.Text;
        }

        public string GetPortName()
        {
            return ports.Text.Trim();//bluetooth.PortName;
        }
        
        public int GetCourseId()
        {
            return courseId;
        }

        private string getMySqlDateTimeFormat(string datetime)
        {
            string month, day, year, time, mysqlDateTime;
            month = "";
            day = "";
            year = "";
            time = "00:00:00";
            switch (datetime.Length)
            {
                case 20:
                    month = "0" + datetime.Substring(0, 1);
                    day = "0" + datetime.Substring(2, 1);
                    year = datetime.Substring(4, 4);
                    break;
                case 21:
                    if (datetime.Substring(1, 1) == "/")
                    {
                        month = "0" + datetime.Substring(0, 1);
                        day = datetime.Substring(2, 2);
                        year = datetime.Substring(5, 4);
                    }
                    else if (datetime.Substring(2, 1) == "/")
                    {
                        month = datetime.Substring(0, 2);
                        day = "0" + datetime.Substring(3, 1);
                        year = datetime.Substring(5, 4);
                    }
                    break;
                case 22:
                    month = datetime.Substring(0, 2);
                    day =  datetime.Substring(3, 2);
                    year = datetime.Substring(6, 4);
                    break;
                default:
                    break;
            }
            mysqlDateTime = year + "-" + month + "-" + day + " " + time;
            return mysqlDateTime;
        }

        private void setCourseId(string course)
        {
            Helper.connection.Open();

            Helper.cmd = new MySqlCommand("SELECT CourseId FROM course WHERE Course = '" + course + "'", Helper.connection);
            MySqlDataReader sdr = Helper.cmd.ExecuteReader();
            while (sdr.Read())
            {
                courseId = sdr.GetInt16(0);
            }
            sdr.Close();
            
            CourseName = selectCourse.Text;
           
            Helper.connection.Close();
        }

        private void addCourse()
        {
            string query = "INSERT INTO course(Course) VALUES(@Course)";
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.cmd.Parameters.AddWithValue("@Course", courseTxtBox.Text.Trim());
            Helper.OpenConnection();
            int rowsAffected = Helper.cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                //attendanceUltraGrid.Text = courseTxtBox.Text;
                //attendanceUltraGrid.Visible = true;
                courseTxtBox.Text = "";
                MessageBox.Show("Course added successfully, select course from drop down");
            }
            Helper.CloseConnection();
        }

        private void RegisterStudent()
        {
            try
            {
                //Initialize a file stream to read the image file
                FileStream fs = new FileStream(@imagename, FileMode.Open, FileAccess.Read);
                //Initialize a byte array with size of stream
                byte[] imgByteArr = new byte[fs.Length];
                //Read data from the file stream and put into the byte array
                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                string CmdString = "INSERT INTO profile(Name, Department, College, RegNumber, " +
                "MatricNumber, CourseofStudy, Level, Gender, DateofBirth, FeesPaid, Passport) " +
                "VALUES(@Name, @Department, @College, @RegNumber, @MatricNumber, @CourseofStudy, " +
                "@Level, @Gender, @DateofBirth, @FeesPaid, @Passport)";
                Helper.cmd = new MySqlCommand(CmdString, Helper.connection);

                Helper.cmd.Parameters.AddWithValue("@Name", fullName.Text);
                Helper.cmd.Parameters.AddWithValue("@Department", dept.Text);
                Helper.cmd.Parameters.AddWithValue("@College", college.Text);
                Helper.cmd.Parameters.AddWithValue("@RegNumber", regNo.Text);
                Helper.cmd.Parameters.AddWithValue("@MatricNumber", matNumber.Text);
                Helper.cmd.Parameters.AddWithValue("@CourseofStudy", courseOfStudy.Text);
                Helper.cmd.Parameters.AddWithValue("@Level", level.Text);
                Helper.cmd.Parameters.AddWithValue("@Gender", gender.Value);
                Helper.cmd.Parameters.AddWithValue("@DateofBirth", dateOfBirth.Value);
                Helper.cmd.Parameters.AddWithValue("@FeesPaid", fees.Value);
                Helper.cmd.Parameters.AddWithValue("@Passport", imgByteArr);
 
                Helper.connection.Open();
                int RowsAffected = Helper.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Student registered successfully");
                    fullName.Text = "";
                    dept.Text= "";
                    college.Text= "";
                    regNo.Text= "";
                    matNumber.Text= "";
                    courseOfStudy.Text= "";
                    level.Text= "";
                    dateOfBirth.Text= DateTime.Today.ToShortDateString();
                    imgByteArr = null;
                    imagename = null;
                    IsTagScanned = false;
                    
                }
                else
                {
                    MessageBox.Show("Incomplete data!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Helper.connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Helper.connection.State == ConnectionState.Open)
                {
                    Helper.connection.Close();
                }
            }
        }

        private void pullStudentData(string regNo)
        {
            string query = "SELECT * FROM profile WHERE RegNumber = '"+ regNo.Trim() +"'";
            Helper.connection.Open();
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.da = new MySqlDataAdapter(Helper.cmd);   
            DataTable dt = new DataTable();
            Helper.da.Fill(dt);
            //MessageBox.Show(regNo);
            //nameLabel.Text = dt.Rows[0][1].ToString();
            SetNameText(dt.Rows[0][1].ToString());
            //deptLabel.Text = dt.Rows[0][2].ToString();
            SetDeptText(dt.Rows[0][2].ToString());
            //collegeLabel.Text = dt.Rows[0][3].ToString();
            SetCollegeText(dt.Rows[0][3].ToString());
            //regNumLabel.Text = dt.Rows[0][4].ToString();
            SetRegNoText(dt.Rows[0][4].ToString());
            //matricLabel.Text = dt.Rows[0][5].ToString();
            SetMatricText(dt.Rows[0][5].ToString());
            //courseLabel.Text = dt.Rows[0][6].ToString();
            SetCourseText(dt.Rows[0][6].ToString());
            //levelLabel.Text = dt.Rows[0][7].ToString();
            SetLevelText(dt.Rows[0][7].ToString());
            //genderLabel.Text = dt.Rows[0][8].ToString();
            SetGenderText(dt.Rows[0][8].ToString());
            //dobLabel.Text = dt.Rows[0][9].ToString().Substring(0,10);
            SetDobText(dt.Rows[0][9].ToString().Substring(0, 10));
            if (Convert.ToInt16(dt.Rows[0][10]) == 1)
            {
                this.feesPanel.BackgroundImage = global::NFC.Properties.Resources.Fp;
            }
            else if (Convert.ToInt16(dt.Rows[0][10]) == 0)
            {
                this.feesPanel.BackgroundImage = global::NFC.Properties.Resources.fp2;
            }

            byte[] img = (byte[])dt.Rows[0][11]; 
            MemoryStream ms = new MemoryStream(img);
            pictureBox.Image = Image.FromStream(ms);
            Helper.da.Dispose();
            Helper.connection.Close();
        }

        #region junk
        //private void takeAttendance(string regNo)
        //{
        //    string query = "SELECT Name, Department, College, RegNumber FROM profile WHERE RegNumber = '" + regNo.Trim() + "'";
        //    OpenConnection();
        //    cmd = new MySqlCommand(query, connection);
        //    da = new MySqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    rowCount = this.attendanceUltraGrid.DisplayLayout.Rows.Count;
        //    //MessageBox.Show(rowCount.ToString());
        //   // this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();

        //    UltraGridRow row =  this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();
        //    if (row == null)
        //    {
        //        row = this.attendanceUltraGrid.DisplayLayout.Bands[0].AddNew();
        //        row.ParentCollection.Move(row, rowCount);
        //    }
        //    else
        //    {
        //        row.ParentCollection.Move(row, rowCount);
        //    }
            
        //    this.attendanceUltraGrid.ActiveRowScrollRegion.ScrollRowIntoView(row);

        //    //this.bindingNavigator1.BindingSource.AddNew();
        //    //this.bindingNavigator1.BindingSource.MoveLast();

        //    this.attendanceUltraGrid.Rows[rowCount].Cells[6].Value = courseId;
        //    this.attendanceUltraGrid.Rows[rowCount].Cells[1].Value = dt.Rows[0][0].ToString();
        //    this.attendanceUltraGrid.Rows[rowCount].Cells[2].Value = dt.Rows[0][3].ToString();
        //    this.attendanceUltraGrid.Rows[rowCount].Cells[3].Value = dt.Rows[0][1].ToString();
        //    this.attendanceUltraGrid.Rows[rowCount].Cells[4].Value = dt.Rows[0][2].ToString();
        //    this.attendanceUltraGrid.Rows[rowCount].Cells[5].Value = DateTime.Now.ToShortDateString();
        //    this.attendanceUltraGrid.Rows[rowCount].Activate();
        //    MethodInvoker action = () => this.attendanceUltraGrid.Refresh();
        //    attendanceUltraGrid.BeginInvoke(action);
            
            
        //    //this.resultUltraGrid.Rows[rowCount].Cells[19].Value = semesterId;
        //    //this.resultUltraGrid.Rows[rowCount].Cells[20].Value = sessionId;
        //    //this.resultUltraGrid.Rows[rowCount].Cells[21].Value = studentsDeptId;
        //    CloseConnection();
        //}
#endregion

        private void ScanDataReceived()
        {
            bluetoothData = bluetooth.ReadLine();
            //MessageBox.Show(bluetoothData);
            if (bluetoothData.Trim() == null || bluetoothData.Trim() == "")
            {
                MessageBox.Show("Invalid NFC wristband Identification");
                return;
            }
            else if (bluetoothData.Substring(0, 2) == "MO")
            {
                pullStudentData(bluetoothData);

                //if (ultraTabPageControl2.InvokeRequired)
                //{
                MethodInvoker action = () => makePanelVisible();
                ultraTabPageControl2.BeginInvoke(action);
                //}
                //makePanelVisible();
            }
            else
            {
                MessageBox.Show("Invalid NFC wristband Identification");
                return;
            }
        }

        //private void attendDataReceived()
        //{
        //    bluetoothData = bluetooth.ReadLine();
        //    //buildData.Clear();
        //    if (bluetoothData.Substring(0, 2) == "MO")
        //    {
        //        takeAttendance(bluetoothData);
        //    }
        //}

        private void makePanelInvisible()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            newCourseTxtBox.Visible = false;
            submitBtn.Visible = false;
            cancelBtn.Visible = false;
            //attendanceUltraGrid.Visible = false;
          //  bindingNavigator1.Visible = false;
            scanBandPic.Visible = true;
        }

        private void makePanelVisible()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            scanBandPic.Visible = false;
        }

        private void SetNameText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.nameLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetNameText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.nameLabel.Text = text;
            }
        }

        private void SetDeptText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.deptLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDeptText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.deptLabel.Text = text;
            }
        }

        private void SetCollegeText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.collegeLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCollegeText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.collegeLabel.Text = text;
            }
        }

        private void SetRegNoText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.regNumLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetRegNoText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.regNumLabel.Text = text;
            }
        }

        private void SetMatricText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.matricLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetMatricText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.matricLabel.Text = text;
            }
        }

        private void SetCourseText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.courseLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCourseText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.courseLabel.Text = text;
            }
        }

        private void SetLevelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.levelLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLevelText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.levelLabel.Text = text;
            }
        }

        private void SetGenderText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.genderLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetGenderText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.genderLabel.Text = text;
            }
        }

        private void SetDobText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (this.dobLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDobText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.dobLabel.Text = text;
            }
        }

       
        private void ultraTabSharedControlsPage1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
        {
           // if(!bluetooth.IsOpen)
            //{
               // bluetooth.Open();
           // }
        }

        private void bluetooth_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (tabControl.Tabs["tab2"].Selected == true)
            {
                ScanDataReceived();
            }
            else if (tabControl.Tabs["tab3"].Selected == true)
            {
               // attendDataReceived();
            }
            
            
        }

        private void regBtn_Click(object sender, System.EventArgs e)
        {
                appearance10.BackColor = System.Drawing.Color.White;
                appearance10.FontData.Name = "Leelawadee UI";
                appearance10.FontData.SizeInPoints = 9F;
                appearance10.ForeColor = System.Drawing.Color.Black;
                this.regBtn.Appearance = appearance10;
                this.regBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
                appearance11.BackColor = System.Drawing.Color.Silver;
                this.regBtn.HotTrackAppearance = appearance11;
                //
                appearance12.BackColor = System.Drawing.Color.DimGray;
                appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.GlassLeft50Bright;
                appearance12.BorderAlpha = Infragistics.Win.Alpha.Transparent;
                appearance12.FontData.Name = "Segoe UI Semibold";
                appearance12.FontData.SizeInPoints = 10F;
                appearance12.ForeColor = System.Drawing.Color.White;
                this.scanBtn.Appearance = appearance12;
                this.scanBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
                appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                this.scanBtn.HotTrackAppearance = appearance13;
            //
                this.attendBtn.Appearance = appearance12;
                this.attendBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
                this.attendBtn.HotTrackAppearance = appearance13;
            //
                this.editBtn.Appearance = appearance12;
                this.editBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
                this.editBtn.HotTrackAppearance = appearance13;

            tabControl.Tabs["tab1"].Selected = true;
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.FontData.Name = "Leelawadee UI";
            appearance10.FontData.SizeInPoints = 9F;
            appearance10.ForeColor = System.Drawing.Color.Black;
            this.scanBtn.Appearance = appearance10;
            this.scanBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            appearance11.BackColor = System.Drawing.Color.Silver;
            this.scanBtn.HotTrackAppearance = appearance11;
            //
            appearance12.BackColor = System.Drawing.Color.DimGray;
            appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.GlassLeft50Bright;
            appearance12.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance12.FontData.Name = "Segoe UI Semibold";
            appearance12.FontData.SizeInPoints = 10F;
            appearance12.ForeColor = System.Drawing.Color.White;
            this.regBtn.Appearance = appearance12;
            this.regBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.regBtn.HotTrackAppearance = appearance13;
            //
            this.attendBtn.Appearance = appearance12;
            this.attendBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.attendBtn.HotTrackAppearance = appearance13;
            //
            this.editBtn.Appearance = appearance12;
            this.editBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.editBtn.HotTrackAppearance = appearance13;
            tabControl.Tabs["tab2"].Selected = true;
        }

        private void attendBtn_Click(object sender, EventArgs e)
        {
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.FontData.Name = "Leelawadee UI";
            appearance10.FontData.SizeInPoints = 9F;
            appearance10.ForeColor = System.Drawing.Color.Black;
            this.attendBtn.Appearance = appearance10;
            this.attendBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            appearance11.BackColor = System.Drawing.Color.Silver;
            this.attendBtn.HotTrackAppearance = appearance11;
            //
            appearance12.BackColor = System.Drawing.Color.DimGray;
            appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.GlassLeft50Bright;
            appearance12.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance12.FontData.Name = "Segoe UI Semibold";
            appearance12.FontData.SizeInPoints = 10F;
            appearance12.ForeColor = System.Drawing.Color.White;
            this.regBtn.Appearance = appearance12;
            this.regBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.regBtn.HotTrackAppearance = appearance13;
            //
            this.scanBtn.Appearance = appearance12;
            this.scanBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.scanBtn.HotTrackAppearance = appearance13;
            //
            this.editBtn.Appearance = appearance12;
            this.editBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.editBtn.HotTrackAppearance = appearance13;
            tabControl.Tabs["tab3"].Selected = true;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.FontData.Name = "Leelawadee UI";
            appearance10.FontData.SizeInPoints = 9F;
            appearance10.ForeColor = System.Drawing.Color.Black;
            this.editBtn.Appearance = appearance10;
            this.editBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            appearance11.BackColor = System.Drawing.Color.Silver;
            this.editBtn.HotTrackAppearance = appearance11;
            //
            appearance12.BackColor = System.Drawing.Color.DimGray;
            appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.GlassLeft50Bright;
            appearance12.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance12.FontData.Name = "Segoe UI Semibold";
            appearance12.FontData.SizeInPoints = 10F;
            appearance12.ForeColor = System.Drawing.Color.White;
            this.regBtn.Appearance = appearance12;
            this.regBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.regBtn.HotTrackAppearance = appearance13;
            //
            this.attendBtn.Appearance = appearance12;
            this.attendBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.attendBtn.HotTrackAppearance = appearance13;
            //
            this.scanBtn.Appearance = appearance12;
            this.scanBtn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.scanBtn.HotTrackAppearance = appearance13;
            tabControl.Tabs["tab4"].Selected = true;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (btnState == false)
            {
                if (ports.Text == "")
                {
                    MessageBox.Show("Please select a bluetooth COM Port");
                    return;
                }
                this.btConnect.BackgroundImage = global::NFC.Properties.Resources.on_3x;
                //StartTime();
                if (!bluetooth.IsOpen)
                {
                    try
                    {
                        bluetooth.PortName = ports.Text;
                        bluetooth.Open(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.btConnect.BackgroundImage = global::NFC.Properties.Resources.off_3x;
                        btnState = false;
                        return;
                    }
                }
                btnState = true;
            }
            else if (btnState == true)
            {
                this.btConnect.BackgroundImage = global::NFC.Properties.Resources.off_3x;
                //EndTime();
                bluetooth.Close();
                btnState = false;
            }
        }

        private void idScan_Click(object sender, EventArgs e)
        {
            if (regNo.Text=="")
            {
                MessageBox.Show("Please enter student's registration number.");
                return;
            }
            try
            {
                if (!bluetooth.IsOpen)
                {
                    bluetooth.PortName = ports.Text;
                    bluetooth.Open();
                    btData = regNo.Text + "..";
                    bluetooth.WriteLine(btData.Trim());
                    bluetooth.DiscardOutBuffer();
                    IsTagScanned = true;
                }
                else
                {
                    btData = regNo.Text + "..";
                    bluetooth.WriteLine(btData.Trim());
                    bluetooth.DiscardOutBuffer();
                    IsTagScanned = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            try
            {

                FileDialog fldlg = new OpenFileDialog();

                //specify your own initial directory

                fldlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                //this will allow only those file extensions to be added

                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png";

                if (fldlg.ShowDialog() == DialogResult.OK)
                {

                    imagename = fldlg.FileName;
                    filePathDisplay.Text = imagename;
                    Bitmap newimg = new Bitmap(imagename);

                    //passport.SizeMode = PictureBoxSizeMode.StretchImage;

                    passport.Image = (Image)newimg;

                }

                fldlg = null;

            }

            catch (System.ArgumentException ae)
            {

                imagename = " ";

                MessageBox.Show(ae.Message.ToString());

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());

            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (passport.Image == null || imagename == null)
            {
                MessageBox.Show("Please select a passport sized photograph.");
                return;
            }
            else if (regNo.Text == "")
            {
                MessageBox.Show("Please input student's registration number");
                return;
            }
            else if (fullName.Text == "")
            {
                MessageBox.Show("Please input student's name");
                return;
            }
            else if (matNumber.Text == "")
            {
                MessageBox.Show("Please input student's matric number");
                return;
            }
            else if (dateOfBirth.Text == DateTime.Today.ToShortDateString() || dateOfBirth.Text == "")
            {
                MessageBox.Show("Please input student's correct date of birth");
                return;
            }
            else if (gender.Value == null)
            {
                MessageBox.Show("Please select student's gender");
                return;
            }
            else if (level.Text == "")
            {
                MessageBox.Show("Please select student's level");
                return;
            }
            else if (college.Text == "")
            {
                MessageBox.Show("Please select student's college");
                return;
            }
            else if (dept.Text == "")
            {
                MessageBox.Show("Please select student's department");
                return;
            }
            else if (courseOfStudy.Text == "")
            {
                MessageBox.Show("Please select student's course of study");
                return;
            }
            else if (fees.Value == null)
            {
                MessageBox.Show("Please select student's fees status");
                return;
            }
            else if (!IsTagScanned)
            {
                MessageBox.Show("Please scan tag with external NFC device");
                return;
            }

            RegisterStudent();

         
        }

        private void ports_Click(object sender, EventArgs e)
        {
            ports.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                ports.Items.Add(s);
            }  
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            makePanelInvisible();
        }

        private void WeatId_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentprofileDataSet5.department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.studentprofileDataSet5.department);
            // TODO: This line of code loads data into the 'studentprofileDataSet4.course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.studentprofileDataSet4.course);
            // TODO: This line of code loads data into the 'studentprofileDataSet3.college' table. You can move, or remove it, as needed.
            this.collegeTableAdapter1.Fill(this.studentprofileDataSet3.college);
            // TODO: This line of code loads data into the 'studentprofileDataSet2.attendance' table. You can move, or remove it, as needed.
            this.attendanceTableAdapter.Fill(this.studentprofileDataSet2.attendance);
            // TODO: This line of code loads data into the 'studentprofileDataSet1.college' table. You can move, or remove it, as needed.
            this.collegeTableAdapter.Fill(this.studentprofileDataSet1.college);

        }

        private void LoadDepartment()
        {
            dept.Text = "";
            dept.Items.Clear();
            //SqlCeConnection con = new SqlCeConnection(@"Data Source=|DataDirectory|\GraderDB.sdf");
            Helper.connection.Open();

            Helper.cmd = new MySqlCommand("SELECT idcollege FROM college WHERE Code = '" + college.Text.Trim() + "'", Helper.connection);
            MySqlDataReader sdr = Helper.cmd.ExecuteReader();
            while (sdr.Read())
            {
                collegeId = sdr.GetInt16(0);
            }
            sdr.Close();
            //MessageBox.Show(collegeId.ToString());
            Helper.cmd = new MySqlCommand("SELECT Department FROM department WHERE idcollege= '" + collegeId + "'", Helper.connection);

            using (MySqlDataReader saReader = Helper.cmd.ExecuteReader())
            {
                while (saReader.Read())
                {
                    dept.Items.Add(saReader["Department"].ToString());
                }
            }
            //sdr.Close();
            Helper.connection.Close();
        }

        private void college_ValueChanged(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        private void dept_ValueChanged(object sender, EventArgs e)
        {
            courseOfStudy.Text = dept.Text;
        }

        private void srtBtn_Click(object sender, EventArgs e)
        {
            if (selectCourse.Text == "")
            {
                MessageBox.Show("Select course, if none is available, please add a course");
                return;
            }

            setCourseId(selectCourse.Text.Trim());
            if (bluetooth.IsOpen)
            {
                bluetooth.Close();
                System.Threading.Thread.Sleep(200);
            }
            isRecord = false;
            Attendance attend = new Attendance();
            attend.ShowDialog();
        }

        private void addCourseBtn_Click(object sender, EventArgs e)
        {
            if (courseTxtBox.Text == "")
            {
                MessageBox.Show("Enter course!");
                return;
            }
            addCourse();
        }

        private void retrieveRecords()
        {
           
            string query = "INSERT INTO attendance (Name, RegNumber, Department, College, DateTime, CourseId, Level, Type)" +
                " SELECT Name, RegNumber, Department, College, DateTime, CourseId, Level, Type FROM allattendance WHERE " +
                "CourseId = '" + courseId + "' AND Type = '" + recAttendTypeCombo.Text.Trim() + "' AND Department = '" +
               recDeptCombo.Text.Trim() + "' AND DateTime = '" + getMySqlDateTimeFormat(recAttendDate.Value.ToString()) + "'";
            Helper.OpenConnection();
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.cmd.ExecuteNonQuery();
            Helper.CloseConnection();

        }

        public void OpenBluetooth()
        {
            try
            {
                bluetooth.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                System.Threading.Thread.Sleep(200);
                bluetooth.Open();
            }
            
        }

        private void selectCourse_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentprofileDataSet4.course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.studentprofileDataSet4.course);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void updateCourseBtn_Click(object sender, EventArgs e)
        {
            if (courseCombo.Text=="")
            {
                MessageBox.Show("Select a course to update!");
                return;
            }
            this.topPanel.Size = new System.Drawing.Size(912, 155);
            newCourseTxtBox.Visible = true;
            submitBtn.Visible = true;
            cancelBtn.Visible = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.topPanel.Size = new System.Drawing.Size(912, 113);
            newCourseTxtBox.Visible = false;
            submitBtn.Visible = false;
            cancelBtn.Visible = false;
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            if (newCourseTxtBox.Text=="")
            {
                MessageBox.Show("Enter course's new name");
                return;
            }

            string query = "UPDATE course SET Course = '" + newCourseTxtBox.Text.Trim() + "' WHERE Course = '" + courseCombo.Text.Trim() + "'";
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.OpenConnection();
            int x = Helper.cmd.ExecuteNonQuery();
            if (x>0)
            {
                MessageBox.Show("Course updated successfully!");
            }
            Helper.CloseConnection();
            this.topPanel.Size = new System.Drawing.Size(912, 113);
            courseCombo.Text = newCourseTxtBox.Text;
            newCourseTxtBox.Text = "";
            newCourseTxtBox.Visible = false;
            submitBtn.Visible = false;
            cancelBtn.Visible = false;
        }

        private void courseCombo_BeforeDropDown(object sender, CancelEventArgs e)
        {
            this.courseTableAdapter.Fill(this.studentprofileDataSet4.course);
        }

        private void selectCourse_BeforeDropDown(object sender, CancelEventArgs e)
        {
            this.courseTableAdapter.Fill(this.studentprofileDataSet4.course);
        }

        private void recAttendCourseCombo_BeforeDropDown(object sender, CancelEventArgs e)
        {
            this.courseTableAdapter.Fill(this.studentprofileDataSet4.course);
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (courseCombo.Text == "")
            {
                MessageBox.Show("Select course to delete");
                return;
            }

            string query = "DELETE FROM course WHERE Course = '" + courseCombo.Text.Trim() + "'";
            Helper.cmd = new MySqlCommand(query, Helper.connection);
            Helper.OpenConnection();
            int x = Helper.cmd.ExecuteNonQuery();
            if (x > 0)
            {
                MessageBox.Show("Course deleted successfully!");
            }
            Helper.CloseConnection();
        }

        private void atdRec_Click(object sender, EventArgs e)
        {
            if (recAttendTypeCombo.Text == "")
            {
                MessageBox.Show("Select the attendance type to view records");
                return;
            }
            else if (recAttendCourseCombo.Text == "")
            {
                MessageBox.Show("Select a course to view records");
                return;
            }
            else if (recDeptCombo.Text == "")
            {
                MessageBox.Show("Select a department to view records");
                return; 
            }
            else if (recAttendDate.Text == "")
            {
                MessageBox.Show("Select a date to view records");
                return;
            }
            setCourseId(recAttendCourseCombo.Text.Trim());
            //MessageBox.Show(getMySqlDateTimeFormat(recAttendDate.Value.ToString()));
            retrieveRecords();
            isRecord = true;
            Attendance attend = new Attendance();
            attend.ShowDialog();
        }
    }
}
