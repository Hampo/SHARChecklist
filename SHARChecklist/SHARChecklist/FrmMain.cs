using System;
using System.Linq;
using System.Windows.Forms;

namespace SHARChecklist
{
	public partial class FrmMain : Form
	{
		private static readonly string Version;
		
		static FrmMain()
		{
			string version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
			while (version.EndsWith(".0"))
				version = version.Substring(0, version.Length - 2);
			Version = version;
        }
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

		private void ResetStats()
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

		private void TSMIExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void TSMITopmost_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = TSMITopmost.Checked;
		}

		private void TSMIFormBorder_CheckedChanged(object sender, EventArgs e)
		{
			FormBorderStyle = TSMIFormBorder.Checked ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			Text += $" v{Version}";
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			SHARMem?.Dispose();
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
			TSMIFormBorder.Checked = S.BorderStyle == FormBorderStyle.FixedToolWindow;
			TSMITopmost.Checked = S.Topmost;
			TmrUpdate.Start();
		}

		private SHARMemory.SHAR.Memory SHARMem = null;
		private byte LevelCount = 0;
		private float[] waspTotals = null;
		private float[] gagTotals = null;
		private float[] clothingTotals = null;
		private float[] vehicleTotals = null;
		private bool[] hasBonusReward = null;
		private bool[] hasRaceReward = null;

