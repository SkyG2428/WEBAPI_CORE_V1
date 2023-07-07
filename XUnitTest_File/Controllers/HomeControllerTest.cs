using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnit_WebApi.Controllers;

namespace XUnitTest_File.Controllers
{
    public class HomeControllerTest
    {
        //[Fact]
        //public void HomeController_Index_ValidResult()
        //{
        //    //AAA Methodlogy
        //    // A: Arrange
        //    HomeController controller = new HomeController();
        //    string expectedResult = "X Unit Test Demo Tryel";

        //    // A: Act
        //    string result = controller.Index(); // Calling method

        //    // A: Assert
        //    Assert.Equal(expectedResult, result);

        //}


        [Fact]
        public void HomeController_Index_ValidLargeNumberResult()
        {
            //AAA Methodlogy
            // A: Arrange
            HomeController controller = new HomeController();
            int guessedNumber = 90;
            string expectedResult = "Wrong! Try a bigger Number.";

            // A: Act
            string result = controller.Index(guessedNumber); // Calling method

            // A: Assert
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void HomeController_Index_ValidSmallerNumberResult()
        {
            //AAA Methodlogy
            // A: Arrange
            HomeController controller = new HomeController();
            int guessedNumber = 101;
            string expectedResult = "Wrong! Try a smaller Number.";

            // A: Act
            string result = controller.Index(guessedNumber); // Calling method

            // A: Assert
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void HomeController_Index_ValidCurrectNumberResult()
        {
            //AAA Methodlogy
            // A: Arrange
            HomeController controller = new HomeController();
            int guessedNumber = 100;
            string expectedResult = "You guessed correct number..";

            // A: Act
            string result = controller.Index(guessedNumber); // Calling method

            // A: Assert
            Assert.Equal(expectedResult, result);

        }
    }
}
