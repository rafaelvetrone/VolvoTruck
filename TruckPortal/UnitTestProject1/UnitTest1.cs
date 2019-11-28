using Microsoft.VisualStudio.TestTools.UnitTesting;
using TruckPortal.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HomeController controller = new HomeController();

            var view = controller.Index();


        }

        [Fact]
        public void Teste2()
        {

        }
    }
}
