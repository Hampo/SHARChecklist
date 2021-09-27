using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARChecklist
{
	public class SHARMemory : ProcessMemory
	{
		public static readonly string SHARFuncsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "SHARFuncs.dll");

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
		public GameState _GameState => (GameState)(GameFlow == 0 ? 0 : ReadUInt32(GameFlow + 12));

		public uint RewardManager => ReadUInt32(SelectAddress(0x6C8988, 0x6C8948, 0x6C8948, 0x6C8980));

		public ulong StreetRaceRewardHash(uint Level) => ReadUInt64(ReadUInt32(RewardManager + 4 + 36 * Level) + 4);

		public ulong BonusMissionRewardHash(uint Level) => ReadUInt64(ReadUInt32(RewardManager + 4 + 36 * Level + 4) + 4);

		public uint MaxTokens(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4);

		public uint GagTotal(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4 + 4);

		public uint WaspTotal(uint Level) => ReadUInt32(RewardManager + 4 + 36 * Level + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4);

		public uint MerchandiseCount(uint Level) => ReadUInt32(RewardManager + 4 + 288 + 1600 + 4 + 4 + 36 * Level + 8 * 4);

		[Obsolete("This method is obsolete. Use GetMerchandise instead.", true)]
		public uint MerchandiseIndex(uint Level, uint Index) => ReadUInt32(RewardManager + 4 * (Index + 8 * Level + Level) + 1900);

		public RewardType MerchandiseType(uint Index) => (RewardType)ReadUInt32(Index + 104);

		public uint CharacterSheetManager => ReadUInt32(SelectAddress(0x6C8984, 0x6C8944, 0x6C8944, 0x6C897C));

		public uint CharacterSheet => CharacterSheetManager + 4;

		public string PlayerName => ReadString(CharacterSheet, Encoding.ASCII, 16u);

		public string CardName(uint Level, uint Card) => ReadString(CharacterSheet + 16 + 620 * Level + 17 * Card, Encoding.ASCII, 16u);

		public bool CardCollected(uint Level, uint Card) => ReadByte(CharacterSheet + 16 + 620 * Level + 17 * Card + 16) != 0;

		public string MissionName(uint Level, uint Mission) => ReadString(CharacterSheet + 16 + 620 * Level + 120 + 32 * Mission, Encoding.ASCII, 16u);

		public bool MissionCompleted(uint Level, uint Mission) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 32 * (Level == 0 ? Mission + 1 : Mission) + 16) != 0;

		public string StreetRaceName(uint Level, uint StreetRace) => ReadString(CharacterSheet + 16 + 620 * Level + 120 + 256 + 32 * StreetRace, Encoding.ASCII, 16u);

		public bool StreetRaceCompleted(uint Level, uint StreetRace) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 32 * StreetRace + 16) != 0;

		public string BonusMissionName(uint Level) => ReadString(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96, Encoding.ASCII, 16u);

		public bool BonusMissionCompleted(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 16) != 0;

		public string GambleRaceName(uint Level) => ReadString(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32, Encoding.ASCII, 16u);

		public bool GambleRaceCompleted(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 16) != 0;

		public bool FMVUnlocked(uint Level) => ReadByte(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32) != 0;

		public uint VehiclesCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4);

		public uint CharacterClothingCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4);

		public uint WaspCamerasCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4);

		public string CurrentSkinName(uint Level) => ReadString(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4, Encoding.ASCII, 16u);

		public uint GagsCount(uint Level) => ReadUInt32(CharacterSheet + 16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4 + 16);

		private uint GetLevelsFunc => GetModuleProcAddress("Hacks.dll", 28);

		private uint GetLevelsFuncOld => GetModuleProcAddress("CustomStatsTotals.lmlh", "?GetLevels@@YAIXZ");

		[Obsolete("This method is obsolete. Use LevelCount instead.", true)]
		public uint LevelCountOld()
		{
			uint getLevelsFunc = GetLevelsFuncOld;
			if (getLevelsFunc == 0)
				getLevelsFunc = GetLevelsFunc;
			if (getLevelsFunc == 0)
				return 7u;
			if (ReadByte(getLevelsFunc) != 0xA1 || ReadByte(getLevelsFunc + 5) != 0xC3)
				return 7u;
			return ReadUInt32(ReadUInt32(getLevelsFunc + 1));
		}

		public byte LevelCount => ReadByte(SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3);

		public string[] GetLoadedHacks()
		{
			uint EventHacks = GetModuleProcAddress("Hacks.dll", 3151);
			if (EventHacks == 0)
				return null;

			uint HacksLoaded = ReadUInt32(EventHacks + 8);
			if (HacksLoaded == 0)
				return null;

			string[] Hacks = new string[HacksLoaded];

			int i = 0;
			uint node = ReadUInt32(EventHacks);
			while (node != 0)
			{
				uint hack = ReadUInt32(node + 12);
				Hacks[i] = ReadString(hack + 562, Encoding.Unicode, 128u);
				i++;

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
				if (ReadString(hack + 562, Encoding.Unicode, 128u).Equals(HackName, StringComparison.OrdinalIgnoreCase))
					return true;

				node = ReadUInt32(node + 8);
			}

			return false;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct GetMerchandiseParams
		{
			public IntPtr Func;
			public IntPtr RewardManager;
			public uint Level;
			public uint Index;
		}

		private readonly byte[] GetMerchandiseBytes = new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x0C, 0x8B, 0x4D, 0x08, 0x8B, 0x01, 0x89, 0x45, 0xF4, 0x8B, 0x41, 0x04, 0x89, 0x45, 0xFC, 0x8B, 0x41, 0x08, 0x89, 0x45, 0x08, 0x8B, 0x41, 0x0C, 0x89, 0x45, 0xF8, 0x8B, 0x45, 0x08, 0x8B, 0x55, 0xFC, 0x8B, 0x4D, 0xF8, 0xFF, 0x55, 0xF4, 0x8B, 0xE5, 0x5D, 0xC2, 0x04, 0x00 };
		private readonly IntPtr GetMerchandiseAddress = IntPtr.Zero;
		public uint GetMerchandise(uint level, uint index)
		{
			GetMerchandiseParams parameter = new GetMerchandiseParams
			{
				Func = (IntPtr)SelectAddress(0x4622E0, 0x4623B0, 0x462020, 0x461E20),
				RewardManager = (IntPtr)RewardManager,
				Level = level,
				Index = index
			};
			if (GetMerchandiseAddress != IntPtr.Zero)
				return Execute(GetMerchandiseAddress, parameter);
			return Execute(GetMerchandiseBytes, parameter);
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
			GetMerchandiseAddress = InjectFunction(GetMerchandiseBytes);
		}
	}
}
