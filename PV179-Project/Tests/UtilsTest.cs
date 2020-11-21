using System;
using BusinessLayer.Utils;
using Xunit;

namespace Tests
{
    public class UtilsTest
    {
        [Fact]
        public void DateParsingTest()
        {
            var date = Utils.ParseDate("17/11/2020");
            var actual = new DateTime(2020, 11, 17);
            Assert.Equal(actual, date);
        }
    }
}