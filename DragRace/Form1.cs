using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DragRacerPrep;
using System.Collections;

namespace DragRace
{
    public partial class Form1 : Form
    {
        DragRacerBase Cheetah = new DragRacerBase();
        DragRacerBase Lion = new DragRacerBase();
        DragRacerBase Wolf = new DragRacerBase();
        DragRacerBase Monkey = new DragRacerBase();

        int minSpeed = 300;
        int maxSpeed = 500;

        int attemptElho = 5;
        string username = "user";
        string password = "admin";

        int finishedAtPosition;

        int teller;

        bool jeromeClicked;
        bool SoetensClicked;
        bool KelderClicked;
        bool CiscoClicked;

        bool messege;

        int timerWatch;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 6;

            initViewsElho();

            pgbAttemptsElho.Maximum = attemptElho;
            pgbAttemptsElho.Value = attemptElho;

            NoAccess();

            tbPasswordElho.PasswordChar = '*'; // dit maakt de karaters in de textbox *
        }

        private void btnStartRaceElho_Click(object sender, EventArgs e)
        {
            teller++;
            StartRace();
            attemptElho++;

            lblChosenPlayerElho.Text = "";

            rtbStaticticsElho.AppendText("Started the race without Choosing a Player\n");

            #region setting line colors right
            pnlRaceJeromeTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeBotElho.BackColor = Color.FromArgb(63, 184, 232);

            pnlRaceSoetensTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceSoetensBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoBotElho.BackColor = Color.FromArgb(63, 184, 232);
            #endregion
        }

        /// <summary>
        /// zorgt ervoor de personen voortbewegen en weet wanneer wie is gefinished
        /// </summary>
        private void timerSetting(DragRacerBase m_NameElho, PictureBox m_pbNameElho, Label m_lblNaamPositionElho, string m_stringNameElho, Label m_lblNaamResultHomeElho, Label lblnaamtijd)
        {
            
            if (pnlFinishElho.Left > m_NameElho.GetActualPositionInLaneRounded)
            {
                m_NameElho.calculateCurrentPosition();
                m_pbNameElho.Left = m_NameElho.GetActualPositionInLaneRounded;
            }
            else
            {
                if (m_NameElho.flagged == false)
                {
                    finishedAtPosition++;

                    m_NameElho.endPos = finishedAtPosition;

                    m_lblNaamPositionElho.Text = finishedAtPosition.ToString();
                    m_lblNaamResultHomeElho.Text = finishedAtPosition.ToString();

                    
                    m_NameElho.opgeslagenPos = m_NameElho.opgeslagenPos + finishedAtPosition;
                    m_NameElho.opgeslagenTijd = timerWatch + timerWatch;

                    lblnaamtijd.Text = Convert.ToString(timerWatch);

                    m_NameElho.flagged = true;
                }
            }
        }

        #region timerPersonen

        private void timerJerome()
        {
            timerSetting(Cheetah, pbJeromeElho, lblJeromePositionElho, "jerome", lblJeromeResultHomeElho,lblJeromeTijd);

        }

        private void timerSoetens()
        {
            timerSetting(Lion, pbSoetensElho, lblSoetensPositionElho, "soetens", lblSoetensResultHomeElho,lblSoetensTijd);

        }

        private void timerKelder()
        {
            timerSetting(Wolf,pbKelderElho,lblKelderPositionElho,"kelder",lblKelderResultHomeElho, lblKelderTijd);
            
        }

        private void timerCisco()
        {
            timerSetting(Monkey, pbCiscoElho, lblCiscoPositionElho,"cisco", lblCiscoResultHomeElho, lblCiscoTijd);
            
        }

        #endregion

