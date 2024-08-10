using Xunit;
using zhankui_wang_Practice_Asst_5.Utilities;

namespace PracticeAsst5_test
{
    public class UtilityTests
    {
        [Theory]
        [InlineData("m6h 2x4", "M6H2X4")]
        [InlineData(" M6H2X4 ", "M6H2X4")]
        [InlineData("m6h2x4", "M6H2X4")]
        [InlineData("M6H 2X4", "M6H2X4")]
        [InlineData("M 6H 2X 4", "M6H2X4")]
        [InlineData("m6h 2x4  ", "M6H2X4")] 
        public void PostalCode_ReturnsFormattedPostalCode(string inputPostalCode, string expected)
        {
            // Act
            string result = Utility.PostalCode(inputPostalCode);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
