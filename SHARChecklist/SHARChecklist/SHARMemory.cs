using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARChecklist
{
	public class SHARMemory : ProcessMemory
	{
		public enum GameVersions
		{
			ReleaseEnglish,
			ReleaseInternational,
			BestSellerSeries,
			Demo,
			Unknown
		}

		public enum GameSubVersions
		{
			French,
			German,
			Spanish,
			English,
			Unknown
		}

		public enum GameState
		{
			PreLicence,
			Licence,
			MainMenu,
			DemoLoading,
			DemoInGame,
			BonusSetup,
			BonusLoading,
			BonusInGame,
			NormalLoading,
			NormalInGame,
			NormalPaused,
			Exit
		}

		public GameVersions GameVersion;
		public GameSubVersions GameSubVersion;

		public uint GameFlow => ReadUInt32(SelectAddress(7114772u, 7114708u, 7114708u, 7114764u));
		public GameState _GameState => (GameState)checked(GameFlow == 0 ? 0 : ReadUInt32(GameFlow + 12u));

		public uint RewardManager => ReadUInt32(SelectAddress(7113096u, 7113032u, 7113032u, 7113088u));

		public uint MaxTokens(uint Level)
		{
			checked
			{
				return ReadUInt32(RewardManager + 4 + 36 * Level + 24);
			}
		}

		public uint GagTotal(uint Level)
		{
			checked
			{
				return ReadUInt32(RewardManager + 4 + 36 * Level + 28);
			}
		}

		public uint WaspTotal(uint Level)
		{
			checked
			{
				return ReadUInt32(RewardManager + 4 + 36 * Level + 32);
			}
		}

		public uint CharacterSheetManager => ReadUInt32(SelectAddress(7113092u, 7113028u, 7113028u, 7113084u));

		public uint CollectorCardsCount(uint Level)
		{
			checked
			{
				return ReadUInt32(CharacterSheetManager - 280 - 32 * Level);
			}
		}

		public uint VehiclesCount(uint Level)
        {
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 560);
			}
		}

		public uint CharacterClothingCount(uint Level)
        {
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 564);
			}
		}

		public uint WaspCamerasCount(uint Level)
        {
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 568);
			}
		}

		public uint GagsCount(uint Level)
        {
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 588);
			}
		}

		public uint MissionComplete(uint Level, uint Mission)
		{
			checked
			{
				uint offset = 620 * Level + 156 + 32 * Mission;
				if (Level == 0)
					offset += 32;
				return ReadUInt32(CharacterSheetManager + offset);
			}
		}

		public uint BonusMissionComplete(uint Level)
		{
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 508);
			}
		}

		public uint StreetRaceComplete(uint Level, uint Race)
		{
			checked
			{
				return ReadUInt32(CharacterSheetManager + 620 * Level + 412 + 32 * Race);
			}
		}

		public uint BonusMovieWatched()
		{
			checked
			{
				return ReadUInt32(CharacterSheetManager + 1796);
			}
		}

		public uint GetLevelsFunc() => GetModuleProcAddress("Hacks.dll", 28);

		public uint GetLevelsFuncOld() => GetModuleProcAddress("CustomStatsTotals.lmlh", "?GetLevels@@YAIXZ");

		public uint LevelCount()
        {
			checked
            {
				uint getLevelsFunc = GetLevelsFuncOld();
				if (getLevelsFunc == 0)
					getLevelsFunc = GetLevelsFunc();
				if (getLevelsFunc == 0)
						return 7u;
				if (ReadByte(getLevelsFunc) != 0xA1)
					return 7u;
				if (ReadByte(getLevelsFunc + 5) != 0xC3)
					return 7u;
				return ReadUInt32(ReadUInt32(getLevelsFunc + 1));
            }
        }

		private GameVersions DetectVersion(ref GameSubVersions GameSubVersion)
		{
			switch (ReadUInt32(5849087u))
			{
				case 4209509573u:
					return GameVersions.Demo;
				case 1267409524u:
					return GameVersions.ReleaseEnglish;
				case 3380997427u:
					switch (ReadUInt32(4804529u))
                    {
						case 4186225384u:
							GameSubVersion = GameSubVersions.French;
							break;
						case 4139854568u:
							GameSubVersion = GameSubVersions.German;
							break;
						case 4139842280u:
							GameSubVersion = GameSubVersions.Spanish;
							break;
						case 4139846376u:
							GameSubVersion = GameSubVersions.English;
							break;
						default:
							GameSubVersion = GameSubVersions.Unknown;
							break;
					};
					return GameVersions.ReleaseInternational;
				case 4232482053u:
					return GameVersions.BestSellerSeries;
				default:
					return GameVersions.Unknown;
			}
		}

		private uint SelectAddress(uint ReleaseEnglishAddress, uint DemoAddress, uint ReleaseInternationalAddress, uint BestSellerSeriesAddress)
		{
			if (GameVersion == GameVersions.ReleaseEnglish)
				return ReleaseEnglishAddress;
			if (GameVersion == GameVersions.Demo)
				return DemoAddress;
			if (GameVersion == GameVersions.ReleaseInternational)
				return ReleaseInternationalAddress;
			if (GameVersion == GameVersions.BestSellerSeries)
				return BestSellerSeriesAddress;
			throw new Exception("Unrecognised game version.");
		}

		public SHARMemory(Process Process) : base(Process)
		{
			GameVersion = DetectVersion(ref GameSubVersion);
		}
	}
}
