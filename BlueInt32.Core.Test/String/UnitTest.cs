using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueInt32.Core.String;

namespace BlueInt32.Core.Test.String
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ToInt()
        {
            string value = "toto";

            int? actual = value.ToInt();
            int? expected = null;
            Assert.AreEqual(actual, expected, "La convertion a réussi alors que la chaine était invalide");

            value = "1";

            actual = value.ToInt();
            expected = 1;
            Assert.AreEqual(actual, expected, "La convertion a échouée alors que la chaine était un entier valide");
        }
    }
}