        /// <summary>
        /// methode dat alles installeert in de racers
        /// </summary>
        private void setters(DragRacerBase a_name, PictureBox m_pbName, Label m_lblNaamSnelheid, Label m_lblnaamStepSize)
        {
            m_pbName.Left = 3;
            a_name.resetPosition(m_pbName.Left);
            a_name.calculateCurrentPosition();
            a_name.flagged = false;
            #region jerome sneller en cisco langzamer!
            /*if (m_name == Cheetah)
            {
                Cheetah.setSpeed(maxSpeed, 1000);
            }
            else if (m_name == Monkey)
            {
                Monkey.setSpeed(50, minSpeed);
            }

            else
            {
                m_name.setSpeed(minSpeed, maxSpeed);
            }*/
            #endregion
            a_name.setSpeed(minSpeed, maxSpeed);

            m_lblNaamSnelheid.Text = Convert.ToString(a_name.GetTunedRacerSpeed);
            a_name.calculateStepSize(pnlFinishElho.Left);
            m_lblnaamStepSize.Text = Convert.ToString(a_name.GetStepSizePerTick);
        }

        #region startPersonen

        private void startKelder()
        {
            setters(Wolf, pbKelderElho,lblKelderSpeedElho,lblKelderStepSizeElho);
        }

        private void startSoetens()
        {
            setters(Lion, pbSoetensElho,lblSoetensSpeedElho,lblSoetensStepSizeElho);
        }

        private void startCisco()
        {
            setters(Monkey, pbCiscoElho,lblCiscoSpeedElho,lblCiscoStepSizeElho);
        }

        private void startJerome()
        {
            setters(Cheetah, pbJeromeElho,lblJeromeSpeedElho,lblJeromeStepSizeElho);
        }
        #endregion 

        private void tmRacerElho_Tick(object sender, EventArgs e)
        {
            timerWatch++;
            //raceTime++;
            //lblTimeElho.Text = raceTime.ToString();

            if (timerWatch >= 10)
            {
                lblTimeElho.Text = "00" + Convert.ToString(timerWatch / 100);

            }
            else
            {
                lblTimeElho.Text = "00:0" + Convert.ToString(timerWatch / 100);
            }

            
            timerJerome();

            timerSoetens();

            timerKelder();

            timerCisco();

            flaggedFinish();


            if (tmRacerElho.Enabled == true)
            {
                btnStartRaceElho.Enabled = false;
            }else if(tmRacerElho.Enabled == false)
            {
                btnStartRaceElho.Enabled = true;
            }


            #region Chosen Player Jerome finished
            if (Cheetah.flagged == true && messege == false)
            {
                if (jeromeClicked == true)
                {
                    messege = true;

                    if (Cheetah.endPos == 1)
                    {
                        MessageBox.Show("YOU WON\n\n1ST PLACE");
                    }
                    else
                    {
                        MessageBox.Show("You didn't won \n " + Cheetah.endPos + " Place");
                    }
                }
            }
            #endregion
            #region Chosen Player Soetens finished
            if (Lion.flagged == true && messege == false)
            {
                if (SoetensClicked == true)
                {
                    messege = true;

                    if (Lion.endPos == 1)
                    {
                        MessageBox.Show("YOU WON\n\n 1ST PLACE");
                    }
                    else
                    {
                        MessageBox.Show("You didn't won \n " + Lion.endPos + " Place");
                    }
                }
            }
            #endregion
            #region Chosen Player Kelder finished
            if (Wolf.flagged == true && messege == false)
            {
                if (KelderClicked == true)
                {
                    messege = true;

                    if (Wolf.endPos == 1)
                    {
                        MessageBox.Show("YOU WON\n\n 1ST PLACE");
                    }
                    else
                    {
                        MessageBox.Show("You didn't won \n " + Wolf.endPos + " Place");
                    }
                }
            }
            #endregion
            #region Chosen Player Cisco finished
            if (Monkey.flagged == true && messege == false)
            {
                if (CiscoClicked == true)
                {
                    messege = true;

                    if (Monkey.endPos == 1)
                    {
                        MessageBox.Show("YOU WON\n\n 1ST PLACE");
                    }
                    else
                    {
                        MessageBox.Show("You didn't won \n " + Monkey.endPos + " Place");
                    }
                }
            }
            #endregion


        }
        
