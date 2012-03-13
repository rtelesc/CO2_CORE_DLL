using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ComponentAce.Compression.Libs.zlib;

namespace CO2_CORE_DLL.IO
{
    /// <summary>
    /// NetDragon Data Package
    /// </summary>
    public partial class NetDragonDatPkg
    {
        private TPI Package = null;

        /// <summary>
        /// Create a new NetDragonDatPkg handle.
        /// </summary>
        public NetDragonDatPkg()
        {
            this.Package = new TPI();
        }

        ~NetDragonDatPkg()
        {
            Package.Close();
            Package = null;
        }

        /// <summary>
        /// Open the specified NetDragonDatPkg package. (TPI/TPD)
        /// </summary>
        public void Open(String Source)
        {
            Source = Source.ToLower().Replace(".tpd", ".tpi");
            Package.Open(Source);
        }

        /// <summary>
        /// Close the file, reset the dictionary and free all the used memory.
        /// </summary>
        public void Close() { Package.Close(); }

        /// <summary>
        /// Extract all files contained in the package in the folder pointed by the destination path.
        /// </summary>
        public void ExtractAll(String Destination) { Package.ExtractAll(Destination); }

        /// <summary>
        /// Pack the folder pointed by the path (source) in a package pointed by the other path (destination).
        /// </summary>
        public static void Pack(String Source, String Destination) { TPI.Pack(Source, Destination); }
    }
}