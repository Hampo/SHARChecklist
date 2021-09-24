using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARChecklist
{
    public partial class FrmMain : Form
    {
        private static readonly string Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString().Replace(".0", "");
        public static Settings S = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void resetStats()
        {
            LblPercentageComplete.Text = "0%";
            LblStoryMissions.Text = "0";
            LblBonusMissions.Text = "0";
            LblStreetRaces.Text = "0";
            LblCollectorCards.Text = "0";
            LblCharacterClothing.Text = "0";
            LblVehicles.Text = "0";
            LblWaspCameras.Text = "0";
            LblGags.Text = "0";
            LblMovies.Text = "0";

            LblStoryMissionsTotal.Text = "/?";
            LblBonusMissionsTotal.Text = "/?";
            LblStreetRacesTotal.Text = "/?";
            LblCollectorCardsTotal.Text = "/?";
            LblCharacterClothingTotal.Text = "/?";
            LblVehiclesTotal.Text = "/?";
            LblWaspCamerasTotal.Text = "/?";
            LblGagsTotal.Text = "/?";
            LblMoviesTotal.Text = "/?";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void topmostToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topmostToolStripMenuItem.Checked;
        }

        private void formBorderToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            FormBorderStyle = formBorderToolStripMenuItem.Checked ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Text += $" v{Version}";
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (S != null)
            {
                S.Location = Location;
                S.BorderStyle = FormBorderStyle;
                S.Topmost = TopMost;
                S.Save();
            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            S = Settings.Load();
            Location = S.Location;
            formBorderToolStripMenuItem.Checked = S.BorderStyle == FormBorderStyle.FixedToolWindow;
            topmostToolStripMenuItem.Checked = S.Topmost;
            UpdateStats();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private IntPtr GetGameWindow()
        {
            return FindWindow("The Simpsons Hit & Run", null);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpwdProcessId);

        private async void UpdateStats()
        {
            Process p = null;
            SHARMemory SHARMem = null;
            byte LevelCount = 7;
            bool IncreasedRewardLimits = false;
            while (true)
            {
                if (p == null)
                {
                    IntPtr GameWindow = GetGameWindow();
                    if (GameWindow != IntPtr.Zero)
                    {
                        int ProcessId = 0;
                        GetWindowThreadProcessId(GameWindow, ref ProcessId);
                        Process SHAR = Process.GetProcessById(ProcessId);
                        if (SHAR != null)
                        {
                            p = SHAR;
                            SHARMem = new SHARMemory(p);
                            LevelCount = SHARMem.LevelCount;
                            IncreasedRewardLimits = SHARMem.IsHackLoaded("IncreasedRewardLimits");
                        }
                        else
                        {
                            resetStats();
                        }
                    }
                    else
                    {
                        resetStats();
                    }
                }
                else
                {
                    if (p.HasExited)
                    {
                        SHARMem.Dispose();
                        SHARMem = null;
                        p.Dispose();
                        p = null;
                        resetStats();
                    }
                    else if ((int)SHARMem._GameState < 2)
                    {
                        resetStats();
                    }
                    else
                    {
                        try
                        {
                            uint storyMissionTotal = 0;
                            uint bonusMissionTotal = 0;
                            uint streetRaceTotal = 0;
                            uint collectorCardTotal = 0;
                            uint characterClothingTotal = 0;
                            uint vehiclesTotal = 0;
                            uint waspCamerasTotal = 0;
                            uint gagsTotal = 0;
                            float levelsTotal = 0f;
                            uint moviesTotal;

                            float[] waspTotals = new float[LevelCount];
                            float[] gagTotals = new float[LevelCount];
                            float[] clothingTotals = new float[LevelCount];
                            float[] vehicleTotals = new float[LevelCount];
                            bool[] hasBonusReward = new bool[LevelCount];
                            bool[] hasRaceReward = new bool[LevelCount];
                            for (uint i = 0; i < LevelCount; i++)
                            {
                                waspTotals[i] = SHARMem.WaspTotal(i);
                                gagTotals[i] = SHARMem.GagTotal(i);
                                if (IncreasedRewardLimits)
                                {
                                    //TODO: Support this. Inject code?
                                    clothingTotals[i] = 0;
                                    vehicleTotals[i] = 0;
                                }
                                else
                                {
                                    uint MerchandiseCount = SHARMem.MerchandiseCount(i);
                                    float clothingTotal = 0;
                                    float vehicleTotal = 0;

                                    ulong bonusMissionHash = SHARMem.BonusMissionRewardHash(i);
                                    ulong streetRaceHash = SHARMem.StreetRaceRewardHash(i);
                                    hasBonusReward[i] = bonusMissionHash != 0;
                                    hasRaceReward[i] = streetRaceHash != 0;
                                    if (hasBonusReward[i])
                                        vehicleTotal++;
                                    if (hasRaceReward[i])
                                        vehicleTotal++;
                                    for (uint j = 0; j < MerchandiseCount; j++)
                                    {
                                        switch (SHARMem.MerchandiseType(SHARMem.MerchandiseIndex(i, j)))
                                        {
                                            case SHARMemory.RewardType.SkinOther:
                                                clothingTotal++;
                                                break;
                                            case SHARMemory.RewardType.Car:
                                                vehicleTotal++;
                                                break;
                                        }
                                    }
                                    clothingTotals[i] = clothingTotal;
                                    vehicleTotals[i] = vehicleTotal;
                                }
                            }

                            LblStoryMissionsTotal.Text = $"/{LevelCount * 7}";
                            LblBonusMissionsTotal.Text = $"/{LevelCount}";
                            LblStreetRacesTotal.Text = $"/{LevelCount * 3}";
                            LblCollectorCardsTotal.Text = $"/{LevelCount * 7}";
                            LblCharacterClothingTotal.Text = $"/{(int)clothingTotals.Sum()}";
                            LblVehiclesTotal.Text = $"/{(int)vehicleTotals.Sum()}";
                            LblWaspCamerasTotal.Text = $"/{(int)waspTotals.Sum()}";
                            LblGagsTotal.Text = $"/{(int)gagTotals.Sum()}";
                            LblMoviesTotal.Text = "/1";

                            for (uint i = 0; i < LevelCount; i++)
                            {
                                uint levelMissions = 0;
                                for (uint j = 0; j < 7; j++)
                                {
                                    if (SHARMem.MissionCompleted(i, j))
                                        levelMissions++;
                                }
                                storyMissionTotal += levelMissions;

                                uint levelBM = SHARMem.BonusMissionCompleted(i) ? 1u : 0u;
                                bonusMissionTotal += levelBM;

                                uint levelRaces = 0;
                                for (uint j = 0; j < 3; j++)
                                {
                                    if (SHARMem.StreetRaceCompleted(i, j))
                                        levelRaces++;
                                }
                                streetRaceTotal += levelRaces;

                                uint levelCards = 0;
                                for (uint j = 0; j < 7; j++)
                                {
                                    if (SHARMem.CardCollected(i, j))
                                        levelCards++;
                                }
                                collectorCardTotal += levelCards;

                                uint levelClothing = SHARMem.CharacterClothingCount(i);
                                characterClothingTotal += levelClothing;

                                uint levelVehicles = SHARMem.VehiclesCount(i);
                                if (hasBonusReward[i])
                                    levelVehicles += levelBM;
                                if (hasRaceReward[i] && levelRaces == 3)
                                    levelVehicles += 1;
                                vehiclesTotal += levelVehicles;

                                uint levelWasps = SHARMem.WaspCamerasCount(i);
                                waspCamerasTotal += levelWasps;

                                uint levelGags = SHARMem.GagsCount(i);
                                gagsTotal += levelGags;

                                float levelComplete = 0;
                                float divider = 0;

                                levelComplete += levelMissions / 7f;
                                divider++;

                                levelComplete += levelBM;
                                divider++;

                                levelComplete += levelRaces / 3f;
                                divider++;

                                if (clothingTotals[i] != 0)
                                {
                                    levelComplete += levelClothing / clothingTotals[i];
                                    divider++;
                                }

                                if (vehicleTotals[i] != 0)
                                {
                                    levelComplete += levelVehicles / vehicleTotals[i];
                                    divider++;
                                }

                                levelComplete += levelCards / 7f;
                                divider++;

                                if (waspTotals[i] != 0)
                                {
                                    levelComplete += levelWasps / waspTotals[i];
                                    divider++;
                                }

                                if (gagTotals[i] != 0)
                                {
                                    levelComplete += levelGags / gagTotals[i];
                                    divider++;
                                }

                                levelComplete /= divider;
                                levelsTotal += levelComplete * 100f;
                            }
                            moviesTotal = SHARMem.FMVUnlocked(2u) ? 1u : 0u;

                            LblStoryMissions.Text = storyMissionTotal.ToString();
                            LblBonusMissions.Text = bonusMissionTotal.ToString();
                            LblStreetRaces.Text = streetRaceTotal.ToString();
                            LblCollectorCards.Text = collectorCardTotal.ToString();
                            LblCharacterClothing.Text = characterClothingTotal.ToString();
                            LblVehicles.Text = vehiclesTotal.ToString();
                            LblWaspCameras.Text = waspCamerasTotal.ToString();
                            LblGags.Text = gagsTotal.ToString();
                            LblMovies.Text = moviesTotal.ToString();

                            levelsTotal /= LevelCount * 1f;

                            float complete = levelsTotal * 0.99f;
                            if (moviesTotal > 0)
                                complete += 1;
                            LblPercentageComplete.Text = $"{complete:f4}%";
                        }
                        catch (Exception ex)
                        {
                            resetStats();
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }

                await Task.Delay(1000);
            }
        }
    }
}
