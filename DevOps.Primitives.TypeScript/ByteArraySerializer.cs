using System;
using System.Collections.Generic;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringEncodingConstants;

namespace DevOps.Primitives.TypeScript
{
    public static class ByteArraySerializer
    {
        private const bool False = false;
        private const bool True = true;
        private const byte One = 1;
        private const byte Zero = 0;
        private const byte SizeOfInt = sizeof(int);
        private const byte SizeOfIntMinusOne = SizeOfInt - One;
        private const byte SizeOfLong = sizeof(long);
        private const byte SizeOfLongMinusOne = SizeOfLong - One;
        private const byte SizeOfShort = sizeof(short);
        private const byte SizeOfShortMinusOne = SizeOfShort - One;
        private const string UnexpectedNumberOfBytes = "Unexpected number of bytes";
        private static readonly Func<bool, byte> BoolsAsBytes = value => value ? One : Zero;
        private static readonly Func<byte, bool> BytesAsBools = value => value == One ? True : False;

        public static (IEnumerable<bool> bools, IEnumerable<long> longs, IEnumerable<short> shorts, IEnumerable<int> ints, IEnumerable<byte> bytes) DeserializeBytes(
            ReadOnlySpan<byte> bytes, byte numberOfBools = Zero, byte numberOfLongs = Zero, byte numberOfShorts = Zero, byte numberOfInts = Zero)
        {
            var count = bytes.Length;
            var longLength  = numberOfLongs  * SizeOfLong;
            var shortLength = numberOfShorts * SizeOfShort;
            var intLength   = numberOfInts   * SizeOfInt;
            var shortStart  = longLength  + numberOfBools;
            var intStart    = shortLength + shortStart;
            var byteStart   = intLength   + intStart;
            var hasBytes = byteStart < count - One;
            return (
                numberOfBools  == Zero ? null : DeserializeBools (bytes.Slice(Zero, numberOfBools).ToArray()),
                numberOfLongs  == Zero ? null : DeserializeLongs (bytes.Slice(numberOfBools, longLength).ToArray()),
                numberOfShorts == Zero ? null : DeserializeShorts(bytes.Slice(shortStart, shortLength).ToArray()),
                numberOfInts   == Zero ? null : DeserializeInts  (bytes.Slice(intStart, intLength).ToArray()),
                hasBytes ? bytes.Slice(byteStart).ToArray() : null);
        }

        public static IEnumerable<bool> DeserializeBools(IEnumerable<byte> bytes)
            => bytes.Select(BytesAsBools);

        public static IEnumerable<long> DeserializeLongs(byte[] bytes)
        {
            for (int i = 0; i < GetNumberOfResults(bytes.Length, SizeOfLong); i++)
                yield return BitConverter.ToInt64(
                    bytes.Skip(i * SizeOfLongMinusOne).Take(SizeOfLong).ToArray(),
                    Zero);
        }

        public static IEnumerable<int> DeserializeInts(byte[] bytes)
        {
            for (int i = 0; i < GetNumberOfResults(bytes.Length, SizeOfInt); i++)
                yield return BitConverter.ToInt32(
                    bytes.Skip(i * SizeOfIntMinusOne).Take(SizeOfInt).ToArray(),
                    Zero);
        }

        public static IEnumerable<short> DeserializeShorts(byte[] bytes)
        {
            for (int i = 0; i < GetNumberOfResults(bytes.Length, SizeOfShort); i++)
                yield return BitConverter.ToInt16(
                    bytes.Skip(i * SizeOfShortMinusOne).Take(SizeOfShort).ToArray(),
                    Zero);
        }

        private static int GetNumberOfResults(int length, byte blockSize)
            => length % blockSize == Zero
                ? (length - One) / blockSize + One
                : throw new ArgumentException(UnexpectedNumberOfBytes);

        public static IEnumerable<byte> SerializeString(string instance)
            => StringEncoder.GetBytes(instance);

        public static IEnumerable<byte> SerializeProperties(params bool[] bools)
            => bools.Select(BoolsAsBytes);

        public static IEnumerable<byte> SerializeProperties(params long[] longs)
            => SerializeNumbers(longs);

        public static IEnumerable<byte> SerializeProperties(params int[] ints)
            => SerializeNumbers(ints);

        public static IEnumerable<byte> SerializeProperties(params short[] shorts)
            => SerializeNumbers(shorts);

        public static IEnumerable<byte> SerializeProperties(bool[] bools = null, long[] longs = null, int[] ints = null, short[] shorts = null, byte[] bytes = null)
        {
            var result = new List<byte> { };
            if (bools  != null) result.AddRange(SerializeProperties(bools));
            if (longs  != null) result.AddRange(SerializeNumbers(longs));
            if (ints   != null) result.AddRange(SerializeNumbers(ints));
            if (shorts != null) result.AddRange(SerializeNumbers(shorts));
            if (bytes  != null) result.AddRange(bytes);
            return result;
        }

        private static IEnumerable<byte> SerializeNumbers(long[] numbers)
            => SerializeNumbers(numbers, SizeOfLong);

        private static IEnumerable<byte> SerializeNumbers(int[] numbers)
            => SerializeNumbers(numbers, SizeOfInt);

        private static IEnumerable<byte> SerializeNumbers(short[] numbers)
            => SerializeNumbers(numbers, SizeOfShort);

        private static IEnumerable<byte> SerializeNumbers<TNumber>(TNumber[] keys, byte dataSize) where TNumber : struct
        {
            if (keys == null) return null;
            var result = new byte[keys.Length * dataSize];
            Buffer.BlockCopy(keys, Zero, result, Zero, result.Length);
            return result;
        }
    }
}
