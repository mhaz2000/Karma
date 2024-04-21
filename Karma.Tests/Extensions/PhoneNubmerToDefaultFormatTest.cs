using FluentAssertions;
using Karma.Application.Extensions;

namespace Karma.Tests.Extensions
{
    public class PhoneNubmerToDefaultFormatTest
    {
        [Fact]
        public void Must_Change_Phone_Number_To_Defautl_Format()
        {
            //Arrange
            string firstPhoneNumber = "+989109828926";
            string expectedFirstPhoneNumber = "09109828926";


            string secondPhoneNumber = "09207831300";
            string expectedSecondPhoneNumber = "09207831300";

            //Act
            var formatedFirstPhoneNubmer = firstPhoneNumber.ToDefaultFormat();
            var formatedSecondPhoneNubmer = secondPhoneNumber.ToDefaultFormat();

            //Assert
            expectedFirstPhoneNumber.Should().Be(formatedFirstPhoneNubmer);
            expectedSecondPhoneNumber.Should().Be(formatedSecondPhoneNubmer);
        }
    }
}
