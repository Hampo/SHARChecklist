using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARChecklist
{
	public class SHARMemory : ProcessMemory
	{
		private static string ZeroTerminate(string String)
		{
			int num = String.IndexOf('\0');
			if (num == -1)
			{
				return String;
			}
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

		public enum Cheats
		{
			Unknown,
			Unknown2,
			UnlockAllOutfits,
			UnlockAllStoryMissions,
			Unknown3,
			UnlockAllRewardVehicles,
			Unknown4,
			NoTopSpeed,
			HighAcceleration,
			CarJumpOnHorn,
			FlamingCar,
			OneTapTrafficDeath,
			x5StageTime,
			Unknown5,
			KickSwapsCharacterModel,
			Unknown6,
			UnlockAllCameras,
			Unknown7,
			PlayCreditsDialogue,
			ShowSpeedometer,
			RedBrick,
			InvincibleCar,
			ShowTree,
			Trippy
		}

		public GameVersions GameVersion;
		public GameSubVersions GameSubVersion;

		public uint GameFlow => ReadUInt32(SelectAddress(7114772u, 7114708u, 7114708u, 7114764u));
		public GameState _GameState => (GameState)checked(GameFlow == 0 ? 0 : ReadUInt32(GameFlow + 12u));
		public uint CurrentGameplayManager => ReadUInt32(SelectAddress(7113112u, 7113048u, 7113048u, 7113104u));
		public uint CarSlot(uint Index) => checked(CurrentGameplayManager + 44u + 124u * Index);
		public uint GameplayManagerCar
		{
			get
			{
				return ReadUInt32(checked(CurrentGameplayManager + 1188u));
			}
			set
			{
				WriteUInt32(checked(CurrentGameplayManager + 1188u), value);
			}
		}
		public uint CarSlotCar(uint CarSlot) => ReadUInt32(checked(CarSlot + 112u));

		public uint HitNRunManager => ReadUInt32(SelectAddress(7111904u, 7111840u, 7111840u, 7111896u));

		public uint VehicleCentral => ReadUInt32(SelectAddress(7111896u, 7111832u, 7111832u, 7111888u));

		public uint Car(uint Index) => ReadUInt32(checked(VehicleCentral + 180u + Index * 4u));

		public string CarName(uint Car)
		{
			checked
			{
				uint num = ReadUInt32(Car + 248u);
				StringBuilder name = new StringBuilder();
				uint num2 = 0u;
				while (true)
				{
					byte b = ReadByte(num + num2);
					if (b == 0)
					{
						break;
					}
					name.Append(Convert.ToChar(b));
					num2++;
				}
				return name.ToString();
			}
		}

		public float HitAndRun
		{
			get
			{
				return ReadSingle(checked(HitNRunManager + 4u));
			}
			set
			{
				WriteSingle(checked(HitNRunManager + 4u), value);
			}
		}

		public float HitAndRunTime
		{
			get
			{
				return ReadSingle(checked(HitNRunManager + 168u));
			}
			set
			{
				WriteSingle(checked(HitNRunManager + 168u), value);
			}
		}

		private uint EnabledCheats
		{
			get
			{
				return ReadUInt32(SelectAddress(7111712u, 7111648u, 7111648u, 7111704u));
			}
			set
			{
				WriteUInt32(SelectAddress(7111712u, 7111648u, 7111648u, 7111704u), value);
			}
		}

		public uint RewardManager => ReadUInt32(SelectAddress(7113096u, 7113032u, 7113032u, 7113088u));

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

		public int Coins
		{
			get
			{
				return ReadInt32(checked(CharacterSheetManager + 4380u));
			}
			set
			{
				WriteInt32(checked(CharacterSheetManager + 4380u), value);
			}
		}

		private uint GUIScreenHUD => ReadUInt32(SelectAddress(7113008u, 7112944u, 7112944u, 7113000u));

		public int DisplayCoins
		{
			get
			{
				return ReadInt32(checked(ReadUInt32(GUIScreenHUD + 936u) + 36u));
			}
			set
			{
				WriteInt32(checked(ReadUInt32(GUIScreenHUD + 936u) + 36u), value);
			}
		}

		/*public Vector3 GetCharacterPosition(uint Character)
		{
			checked
			{
				uint num = Character + 100u;
				return new Vector3(ReadSingle(num), ReadSingle(num + 4u), ReadSingle(num + 8u));
			}
		}

		public void SetCharacterPosition(uint Character, Vector3 Position)
		{
			checked
			{
				uint num = Character + 100u;
				WriteSingle(num, Position.X);
				WriteSingle(num + 4u, Position.Y);
				WriteSingle(num + 8u, Position.Z);
			}
		}*/

		public float CharacterRotation(uint Character) => ReadSingle(checked(Character + 272u));

		public bool CharacterInCar(uint Character) => ReadUInt32(checked(Character + 348u)) != 0;

		public uint CharacterManager => ReadUInt32(SelectAddress(7111792u, 7111728u, 7111728u, 7111784u));

		public uint Characters(uint Index) => ReadUInt32(checked(CharacterManager + 192u + Index * 4u));

		public string CharacterName(uint Index) => ZeroTerminate(Encoding.ASCII.GetString(ReadBytes(checked(CharacterManager + 448u + Index * 64u), 64u)));

		public string CharacterAnimationSet(uint Index) => ZeroTerminate(Encoding.ASCII.GetString(ReadBytes(checked(CharacterManager + 4544u + Index * 64u), 64u)));

		public uint Player => Characters(0u);

		public uint CharacterCar(uint Character) => ReadUInt32(checked(Character + 348u));

		/*public Matrix CarMatrix(uint Car)
		{
			checked
			{
				uint num = Car + 184u;
				Matrix result = default;
				result.M11 = ReadSingle(num);
				result.M12 = ReadSingle(num + 4u);
				result.M13 = ReadSingle(num + 8u);
				result.M14 = ReadSingle(num + 12u);
				result.M21 = ReadSingle(num + 16u);
				result.M22 = ReadSingle(num + 20u);
				result.M23 = ReadSingle(num + 24u);
				result.M24 = ReadSingle(num + 28u);
				result.M31 = ReadSingle(num + 32u);
				result.M32 = ReadSingle(num + 36u);
				result.M33 = ReadSingle(num + 40u);
				result.M34 = ReadSingle(num + 44u);
				result.M41 = ReadSingle(num + 48u);
				result.M42 = ReadSingle(num + 52u);
				result.M43 = ReadSingle(num + 56u);
				result.M44 = ReadSingle(num + 60u);
				return result;
			}
		}

		public void SetCarMatrix(uint Car, Matrix CarMatrix)
		{
			uint num = Car + 184u;
			WriteSingle(num, CarMatrix.M11);
			WriteSingle(num + 4u, CarMatrix.M12);
			WriteSingle(num + 8u, CarMatrix.M13);
			WriteSingle(num + 12u, CarMatrix.M14);
			WriteSingle(num + 16u, CarMatrix.M21);
			WriteSingle(num + 20u, CarMatrix.M22);
			WriteSingle(num + 24u, CarMatrix.M23);
			WriteSingle(num + 28u, CarMatrix.M24);
			WriteSingle(num + 32u, CarMatrix.M31);
			WriteSingle(num + 36u, CarMatrix.M32);
			WriteSingle(num + 40u, CarMatrix.M33);
			WriteSingle(num + 44u, CarMatrix.M34);
			WriteSingle(num + 48u, CarMatrix.M41);
			WriteSingle(num + 52u, CarMatrix.M42);
			WriteSingle(num + 56u, CarMatrix.M43);
			WriteSingle(num + 60u, CarMatrix.M44);
		}*/

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
