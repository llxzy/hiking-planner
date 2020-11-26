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

        [Fact]
        public void PasswordEncodeTest()
        {
            const string password = "TotallySecureUnbreakablePassword1337";
            const string expected = 
                "A6F21D7638BD9A36A67DFEB9CCBCB719CD588EA43A76B93ABD6E687114B47C6DE8F4803E70B5EAC827BCA3C39928AC9DB148F7D354F8772AA6C5B508CAD783EF";
            Assert.True(HashingUtils.Validate(password, expected));
        }
    }
}