//using System;
//using System.IO;
//using System.Text;
//using System.Runtime.InteropServices;

//namespace CO2_CORE_DLL.IO
//{
//    /// <summary>
//    /// Windsould Date File
//    /// </summary>
//    public unsafe class WDF
//    {
//        public const UInt32 WDF_ID = 0x57444650;
//        public const Int32 WDF_MAXFILE = 0x10000;

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct Header
//        {
//            public UInt32 Id;
//            public Int32 Number;
//            public UInt32 Offset;
//        };

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct Entry
//        {
//            public UInt32 UID;
//            public UInt32 Offset;
//            public UInt32 Size;
//            public UInt32 Space;
//        };

//        private Header* pHeader = null;

//        public WDF()
//        {
//            pHeader = (Header*)Kernel.calloc(sizeof(Header));
//        }

//        ~WDF()
//        {
//            Kernel.free(pHeader);
//        }

//        public static void Write(String Source, String Destination)
//        {
//            DirectoryInfo DI = new DirectoryInfo(Source);
//            FileInfo[] Files = DI.GetFiles("*.*", SearchOption.AllDirectories);

//            if (Files.Length > WDF_MAXFILE)
//                throw new Exception("A WDF package can't contains more than " + WDF_MAXFILE + " files!");

//            Header* pHeader = (Header*)Kernel.malloc(sizeof(Header));
//            pHeader->Id = WDF_ID;
//            pHeader->Number = Files.Length;
//            pHeader->Offset = (UInt32)sizeof(Header);

//            UInt32[] Offsets = new UInt32[Files.Length];
//            for (Int32 i = 0; i < pHeader->Number; i++)
//            {
//                Offsets[i] = pHeader->Offset;
//                pHeader->Offset += (UInt32)Files[i].Length;
//            }

//            Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];
//            using (FileStream FStream = new FileStream(Destination, FileMode.Create, FileAccess.Write, FileShare.Read))
//            {
//                Console.Write("Writing header... ");
//                Kernel.memcpy(Buffer, pHeader, sizeof(Header));
//                FStream.Write(Buffer, 0, sizeof(Header));
//                Console.WriteLine("Ok!");

//                Console.Write("Writing data... ");
//                for (Int32 i = 0; i < pHeader->Number; i++)
//                {
//                    Console.Write("\rWriting data... {0}%", i * 100 / pHeader->Number);

//                    using (FileStream Reader = new FileStream(Files[i].FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
//                    {
//                        Int32 Length = Reader.Read(Buffer, 0, Kernel.MAX_BUFFER_SIZE);
//                        while (Length > 0)
//                        {
//                            FStream.Write(Buffer, 0, Length);
//                            Length = Reader.Read(Buffer, 0, Kernel.MAX_BUFFER_SIZE);
//                        }
//                    }
//                }
//                Console.WriteLine("\b\b\bOk!");

//                Console.Write("Writing entries... ");
//                using (StreamWriter Writer = new StreamWriter(Destination.Replace(".wdf", ".lst"), false, Encoding.Default))
//                {
//                    for (Int32 i = 0; i < pHeader->Number; i++)
//                    {
//                        Console.Write("\rWriting entries... {0}%", i * 100 / pHeader->Number);

//                        Entry* pEntry = (Entry*)Kernel.malloc(sizeof(Entry));

//                        String InternalPath = Files[i].FullName.Replace(Environment.CurrentDirectory + "\\", "");
//                        UInt32 UID = String2ID(InternalPath);
//                        Writer.WriteLine("{0}={1}", UID, InternalPath);

//                        pEntry->UID = UID;
//                        pEntry->Offset = Offsets[i];
//                        pEntry->Size = (UInt32)Files[i].Length;
//                        pEntry->Space = 0;

//                        Kernel.memcpy(Buffer, pEntry, sizeof(Entry));
//                        FStream.Write(Buffer, 0, sizeof(Entry));
//                        Kernel.free(pEntry);
//                    }
//                }
//                Console.WriteLine("\b\b\bOk!");
//            }

//            Kernel.free(pHeader);
//        }

//        public static UInt32 String2ID(String Str)
//        {
//            //x86 - 32 bits - Registers
//            UInt32 eax, ebx, ecx, edx, edi, esi;
//            UInt64 num = 0;

//            UInt32 v;
//            Int32 i;
//            UInt32* m = (UInt32*)Kernel.calloc(sizeof(UInt32) * 0x046);
//            Byte* buffer = (Byte*)Kernel.calloc(sizeof(Byte) * 0x100);

//            Str = Str.ToLowerInvariant();
//            Str = Str.Replace('\\', '/');

//            for (i = 0; i < Str.Length; i++)
//                buffer[i] = (Byte)Str[i];

//            Int32 Length = (Str.Length % 4 == 0 ? Str.Length / 4 : Str.Length / 4 + 1);
//            for (i = 0; i < Length; i++)
//                m[i] = *(((UInt32*)buffer) + i);
//            m[i++] = 0x9BE74448;
//            m[i++] = 0x66F42C48;

//            v = 0xF4FA8928;

//            edi = 0x7758B42B;
//            esi = 0x37A8470E;

//            for (ecx = 0; ecx < i; ecx++)
//            {
//                ebx = 0x267B0B11;
//                v = (v << 1) | (v >> 0x1F);
//                ebx ^= v;
//                eax = m[ecx];
//                esi ^= eax;
//                edi ^= eax;
//                edx = ebx;
//                edx += edi;
//                edx |= 0x02040801;
//                edx &= 0xBFEF7FDF;
//                num = edx;
//                num *= esi;
//                eax = (UInt32)num;
//                edx = (UInt32)(num >> 0x20);
//                if (edx != 0)
//                    eax++;
//                num = eax;
//                num += edx;
//                eax = (UInt32)num;
//                if (((UInt32)(num >> 0x20)) != 0)
//                    eax++;
//                edx = ebx;
//                edx += esi;
//                edx |= 0x00804021;
//                edx &= 0x7DFEFBFF;
//                esi = eax;
//                num = edi;
//                num *= edx;
//                eax = (UInt32)num;
//                edx = (UInt32)(num >> 0x20);
//                num = edx;
//                num += edx;
//                edx = (UInt32)num;
//                if (((UInt32)(num >> 0x20)) != 0)
//                    eax++;
//                num = eax;
//                num += edx;
//                eax = (UInt32)num;
//                if (((UInt32)(num >> 0x20)) != 0)
//                    eax += 2;
//                edi = eax;
//            }
//            esi ^= edi;
//            v = esi;

//            Kernel.free(buffer);
//            Kernel.free(m);
//            return v;
//        }
//    }
//}
