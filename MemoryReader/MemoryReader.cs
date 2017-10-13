using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace FantomMemoryReader
{
    public class MemoryReader
    {
        #region WinApi_Imports
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int dwSize,
            ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten);

        private const int PROCESS_WM_READ = 0x0010;
        private const int PROCESS_WM_WRITE = 0x0020;
        private const int PROCESS_WM_OPERATION = 0x0008;
        private static IntPtr processHandleR;
        #endregion
        #region findProcess
        private Process process;
        private byte[] buffer;
        private String name = "";
        private int bytesRead;
        private IntPtr bytesWrite;
        private int textLength;
        /// <summary>
        /// Enter process name without .exe or other extensions!
        /// </summary>
        /// <param name="processName">FarCry4</param>
        public MemoryReader(String processName)
        {
            try
            {
                name = processName;
                process = Process.GetProcessesByName(processName)[0];
                processHandleR = OpenProcess(PROCESS_WM_READ | PROCESS_WM_WRITE | PROCESS_WM_OPERATION, false, process.Id);
            }
            catch (Exception)
            {
                throw new Exception("Process " + name + " not found, or you dont have the necessary rights!");
            }
        }

        public override string ToString()
        {
            return "Current process: " + name;
        }
        private bool isRunning()
        {

            if (processHandleR == IntPtr.Zero | process.HasExited == true)
            {
                throw new Exception("Process " + name + " is not running!");
            }
            return true;
        }
        #endregion
        #region writeMemory
        /// <summary>
        ///  Writes integer value to memory address.
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        /// <param name="value">Example integer value: 65 </param>
        public void writeMemoryInt(int address, int value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes integer value to x64 memory address.
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        /// <param name="value">Example integer value: 65 </param>
        public void writeMemoryInt64(Int64 address, Int64 value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[8];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes float value to memory addres.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="value">Example for integer value: 35.24000 </param>
        public void writeMemoryFloat(int address, float value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes float value to x64 memory address.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="value">Example for integer value: 35.24000 </param>
        public void writeMemoryFloat64(Int64 address, float value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes double value to memory.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="value">Example for integer value: 35.14 </param>
        public void writeMemoryDouble(int address, double value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[8];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes double value to x64 memory address.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="value">Example for integer value: 35.14 </param>
        public void writeMemoryDouble64(Int64 address, double value)
        {
            if (isRunning() == true)
            {
                bytesWrite = (IntPtr)BitConverter.GetBytes(value).Length;
                buffer = new byte[8];
                buffer = BitConverter.GetBytes(value);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes text value to memory address.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="text">Example for text value: Mitsubishi Evo </param>
        public void writeMemoryText(int address, String text)
        {
            if (isRunning() == true)
            {
                textLength = text.Length;
                bytesWrite = (IntPtr)textLength;
                buffer = new byte[textLength];
                buffer = Encoding.ASCII.GetBytes(text);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        /// <summary>
        ///  Writes text value to x64 memory address.
        /// </summary>
        /// <param name="address">Example for address: 0x0017BD70</param>
        /// <param name="text">Example for text value: Mitsubishi Evo </param>
        public void writeMemoryText64(Int64 address, String text)
        {
            if (isRunning() == true)
            {
                textLength = text.Length;
                bytesWrite = (IntPtr)textLength;
                buffer = new byte[textLength];
                buffer = Encoding.ASCII.GetBytes(text);
                WriteProcessMemory(processHandleR, (IntPtr)address, buffer, buffer.Length, out bytesWrite);
            }
        }
        #endregion
        #region readMemory
        /// <summary>
        ///  Reads integer value from memory address.
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public int readMemoryInt(int address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[4];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToInt32(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads integer value from x64 memory address.
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public Int64 readMemoryInt64(Int64 address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[8];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToInt64(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads float value from memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public float readMemoryFloat(int address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[4];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToSingle(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads float value from x64 memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public float readMemoryFloat64(Int64 address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[4];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToSingle(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads double value from memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public double readMemoryDouble(int address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[8];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToDouble(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads double value from x64 memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        public double readMemoryDouble64(Int64 address)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[8];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToDouble(buffer, 0);
            }
            return 0;
        }
        /// <summary>
        ///  Reads text value from memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        /// <param name="bytesToRead">Size of bytes to read</param>
        public String readMemoryText(int address, int bytesToRead)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[bytesToRead];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToString(buffer, 0);
            }
            return null;
        }
        /// <summary>
        ///  Reads text value from x64 memory address!
        /// </summary>
        /// <param name="address">Example address: 0x0017BD70</param>
        /// <param name="bytesToRead">Size of bytes to read</param>
        public String readMemoryText64(Int64 address, int bytesToRead)
        {
            if (isRunning() == true)
            {
                bytesRead = 0;
                buffer = new byte[bytesToRead];
                ReadProcessMemory((int)processHandleR, (IntPtr)address, buffer, buffer.Length, ref bytesRead);
                return BitConverter.ToString(buffer, 0);
            }
            return null;
        }
        #endregion
    }
}

/*
         Created by Dragan Puljić aka Fantom
         Contact: srbfantom@gmail.com 
         13.10.2017 13:37PM
         Version: V1.0 Public
*/
