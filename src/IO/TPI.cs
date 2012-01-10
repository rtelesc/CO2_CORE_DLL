//using System;
//using System.IO;
//using System.Text;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using ComponentAce.Compression.Libs.zlib;

//namespace CO2_CORE_DLL.IO
//{
//    /// <summary>
//    /// NetDragon Data Package (Information)
//    /// </summary>
//    public unsafe class TPI
//    {
//        public const String TPI_IDENTIFIER = "NetDragonDatPkg\0";
//        public const Int64 TPI_VERSION = 1000;

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct Header
//        {
//            public fixed Byte Identidier[0x10];
//            public Int64 Version;
//            public Int32 Unknown1; //0x01
//            public Int32 Unknown2; //0x03
//            public Int32 Unknown3; //0x30
//            public Int32 EntriesCount;
//            public Int32 LastEntry; //Seems to be the last entry offset...
//            public Int32 Unknown4; //0x00
//        };

//        public struct Entry
//        {
//            public Byte PathLength;
//            public String Path;
//            public Int16 Unknown1; //0x01
//            public Int32 RealSize;
//            public Int32 NewSize;
//            public Int32 NewSize2;
//            public Int32 RealSize2;
//            public Int32 DataOffset;
//        }

//        private Header* pHeader;
//        private List<Entry> Entries;

//        private String TpiFile;
//        private String TpdFile;

//        private Encoding Encoding;

//        public TPI(String File)
//        {
//            Encoding = Encoding.GetEncoding("iso-8859-1");

//            File = File.ToLowerInvariant();
//            if (File.EndsWith(".tpd"))
//            {
//                TpdFile = File;
//                TpiFile = File.Substring(0, File.Length - 4) + ".tpi";
//            }
//            else if (File.EndsWith(".tpi"))
//            {
//                TpdFile = File.Substring(0, File.Length - 4) + ".tpd";
//                TpiFile = File;
//            }
//            else
//                throw new Exception("CO2_CORE_DLL::FILE::TPI() -> Invalid file extension!");

//            if (!System.IO.File.Exists(TpiFile))
//                throw new Exception("CO2_CORE_DLL::FILE::TPI() -> The specified file doesn't exist!");

//            if (!System.IO.File.Exists(TpdFile))
//                throw new Exception("CO2_CORE_DLL::FILE::TPI() -> The specified file doesn't exist!");

//            pHeader = (Header*)Kernel.malloc(sizeof(Header));

//            using (FileStream Stream = new FileStream(TpiFile, FileMode.Open, FileAccess.Read, FileShare.Read))
//            {
//                Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];

//                Stream.Read(Buffer, 0, sizeof(Header));
//                Kernel.memcpy((Byte*)pHeader, Buffer, sizeof(Header));

//                Entries = new List<Entry>(pHeader->EntriesCount);
//                using (BinaryReader Reader = new BinaryReader(Stream, Encoding))
//                {
//                    for (Int32 i = 0; i < pHeader->EntriesCount; i++)
//                    {
//                        Entry Entry = new Entry();

//                        Entry.PathLength = Reader.ReadByte();
//                        Entry.Path = Encoding.GetString(Reader.ReadBytes(Entry.PathLength));
//                        Entry.Unknown1 = Reader.ReadInt16();
//                        Entry.RealSize = Reader.ReadInt32();
//                        Entry.NewSize = Reader.ReadInt32();
//                        Entry.NewSize2 = Reader.ReadInt32();
//                        Entry.RealSize2 = Reader.ReadInt32();
//                        Entry.DataOffset = Reader.ReadInt32();

//                        Entries.Add(Entry);
//                    }
//                }
//            }
//        }

//        ~TPI()
//        {
//            if (pHeader != null)
//                Kernel.free(pHeader);
//        }

//        public void Extract(String Path, ref Byte[] Data)
//        {

//        }

//        public void ExtractAll(String Path)
//        {
//            Path = Path.Replace('/', '\\');
//            if (!Path.EndsWith("\\"))
//                Path += "\\";

//            Console.Write("Extracted: 0%");
//            using (FileStream Stream = new FileStream(TpdFile, FileMode.Open, FileAccess.Read, FileShare.Read))
//            {
//                Int32 i = 0;
//                foreach (Entry Entry in Entries)
//                {
//                    Console.Write("\rExtracted: {0}%", i * 100 / pHeader->EntriesCount);

//                    String Output = Path + Entry.Path.Replace("/", "\\");
//                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(Output)))
//                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Output));

//                    using (FileStream outStream = new FileStream(Output, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
//                    {
//                        ZOutputStream outZStream = new ZOutputStream(outStream);

//                        Stream.Seek(Entry.DataOffset, SeekOrigin.Begin);
//                        try
//                        {
//                            Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];

//                            Int32 Count = Entry.NewSize / Kernel.MAX_BUFFER_SIZE;
//                            Int32 Read = 0;

//                            for (Int64 x = 0; x < Count; x++)
//                            {
//                                Read = Stream.Read(Buffer, 0, Kernel.MAX_BUFFER_SIZE);
//                                outZStream.Write(Buffer, 0, Read);
//                            }

//                            Read = Stream.Read(Buffer, 0, Entry.NewSize % Kernel.MAX_BUFFER_SIZE);
//                            outZStream.Write(Buffer, 0, Read);

//                            outStream.Flush();
//                        }
//                        finally { outZStream.Close(); }
//                    }
//                    i++;
//                }
//            }
//            Console.Write("\rExtracted: 100%");
//        }

//        public static void Write(String Source, String Destination)
//        {

//        }
//    }
//}