		private void TmrUpdate_Tick(object sender, EventArgs e)
		{
			if (SHARMem == null)
            {
                SHARMem = null;
                ResetStats();

				var p = SHARMemory.SHAR.Memory.GetSHARProcess();
                if (p == null)
					return;

				SHARMem = new SHARMemory.SHAR.Memory(p);
            }

			if (!SHARMem.IsRunning)
            {
                SHARMem.Dispose();
                SHARMem = null;
                ResetStats();

                LevelCount = 0;
                waspTotals = null;
                gagTotals = null;
                clothingTotals = null;
                vehicleTotals = null;
                hasBonusReward = null;
                hasRaceReward = null;
				return;
            }

			var context = SHARMem.Singletons.GameFlow?.CurrentContext;
            if (context == null || context == SHARMemory.SHAR.Classes.GameFlow.GameState.PreLicence || context == SHARMemory.SHAR.Classes.GameFlow.GameState.Licence)
			{
				ResetStats();
				return;
			}

			try
			{
				var rewardsManager = SHARMem.Singletons.RewardsManager;
				if (rewardsManager == null)
					return;

				var characterSheet = SHARMem.Singletons.CharacterSheetManager?.CharacterSheet;
				if (characterSheet == null)
					return;

				if (LevelCount == 0)
				{

					LevelCount = SHARMem.Globals.LevelCount;

					waspTotals = new float[LevelCount];
					gagTotals = new float[LevelCount];
					clothingTotals = new float[LevelCount];
					vehicleTotals = new float[LevelCount];
					hasBonusReward = new bool[LevelCount];
					hasRaceReward = new bool[LevelCount];
					
					for (int level = 0; level < LevelCount; level++)
					{
						var levelRewards = rewardsManager.RewardsList[level];

						waspTotals[level] = levelRewards.TotalWaspsInLevel;
						gagTotals[level] = levelRewards.TotalGagsInLevel;
						float clothingTotal = 0;
						float vehicleTotal = 0;

						/*var levelMerchandises = rewardsManager.LevelTokenStoreList[level].Merchandises.ToArray();
						foreach (var merchandise in levelMerchandises)
						{
							switch (merchandise.RewardType)
							{
								case SHARMemory.SHAR.Classes.Reward.RewardTypes.SkinOther:
									clothingTotal++;
									break;
								case SHARMemory.SHAR.Classes.Reward.RewardTypes.PlayerCar:
									vehicleTotal++;
									break;
							}
						}*/
						var merchandiseCount = rewardsManager.LevelTokenStoreList[level].Counter;
                        for (uint merchandiseIndex = 0; merchandiseIndex < merchandiseCount; merchandiseIndex++)
						{
							var merchandise = SHARMem.Functions.GetMerchandise((uint)level, merchandiseIndex);
							switch (merchandise.RewardType)
							{
								case SHARMemory.SHAR.Classes.Reward.RewardTypes.SkinOther:
									clothingTotal++;
									break;
								case SHARMemory.SHAR.Classes.Reward.RewardTypes.PlayerCar:
									vehicleTotal++;
									break;
							}
						}
						clothingTotals[level] = clothingTotal;
						vehicleTotals[level] = vehicleTotal;

						hasBonusReward[level] = levelRewards.BonusMission != null;
						hasRaceReward[level] = levelRewards.StreetRace != null;
						if (hasBonusReward[level])
							vehicleTotals[level]++;
						if (hasRaceReward[level])
							vehicleTotals[level]++;
					}
				}

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

				LblStoryMissionsTotal.Text = $"/{LevelCount * 7}";
				LblBonusMissionsTotal.Text = $"/{LevelCount}";
				LblStreetRacesTotal.Text = $"/{LevelCount * 3}";
				LblCollectorCardsTotal.Text = $"/{LevelCount * 7}";
				LblCharacterClothingTotal.Text = $"/{(int)clothingTotals.Sum()}";
				LblVehiclesTotal.Text = $"/{(int)vehicleTotals.Sum()}";
				LblWaspCamerasTotal.Text = $"/{(int)waspTotals.Sum()}";
				LblGagsTotal.Text = $"/{(int)gagTotals.Sum()}";
				LblMoviesTotal.Text = "/1";

				for (int level = 0; level < LevelCount; level++)
				{
					var levelRecord = characterSheet.LevelList[level];

					uint levelMissions = 0;
					for (int mission = 0; mission < 7; mission++)
					{
						if (levelRecord.Missions.List[mission].Completed)
							levelMissions++;
					}
					storyMissionTotal += levelMissions;

					uint levelBM = levelRecord.BonusMission.Completed ? 1u : 0u;
					bonusMissionTotal += levelBM;

					uint levelRaces = 0;
					for (int race = 0; race < 3; race++)
					{
						if (levelRecord.StreetRaces.List[race].Completed)
							levelRaces++;
					}
					streetRaceTotal += levelRaces;

					uint levelCards = 0;
					for (int card = 0; card < 7; card++)
					{
						if (levelRecord.Cards.List[card].Completed)
							levelCards++;
					}
					collectorCardTotal += levelCards;

					uint levelClothing = (uint)levelRecord.NumSkinsPurchased;
					characterClothingTotal += levelClothing;

					uint levelVehicles = (uint)levelRecord.NumCarsPurchased;
					if (hasBonusReward[level])
						levelVehicles += levelBM;
					if (hasRaceReward[level] && levelRaces == 3)
						levelVehicles += 1;
					vehiclesTotal += levelVehicles;

					uint levelWasps = (uint)levelRecord.WaspsDestroyed;
					waspCamerasTotal += levelWasps;

					uint levelGags = (uint)levelRecord.GagsViewed;
					gagsTotal += levelGags;

					float levelComplete = 0;
					float divider = 0;

					levelComplete += levelMissions / 7f;
					divider++;

					levelComplete += levelBM;
					divider++;

					levelComplete += levelRaces / 3f;
					divider++;

					if (clothingTotals[level] != 0)
					{
						levelComplete += levelClothing / clothingTotals[level];
						divider++;
					}

					if (vehicleTotals[level] != 0)
					{
						levelComplete += levelVehicles / vehicleTotals[level];
						divider++;
					}

					levelComplete += levelCards / 7f;
					divider++;

					if (waspTotals[level] != 0)
					{
						levelComplete += levelWasps / waspTotals[level];
						divider++;
					}

					if (gagTotals[level] != 0)
					{
						levelComplete += levelGags / gagTotals[level];
						divider++;
					}

					levelComplete /= divider;
					levelsTotal += levelComplete * 100f;
				}
				moviesTotal = characterSheet.LevelList[2].FMVUnlocked ? 1u : 0u;

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
				ResetStats();
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
