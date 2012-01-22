// * ************************************************************
// * * START:                                       material.cs *
// * ************************************************************

// * ************************************************************
// *                      INFORMATIONS
// * ************************************************************
// * Material class for the library.
// * material.cs
// * 
// * --
// *
// * Feel free to use this class in your projects, but don't
// * remove the header to keep the paternity of the class.
// * 
// * ************************************************************
// *                      CREDITS
// * ************************************************************
// * Originally created by CptSky (January 22th, 2012)
// * Copyright (C) 2012 CptSky
// *
// * ************************************************************

using System;

namespace CO2_CORE_DLL.IO
{
    /// <summary>
    /// Material (Wrapper)
    /// </summary>
    public unsafe class Material : DBC.MATR
    {
        /// <summary>
        /// Create a new Material instance to handle the TQ's Material file.
        /// </summary>
        public Material() : base() { }

        ~Material() { Clear(); }
    }
}

// * ************************************************************
// * * END:                                         material.cs *
// * ************************************************************