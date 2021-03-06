/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granteM, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
  IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace TA4Net.Test.Indicators.volume
{
    using TA4Net;
    using TA4Net.Indicators.Volume;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class PVIIndicatorTest
    {

        [TestMethod]
        public void getValue()
        {

            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(1355.69M, 2739.55M));
            bars.Add(new MockBar(1325.51M, 3119.46M));
            bars.Add(new MockBar(1335.02M, 3466.88M));
            bars.Add(new MockBar(1313.72M, 2577.12M));
            bars.Add(new MockBar(1319.99M, 2480.45M));
            bars.Add(new MockBar(1331.85M, 2329.79M));
            bars.Add(new MockBar(1329.04M, 2793.07M));
            bars.Add(new MockBar(1362.16M, 3378.78M));
            bars.Add(new MockBar(1365.51M, 2417.59M));
            bars.Add(new MockBar(1374.02M, 1442.81M));
            ITimeSeries series = new MockTimeSeries(bars);

            PVIIndicator pvi = new PVIIndicator(series);
            Assert.AreEqual(pvi.GetValue(0), 1000);
            Assert.AreEqual(pvi.GetValue(1), 977.7382734991037774122402614M);
            Assert.AreEqual(pvi.GetValue(2), 984.7531515316923485457589862M);
            Assert.AreEqual(pvi.GetValue(3), 984.7531515316923485457589862M);
            Assert.AreEqual(pvi.GetValue(4), 984.7531515316923485457589862M);
            Assert.AreEqual(pvi.GetValue(5), 984.7531515316923485457589862M);
            Assert.AreEqual(pvi.GetValue(6), 982.6754728473029236860423644M);
            Assert.AreEqual(pvi.GetValue(7), 1007.1639846006757889365101630M);
            Assert.AreEqual(pvi.GetValue(8), 1007.1639846006757889365101630M);
            Assert.AreEqual(pvi.GetValue(9), 1007.1639846006757889365101630M);
        }
    }
}