/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granted, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Triple exponential moving average indicator.
     * </p>
     * a.k.a TRIX
     * </p>
     * TEMA needs "3 * period - 2" of data to start producing values in contrast to
     * the period samples needed by a regular EMA.
     * </p>
     * see https://en.wikipedia.org/wiki/Triple_exponential_moving_average
     */
    public class TripleEMAIndicator : CachedIndicator<decimal>
    {
        private readonly int _timeFrame;
        private readonly EMAIndicator _ema;
        private readonly EMAIndicator _emaEma;
        private readonly EMAIndicator _emaEmaEma;

        /**
         * Constructor.
         * 
         * @param indicator the indicator
         * @param timeFrame the time frame
         */
        public TripleEMAIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _timeFrame = timeFrame;
            _ema = new EMAIndicator(indicator, timeFrame);
            _emaEma = new EMAIndicator(_ema, timeFrame);
            _emaEmaEma = new EMAIndicator(_emaEma, timeFrame);
        }


        protected override decimal Calculate(int index)
        {

            // trix = 3 * ema - 3 * emaEma + emaEmaEma 
            return Decimals.THREE.MultipliedBy(
                    _ema.GetValue(index)
                        .Minus(_emaEma.GetValue(index)))
                        .Plus(_emaEmaEma.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, EmaIndicator: {_ema.GetConfiguration()}, EmaEmaIndicator: {_emaEma.GetConfiguration()}, EmaEmaEmaIndicator: {_emaEmaEma.GetConfiguration()}";
        }
    }
}
