using System;
using System.Collections.Generic;
using CommonTestUtils;
using GitExtUtils;
using NUnit.Framework;

namespace GitExtUtilsTests
{
    [TestFixture]
    public sealed class LazyStringSplitTests
    {
        [TestCase("a;b;c", ';', new[] { "a", "b", "c" })]
        [TestCase("a_b_c", '_', new[] { "a", "b", "c" })]
        [TestCase("aa;bb;cc", ';', new[] { "aa", "bb", "cc" })]
        [TestCase("aaa;bbb;ccc", ';', new[] { "aaa", "bbb", "ccc" })]
        [TestCase(";a", ';', new[] { "", "a" })]
        [TestCase("a;", ';', new[] { "a", "" })]
        [TestCase(";a;b;c", ';', new[] { "", "a", "b", "c" })]
        [TestCase("a;b;c;", ';', new[] { "a", "b", "c", "" })]
        [TestCase(";a;b;c;", ';', new[] { "", "a", "b", "c", "" })]
        [TestCase(";;a;;b;;c;;", ';', new[] { "", "", "a", "", "b", "", "c", "", "" })]
        [TestCase("", ';', new[] { "" })]
        [TestCase(";", ';', new[] { "", "" })]
        [TestCase(";;", ';', new[] { "", "", "" })]
        [TestCase(";;;", ';', new[] { "", "", "", "" })]
        [TestCase(";;;a", ';', new[] { "", "", "", "a" })]
        [TestCase("a;;;", ';', new[] { "a", "", "", "" })]
        [TestCase(";a;;", ';', new[] { "", "a", "", "" })]
        [TestCase(";;a;", ';', new[] { "", "", "a", "" })]
        [TestCase("a", ';', new[] { "a" })]
        [TestCase("aa", ';', new[] { "aa" })]
        public void None(string input, char delimiter, string[] expected)
        {
            // This boxes
            IEnumerable<string> actual = new LazyStringSplit(input, delimiter, StringSplitOptions.None);

            AssertEx.SequenceEqual(expected, actual);

            // Non boxing foreach
            List<string> list = new();

            foreach (var s in new LazyStringSplit(input, delimiter, StringSplitOptions.None))
            {
                list.Add(s);
            }

            AssertEx.SequenceEqual(expected, list);

            // Equivalence with string.Split
            AssertEx.SequenceEqual(expected, input.Split(new[] { delimiter }, StringSplitOptions.None));
        }

        [TestCase("a;b;c", ';', new[] { "a", "b", "c" })]
        [TestCase("a_b_c", '_', new[] { "a", "b", "c" })]
        [TestCase("aa;bb;cc", ';', new[] { "aa", "bb", "cc" })]
        [TestCase("aaa;bbb;ccc", ';', new[] { "aaa", "bbb", "ccc" })]
        [TestCase(";a", ';', new[] { "a" })]
        [TestCase("a;", ';', new[] { "a" })]
        [TestCase(";a;b;c", ';', new[] { "a", "b", "c" })]
        [TestCase("a;b;c;", ';', new[] { "a", "b", "c" })]
        [TestCase(";a;b;c;", ';', new[] { "a", "b", "c" })]
        [TestCase(";;a;;b;;c;;", ';', new[] { "a", "b", "c" })]
        [TestCase("", ';', new string[0])]
        [TestCase(";", ';', new string[0])]
        [TestCase(";;", ';', new string[0])]
        [TestCase(";;;", ';', new string[0])]
        [TestCase(";;;a", ';', new[] { "a" })]
        [TestCase("a;;;", ';', new[] { "a" })]
        [TestCase(";a;;", ';', new[] { "a" })]
        [TestCase(";;a;", ';', new[] { "a" })]
        [TestCase("a", ';', new[] { "a" })]
        [TestCase("aa", ';', new[] { "aa" })]
        public void RemoveEmptyEntries(string input, char delimiter, string[] expected)
        {
            // This boxes
            IEnumerable<string> actual = new LazyStringSplit(input, delimiter, StringSplitOptions.RemoveEmptyEntries);

            AssertEx.SequenceEqual(expected, actual);

            // Non boxing foreach
            List<string> list = new();

            foreach (var s in new LazyStringSplit(input, delimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(s);
            }

            AssertEx.SequenceEqual(expected, list);

            // Equivalence with string.Split
            AssertEx.SequenceEqual(expected, input.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries));
        }

        [Test]
        public void Constructor_WithNullInput_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new LazyStringSplit(null!, ' ', StringSplitOptions.RemoveEmptyEntries));
        }

        // Arrange
        [TestCase("refs/heads/develop", '/', "develop")]
        [TestCase("refs/", '/', "")]
        [TestCase("refs", '/', "refs")]
        [TestCase("", '/', "")]
        public void LastOrDefault_(string name, char separator, string? expected)
        {
            // Act
            string? lastOrDefault = name.LazySplit(separator).LastOrDefault();

            // Assert
            Assert.AreEqual(expected, lastOrDefault);
        }
    }
}
