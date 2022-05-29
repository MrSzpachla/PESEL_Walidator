using Microsoft.VisualStudio.TestTools.UnitTesting;
using walidator;
namespace walidator_TestProject
{
    [TestClass]
    public class UnitTest1
    {
        PESELWalidator walidat = new PESELWalidator("65071209862");
        string poprawnyp = "Pesel jest nie Poprawny";
        
        [TestMethod]
        public void Poprawnosc()
        {
            Assert.IsTrue(walidat.poprawny,poprawnyp);
        }
        [TestMethod]
        public void Plec()
        {
            Assert.AreEqual("Kobieta", walidat.Plec());
        }
        [TestMethod]
        public void rok()
        {
            Assert.AreEqual("12 Lipiec 1965 r.", walidat.DataUrodzenia());
        }
        [TestMethod]
        public void suma()
        {
            Assert.AreEqual(2,walidat.SumaKontrolna());
        }
    }
}