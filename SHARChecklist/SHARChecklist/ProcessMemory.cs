using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SHARChecklist
{
	public class ProcessMemory : IDisposable
	{
		public Process Process;

		private const int PROCESS_VM_OPERATION = 8;

		private const int PROCESS_VM_WRITE = 32;

		private const int PAGE_READWRITE = 4;

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool VirtualProtectEx(IntPtr hProcess, UIntPtr lpAddress, int dwSize, int flNewProtect, [Out] int lpflOldProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, IntPtr nSize, UIntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadLibraryEx(string dllToLoad, IntPtr hFile, LoadLibraryFlags flags);

		[System.Flags]
		public enum LoadLibraryFlags : uint
		{
			DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
			LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
			LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
			LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
			LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
			LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
			LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
			LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
			LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void FreeLibrary(IntPtr module);

		[DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
		private extern static UIntPtr GetProcAddressOrdinal(IntPtr hwnd, UIntPtr procedureName);

		[DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
		private extern static UIntPtr GetProcAddress(IntPtr hwnd, string procedureName);

		public ProcessMemory(Process Process)
		{
			this.Process = Process;
		}

		public void Read(uint Address, byte[] Buffer, uint Read = 0u)
		{
			UIntPtr lpNumberOfBytesRead = default;
			if (!ReadProcessMemory(Process.Handle, new UIntPtr(Address), Buffer, new IntPtr(Buffer.Length), lpNumberOfBytesRead))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			Read = lpNumberOfBytesRead.ToUInt32();
		}

		public byte ReadByte(uint Address)
		{
			byte[] array = new byte[1];
			Read(Address, array);
			return array[0];
		}

		public byte[] ReadBytes(uint Address, uint Length)
		{
			checked
			{
				byte[] array = new byte[(int)(unchecked((long)Length) - 1L) + 1];
				Read(Address, array);
				return array;
			}
		}

		public double ReadDouble(uint Address)
		{
			byte[] array = new byte[8];
			Read(Address, array);
			return BitConverter.ToDouble(array, 0);
		}

		public float ReadSingle(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToSingle(array, 0);
		}

		public short ReadInt16(uint Address)
		{
			byte[] array = new byte[2];
			Read(Address, array);
			return BitConverter.ToInt16(array, 0);
		}

		public int ReadInt32(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToInt32(array, 0);
		}

		public long ReadInt64(uint Address)
		{
			byte[] array = new byte[8];
			Read(Address, array);
			return BitConverter.ToInt64(array, 0);
		}

		public uint ReadUInt32(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToUInt32(array, 0);
		}

		public ulong ReadUInt64(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToUInt64(array, 0);
		}

		public ushort ReadUInt16(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToUInt16(array, 0);
		}

		public IntPtr GetModuleBaseAddress(string ModuleName)
        {
			ProcessModule hacksModule = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
			return hacksModule == null ? IntPtr.Zero : hacksModule.BaseAddress;
		}

		public uint GetModuleProcAddress(string ModuleName, uint Proc)
        {
			ProcessModule module = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
			if (module == null)
            {
				Console.WriteLine($"Couldn't find module: {ModuleName}");
				return 0;
			}

			IntPtr dll = LoadLibraryEx(module.FileName, IntPtr.Zero, LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
			if (dll == IntPtr.Zero)
			{
				Console.WriteLine($"Couldn't load module: {module.FileName}");
				return 0;
			}

			UIntPtr method = GetProcAddressOrdinal(dll, (UIntPtr)Proc);
			if (method == UIntPtr.Zero)
			{
				Console.WriteLine($"Couldn't find method: {Proc}");
				FreeLibrary(dll);
				return 0;
			}

			uint offset = (uint)(method.ToUInt32() - dll.ToInt32());

			FreeLibrary(dll);
			return (uint)(GetModuleBaseAddress(ModuleName).ToInt32() + offset);
		}

		public uint GetModuleProcAddress(string ModuleName, string Proc)
        {
			ProcessModule module = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
			if (module == null)
            {
				Console.WriteLine($"Couldn't find module: {ModuleName}");
				return 0;
			}

			IntPtr dll = LoadLibraryEx(module.FileName, IntPtr.Zero, LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
			if (dll == IntPtr.Zero)
			{
				Console.WriteLine($"Couldn't load module: {module.FileName}");
				return 0;
			}

			UIntPtr method = GetProcAddress(dll, Proc);
			if (method == UIntPtr.Zero)
			{
				Console.WriteLine($"Couldn't find method: {Proc}");
				FreeLibrary(dll);
				return 0;
			}

			uint offset = (uint)(method.ToUInt32() - dll.ToInt32());

			FreeLibrary(dll);
			return (uint)(GetModuleBaseAddress(ModuleName).ToInt32() + offset);
		}

        public void Dispose()
        {
			Process.Dispose();
        }
    }
}
