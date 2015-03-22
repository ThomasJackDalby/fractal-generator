using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fractal_Generator;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Fractal_Generator_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PowTest()
        {
            Complex z = new Complex(1,1);

            Assert.AreEqual(z * z, z.Pow(2));
            Assert.AreEqual(z * z * z, z.Pow(3));
            Assert.AreEqual(z * z * z * z, z.Pow(4));
            Assert.AreEqual(z * z * z * z * z, z.Pow(5));
            Assert.AreEqual(z * z * z * z * z * z, z.Pow(6));
            Assert.AreEqual(z, z.Pow(1));
            Assert.AreEqual(new Complex(1,0), z.Pow(0));
        }


        [TestMethod]
        public void CreateLargeBitMapTest()
        {
 



         
        }
    }
}
