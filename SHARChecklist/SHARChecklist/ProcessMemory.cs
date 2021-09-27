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

		private const int PROCESS_CREATE_THREAD = 0x0002;
		private const int PROCESS_QUERY_INFORMATION = 0x0400;
		private const int PROCESS_VM_OPERATION = 0x0008;
		private const int PROCESS_VM_WRITE = 0x0020;
		private const int PROCESS_VM_READ = 0x0010;

		private const uint MEM_COMMIT = 0x00001000;
		private const uint MEM_RESERVE = 0x00002000;
		private const uint MEM_RELEASE = 0x00008000;
		private const uint PAGE_READWRITE = 4;

		[Flags]
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
		private static extern bool VirtualProtectEx(IntPtr hProcess, UIntPtr lpAddress, int dwSize, int flNewProtect, [Out] int lpflOldProtect);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, IntPtr nSize, UIntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadLibraryEx(string dllToLoad, IntPtr hFile, LoadLibraryFlags flags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void FreeLibrary(IntPtr module);

		[DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
		private extern static UIntPtr GetProcAddressOrdinal(IntPtr hwnd, UIntPtr procedureName);

		[DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
		private extern static UIntPtr GetProcAddress(IntPtr hwnd, string procedureName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool GetExitCodeThread(IntPtr hHandle, out uint lpExitCode);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		private List<IntPtr> ASMFunctions = new List<IntPtr>();

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
				byte[] array = new byte[Length];
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

		public ushort ReadUInt16(uint Address)
		{
			byte[] array = new byte[2];
			Read(Address, array);
			return BitConverter.ToUInt16(array, 0);
		}

		public uint ReadUInt32(uint Address)
		{
			byte[] array = new byte[4];
			Read(Address, array);
			return BitConverter.ToUInt32(array, 0);
		}

		public ulong ReadUInt64(uint Address)
		{
			byte[] array = new byte[8];
			Read(Address, array);
			return BitConverter.ToUInt64(array, 0);
		}
		private static string NullTerminate(string String)
		{
			int num = String.IndexOf('\0');
			if (num == -1)
				return String;

			return String.Substring(0, num);
		}

		public string ReadString(uint Address, Encoding Encoding, uint maxLength=512)
		{
			return NullTerminate(Encoding.GetString(ReadBytes(Address, maxLength)));
		}

		public IntPtr GetModuleBaseAddress(string ModuleName)
		{
			ProcessModule hacksModule = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
			return hacksModule == null ? IntPtr.Zero : hacksModule.BaseAddress;
		}

		private readonly Dictionary<string, Dictionary<uint, uint>> ordinalAddressCache = new Dictionary<string, Dictionary<uint, uint>>();
		public uint GetModuleProcAddress(string ModuleName, uint Proc)
		{
			ModuleName = ModuleName.ToLower();
			if (ordinalAddressCache.ContainsKey(ModuleName) && ordinalAddressCache[ModuleName].ContainsKey(Proc))
				return ordinalAddressCache[ModuleName][Proc];

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
			uint address = (uint)(GetModuleBaseAddress(ModuleName).ToInt32() + offset);

			if (!ordinalAddressCache.ContainsKey(ModuleName))
				ordinalAddressCache[ModuleName] = new Dictionary<uint, uint>();

			ordinalAddressCache[ModuleName][Proc] = address;

			return address;
		}

		private readonly Dictionary<string, Dictionary<string, uint>> namedAddressCache = new Dictionary<string, Dictionary<string, uint>>();
		public uint GetModuleProcAddress(string ModuleName, string Proc)
		{
			ModuleName = ModuleName.ToLower();
			if (namedAddressCache.ContainsKey(ModuleName) && namedAddressCache[ModuleName].ContainsKey(Proc))
				return namedAddressCache[ModuleName][Proc];

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
			uint address = (uint)(GetModuleBaseAddress(ModuleName).ToInt32() + offset);

			if (!namedAddressCache.ContainsKey(ModuleName))
				namedAddressCache[ModuleName] = new Dictionary<string, uint>();

			namedAddressCache[ModuleName][Proc] = address;

			return address;
		}

		protected void Inject(string path)
		{
			IntPtr procHandle = IntPtr.Zero;
			IntPtr allocMemAddress = IntPtr.Zero;
			IntPtr hThread = IntPtr.Zero;
			try
			{
				procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
				if (procHandle == IntPtr.Zero)
					throw new Win32Exception();


				UIntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryW");
				byte[] pathBytes = Encoding.Unicode.GetBytes(path + '\0');
				uint pathLength = (uint)pathBytes.Length;
				allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, pathLength, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
				if (allocMemAddress == IntPtr.Zero)
					throw new Win32Exception();

				if (!WriteProcessMemory(procHandle, allocMemAddress, pathBytes, pathLength, out _))
					throw new Win32Exception();

				hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, (IntPtr)loadLibraryAddr.ToUInt32(), allocMemAddress, 0, IntPtr.Zero);
				if (hThread == IntPtr.Zero)
					throw new Win32Exception();

				if (WaitForSingleObject(hThread, 0xFFFFFFFF) == 0xFFFFFFFF)
					throw new Win32Exception();
			}
			finally
			{
				if (hThread != IntPtr.Zero)
					if (!CloseHandle(hThread))
						throw new Win32Exception();

				if (allocMemAddress != IntPtr.Zero)
					if(!VirtualFreeEx(procHandle, allocMemAddress, 0, MEM_RELEASE))
						throw new Win32Exception();

				if (procHandle != IntPtr.Zero)
					if (!CloseHandle(procHandle))
						throw new Win32Exception();
			}
		}

		protected uint Execute(IntPtr Address, object Parameter)
		{
			IntPtr procHandle = IntPtr.Zero;
			IntPtr allocMemAddress = IntPtr.Zero;
			IntPtr hThread = IntPtr.Zero;
			IntPtr paramsMemory = IntPtr.Zero;
			try
			{
				procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
				if (procHandle == IntPtr.Zero)
					throw new Win32Exception();

				uint paramsSize = (uint)Marshal.SizeOf(Parameter);
				paramsMemory = Marshal.AllocHGlobal((int)paramsSize);
				Marshal.StructureToPtr(Parameter, paramsMemory, false);

				allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, paramsSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
				if (allocMemAddress == IntPtr.Zero)
					throw new Win32Exception();

				if (!WriteProcessMemory(procHandle, allocMemAddress, paramsMemory, paramsSize, out _))
					throw new Win32Exception();

				hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, Address, allocMemAddress, 0, IntPtr.Zero);
				if (hThread == IntPtr.Zero)
					throw new Win32Exception();

				if (WaitForSingleObject(hThread, 0xFFFFFFFF) == 0xFFFFFFFF)
					throw new Win32Exception();

				if (!GetExitCodeThread(hThread, out uint retVal))
					throw new Win32Exception();

				return retVal;
			}
			finally
			{
				if (paramsMemory != IntPtr.Zero)
					Marshal.FreeHGlobal(paramsMemory);

				if (hThread != IntPtr.Zero)
					if (!CloseHandle(hThread))
						throw new Win32Exception();

				if (allocMemAddress != IntPtr.Zero)
					if(!VirtualFreeEx(procHandle, allocMemAddress, 0, MEM_RELEASE))
						throw new Win32Exception();

				if (procHandle != IntPtr.Zero)
					if (!CloseHandle(procHandle))
						throw new Win32Exception();
			}
		}

		protected uint Execute(byte[] Bytes, object Parameter)
		{
			IntPtr procHandle = IntPtr.Zero;
			IntPtr funcMemoryAddress = IntPtr.Zero;
			try
			{
				procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
				if (procHandle == IntPtr.Zero)
					throw new Win32Exception();

				uint funcSize = (uint)Bytes.Length;
				funcMemoryAddress = VirtualAllocEx(procHandle, IntPtr.Zero, funcSize, MEM_COMMIT | MEM_RESERVE, 0x40);
				if (funcMemoryAddress == IntPtr.Zero)
					throw new Win32Exception();

				return Execute(funcMemoryAddress, Parameter);
			}
			finally
			{
				if (funcMemoryAddress != IntPtr.Zero)
					if (!VirtualFreeEx(procHandle, funcMemoryAddress, 0, MEM_RELEASE))
						throw new Win32Exception();

				if (procHandle != IntPtr.Zero)
					if (!CloseHandle(procHandle))
						throw new Win32Exception();
			}
		}

		protected IntPtr InjectFunction(byte[] Bytes)
		{
			IntPtr procHandle = IntPtr.Zero;
			try
			{
				procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
				if (procHandle == IntPtr.Zero)
					throw new Win32Exception();

				uint funcSize = (uint)Bytes.Length;
				IntPtr funcMemoryAddress = VirtualAllocEx(procHandle, IntPtr.Zero, funcSize, MEM_COMMIT | MEM_RESERVE, 0x40);

				if (funcMemoryAddress == IntPtr.Zero)
					throw new Win32Exception();

				if (!WriteProcessMemory(procHandle, funcMemoryAddress, Bytes, funcSize, out _))
					throw new Win32Exception();

				ASMFunctions.Add(funcMemoryAddress);
				return funcMemoryAddress;
			}
			finally
			{
				if (procHandle != IntPtr.Zero)
					if (!CloseHandle(procHandle))
						throw new Win32Exception();
			}
		}

		public void Dispose()
		{
			IntPtr procHandle = IntPtr.Zero;
			try
			{
				procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
				if (procHandle != IntPtr.Zero)
					foreach (IntPtr Address in ASMFunctions)
						if (Address != IntPtr.Zero)
							VirtualFreeEx(procHandle, Address, 0, MEM_RELEASE);
			}
			finally
			{
				if (procHandle != IntPtr.Zero)
					CloseHandle(procHandle);
			}
			Process.Dispose();
		}
	}
}