        /// <summary>
        /// zorgt ervoor dat alle players gaan rijden
        /// </summary>
        private void StartRace()
        {
            tmRacerElho.Enabled = true;
            timerWatch = 0;
            finishedAtPosition = 0;

            startJerome();

            startSoetens();

            startKelder();

            startCisco();

            pnlJeromeWinnerElho.BackColor = Color.Transparent;
            pnlSoetensWinnerElho.BackColor = Color.Transparent;
            pnlKelderWinnerElho.BackColor = Color.Transparent;
            pnlCiscoWinnelElho.BackColor = Color.Transparent;
            
        }

        /// <summary>
        /// hier controleer je of de racers zijn geflagged dus gefinished en zet de timer uit en zet in de debug wie heeft gewonnen
        /// </summary>
        private void flaggedFinish()
        {

            if (Cheetah.flagged &&
                Lion.flagged &&
                Wolf.flagged &&
                Monkey.flagged)
            {
                tmRacerElho.Enabled = false;
                #region Won/yes or no

                if (Cheetah.endPos == 1)
                {
                    lblWinnerElho.Text = "Cheetah!";
                    pnlJeromeWinnerElho.BackColor = Color.Goldenrod;
                }

                if (Lion.endPos == 1)
                {
                    lblWinnerElho.Text = "Lion!";
                    pnlSoetensWinnerElho.BackColor = Color.Goldenrod;
                }

                if (Wolf.endPos == 1)
                {
                    lblWinnerElho.Text = "Wolf!";
                    pnlKelderWinnerElho.BackColor = Color.Goldenrod;
                }

                if (Monkey.endPos == 1)
                {
                    lblWinnerElho.Text = "Monkey!";
                    pnlCiscoWinnelElho.BackColor = Color.Goldenrod;
                }
#endregion

                #region Messegebox: do you want to go to the results
                DialogResult resultElho = MessageBox.Show("Do you want to go to the results?", "Results?", MessageBoxButtons.YesNo);
                if (resultElho == DialogResult.Yes)
                {
                    //yes...
                    tbcApplicationElho.SelectedIndex = 4;
                }
                else if (resultElho == DialogResult.No)
                {
                    //no...

                }
                #endregion

                rtbStaticticsElho.AppendText("Iedereen is gefinisht!\n");
                rtbStaticticsElho.AppendText("EINDPOSITIES:\t" +"Cheetah: "+ Cheetah.endPos + "\t" +"Lion: "+ Lion.endPos + "\t" + "Wolf: "+ Wolf.endPos + "\t" +"Monkey: "+ Monkey.endPos + "\n");
            }
        }

        /// <summary>
        /// Puts the speed and stepsize in the labels
        /// </summary>
        /// <param name="m_name"></param>
        private void infoSpeedAndStepElho(DragRacerBase m_name)
        {
            lblSpeedElho.Text = Convert.ToString(m_name.GetTunedRacerSpeed);
            lblStepSize.Text = Convert.ToString(m_name.GetStepSizePerTick);


            
            if (timerWatch >= 10)
            {
                lblTimeElho.Text = "00" + Convert.ToString(timerWatch / 100);

            }
            else
            {
                lblTimeElho.Text = "00:0" + Convert.ToString(timerWatch / 100);
            }
        }

        private void tbpAvgResultsElho_Enter(object sender, EventArgs e)
        {
            rtbStaticticsElho.AppendText("Enterd tabpage AvgResults\n");

            int m_GemiddeldePositieJerome;
            int m_GemiddeldePositieSoetens;
            int m_GemiddeldePositieKelder;
            int m_GemiddeldePositieCisco;

            int m_GemiddeldeTijdJerome;
            int m_GemiddeldeTijdSoetens;
            int m_GemiddeldeTijdKelder;
            int m_GemiddeldeTijdCisco;

            try
            {
                m_GemiddeldePositieJerome = Cheetah.opgeslagenPos / teller;
                tbJeromeAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieJerome);

                m_GemiddeldePositieSoetens = Lion.opgeslagenPos / teller;
                tbSoetensAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieSoetens);

