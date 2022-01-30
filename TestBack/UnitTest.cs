using API.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBack
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public  void CountryNameSearchTest()
        {
            string result = "VEN".CountryNameByISO().Result;
            Assert.AreEqual(result, "Venezuela");
        }

        [TestMethod]
        public void validNITTest()
        {
            bool result = "000.111.222.5".ValidNit();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void validEmailTest()
        {
            bool result = "jleal@vanguarsoft.com.ve".ValidMail();
            Assert.IsTrue(result);
        }
    }
}