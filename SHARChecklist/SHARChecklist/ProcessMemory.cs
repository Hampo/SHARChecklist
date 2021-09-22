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

		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, IntPtr lpBuffer, int iSize, ref int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out UIntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

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

		public void Write(uint Address, byte[] Buffer, uint Written = 0u)
		{
			IntPtr intPtr = OpenProcess(40, bInheritHandle: false, Process.Id);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			try
			{
				int num = default;
				bool flag = VirtualProtectEx(intPtr, new UIntPtr(Address), Buffer.Length, 4, num);
				try
				{
					if (!WriteProcessMemory(Process.Handle, new UIntPtr(Address), Buffer, new IntPtr(Buffer.Length), out UIntPtr lpNumberOfBytesWritten))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					Written = lpNumberOfBytesWritten.ToUInt32();
				}
				finally
				{
					if (flag)
					{
						VirtualProtectEx(intPtr, new UIntPtr(Address), Buffer.Length, num, num);
					}
				}
			}
			finally
			{
				CloseHandle(intPtr);
			}
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

		public void WriteByte(uint Address, byte Value)
		{
			Write(Address, new byte[1]
			{
			Value
			});
		}

		public void WriteBytes(uint Address, byte[] Value)
		{
			Write(Address, Value);
		}

		public void WriteDouble(uint Address, double Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteSingle(uint Address, float Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteInt16(uint Address, short Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteInt32(uint Address, int Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteInt64(uint Address, long Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteUInt16(uint Address, ushort Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteUInt32(uint Address, uint Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

		public void WriteUInt64(uint Address, ulong Value)
		{
			byte[] bytes = BitConverter.GetBytes(Value);
			Write(Address, bytes);
		}

        public void Dispose()
        {
			Process.Dispose();
        }
    }
}
