﻿using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using NIST.CVP.ACVTS.Libraries.Math.Exceptions;
using NIST.CVP.ACVTS.Libraries.Math.Helpers;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Math.Tests
{
    [TestFixture, UnitTest]
    public class BitStringTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(16000)]
        public void ShouldCreateInstanceWithLength(int length)
        {
            BitString subject = new BitString(length);
            Assert.AreEqual(length, subject.BitLength);
        }

        [Test]
        [TestCase("test1", new byte[] { 1 })]
        [TestCase("test2", new byte[] { 1, 2, 3, 4 })]
        [TestCase("test3", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase("test4", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        public void ShouldCreateInstanceWithByteArray(string label, byte[] bytes)
        {
            // Act
            BitString subject = new BitString(bytes);
            var results = subject.ToBytes();

            // Assert
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(bytes[i], results[i]);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        [TestCase(256)]
        [TestCase(1500)]
        [TestCase(int.MaxValue)]
        public void ShouldCreateInstanceWithByteArrayAndExpectedValue(int testInt)
        {
            var bytes = BitConverter.GetBytes(testInt).Reverse().ToArray();

            var subject = new BitString(bytes);
            Assert.AreEqual(new BigInteger(testInt), subject.ToBigInteger());
        }

        [Test]
        public void ShouldCreateInstanceWithBitArray()
        {
            // Arrange
            bool[] bits = { true, false, true, true };

            // Act
            BitString subject = new BitString(new BitArray(bits));

            // Assert
            for (int i = 0; i < subject.BitLength; i++)
            {
                Assert.AreEqual(bits[i], subject.Bits[i]);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        [TestCase(256)]
        [TestCase(1500)]
        [TestCase(int.MaxValue)]
        public void ShouldCreateInstanceWithBitArrayAndExpectedValue(int testInt)
        {
            // Arrange
            var bytesInMSB = BitConverter.GetBytes(testInt).Reverse().ToArray();
            BigInteger expectedBi = new BigInteger(testInt);

            // Act
            BitString subject = new BitString(bytesInMSB);
            var result = subject.ToBigInteger();

            // Assert
            Assert.AreEqual(expectedBi, result);
        }

        [Test]
        [TestCase(1)]
        [TestCase(255)]
        [TestCase(256)]
        [TestCase(1500)]
        [TestCase(int.MaxValue)]
        public void ShouldCreateInstanceWithBigInteger(int testInt)
        {
            // Arrange
            var testBigInt = new BigInteger(testInt);

            // Act
            BitString subject = new BitString(testBigInt);
            var resultBigInt = subject.ToPositiveBigInteger();

            // Assert
            Assert.AreEqual(testBigInt, resultBigInt);
        }

        [Test]
        public void ShouldCreateInstanceWithBigIntegerAndSetLength()
        {
            // Arrange
            byte[] bytes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            BigInteger bi = new BigInteger(bytes);
            int bitsToAByte = 8;
            int totalBits = bytes.Length * bitsToAByte;
            int setBitLengthTo = totalBits + bitsToAByte; // one additional byte over what's provided in byte array

            // Act
            BitString subject = new BitString(bi, setBitLengthTo);

            // Assert
            Assert.AreEqual(setBitLengthTo, subject.Bits.Length, $"Resulting bits length should be {setBitLengthTo}");
            Assert.AreEqual(bi, subject.ToBigInteger());
        }

        [Test]
        public void ShouldAddFalseBitsBeforeBigInt()
        {
            // Arrange
            byte[] bytes = { 1 };
            BigInteger bi = new BigInteger(bytes);
            int bitsToAByte = 8;
            int totalBits = bytes.Length * bitsToAByte;
            int setBitLengthTo = totalBits + bitsToAByte; // one additional byte over what's provided in byte array

            // Act
            BitString subject = new BitString(bi, setBitLengthTo);
            var results = subject.ToBytes();

            // Assert
            Assert.That(results.Length == 2);
            Assert.IsTrue(results[0] == 0);
            Assert.IsTrue(results[1] == 1);
        }

        [Test]
        [TestCase("527545222104387855173243238699327730047054175268400693537830017395674818449082107960567566340098847877132585460096692096553839137055417",
            451, "00B9CE8A63964C06E12E311DAFEF08BF936D0805487D700612A0C5C2A4462AFAFA75EAFFE68F51EA2C29E909DA802E321A79C8D62EEDC6E2B9")]
        [TestCase("741109383330151771707690665109313374760543",
            147, "000881ECBB5799BFF3A99A34391CE9C2E9AE5F")]
        public void ShouldProduceSameHexIfMSByteIs0AndBooleanFlagged(string bigIntDec, int bitLength, string hex)
        {
            // Arrange
            var bigint = BigInteger.Parse(bigIntDec);

            // Act
            var bs = new BitString(bigint, bitLength % 8 == 0 ? bitLength : bitLength + 8 - bitLength % 8, false);

            // Assert
            Assert.AreEqual(bigint, bs.ToPositiveBigInteger());
            Assert.AreEqual(hex, bs.ToHex());
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ShouldCreateEmptyBitStringIfEmptyOrNullHexSupplied(string hexValue)
        {
            var subject = new BitString(hexValue);
            Assert.IsNotNull(subject);
            Assert.AreEqual(0, subject.BitLength);
        }

        [Test]
        // MSB
        [TestCase(1, "01")]
        [TestCase(10, "0A")]
        [TestCase(15, "0F")]
        [TestCase(1500, "05 DC")]
        [TestCase(int.MaxValue, "7F FF FF FF")]
        //[Test]
        //// LSB
        //[TestCase(1, "01")]
        //[TestCase(10, "0A")]
        //[TestCase(15, "0F")]
        //[TestCase(1500, "DC 05")]
        //[TestCase(int.MaxValue, "FF FF FF 7F")]
        public void ShouldCreateInstanceFromHexStringInt(int testExpectation, string hexValue)
        {
            // Arrange
            var expectedResult = new BigInteger(testExpectation);

            // Act
            var subject = new BitString(hexValue);
            var result = subject.ToBigInteger();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        // MSB
        [TestCase(1, "01")]
        [TestCase(10, "0A")]
        [TestCase(15, "0F")]
        [TestCase(1500, "05 DC")]
        [TestCase(int.MaxValue, "7F FF FF FF")]
        [TestCase(long.MaxValue, "7F FF FF FF FF FF FF FF")]
        //[Test]
        //// LSB
        //[TestCase(1, "01")]
        //[TestCase(10, "0A")]
        //[TestCase(15, "0F")]
        //[TestCase(1500, "DC 05")]
        //[TestCase(int.MaxValue, "FF FF FF 7F")]
        //[TestCase(long.MaxValue, "FF FF FF FF FF FF FF 7F")]
        public void ShouldCreateInstanceFromHexStringLong(long testExpectation, string hexValue)
        {
            // Arrange
            var expectedResult = new BigInteger(testExpectation);

            // Act
            var subject = new BitString(hexValue);
            var result = subject.ToBigInteger();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(5, "FA", "11111")]
        [TestCase(6, "FA", "111110")]
        [TestCase(7, "FA", "1111101")]
        [TestCase(8, "FA", "11111010")]
        [TestCase(-1, "FA", "11111010")]
        [TestCase(5, "FAFA", "11111")]
        [TestCase(6, "FAFA", "111110")]
        [TestCase(7, "FAFA", "1111101")]
        [TestCase(8, "FAFA", "11111010")]
        [TestCase(13, "FAFA", "1111101011111")]
        [TestCase(14, "FAFA", "11111010111110")]
        [TestCase(15, "FAFA", "111110101111101")]
        [TestCase(16, "FAFA", "1111101011111010")]
        [TestCase(-1, "FAFA", "1111101011111010")]
        public void ShouldTruncateFromLeastSignificantBits(int length, string hex, string expectedBitsString)
        {
            var chars = expectedBitsString.ToCharArray();
            Array.Reverse(chars);
            var reversedString = new string(chars);
            var bits = new BitString(MsbLsbConversionHelpers.GetBitArrayFromStringOf1sAnd0s(reversedString));

            var result = new BitString(hex, length);

            Assert.AreEqual(bits.ToHex(), result.ToHex());
        }

        [Test]
        [TestCase(1, "01", "1")]
        [TestCase(2, "02", "10")]
        [TestCase(2, "03", "11")]
        [TestCase(3, "05", "101")]
        [TestCase(3, "06", "110")]
        [TestCase(4, "07", "0111")]
        [TestCase(5, "13", "1 0011")]
        [TestCase(6, "34", "11 0100")]
        [TestCase(7, "73", "111 0011")]
        [TestCase(8, "AB", "1010 1011")]
        [TestCase(9, "CA01", "1100 1010 1")]
        [TestCase(9, "3400", "0011 0100 0")]
        [TestCase(10, "4902", "0100 1001 10")]
        [TestCase(10, "5603", "0101 0110 11")]
        [TestCase(10, "E301", "1110 0011 01")]
        [TestCase(15, "FA74", "1111 1010 111 0100")]
        [TestCase(16, "BEEF", "1011 1110 1110 1111")]
        [TestCase(17, "BEEF01", "1011 1110 1110 1111 1")]
        public void ShouldCreateInstanceFromHexWithReverseTruncation(int length, string hex, string expectedString)
        {
            var chars = expectedString.ToCharArray();
            Array.Reverse(chars);
            var reversedString = new string(chars);

            var bits = MsbLsbConversionHelpers.GetBitArrayFromStringOf1sAnd0s(reversedString);
            var expectedResult = new BitString(bits);

            var result = new BitString(hex, length, false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("00", false)]
        [TestCase("0", true)]
        [TestCase("FFFF", false)]
        [TestCase("FFF", true)]
        public void ShouldThrowWhenBitStringInvalidLength(string hex, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<InvalidBitStringLengthException>(() => new BitString(hex));
            }
            else
            {
                Assert.DoesNotThrow(() => new BitString(hex));
            }
        }

        [Test]
        public void ShouldReturnMostSignificantByteAtIndex0()
        {
            byte[] bytesInMSB = new byte[3] { 1, 2, 3 };
            BitString subject = new BitString(bytesInMSB);

            var result = subject[0];
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ShouldReturnLeastSignificantByteAtLastIndex()
        {
            byte[] bytesInMSB = new byte[3] { 1, 2, 3 };
            BitString subject = new BitString(bytesInMSB);

            var result = subject[2];
            Assert.AreEqual(3, result);
        }

        [Test]
        public void ShouldSetMostSignificantByteAt0Index()
        {
            BitString subject = new BitString(128);
            subject[0] = 255;

            var results = subject.ToBytes();
            Assert.AreEqual(255, results.First(), "index 0");
            Assert.AreEqual(0, results.Last(), "last index");
        }

        [Test]
        public void ShouldSetLeastSignificantByteAtLastIndex()
        {
            BitString subject = new BitString(128);
            subject[subject.BitLength / 8 - 1] = 255;

            var results = subject.ToBytes();
            Assert.AreEqual(255, results.Last(), "last index");
            Assert.AreEqual(0, results.First(), "index 0");
        }

        [Test]
        [TestCase("test1", new[] { true }, new[] { true })]
        [TestCase("test2", new[] { false }, new[] { false })]
        [TestCase("test3", new[] { false, true, true, true }, new[] { false, true, true, true })]
        public void EqualsMethodReturnsTrueForLikeBoolArrays(string label, bool[] workingArray, bool[] compareArray)
        {
            // Arrange
            BitArray workingBitArray = new BitArray(workingArray);
            BitArray compareBitArray = new BitArray(compareArray);

            BitString workingBs = new BitString(workingBitArray);
            BitString compareBs = new BitString(compareBitArray);

            // Act
            var results = workingBs.Equals(compareBs);

            // Assert
            Assert.IsTrue(results);
        }

        [Test]
        public void EqualsMethodReturnsFalseWhenCompareObjectIsntAppropriateType()
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { true }));
            int foo = 5;

            // Act
            var results = bs.Equals(foo);

            // Assert
            Assert.IsFalse(results);
        }

        [Test]
        [TestCase("test1", new[] { true }, new[] { true, true })]
        public void EqualsMethodReturnsFalseWhenArraysAreOfDifferentLength(string label, bool[] workingArray, bool[] compareArray)
        {
            // Arrange
            BitArray workingBitArray = new BitArray(workingArray);
            BitArray compareBitArray = new BitArray(compareArray);

            BitString workingBs = new BitString(workingBitArray);
            BitString compareBs = new BitString(compareBitArray);

            // Act
            var results = workingBs.Equals(compareBs);

            // Assert
            Assert.IsFalse(results);
        }

        [Test]
        [TestCase("test1", new[] { true }, new[] { false })]
        [TestCase("test2", new[] { true, true, true }, new[] { true, false, true })]
        public void EqualsMethodReturnsFalseWhenArraysAreOfSimilarLengthDifferingValues(string label, bool[] workingArray, bool[] compareArray)
        {
            // Arrange
            BitArray workingBitArray = new BitArray(workingArray);
            BitArray compareBitArray = new BitArray(compareArray);

            BitString workingBs = new BitString(workingBitArray);
            BitString compareBs = new BitString(compareBitArray);

            // Act
            var results = workingBs.Equals(compareBs);

            // Assert
            Assert.IsFalse(results);
        }

        //[Test]
        //// MSb
        //// less than one byte
        //[TestCase(new bool[] { true }, 1)]
        //[TestCase(new bool[] { true, false, false }, 4)]
        //[TestCase(new bool[] { true, false, false, false }, 8)]
        //[TestCase(new bool[] { true, false, false, false, true }, 17)]
        //// one byte
        //[TestCase(new bool[] { true, false, false, false, false, false, false, false }, 128)]
        //// one byte plus some bits
        //[TestCase(new bool[] { true, false, true, true, true, false, true, true, true, false, false }, 1500)]
        //// two bytes
        //[TestCase(new bool[] { false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, true }, 16449)]
        //[TestCase(new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false }, 32896)]
        //// three bytes
        //[TestCase(new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false }, 8421504)]
        [Test]
        // LSb
        [TestCase(new[] { true }, 1)]
        [TestCase(new[] { false, false, true }, 4)]
        [TestCase(new[] { false, false, false, true }, 8)]
        [TestCase(new[] { true, false, false, false, true }, 17)]
        [TestCase(new[] { false, false, false, false, false, false, false, true }, 128)]
        [TestCase(new[] { true, false, false, false, false, false, true, false, false, false, false, false, false, false, true }, 16449)]
        [TestCase(new[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true }, 32896)]
        [TestCase(new[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true }, 8421504)]
        public void ToBytesConvertsBinaryToBytes(bool[] bits, int expectedResult)
        {
            // Arrange
            BitArray bitArray = new BitArray(bits);
            BitString bitString = new BitString(bitArray);

            var expectedBytes = BitConverter.GetBytes(expectedResult).Reverse().ToArray();
            int offsetDueToZeros = 0;
            for (int i = 0; i < expectedBytes.Length; i++)
            {
                if (expectedBytes[i] == 0)
                {
                    offsetDueToZeros = i;
                }
                else
                {
                    offsetDueToZeros++;
                    break;
                }
            }

            // Act
            var results = bitString.ToBytes();

            // Assert
            for (int i = 0; i < expectedBytes.Length - offsetDueToZeros; i++)
            {
                Assert.AreEqual(expectedBytes[i + offsetDueToZeros], results[i]);
            }
        }

        [Test]
        [TestCase(long.MinValue)]
        [TestCase(long.MinValue + 42)]
        [TestCase(long.MaxValue - 42)]
        [TestCase(long.MaxValue)]
        public void ToBytesLargerThanFourBytes(long largeNumber)
        {
            var bytes = BitConverter.GetBytes(largeNumber).Reverse().ToArray();

            BitString bitString = new BitString(bytes);

            var results = bitString.ToBytes();

            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(bytes[i], results[i]);
            }
        }

        [Test]
        // One Byte w/o 8 bits
        [TestCase("test1", new[] { true })]
        [TestCase("test2", new[] { false, false, true })]
        [TestCase("test3", new[] { false, false, false, true })]
        [TestCase("test4", new[] { true, false, false, false, true })]
        // One byte w/ 8 bits
        [TestCase("test5", new[] { false, false, false, false, false, false, false, true })]
        // Two bytes w/o 16 bits
        [TestCase("test6", new[] { true, false, false, false, false, false, true, false, false, false, false, false, false, false, true })]
        // Two bytes w/ 16 bits
        [TestCase("test7", new[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true })]
        // Three bytes
        [TestCase("test8", new[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true })]
        public void ToBytesReturnsBytesInReverseOrderWhenSpecified(string label, bool[] bits)
        {
            // Arrange
            BitArray bitArray = new BitArray(bits);
            BitString bitString = new BitString(bitArray);

            // Act
            var results = bitString.ToBytes();
            var resultsReverse = bitString.ToBytes(true);

            // Assert
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(results[i], resultsReverse[results.Length - 1 - i]);
            }
        }

        //[Test]
        //[TestCase(1, new byte[] { 1 })]
        //[TestCase(255, new byte[] { 255, 0 })]
        //[TestCase(256, new byte[] { 0, 1 })]
        //[TestCase(1500, new byte[] { 220, 5 })]
        //public void ToBytesShouldBeInLeastSignificantByteOrder(int valueToToBytes, byte[] expectedByteArrayOrder)
        //{
        //    // Arrange
        //    BitString bs = new BitString(new BigInteger(valueToToBytes));

        //    // Act
        //    var subject = bs.ToBytes();

        //    // Assert
        //    for (int i = 0; i < expectedByteArrayOrder.Length; i++)
        //    {
        //        Assert.AreEqual(expectedByteArrayOrder[i], subject[i]);
        //    }
        //}

        [Test]
        [TestCase(1, new byte[] { 1 })]
        [TestCase(256, new byte[] { 1, 0 })]
        [TestCase(1500, new byte[] { 5, 220 })]
        public void ToBytesShouldBeInMostSignificantByteOrder(int valueToToBytes, byte[] expectedByteArrayOrder)
        {
            // Arrange
            BitString bs = new BitString(new BigInteger(valueToToBytes));

            // Act
            var subject = bs.ToBytes();

            // Assert
            for (int i = 0; i < expectedByteArrayOrder.Length; i++)
            {
                Assert.AreEqual(expectedByteArrayOrder[i], subject[i]);
            }
        }

        [Test]
        public void ShouldReturnEmptyByteArrayWithEmptyBitString()
        {
            // Arrange
            BitString bs = new BitString(0);

            // Act
            var subject = bs.ToBytes();

            // Assert
            Assert.AreEqual(0, subject.Length);
        }

        [Test]
        [TestCase("test1", new[] { true })] // 1 bit
        [TestCase("test2", new[] { true, true, true, true })] // 4 bits
        [TestCase("test3", new[] { true, true, true, true, true, true, true, true })] // 8 bits
        public void ToStringShouldNotContainAnySpacesWhenEightBitsOrFewer(string label, bool[] bits)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(bits));

            // Act
            var result = bs.ToString();

            // Assert
            Assert.IsFalse(result.Contains(" "));
        }

        [Test]
        [TestCase("test1", new[] { true, true, true, true }, 0)] // 4 bits
        [TestCase("test2", new[] { true, true, true, true, true, true, true, true }, 0)] // 8 bits
        [TestCase("test3", new[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }, 1)] // 16 bits
        [TestCase("test4", new[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }, 2)] // 17 bits
        public void ToStringShouldContainOneSpaceForEachByteMinusOne(string label, bool[] bits, int numberOfSpaces)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(bits));

            // Act
            var result = bs.ToString();

            // Assert
            Assert.AreEqual(numberOfSpaces, result.Count(c => c == ' '));
        }

        [Test]
        // Single Byte
        [TestCase(1, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000001")]
        [TestCase(254, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 11111110")]
        [TestCase(255, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 11111111")]
        // Multiple bytes
        [TestCase(256, "00000000 00000000 00000000 00000000 00000000 00000000 00000001 00000000")]
        [TestCase(123456, "00000000 00000000 00000000 00000000 00000000 00000001 11100010 01000000")]
        [TestCase(123457, "00000000 00000000 00000000 00000000 00000000 00000001 11100010 01000001")]
        [TestCase(long.MaxValue, "01111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111")]
        public void ToStringShouldReturnABitRepresentationWithASpaceEveryEightBits(long toStringNumber, string expectedToString)
        {
            // Arrange
            var bytes = BitConverter.GetBytes(toStringNumber).Reverse().ToArray();

            BitString bitString = new BitString(bytes);

            // Act
            var results = bitString.ToString();

            // Assert
            Assert.AreEqual(expectedToString, results);
        }

        [Test]
        [TestCase(new[] { false }, "0")]
        [TestCase(new[] { false, false, true, true, false, true }, "101100")]
        [TestCase(new[] { true, false, false, true, false, false, false, true }, "10001001")]
        [TestCase(new[] { true, false, false, true, true, false, false, true, true, false }, "01100110 01")]
        [TestCase(new[] { false, true, false, true, true, false, true, true, true, true, false, false, true, true }, "11001111 011010")]
        [TestCase(new[] { false, false, false, true, true, true, true, true, false, true, true, false, true, false, true, true }, "11010110 11111000")]
        [TestCase(new[] { false, false, false, true, true, true, true, true, false, true, true, false, true, false, true, true, false }, "01101011 01111100 0")]
        public void ToStringShouldReturnCorrectMSBString(bool[] inputBits, string expectedResult)
        {
            var bits = new BitArray(inputBits);
            var subject = new BitString(bits);

            var result = subject.ToString();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void SetShouldReturnFalseWhenIndexGtLength()
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { false, false, false }));
            int length = bs.BitLength;

            // Act
            var result = bs.Set(length, true);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SetShouldReturnFalseWhenIndexLtZero()
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { false, false, false }));
            int length = bs.BitLength;

            // Act
            var result = bs.Set(-1, true);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SetShouldReturnTrueWhenIndexInRange()
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { false, false, false }));
            int length = bs.BitLength;

            // Act
            var result = bs.Set(length - 1, true);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(new[] { false, false, false }, new[] { false, false, true }, 2, true)]
        [TestCase(new[] { true, false, false }, new[] { false, false, false }, 0, false)]
        public void SetShouldChangeBitAtIndex(bool[] original, bool[] expected, int indexToSet, bool valueToUseInSet)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(original));

            // Act
            var result = bs.Set(indexToSet, valueToUseInSet);

            // Assert
            Assert.AreEqual(new BitString(new BitArray(expected)), bs);
        }

        [Test]
        [TestCase(1, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000001")]
        [TestCase(4, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000100")]
        [TestCase(128, "00000000 00000000 00000000 00000000 00000000 00000000 00000000 10000000")]
        [TestCase(1024, "00000000 00000000 00000000 00000000 00000000 00000000 00000100 00000000")]
        [TestCase(1500, "00000000 00000000 00000000 00000000 00000000 00000000 00000101 11011100")]
        [TestCase(int.MaxValue, "00000000 00000000 00000000 00000000 01111111 11111111 11111111 11111111")]
        public void To64BitStringReturnsIntAsBitStringWith64Bits(int numberToConvert, string toStringRepresentation)
        {
            var result = BitString.To64BitString(numberToConvert);
            Assert.AreEqual(toStringRepresentation, result.ToString());
        }

        [Test]
        [TestCase(
            "test1",
            new[] { false },
            new[] { true },
            new[] { true, false }
        )]
        [TestCase(
            "test2",
            new[] { false, false, false, true },
            new[] { false, true, true, false },
            new[] { false, true, true, false, false, false, false, true }
        )]
        [TestCase(
            "test3",
            new[] { false, false, false, false, false, true, false, false },
            new[] { true, true, true, true, false, true, true },
            new[] { true, true, true, true, false, true, true, false, false, false, false, false, true, false, false }
        )]
        public void ConcatenateBitsAppendsRightSideBitsToLeft(string label, bool[] leftSide, bool[] rightSide, bool[] expectedResult)
        {
            // Arrange
            BitString lsBs = new BitString(new BitArray(leftSide));
            BitString rsBs = new BitString(new BitArray(rightSide));

            // Act
            BitString results = BitString.ConcatenateBits(lsBs, rsBs);

            // Assert
            for (int i = 0; i < results.BitLength; i++)
            {
                Assert.AreEqual(expectedResult[i], results.Bits[i]);
            }
        }

        [Test]
        [TestCase(
            "test1",
            new[] { false },
            new[] { true },
            new[] { true, false }
        )]
        [TestCase(
            "test2",
            new[] { false, false, false, true },
            new[] { false, true, true, false },
            new[] { false, true, true, false, false, false, false, true }
        )]
        [TestCase(
            "test3",
            new[] { false, false, false, false, false, true, false, false },
            new[] { true, true, true, true, false, true, true },
            new[] { true, true, true, true, false, true, true, false, false, false, false, false, true, false, false }
        )]
        public void ConcatenateBitsToExistingBits(string label, bool[] leftSide, bool[] rightSide, bool[] expectedResult)
        {
            // Arrange
            BitString subject = new BitString(new BitArray(leftSide));
            BitString newBits = new BitString(new BitArray(rightSide));

            // Act
            BitString results = subject.ConcatenateBits(newBits);

            // Assert
            for (int i = 0; i < results.BitLength; i++)
            {
                Assert.AreEqual(expectedResult[i], results.Bits[i]);
            }
        }

        [Test]
        public void ShouldConcatenateBitsNumberOfTimes()
        {
            var baseBs = new BitString(8);
            var bitsToAppend = new BitString(new byte[] { 0x01 });
            var resultBs = baseBs.ConcatenateBits(bitsToAppend, 4);

            Assert.AreEqual("0001010101", resultBs.ToHex());
        }

        [Test]
        [TestCase(
                new[] { false, true, false, true, false, true, false, true },
                1,
                new[] { true }
                )]
        [TestCase(
                    new[] { false, true, false, true, false, true, false, true },
                    4,
                    new[] { false, true, false, true }
                )]
        [TestCase(
                    new[] { false, true, false, true, false, true, false, true },
                    5,
                    new[] { true, false, true, false, true }
                )]
        [TestCase(
                    new[] { false, true, false, true, false, true, false, true },
                    8,
                    new[] { false, true, false, true, false, true, false, true }
                )]
        public void StaticMostSignificantShouldReturnNumberOfDigitsSpecified(bool[] workingArray, int numberOfBits, bool[] expectedArray)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(workingArray));

            // Act
            var results = BitString.GetMostSignificantBits(numberOfBits, bs);

            // Assert
            Assert.AreEqual(expectedArray, results.Bits);
        }

        [Test]
        [TestCase(
        new[] { false, true, false, true, false, true, false, true },
        1,
        new[] { true }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            4,
            new[] { false, true, false, true }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            5,
            new[] { true, false, true, false, true }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            8,
            new[] { false, true, false, true, false, true, false, true }
        )]
        public void MostSignificantShouldReturnNumberOfDigitsSpecified(bool[] workingArray, int numberOfBits, bool[] expectedArray)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(workingArray));

            // Act
            var results = bs.GetMostSignificantBits(numberOfBits);

            // Assert
            Assert.AreEqual(expectedArray, results.Bits);
        }

        [Test]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            1,
            new[] { false }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            4,
            new[] { false, true, false, true }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            5,
            new[] { false, true, false, true, false }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            8,
            new[] { false, true, false, true, false, true, false, true }
        )]
        public void StaticLeastSignificantShouldReturnNumberOfDigitsSpecified(bool[] workingArray, int numberOfBits, bool[] expectedArray)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(workingArray));

            // Act
            var results = BitString.GetLeastSignificantBits(numberOfBits, bs);

            // Assert
            Assert.AreEqual(expectedArray, results.Bits);
        }

        [Test]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            1,
            new[] { false }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            4,
            new[] { false, true, false, true }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            5,
            new[] { false, true, false, true, false }
        )]
        [TestCase(
            new[] { false, true, false, true, false, true, false, true },
            8,
            new[] { false, true, false, true, false, true, false, true }
        )]
        public void LeastSignificantShouldReturnNumberOfDigitsSpecified(bool[] workingArray, int numberOfBits, bool[] expectedArray)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(workingArray));

            // Act
            var results = bs.GetLeastSignificantBits(numberOfBits);

            // Assert
            Assert.AreEqual(expectedArray, results.Bits);
        }

        [Test]
        [TestCase(new[] { true, false, true }, new[] { false }, 1, 1)]
        [TestCase(new[] { true, false, true }, new[] { true, false }, 0, 2)]
        [TestCase(new[] { true, false, true }, new[] { true, false, true }, 0, 3)]
        public void SubStringReturnsCorrectBitsWhenInvokedWithValidParameters(bool[] testBitString, bool[] expectedBitString, int startIndex, int numberOfBits)
        {
            // Arrange
            BitString testBs = new BitString(new BitArray(testBitString));
            BitString expectedBs = new BitString(new BitArray(expectedBitString));

            // Act
            var results = testBs.Substring(startIndex, numberOfBits);

            // Assert
            Assert.AreEqual(expectedBs, results);
        }

        [Test]
        public void StaticSubStringThrowsArgumentOutOfRangeExceptionWhenStartIndexLessThanZero()
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { true }));

            // Act / Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => BitString.Substring(bs, -1, 0));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void StaticSubStringThrowsArgumentOutOfRangeExceptionWhenStartIndexGreaterThanBitStringLength(int startIndex)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { true }));

            // Act / Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => BitString.Substring(bs, startIndex, 1));
        }

        [Test]
        [TestCase(2, 2)]
        [TestCase(1, 3)]
        [TestCase(0, 4)]
        public void StaticSubStringThrowsArgumentOutOfRangeExceptionWhenStartIndexPlusNumberOfBitsIsGreaterThanLength(int startIndex, int numberOfBits)
        {
            // Arrange
            BitString bs = new BitString(new BitArray(new[] { true, true, true }));

            // Act / Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => BitString.Substring(bs, startIndex, numberOfBits));
        }

        [Test]
        [TestCase(
            new[] { true, false, true },
            new[] { true, false },
            0,
            2
        )]
        [TestCase(
            new[] { false, false, false, true },
            new[] { true },
            3,
            1
        )]
        public void StaticSubStringReturnsCorrectBitsWhenInvokedWithValidParameters(bool[] testBitString, bool[] expectedBitString, int startIndex, int numberOfBits)
        {
            // Arrange
            BitString testBs = new BitString(new BitArray(testBitString));
            BitString expectedBs = new BitString(new BitArray(expectedBitString));

            // Act
            var results = BitString.Substring(testBs, startIndex, numberOfBits);

            // Assert
            Assert.AreEqual(expectedBs, results);
        }

        [Test]
        [TestCase("FF", 0, 4, "F0")]
        [TestCase("FF00", 0, 4, "F0")]
        [TestCase("000102030405060708090A0B0C0D0E0F", 0, 8, "00")]
        [TestCase("000102030405060708090A0B0C0D0E0F", 8, 8, "01")]
        [TestCase("000102030405060708090A0B0C0D0E0F", 8, 16, "0102")]
        public void ShouldMsbSubstringCorrectly(string inputHex, int startIndex, int numberOfBits, string expectedHex)
        {
            var input = new BitString(inputHex);
            var result = input.MSBSubstring(startIndex, numberOfBits);

            Assert.AreEqual(new BitString(expectedHex).ToHex(), result.ToHex());
        }

        [Test]
        [TestCase("FF", 4, "F0", 8)]
        [TestCase("FF", 8, "FF", 8)]
        [TestCase("FF", 7, "FE", 8)]
        public void ShouldPadToNextByteBoundryOrReturnOriginalIfAtByteBoundry(string hex, int length, string expectedHex, int expectedLength)
        {
            var hexBs = new BitString(hex, length);
            var expectedBs = new BitString(expectedHex);

            var result = BitString.PadToNextByteBoundry(hexBs);

            Assert.AreEqual(expectedLength, result.BitLength, nameof(expectedLength));
            Assert.AreEqual(expectedBs, result, nameof(expectedHex));
        }

        [Test]
        [TestCase(8, "FF", 4, true, "F0", 8)]
        [TestCase(8, "FF", 8, true, "FF", 8)]
        [TestCase(8, "FF", 7, true, "FE", 8)]
        [TestCase(16, "FF", 4, true, "F000", 16)]
        [TestCase(16, "FF", 8, true, "FF00", 16)]
        [TestCase(16, "FF", 7, true, "FE00", 16)]
        [TestCase(8, "FF", 4, false, "0F", 8)]
        [TestCase(8, "FF", 8, false, "FF", 8)]
        [TestCase(8, "FF", 7, false, "7F", 8)]
        [TestCase(16, "FF", 4, false, "000F", 16)]
        [TestCase(16, "FF", 8, false, "00FF", 16)]
        [TestCase(16, "FF", 7, false, "007F", 16)]
        public void ShouldPadToModulusBoundryOrReturnOriginalIfAtModulusBoundry(int modulus, string hex, int length, bool padLsb, string expectedHex, int expectedLength)
        {
            var hexBs = new BitString(hex, length);
            var expectedBs = new BitString(expectedHex);

            var result = BitString.PadToModulus(hexBs, modulus, padLsb);

            Assert.AreEqual(expectedLength, result.BitLength, nameof(expectedLength));
            Assert.AreEqual(expectedBs.ToHex(), result.ToHex(), nameof(expectedHex));
        }

        [TestCase(8, "FF", 4, "0F", 8)]
        [TestCase(8, "FF", 8, "FF", 8)]
        [TestCase(8, "FF", 7, "7F", 8)]
        [TestCase(16, "FF", 4, "000F", 16)]
        [TestCase(16, "FF", 8, "00FF", 16)]
        [TestCase(16, "FF", 7, "007F", 16)]
        public void ShouldPadToModulusMsbBoundryOrReturnOriginalIfAtModulusBoundry(int modulus, string hex, int length, string expectedHex, int expectedLength)
        {
            var hexBs = new BitString(hex, length);
            var expectedBs = new BitString(expectedHex);

            var result = BitString.PadToModulusMsb(hexBs, modulus);

            Assert.AreEqual(expectedLength, result.BitLength, nameof(expectedLength));
            Assert.AreEqual(expectedBs.ToHex(), result.ToHex(), nameof(expectedHex));
        }

        [Test]
        [TestCase(
            "test1",
            new[] { true, true, false },
            new[] { true, false, false },
            new[] { false, true, false }
        )]
        public void XORShouldReturnNewBitStringRepresentingXOROfTwoBitStrings(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedXorBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.XOR(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedXorBs, results);
        }

        //[Test]
        //[TestCase(
        //    new bool[] { true, true },
        //    new bool[] { true },
        //    new bool[] { true, false }
        //)]
        //[TestCase(
        //    new bool[] { true, false },
        //    new bool[] { true },
        //    new bool[] { true, true }
        //)]
        //[TestCase(
        //    new bool[] { false, true },
        //    new bool[] { true },
        //    new bool[] { false, false }
        //)]
        //[TestCase(
        //    new bool[] { true },
        //    new bool[] { true, false },
        //    new bool[] { true, true }
        //)]
        //[TestCase(
        //    new bool[] { true, false, true, false, true },
        //    new bool[] { false, true, true },
        //    new bool[] { true, false, true, true, false }
        //)]
        [Test]
        [TestCase(
            "test1",
            new[] { true, true },
            new[] { true },
            new[] { false, true }
        )]
        [TestCase(
            "test2",
            new[] { true, false },
            new[] { true },
            new[] { false, false }
        )]
        [TestCase(
            "test3",
            new[] { false, true },
            new[] { true },
            new[] { true, true }
        )]
        [TestCase(
            "test4",
            new[] { true },
            new[] { true, false },
            new[] { false, false }
        )]
        [TestCase(
            "test5",
            new[] { true, false, true, false, true },
            new[] { false, true, true },
            new[] { true, true, false, false, true }
        )]
        public void XORShouldPadZeroesForShorterBitStringAndReturnNewBitString(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedXorBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.XOR(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedXorBs, results);
        }

        [Test]
        [TestCase(
            "1234",
            "ABCD",
            "B9F9"
        )]
        [TestCase(
            "1234567890",
            "ABCDEABCDE",
            "B9F9BCC44E"
        )]
        [TestCase(
            "0001",
            "FFFFFFFE",
            "FFFFFFFF"
        )]
        public void XORShouldNotModifyParameters(string inputA, string inputB, string inputC)
        {
            // Arrange
            var originalA = new BitString(inputA);
            var originalB = new BitString(inputB);
            var expectedResult = new BitString(inputC);

            // Act
            var result = BitString.XOR(originalA, originalB);

            // Assert
            Assert.That(expectedResult.Equals(result));
            Assert.AreEqual(originalA, new BitString(inputA));
            Assert.AreEqual(originalB, new BitString(inputB));
        }

        [Test]
        [TestCase(
            "test1",
            new[] { true, true, true },
            new[] { false, false, false },
            new[] { true, true, true }
        )]
        [TestCase(
            "test2",
            new[] { true, false, true, false },
            new[] { false, false, true, true },
            new[] { true, false, true, true }
        )]
        [TestCase(
            "test3",
            new[] { false, false, true, false, false },
            new[] { false, false, false, true, true },
            new[] { false, false, true, true, true }
        )]
        public void ORShouldReturnNewBitStringWithOrOfTwoBitStrings(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedOrBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.OR(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedOrBs, results);
        }

        [Test]
        [TestCase(
            "test1",
            new[] { true, true, true },
            new[] { false },
            new[] { true, true, true }
        )]
        [TestCase(
            "test2",
            new[] { true, false, true, false },
            new[] { false, false, true },
            new[] { true, false, true, false }
        )]
        [TestCase(
            "test3",
            new[] { false, false, true, false, false },
            new[] { false, false, false },
            new[] { false, false, true, false, false }
        )]
        public void ORShouldPadShorterWithZeroesAndReturnNewBitString(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedOrBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.OR(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedOrBs, results);
        }

        [Test]
        [TestCase(
            "1234",
            "ABCD",
            "BBFD"
        )]
        [TestCase(
            "1234567890",
            "ABCDEABCDE",
            "BBFDFEFCDE"
        )]
        [TestCase(
            "0001",
            "FFFFFFFE",
            "FFFFFFFF"
        )]
        public void ORShouldNotModifyParameters(string inputA, string inputB, string inputC)
        {
            // Arrange
            var originalA = new BitString(inputA);
            var originalB = new BitString(inputB);
            var expectedResult = new BitString(inputC);

            // Act
            var result = BitString.OR(originalA, originalB);

            // Assert
            Assert.That(expectedResult.Equals(result));
            Assert.AreEqual(originalA, new BitString(inputA));
            Assert.AreEqual(originalB, new BitString(inputB));
        }

        [Test]
        [TestCase(
            "test1",
            new[] { true, true, true },
            new[] { false, false, false },
            new[] { false, false, false }
        )]
        [TestCase(
            "test2",
            new[] { true, false, true, false },
            new[] { false, false, true, true },
            new[] { false, false, true, false }
        )]
        [TestCase(
            "test3",
            new[] { false, false, true, true, true },
            new[] { false, false, false, true, true },
            new[] { false, false, false, true, true }
        )]
        public void ANDShouldReturnNewBitStringWithAndOfTwoBitStrings(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedAndBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.AND(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedAndBs, results);
        }

        [Test]
        [TestCase(
            "test1",
            new[] { true, true, true },
            new[] { false },
            new[] { false, false, false }
        )]
        [TestCase(
            "test2",
            new[] { true, false, true, false },
            new[] { false, false, true },
            new[] { false, false, true, false }
        )]
        [TestCase(
            "test3",
            new[] { true, false, true, true, false },
            new[] { true, false, true },
            new[] { true, false, true, false, false }
        )]
        public void ANDShouldPadShorterWithZeroesAndReturnNewBitString(string label, bool[] inputA, bool[] inputB, bool[] expectedResult)
        {
            // Arrange
            BitString bsA = new BitString(new BitArray(inputA));
            BitString bsB = new BitString(new BitArray(inputB));
            BitString expectedAndBs = new BitString(new BitArray(expectedResult));

            // Act
            var results = BitString.AND(bsA, bsB);

            // Assert
            Assert.AreEqual(expectedAndBs, results);
        }

        [Test]
        [TestCase(
            "1234",
            "ABCD",
            "0204"
        )]
        [TestCase(
            "1234567890",
            "ABCDEABCDE",
            "0204423890"
        )]
        [TestCase(
            "0707",
            "EEEEEEEE",
            "00000606"
        )]
        public void ANDShouldNotModifyParameters(string inputA, string inputB, string inputC)
        {
            // Arrange
            var originalA = new BitString(inputA);
            var originalB = new BitString(inputB);
            var expectedResult = new BitString(inputC);

            // Act
            var result = BitString.AND(originalA, originalB);

            // Assert
            Assert.That(expectedResult.Equals(result));
            Assert.AreEqual(originalA, new BitString(inputA));
            Assert.AreEqual(originalB, new BitString(inputB));
        }

        [Test]
        [TestCase(1, 1, 2)]
        [TestCase(5, 5, 10)]
        [TestCase(10000, 1, 10001)]
        [TestCase(255, 1, 256)]
        public void BitStringAdditionShouldProperlyAddBitStrings(int num1, int num2, int expectation)
        {
            BigInteger num1Bi = new BigInteger(num1);
            BigInteger num1B2 = new BigInteger(num2);
            BigInteger expectationBigInteger = new BigInteger(expectation);

            BitString num1bs = new BitString(num1Bi);
            BitString num2bs = new BitString(num1B2);

            var result = BitString.BitStringAddition(num1bs, num2bs).ToBigInteger();

            Assert.AreEqual(expectationBigInteger, result);
        }

        [Test]
        [TestCase("1")]
        [TestCase("1111")]
        public void BitStringAdditionShouldAddABitToAll1BitsDueToCarry(string testBitString)
        {
            BitString bs = new BitString(MsbLsbConversionHelpers.GetBitArrayFromStringOf1sAnd0s(testBitString));

            Assert.AreEqual(testBitString.Length, bs.BitLength, "sanity check");

            var subject = bs.BitStringAddition(BitString.One());

            Assert.AreEqual(testBitString.Length + 1, subject.BitLength, "should be equal");
        }

        // Make all of these actually correct and test this method...
        [Test]
        [TestCase(
            "ABCD",
            "2222",
            16,
            "CDEF"
        )]
        [TestCase(
            "1111",
            "3333",
            8,
            "44"
        )]
        [TestCase(
            "ABCDEF",
            "FEDCBA",
            24,
            "AAAAA9"
        )]
        public void AddWithModuloShouldAddProperly(string inputA, string inputB, int modulo, string inputResult)
        {
            // Arrange
            var leftBS = new BitString(inputA);
            var rightBS = new BitString(inputB);
            var expectedResult = new BitString(inputResult);

            // Act
            var result = BitString.AddWithModulo(leftBS, rightBS, modulo);

            // Assert
            Assert.AreEqual(expectedResult.ToHex(), result.ToHex());
        }

        [Test]
        [TestCase(
            "111111",
            "FFFFFF",
            24,
            "111110"
        )]
        [TestCase(
            "ABCD",
            "2222",
            16,
            "CDEF"
        )]
        [TestCase(
            "1111",
            "3333",
            8,
            "44"
        )]
        [TestCase(
            "ABCDEF",
            "FEDCBA",
            24,
            "AAAAA9"
        )]
        public void AddWithModuloShouldNotModifyParameters(string inputA, string inputB, int modulo, string inputResult)
        {
            // Arrange
            var originalLeft = new BitString(inputA);
            var originalRight = new BitString(inputB);
            var expectedResult = new BitString(inputResult);

            // Act
            var result = BitString.AddWithModulo(originalLeft, originalRight, modulo);

            // Assert
            Assert.That(expectedResult.Equals(result));
            Assert.AreEqual(originalLeft, new BitString(inputA));
            Assert.AreEqual(originalRight, new BitString(inputB));
        }

        [Test]
        [TestCase(
            new[] { true, false, true, false, true, false },
            1,
            new[] { false, true, false, true, false, true }
        )]
        [TestCase(
            new[] { false, false, false, true, true, false },
            4,
            new[] { false, true, true, false, false, false }
        )]
        [TestCase(
            new[] { false, false, true, false, true, false },
            8,
            new[] { true, false, false, false, true, false }
        )]
        [TestCase(
            new[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            3,
            new[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        )]
        public void MSBRotateShouldShiftBitsAndReturnNewBitString(bool[] bits, int distance, bool[] expectedBits)
        {
            // Arrange
            var inputString = new BitString(new BitArray(bits));
            var expectedResult = new BitString(new BitArray(expectedBits));

            // Act
            var results = BitString.MSBRotate(inputString, distance);

            // Assert
            Assert.AreEqual(expectedResult, results);
        }

        [Test]
        [TestCase("00 40 90 00 00 D0 4C 00", 1, "00 81 20 00 01 A0 98 00")]
        [TestCase("00 4C D0 00 00 90 40 00", 1, "00 99 A0 00 01 20 80 00")]
        public void MSBRotateShouldShiftHexBitStrings(string inputHex, int distance, string outputHex)
        {
            var inputString = new BitString(inputHex);
            var expectedResult = new BitString(outputHex);

            var result = BitString.MSBRotate(inputString, distance);

            Assert.AreEqual(expectedResult.ToHex(), result.ToHex());
        }

        [Test]
        [TestCase(
            new[] { true, false, true, false, true, false },
            1,
            new[] { false, true, false, true, false, true }
        )]
        [TestCase(
            new[] { false, false, false, true, true, false },
            4,
            new[] { false, true, true, false, false, false }
        )]
        [TestCase(
            new[] { false, false, true, false, true, false },
            8,
            new[] { true, false, false, false, true, false }
        )]
        public void MSBRotateShouldNotModifyParameters(bool[] bits, int distance, bool[] expectedBits)
        {
            // Arrange
            var originalString = new BitString(new BitArray(bits));
            var expectedResult = new BitString(new BitArray(expectedBits));

            // Act
            var results = BitString.MSBRotate(originalString, distance);

            // Assert
            Assert.That(expectedResult.Equals(results));
            Assert.AreEqual(originalString, new BitString(new BitArray(bits)));
        }

        [Test]
        [TestCase(
            new[] { true, false, true, false, true, false },
            1,
            new[] { false, true, false, true, false, true }
        )]
        [TestCase(
            new[] { false, false, false, true, true, false },
            4,
            new[] { true, false, false, false, false, true }
        )]
        [TestCase(
            new[] { false, false, true, false, true, false },
            8,
            new[] { true, false, true, false, false, false }
        )]
        public void LSBRotateShouldShiftBitsAndReturnNewBitString(bool[] bits, int distance, bool[] expectedBits)
        {
            // Arrange
            var inputString = new BitString(new BitArray(bits));
            var expectedResult = new BitString(new BitArray(expectedBits));

            // Act
            var results = BitString.LSBRotate(inputString, distance);

            // Assert
            Assert.AreEqual(expectedResult, results);
        }

        [Test]
        [TestCase(
            new[] { true, false, true, false, true, false },
            1,
            new[] { false, true, false, true, false, true }
        )]
        [TestCase(
            new[] { false, false, false, true, true, false },
            4,
            new[] { true, false, false, false, false, true }
        )]
        [TestCase(
            new[] { false, false, true, false, true, false },
            8,
            new[] { true, false, true, false, false, false }
        )]
        public void LSBRotateShouldNotModifyParameters(bool[] bits, int distance, bool[] expectedBits)
        {
            // Arrange
            var originalString = new BitString(new BitArray(bits));
            var expectedResult = new BitString(new BitArray(expectedBits));

            // Act
            var results = BitString.LSBRotate(originalString, distance);

            // Assert
            Assert.That(expectedResult.Equals(results));
            Assert.AreEqual(originalString, new BitString(new BitArray(bits)));
        }

        [Test]
        [TestCase("FF", "FE")]
        [TestCase("F00F", "E01E")]
        public void LSBShiftShouldShiftBitsAndReturnNewBitString(string hexToShift, string expectedHex)
        {
            // Arrange
            var inputString = new BitString(hexToShift);
            var expectedResult = new BitString(expectedHex);

            // Act
            var results = BitString.LSBShift(inputString);

            // Assert
            Assert.AreEqual(expectedResult, results);
        }

        [Test]
        [TestCase("FF", "FE")]
        [TestCase("F00F", "E01E")]
        public void LSBShiftShouldNotModifyParameters(string hexToShift, string expectedHex)
        {
            // Arrange
            var originalString = new BitString(hexToShift);
            var expectedResult = new BitString(expectedHex);

            // Act
            var results = BitString.LSBShift(originalString);

            // Assert
            Assert.That(expectedResult.Equals(results));
            Assert.AreEqual(originalString, new BitString(hexToShift));
        }

        [Test]
        [TestCase(1)]
        [TestCase(250)]
        [TestCase(1024)]
        [TestCase(int.MaxValue)]
        public void ToBigIntegerIntReturnsBigIntegerBasedOnBytes(int testInt)
        {
            // Arrange
            var bytes = BitConverter.GetBytes(testInt).Reverse().ToArray();

            BitString bs = new BitString(bytes);
            BigInteger expectedBigInt = new BigInteger(testInt);

            // Act
            var result = bs.ToBigInteger();

            // Assert
            Assert.AreEqual(expectedBigInt, result);
        }

        [Test]
        [TestCase(1)]
        [TestCase(250)]
        [TestCase(1024)]
        [TestCase(int.MaxValue)]
        [TestCase(long.MaxValue)]
        public void ToBigIntegerLongReturnsBigIntegerBasedOnBytes(long testLong)
        {
            // Arrange
            var bytes = BitConverter.GetBytes(testLong).Reverse().ToArray();

            BitString bs = new BitString(bytes);
            BigInteger expectedBigInt = new BigInteger(testLong);

            // Act
            var result = bs.ToBigInteger();

            // Assert
            Assert.AreEqual(expectedBigInt, result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(1231245)]
        [TestCase(999999)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void ToPositiveBigIntegerReturnsCorrectPositiveValue(int testInt)
        {
            var bytes = BitConverter.GetBytes(testInt).Reverse().ToArray();
            var bs = new BitString(bytes);
            var expectedBigInt = BigInteger.Abs(new BigInteger(testInt));

            var result = bs.ToPositiveBigInteger();

            Assert.GreaterOrEqual(result, (BigInteger)0);       // Verify result > 0
            Assert.AreEqual(expectedBigInt, result);
        }

        [Test]
        [TestCase("FF", "FF")]
        [TestCase("00FF", "FF")]
        public void ToPositiveBigIntegerFromHexReturnsCorrectValue(string hex, string expectedHex)
        {
            var bigInt = new BitString(hex).ToPositiveBigInteger();
            Assert.AreEqual(new BitString(bigInt), new BitString(expectedHex));
        }

        [Test]
        [TestCase("01")]
        [TestCase("0A")]
        [TestCase("0F")]
        [TestCase("05DC")]
        [TestCase("7FFFFFFF")]
        public void ToHexShouldReturnSameHexStringFromConstructor(string expectedHexString)
        {
            var bs = new BitString(expectedHexString);
            var result = bs.ToHex();
            Assert.AreEqual(expectedHexString, result);
        }

        [Test]
        [TestCase(1, "01")]
        [TestCase(10, "0A")]
        [TestCase(15, "0F")]
        [TestCase(1500, "05DC")]
        [TestCase(int.MaxValue, "7FFFFFFF")]
        public void ToHexShouldReturnHexStringOfBitString(int testInt, string expectedHex)
        {
            // Arrange
            var testBigInt = new BigInteger(testInt);
            var bs = new BitString(testBigInt);

            // Act
            var results = bs.ToHex();

            // Assert
            // Note MSB
            Assert.AreEqual(expectedHex, results);
        }

        [Test]
        [TestCase("F0", 4, "F0")]
        public void IncompleteByteShouldPadLessSignificantBits(string bitString, int numberOfBits, string expectedBytes)
        {
            var bs = new BitString(bitString, numberOfBits);

            var hex = bs.ToHex();
            Assert.AreEqual(expectedBytes, hex);
        }

        [Test]
        [TestCase(0, "00")]
        [TestCase(1, "01")]
        [TestCase(7, "76")]
        [TestCase(8, "D3")]
        [TestCase(9, "D901")]
        [TestCase(15, "4375")]
        [TestCase(16, "5AE8")]
        [TestCase(17, "91AD00")]
        [TestCase(400, "1234567890ABCDEFABCDABCDABCDABCDABCDABCDABCDABCDABCDEDF4EDFAEDFAEDFABEEFBEEFDEADBEEFDEADBEEFDEADBEEF")]
        [TestCase(401, "FACEBEEFBEADDEADACEDFADECAFEBABECEEDFEEDFACEBEEFBEADDEADACEDFADECAFEBABECEEDFEEDDEAFBEEFDEAFBEEDAEFD01")]
        public void LittleEndianInputShouldMatchLittleEndianOutput(int length, string hex)
        {
            var subject = new BitString(hex, length, false);
            var result = subject.ToLittleEndianHex();
            Assert.AreEqual(hex, result, subject.ToString());
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(1500)]
        public void GetHashCodeShouldBeConsistent(int testInt)
        {
            // Arrange
            var testBigInt = new BigInteger(testInt);
            var bs1 = new BitString(testBigInt);
            var bs2 = new BitString(testBigInt);

            // Act
            var firstHash = bs1.GetHashCode();
            var secondHash = bs2.GetHashCode();

            // Assert
            Assert.That(bs1.Equals(bs2));
            Assert.AreEqual(firstHash, secondHash);
        }

        // The logic here is a -> b
        // Equality of BitStrings corresponds to a
        // Matching GetHashCodes corresponds to b
        // If two BitStrings are equal, then their GetHashCodes must also be equal
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(1500)]
        public void EqualityShouldImplyMatchingGetHashCode(int testInt)
        {
            // Arrange
            var testBigInt = new BigInteger(testInt);
            var bs1 = new BitString(testBigInt);
            var bs2 = bs1;

            // Act
            var firstHash = bs1.GetHashCode();
            var secondHash = bs2.GetHashCode();

            // Assert
            Assert.That(bs1.Equals(bs2));
            Assert.AreEqual(firstHash, secondHash);
        }

        // Using the contrapositive, ~b -> ~a
        // Inequality of BitStrings corresponds to ~a
        // Mismatching GetHashCodes corresponds to ~b
        // If their GetHashCodes are unequal, then two BitStrings must be unequal
        [Test]
        [TestCase(1, 2)]
        [TestCase(10, 11)]
        [TestCase(15, 51)]
        [TestCase(1500, 1050)]
        public void MismatchingGetHashCodeShouldImplyInequality(int testInt1, int testInt2)
        {
            // Arange
            var testBigInt1 = new BigInteger(testInt1);
            var testBigInt2 = new BigInteger(testInt2);
            var bs1 = new BitString(testBigInt1);
            var bs2 = new BitString(testBigInt2);

            // Act
            var firstHash = bs1.GetHashCode();
            var secondHash = bs2.GetHashCode();

            // Assert
            Assert.That(firstHash != secondHash);
            Assert.AreNotEqual(bs1, bs2);
        }

        [Test]
        [TestCase("80", 0, new bool[] { })]
        [TestCase("80", 1, new[] { true })]
        [TestCase("F0", 2, new[] { true, true })]
        [TestCase("C0", 3, new[] { false, true, true })]
        [TestCase("70", 4, new[] { true, true, true, false })]
        [TestCase("8A", 5, new[] { true, false, false, false, true })]
        [TestCase("FF", 6, new[] { true, true, true, true, true, true })]
        [TestCase("8E", 7, new[] { true, true, true, false, false, false, true })]
        [TestCase("AB", 8, new[] { true, true, false, true, false, true, false, true })]
        [TestCase("8003", 9, new[] { false, false, false, false, false, false, false, false, true })]
        public void ShouldReturnBitStringFromBitOrientedConstructor(string hex, int len, bool[] expected)
        {
            var subject = new BitString(hex, len);
            var expectedResult = new BitString(new BitArray(expected));

            Assert.AreEqual(len, expectedResult.BitLength);
            Assert.AreEqual(expectedResult, subject);
        }

        [Test]
        [TestCase("80", 1, "80")]
        [TestCase("30", 2, "00")]
        [TestCase("D0", 3, "C0")]
        [TestCase("E0", 4, "E0")]
        [TestCase("AF", 5, "A8")]
        [TestCase("BF", 6, "BC")]
        [TestCase("CF", 7, "CE")]
        [TestCase("55", 8, "55")]
        [TestCase("ABCD", 9, "AB80")]
        [TestCase("BEEF FACE", 30, "BEEF FACC")]
        public void ShouldConvertBitOrientedBitStringToHex(string hex, int len, string expectedHex)
        {
            var subject = new BitString(hex, len);
            var expectedResult = new BitString(expectedHex);
            var result = subject.ToHex();

            Assert.AreEqual(expectedResult.ToHex(), result);
        }

        [Test]
        [TestCase("00", false)]
        [TestCase("10", false)]
        [TestCase("80", true)]
        [TestCase("FF", true)]
        public void ShouldConvertBitStringToBool(string hex, bool expectedResult)
        {
            var subject = new BitString(hex);
            var result = subject.ToBool();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("All even sets first bit to false", new[] { true, true, true, true, true, true, true, true }, false)]
        [TestCase("first bit false, stays false", new[] { false, true, true, true, true, true, true, true }, false)]
        [TestCase("two first bits false (even number of odds), set first bit true", new[] { false, false, true, true, true, true, true, true }, true)]
        [TestCase("first three bits false, odd number of falses, keep first bit false", new[] { false, false, false, true, true, true, true, true }, false)]
        public void ShouldSetOddParityBit(string label, bool[] bits, bool expectation)
        {
            BitArray ba = new BitArray(bits);
            var subject = new BitString(ba);
            var result = subject.ToOddParityBitString();

            Assert.AreEqual(expectation, result.GetLeastSignificantBits(1).Bits[0], label);
        }

        [Test]
        public void ShouldReturnOne()
        {
            var subject = BitString.One();
            Assert.AreEqual(1, subject.BitLength);
            Assert.IsTrue(subject.ToBool());
        }

        [Test]
        public void ShouldReturnZero()
        {
            var subject = BitString.Zero();
            Assert.AreEqual(1, subject.BitLength);
            Assert.IsFalse(subject.ToBool());
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(1000)]
        public void ShouldReturnOnes(int count)
        {
            var subject = BitString.Ones(count);
            Assert.AreEqual(count, subject.BitLength);

            for (var i = 0; i < count; i++)
            {
                Assert.IsTrue(subject.ToBool(i));
            }
        }

        [Test]
        public void ShouldReturnTwo()
        {
            var subject = BitString.Two();
            Assert.AreEqual("80", subject.ToHex()); // BigEndian hex, 10 - 000000
            Assert.AreEqual(2, subject.BitLength);
        }

        private static object[] _test_GetAtLeastZeroLengthBitString = {
            new object[]
            {
                null,
                new BitString(0)
            },
            new object[]
            {
                new BitString("CAFE"),
                new BitString("CAFE"),
            },
            new object[]
            {
                new BitString("CAFECAFE"),
                new BitString("CAFECAFE"),
            },
        };

        [Test]
        [TestCaseSource(nameof(_test_GetAtLeastZeroLengthBitString))]
        public void ShouldGetAtLeastZeroLengthBitString(BitString testValue, BitString expectedValue)
        {
            var result = BitString.GetAtLeastZeroLengthBitString(testValue);

            Assert.AreEqual(expectedValue.ToHex(), result.ToHex());
        }

        private static object[] _test_IsZeroLengthOrNull = {
            new object[]
            {
                null,
                true
            },
            new object[]
            {
                new BitString(0),
                true
            },
            new object[]
            {
                new BitString(1),
                false
            },
            new object[]
            {
                new BitString(42),
                false
            }
        };

        [Test]
        [TestCaseSource(nameof(_test_IsZeroLengthOrNull))]
        public void ShouldIsZeroLengthOrNullCorrectly(BitString testValue, bool expectedResult)
        {
            var result = BitString.IsZeroLengthOrNull(testValue);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
