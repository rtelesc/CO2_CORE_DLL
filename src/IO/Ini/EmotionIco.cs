// * ************************************************************
// * * START:                                     emotionico.cs *
// * ************************************************************

// * ************************************************************
// *                      INFORMATIONS
// * ************************************************************
// * EmotionIco class for the library.
// * emotionico.cs
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
    /// EmotionIco (Wrapper)
    /// </summary>
    public unsafe class EmotionIco : DBC.EMOI
    {
        /// <summary>
        /// Create a new EmotionIco instance to handle the TQ's EmotionIco file.
        /// </summary>
        public EmotionIco() : base() { }

        ~EmotionIco() { Clear(); }
    }
}

// * ************************************************************
// * * END:                                       emotionico.cs *
// * ************************************************************