                m_GemiddeldePositieKelder = Wolf.opgeslagenPos / teller;
                tbKelderAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieKelder);

                m_GemiddeldePositieCisco = Monkey.opgeslagenPos / teller;
                tbCiscoAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieCisco);


                m_GemiddeldeTijdJerome = Cheetah.opgeslagenTijd / teller;
                textBox6.Text = Convert.ToString(m_GemiddeldeTijdJerome);

                m_GemiddeldeTijdSoetens = Lion.opgeslagenTijd / teller;
                textBox9.Text = Convert.ToString(m_GemiddeldeTijdSoetens);

                m_GemiddeldeTijdKelder = Wolf.opgeslagenTijd / teller;
                textBox11.Text = Convert.ToString(m_GemiddeldeTijdKelder);

                m_GemiddeldeTijdCisco = Monkey.opgeslagenTijd / teller;
                textBox13.Text = Convert.ToString(m_GemiddeldeTijdCisco);


                textBox1.Text = "Cheetah";
                textBox2.Text = "Lion";
                textBox3.Text = "Wolf";
                textBox4.Text = "Monkey";
            }
            catch
            {
                MessageBox.Show("Average Statictics not calculated yet\nYou need to race more to see the average results");
            }
            
            
        }
        #region pbPlayerCursorToHand
        private void pbPlayerJeromeElho_MouseEnter(object sender, EventArgs e)
        {
            pbPlayerJeromeElho.Cursor = Cursors.Hand;
        }

        private void pbPlayerSoetensElho_MouseEnter(object sender, EventArgs e)
        {
            pbPlayerSoetensElho.Cursor = Cursors.Hand;
        }

        private void pbPlayerKelderElho_MouseEnter(object sender, EventArgs e)
        {
            pbPlayerKelderElho.Cursor = Cursors.Hand;
        }

        private void pbPlayerCiscoElho_MouseEnter(object sender, EventArgs e)
        {
            pbPlayerCiscoElho.Cursor = Cursors.Hand;
        }
        #endregion

        #region startByClickingOnPlayer
        private void pbPlayerJeromeElho_Click(object sender, EventArgs e)
        {
            teller++;

            #region setting line colors right
            pnlRaceJeromeTopElho.BackColor = Color.Goldenrod;
            pnlRaceJeromeBotElho.BackColor = Color.Goldenrod;

            pnlRaceSoetensTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceSoetensBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoBotElho.BackColor = Color.FromArgb(63, 184, 232);
            #endregion

            tbcApplicationElho.SelectedIndex = 0;

            StartRace();

            infoSpeedAndStepElho(Cheetah);

            jeromeClicked = true;
            SoetensClicked = false;
            KelderClicked = false;
            CiscoClicked = false;

            lblChosenPlayerElho.Text = "You Chose Player: Cheetah (lane 1)";

            rtbStaticticsElho.AppendText("Choosen Player Cheetah\n");
            rtbStaticticsElho.AppendText("Race Started\n\n");
        }

        private void pbPlayerSoetensElho_Click(object sender, EventArgs e)
        {
            teller++;

            #region setting line colors right
            pnlRaceSoetensTopElho.BackColor = Color.Goldenrod;
            pnlRaceSoetensBotElho.BackColor = Color.Goldenrod;

            pnlRaceJeromeTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoBotElho.BackColor = Color.FromArgb(63, 184, 232);
            #endregion

            tbcApplicationElho.SelectedIndex = 0;

            StartRace();

            infoSpeedAndStepElho(Lion);

            jeromeClicked = false;
            SoetensClicked = true;
            KelderClicked = false;
            CiscoClicked = false;

            lblChosenPlayerElho.Text = "You Chose Player: Lion (lane 2)";

            rtbStaticticsElho.AppendText("Choosen Player Lion\n");
            rtbStaticticsElho.AppendText("Race Started\n\n");
        }

        private void pbPlayerKelderElho_Click(object sender, EventArgs e)
        {
            teller++;

            #region setting line colors right
            pnlRaceKelderTopElho.BackColor = Color.Goldenrod;
            pnlRaceKelderBotElho.BackColor = Color.Goldenrod;

            pnlRaceSoetensTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceSoetensBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceCiscoBotElho.BackColor = Color.FromArgb(63, 184, 232);
            #endregion

            tbcApplicationElho.SelectedIndex = 0;

            StartRace();

            infoSpeedAndStepElho(Wolf);

            jeromeClicked = false;
            SoetensClicked = false;
            KelderClicked = true;
            CiscoClicked = false;

            lblChosenPlayerElho.Text = "You Chose Player: Wolf (lane 3)";

            rtbStaticticsElho.AppendText("Choosen Player Wolf\n");
            rtbStaticticsElho.AppendText("Race Started\n\n");
        }

        private void pbPlayerCiscoElho_Click(object sender, EventArgs e)
        {
            teller++;

            #region setting line colors right
            pnlRaceCiscoTopElho.BackColor = Color.Goldenrod;
            pnlRaceCiscoBotElho.BackColor = Color.Goldenrod;

            pnlRaceKelderTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceKelderBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceSoetensTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceSoetensBotElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeTopElho.BackColor = Color.FromArgb(63, 184, 232);
            pnlRaceJeromeBotElho.BackColor = Color.FromArgb(63, 184, 232);
            #endregion

            tbcApplicationElho.SelectedIndex = 0;

            StartRace();

            infoSpeedAndStepElho(Monkey);

            jeromeClicked = false;
            SoetensClicked = false;
            KelderClicked = false;
            CiscoClicked = true;

            lblChosenPlayerElho.Text = "You Chose Player: Monkey (lane 4)";

            rtbStaticticsElho.AppendText("Choosen Player Monkey\n");
            rtbStaticticsElho.AppendText("Race Started\n\n");
        }
        #endregion

        #region menuItems 

        private void miJustRaceElho(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 0;
            rtbStaticticsElho.AppendText("Enterd the race area\n");
        }

        private void miChoosePlayerElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 5;
            rtbStaticticsElho.AppendText("Enterd the Choose Player area\n");
        }

        private void miBetsElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 3;
            rtbStaticticsElho.AppendText("Enterd the betting area\n");
        }

        private void miAvgResultsElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 1;
            rtbStaticticsElho.AppendText("Clicked on the AverageStatictics menuitem\n");

            #region bereken gemiddelde score
            rtbStaticticsElho.AppendText("Enterd tabpage AvgResults\n");
            #region test forloop
            //int[] m_avgPositionElho;

            //m_avgPositionElho = new int[4];

            //for (int i = 0; i < m_avgPositionElho.Length; i++)
            //{
            //    m_avgPositionElho[i] = Cheetah.opgeslagenPos / teller; //met dit krijg ik nog steeds het alleen op jerome
            //}
            #endregion

            int m_GemiddeldePositieJerome;
            int m_GemiddeldePositieSoetens;
            int m_GemiddeldePositieKelder;
            int m_GemiddeldePositieCisco;

            int m_GemiddeldeTijdJerome;
            int m_GemiddeldeTijdSoetens;
            int m_GemiddeldeTijdKelder;
            int m_GemiddeldeTijdCisco;

            try
            {
                m_GemiddeldePositieJerome = Cheetah.opgeslagenPos / teller;
                tbJeromeAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieJerome);

                m_GemiddeldePositieSoetens = Lion.opgeslagenPos / teller;
                tbSoetensAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieSoetens);

                m_GemiddeldePositieKelder = Wolf.opgeslagenPos / teller;
                tbKelderAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieKelder);

                m_GemiddeldePositieCisco = Monkey.opgeslagenPos / teller;
                tbCiscoAvgPositionElho.Text = Convert.ToString(m_GemiddeldePositieCisco);


                m_GemiddeldeTijdJerome = Cheetah.opgeslagenTijd / teller;
                textBox6.Text = Convert.ToString(m_GemiddeldeTijdJerome);

                m_GemiddeldeTijdSoetens = Lion.opgeslagenTijd / teller;
                textBox9.Text = Convert.ToString(m_GemiddeldeTijdSoetens);

                m_GemiddeldeTijdKelder = Wolf.opgeslagenTijd / teller;
                textBox11.Text = Convert.ToString(m_GemiddeldeTijdKelder);

                m_GemiddeldeTijdCisco = Monkey.opgeslagenTijd / teller;
                textBox13.Text = Convert.ToString(m_GemiddeldeTijdCisco);
            }
            catch
            {
                MessageBox.Show("Average Statictics not calculated yet\nYou need to race more to see the average results");
            }
            #endregion
        }

        private void miDebugElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 2;
            rtbStaticticsElho.AppendText("Enterd the Debug area\n");
        }

        private void miAboutElho_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deze programma is gemaakt door Oussama EL-Hajoui");
        }

        private void locateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uw applicatie staat op het volgende pad:\n\n" + Application.StartupPath);
            rtbStaticticsElho.AppendText("Checked the location of the application \n");
        }
        #endregion

        /// <summary>
        /// zorgt ervoor dat de tabs onzichtbaar zijn
        /// </summary>
        public void initViewsElho()
        {
            tbcApplicationElho.Appearance = TabAppearance.FlatButtons;
            tbcApplicationElho.ItemSize = new Size(0, 1);
            tbcApplicationElho.SizeMode = TabSizeMode.Fixed;
        }

        private void miStatsElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 4;
        }

        private void btnLoginElho_Click(object sender, EventArgs e)
        {
            rtbStaticticsElho.AppendText("Tryed to log in with the username: " + tbUsernameElho.Text +  " and the password: " + tbPasswordElho.Text +" With " + Convert.ToString(attemptElho) + " Attempts left\n");
            if(tbUsernameElho.Text == username && tbPasswordElho.Text == password)
            {
                MessageBox.Show("You have succesfully logged in");
                Access();
                tbcApplicationElho.SelectedIndex = 5;
                rtbStaticticsElho.AppendText("Succesfully logged in \n\n");
            }
            else
            {
                if (attemptElho <= 0)
                {
                    this.Close();
                    return;
                }
                attemptElho--;
                pgbAttemptsElho.Value = attemptElho;
                lblAttemptsElho.Text = Convert.ToString(attemptElho);
            }

            
        }

        private void tbPasswordElho_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = btnLoginElho;
        }

        public void lblForgotPasswordElho_Click(object sender, EventArgs e)
        {
            pictureBox5.SendToBack();
            pictureBox5.Size = new Size(0, 0);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (tbVerifyPasswordElho.Text == tbNewPasswordElho.Text)
            {
                password = tbNewPasswordElho.Text;
                pictureBox5.BringToFront();
                pictureBox5.Size = new Size(198, 181);
                rtbStaticticsElho.AppendText("Changed the password to " + password + "\n");
            }
            else
            {
                MessageBox.Show("De Wachtwoorden komen niet overeen");
                rtbStaticticsElho.AppendText("Tryed to change the password but the passwods did not match\n");
            }
            
        }

        private void NoAccess()
        {
            miLoginElho.Visible = true;

            miRaceElho.Visible = false;
            miBetsElho.Visible = false;
            miAvgResultsElho.Visible = false;
            miStatsElho.Visible = false;
        }

        private void Access()
        {
            miLoginElho.Visible = false;

            miRaceElho.Visible = true;
            miBetsElho.Visible = true;
            miAvgResultsElho.Visible = true;
            miStatsElho.Visible = true;
        }

        private void miLoginElho_Click(object sender, EventArgs e)
        {
            tbcApplicationElho.SelectedIndex = 6;
            rtbStaticticsElho.AppendText("Enterd the login page");
        }

        private void cbShowPasswordElho_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPasswordElho.Checked)
            {
                 tbPasswordElho.PasswordChar = '\0'; // dit zorgt ervoor dat de karakters weer te zien zijn
            }
            else
            {
                tbPasswordElho.PasswordChar = '*'; // dit maakt de karaters in de textbox *
            }
        }
        private void btnClearElho_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveElho_Click(object sender, EventArgs e)
        {

        }

    }

}

