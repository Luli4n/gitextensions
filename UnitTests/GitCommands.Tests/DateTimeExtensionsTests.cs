using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GitCommandsTests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ToDateTimeOffset_MinValue()
        {
            // Arrage
            DateTime dateTime = new(0001, 01, 01);

            // Act
            var o = dateTime.ToDateTimeOffset();

            // Assert
            Assert.AreEqual(DateTimeOffset.MinValue.GetType(), o.GetType());
            Assert.AreEqual(DateTimeOffset.MinValue, o);
        }
    }
}
