using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueInt32.Core.String.Validation;

namespace BlueInt32.Core.Test.String.Validation
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void IsValidMail()
        {
            string value = "meaudre.lionel.rappfrance.com";

            bool actual = value.IsValidMail();
            bool expected = false;
            Assert.AreEqual(actual, expected, "Le test indique que le mail est valide alors que la chaine était invalide");

            value = "meaudre.lionel@rappfrance";

            actual = value.IsValidMail();
            expected = false;
            Assert.AreEqual(actual, expected, "Le test indique que le mail est valide alors que la chaine était invalide");

            value = "meaudre.lionel@rappfrance.com";

            actual = value.IsValidMail();
            expected = true;
            Assert.AreEqual(actual, expected, "Le test indique que le mail est invalide alors que la chaine était valide");

            value = "meaudre.lionel2@rappfrance.com";

            actual = value.IsValidMail();
            expected = true;
            Assert.AreEqual(actual, expected, "Le test indique que le mail est invalide alors que la chaine était valide");
        }
    }
}
