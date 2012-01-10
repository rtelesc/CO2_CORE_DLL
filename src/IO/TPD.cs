//using System;
//using System.IO;
//using System.Text;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;

//namespace CO2_CORE_DLL.IO
//{
//    /// <summary>
//    /// NetDragon Data Package (Data)
//    /// </summary>
//    public unsafe class TPD
//    {
//        public const String TPD_IDENTIFIER = "NetDragonDatPkg\0";
//        public const Int64 TPD_VERSION = 1000;

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct Header
//        {
//            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 0x10)]
//            public String Identifier;
//            public Int64 Version;
//            public Int32 Unknown1; //0x01
//            public Int32 Unknown2; //0x03
//        };
//    }
//}