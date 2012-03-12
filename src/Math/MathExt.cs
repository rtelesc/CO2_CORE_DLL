using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CO2_CORE_DLL
{
    public unsafe partial class MathExt
    {
        /// <summary>
        /// Random instance. It should not be used directly, but throught the Generate function that is thread-safe implemented.
        /// </summary>
        private static Random Rand = new Random(Environment.TickCount);

        /// <summary>
        /// Generate a number in a specified range. (Number ∈ [Min, Max])
        /// This function is thread-safe, comparatively to the Random class.
        /// </summary>
        public static Int32 Generate(Int32 Min, Int32 Max)
        {
            if (Max != Int32.MaxValue)
                Max++;

            Int32 Value = 0;
            lock (Rand) { Value = Rand.Next(Min, Max); }
            return Value;
        }

        /// <summary>
        /// Generate a number in a the specific range: ∈ [0.0, 1.0]
        /// This function is thread-safe, comparatively to the Random class.
        /// </summary>
        public static Double Generate() { lock (Rand) { return Rand.NextDouble(); } }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// This function is thread-safe, comparatively to the Random class.
        /// </summary>
        public static void Generate(ref Byte[] Bytes) { lock (Rand) { Rand.NextBytes(Bytes); } }

        /// <summary>
        /// Simulate a success rate.
        /// </summary>
        public static Boolean Success(Double Chance) { return ((Double)Generate(0x01, 0xF4240)) / (Double)0x2710 >= 100.0 - Chance; }
    }
}