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
		private static string NullTerminate(string String)
		{
			int num = String.IndexOf('\0');
			if (num == -1)
				return String;

			return String.Substring(0, num);
		}

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

		public enum RewardType : uint
        {
			Null,
			SkinOther,
			SkinGood,
			Car,
			FEToy,
			NumRewards
        }

		public GameVersions GameVersion;
		public GameSubVersions GameSubVersion;

		public uint GameFlow => ReadUInt32(SelectAddress(0x6C9014, 0x6C8FD4, 0x6C8FD4, 0x6C900C));
		public GameState _GameState => (GameState)(GameFlow == 0 ? 0 : ReadUInt32(GameFlow + 12u));

		public uint RewardManager => ReadUInt32(SelectAddress(0x6C8988, 0x6C8948, 0x6C8948, 0x6C8980));

		public uint MaxTokens(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4);

		public uint GagTotal(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4 + 4);

		public uint WaspTotal(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4);

		public uint MerchandiseCount(uint Level) => ReadUInt32(RewardManager + 4 + 288 + 1600 + 4 + 4 + 36 * Level + 8 * 4);

		public uint MerchandiseIndex(uint Level, uint Index) => ReadUInt32(RewardManager + 4 * (Index + 8 * Level + Level) + 1900);

		public RewardType MerchandiseType(uint Index) => (RewardType)ReadUInt32(Index + 104);

		public uint CharacterSheetManager => ReadUInt32(SelectAddress(0x6C8984, 0x6C8944, 0x6C8944, 0x6C897C));

		public uint CharacterSheet => CharacterSheetManager + 4;

		public string PlayerName => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet, 16u)));

		public string CardName(uint Level, uint Card) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 17 * Card, 16u)));

		public bool CardCollected(uint Level, uint Card) => ReadByte(CharacterSheet + 16 + 620 * Level + 17 * Card + 16) != 0;

		public string MissionName(uint Level, uint Mission) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 120 + 32 * Mission, 16u)));

		public bool MissionCompleted(uint Level, uint Mission) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 32 * (Level == 0 ? Mission + 1 : Mission) + 16) != 0;

		public string StreetRaceName(uint Level, uint StreetRace) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 120 + 256 + 32 * StreetRace, 16u)));

		public bool StreetRaceCompleted(uint Level, uint StreetRace) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 32 * StreetRace + 16) != 0;

		public string BonusMissionName(uint Level) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96, 16u)));

		public bool BonusMissionCompleted(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 16) != 0;

		public string GambleRaceName(uint Level) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32, 16u)));

		public bool GambleRaceCompleted(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 16) != 0;

		public bool FMVUnlocked(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32) != 0;

		public uint VehiclesCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4);

		public uint CharacterClothingCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4);

		public uint WaspCamerasCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4);

		public string CurrentSkinName(uint Level) => NullTerminate(Encoding.ASCII.GetString(ReadBytes(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4, 16u)));

		public uint GagsCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4 + 16);

		public uint GetLevelsFunc => GetModuleProcAddress("Hacks.dll", 28);

		public uint GetLevelsFuncOld => GetModuleProcAddress("CustomStatsTotals.lmlh", "?GetLevels@@YAIXZ");

		public uint LevelCountOld()
		{
			uint getLevelsFunc = GetLevelsFuncOld;
			if (getLevelsFunc == 0)
				getLevelsFunc = GetLevelsFunc;
			if (getLevelsFunc == 0)
				return 7u;
			if (ReadByte(getLevelsFunc) != 0xA1)
				return 7u;
			if (ReadByte(getLevelsFunc + 5) != 0xC3)
				return 7u;
			return ReadUInt32(ReadUInt32(getLevelsFunc + 1));
		}

		public byte LevelCount => ReadByte(SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3);

		public List<string> GetLoadedHacks()
        {
			uint EventHacks = GetModuleProcAddress("Hacks.dll", 3151);
			if (EventHacks == 0)
				return null;

			uint HacksLoaded = ReadUInt32(EventHacks + 8);
			if (HacksLoaded == 0)
				return null;

			List<string> Hacks = new List<string>();

			uint node = ReadUInt32(EventHacks);
			while (node != 0)
            {
				uint hack = ReadUInt32(node + 12);
				Hacks.Add(NullTerminate(Encoding.Unicode.GetString(ReadBytes(hack + 562, 128u))));

				node = ReadUInt32(node + 8);
            }

			return Hacks;
		}

		public bool IsHackLoaded(string HackName)
		{
			ProcessModule hackModule = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals($"{HackName}.lmlh", StringComparison.OrdinalIgnoreCase));
			if (hackModule != null)
				return true;

			uint EventHacks = GetModuleProcAddress("Hacks.dll", 3151);
			if (EventHacks == 0)
				return false;

			uint node = ReadUInt32(EventHacks);
			while (node != 0)
			{
				uint hack = ReadUInt32(node + 12);
				if (NullTerminate(Encoding.Unicode.GetString(ReadBytes(hack + 562, 128u))).Equals(HackName, StringComparison.OrdinalIgnoreCase))
					return true;

				node = ReadUInt32(node + 8);
			}

			return false;
		}

		private GameVersions DetectVersion(ref GameSubVersions GameSubVersion)
		{
			switch (ReadUInt32(0x593FFF))
			{
				case 0xFAE804C5:
					return GameVersions.Demo;
				case 0x4B8B2274:
					return GameVersions.ReleaseEnglish;
				case 0xC985ED33:
					switch (ReadUInt32(0x494FB1))
					{
						case 0xF984BAE8:
							GameSubVersion = GameSubVersions.French;
							break;
						case 0xF6C12AE8:
							GameSubVersion = GameSubVersions.German;
							break;
						case 0xF6C0FAE8:
							GameSubVersion = GameSubVersions.Spanish;
							break;
						case 0xF6C10AE8:
							GameSubVersion = GameSubVersions.English;
							break;
						default:
							GameSubVersion = GameSubVersions.Unknown;
							break;
					};
					return GameVersions.ReleaseInternational;
				case 0xFC468D05:
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
