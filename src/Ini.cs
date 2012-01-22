// * ************************************************************
// * * START:                                            ini.cs *
// * ************************************************************

// * ************************************************************
// *                      INFORMATIONS
// * ************************************************************
// * INI wrapper for the library.
// * ini.cs
// * 
// * --
// *
// * Feel free to use this class in your projects, but don't
// * remove the header to keep the paternity of the class.
// * 
// * ************************************************************
// *                      CREDITS
// * ************************************************************
// * Originally created by CptSky (December 7th, 2011)
// * Copyright (C) 2011 CptSky
// *
// * ************************************************************

using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

using System.Diagnostics;

namespace CO2_CORE_DLL
{
    //TODO: The class need to be rewrited to remove all the dependencies to the WinAPI.
    public class Ini
    {
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        private static extern Int32 WritePrivateProfileString(String lpAppName, String lpKeyName, String lpString, String lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern Int32 GetPrivateProfileString(String lpAppName, String lpKeyName, String lpDefault, StringBuilder lpReturnedString, Int32 nSize, String lpFileName);

        private String Path;

        /// <summary>
        /// Open the specified file with an INI reader.
        /// </summary>
        public Ini(String Path)
        {
            this.Path = Path;
        }

        ~Ini()
        {
            Path = null;
        }

        /// <summary>
        /// Write the specified value.
        /// </summary>
        public Int32 WriteValue(String Section, String Key, Object Value)
        {
            return WritePrivateProfileString(Section, Key, Value.ToString(), Path);
        }

        /// <summary>
        /// Read the value as a 8-bits signed integer.
        /// </summary>
        public SByte ReadInt8(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            SByte Value = 0;
            SByte.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 8-bits unsigned integer.
        /// </summary>
        public Byte ReadUInt8(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Byte Value = 0;
            Byte.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 16-bits signed integer.
        /// </summary>
        public Int16 ReadInt16(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Int16 Value = 0;
            Int16.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 16-bits unsigned integer.
        /// </summary>
        public UInt16 ReadUInt16(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            UInt16 Value = 0;
            UInt16.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed integer.
        /// </summary>
        public Int32 ReadInt32(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Int32 Value = 0;
            Int32.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 32-bits unsigned integer.
        /// </summary>
        public UInt32 ReadUInt32(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            UInt32 Value = 0;
            UInt32.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed integer.
        /// </summary>
        public Int64 ReadInt64(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Int64 Value = 0;
            Int64.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 64-bits unsigned integer.
        /// </summary>
        public UInt64 ReadUInt64(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            UInt64 Value = 0;
            UInt64.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 32-bits signed decimal.
        /// </summary>
        public Single ReadFloat(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Single Value = 0;
            Single.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a 64-bits signed decimal.
        /// </summary>
        public Double ReadDouble(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);

            Double Value = 0;
            Double.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a boolean.
        /// </summary>
        public Boolean ReadBoolean(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "false", Buffer, Buffer.Capacity, Path);

            Boolean Value = false;
            Boolean.TryParse(Buffer.ToString(), out Value);
            return Value;
        }

        /// <summary>
        /// Read the value as a string.
        /// </summary>
        public String ReadValue(String Section, String Key)
        {
            StringBuilder Buffer = new StringBuilder(Kernel.MAX_BUFFER_SIZE);
            GetPrivateProfileString(Section, Key, "0", Buffer, Buffer.Capacity, Path);
            return Buffer.ToString();
        }

        ///// <summary>
        ///// Read the value as a string.
        ///// </summary>
        //public String ReadValue2(String Section, String Key)
        //{
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    String Str = "";
        //    IniStrGet(Section, Key, ref Str);
        //    sw.Stop();
        //    Console.WriteLine(sw.ElapsedMilliseconds + " " + sw.ElapsedTicks);
        //    return Str;
        //}

        private Boolean IniStrGet(String Section, String Key, ref String Str)
        {
            if (Section == null || Key == null)
                return false;

            Section = "[" + Section + "]";
            Key = Key.ToLowerInvariant();

            using (StreamReader Stream = new StreamReader(Path, Encoding.GetEncoding("Windows-1252")))
            {
                Boolean Found = false;
                String Line = null;
                while (true)
                {
                    Line = Stream.ReadLine();
                    if (Line == null)
                        break;

                    if (Line.Length < Section.Length)
                        continue;

                    if (0 != String.Compare(Section, 0, Line, 0, Section.Length, true))
                        continue;

                    while (true)
                    {
                        Line = Stream.ReadLine();
                        if (Line == null)
                            goto CLOSE;

                        if (Line.Contains("[") && Line.Contains("]"))
                            goto CLOSE;

                        String TmpLine = Line.Replace(" ", "");
                        TmpLine = TmpLine.Replace("\t", "");
                        TmpLine = TmpLine.ToLowerInvariant();

                        if (TmpLine.StartsWith(Key + "="))
                        {
                            Str = Line;
                            for (Int32 i = 0; i < Line.Length; i++)
                            {
                                if (Line[i] == '=')
                                {
                                    Str = Line.Substring(i + 1, Line.Length - (i + 1));
                                    Str = Str.TrimStart(new Char[] { ' ', '\t' });
                                }
                            }

                            Found = true;
                            goto CLOSE;
                        }
                    }
                }

            CLOSE:
                //if (!Found)
                //    Console.WriteLine("Error: [{0}] {1} not found in {2}.", Section, Key, Path);
                return Found;
            }
        }
    }
}

// * ************************************************************
// * * END:                                              ini.cs *
// * ************************************************************