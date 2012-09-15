using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Irving.Web.Tests
{
    public static class AssertHelper
    {
        public static void EnumerableEqual(IEnumerable<object> expected, IEnumerable<object> actual, string message)
        {
            //if both are null then they are equal and we dont need to compare
            if (expected != null
                || actual != null)
            {
                //if only one is null then the comparision fails
                if ((expected == null && actual != null)
                    || (expected != null && actual == null))
                {
                    Assert.Fail(message);
                }

                //they should be the same size
                Assert.AreEqual(expected.Count(), actual.Count(), message);

                //each item should be the same
                var expectedList = expected.ToList();
                var actualList = actual.ToList();
                for (int i = 0; i < expectedList.Count; i++)
                {
                    Assert.AreEqual(expectedList[i], actualList[i], message);
                }
            }
        }
    }
}
