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
using TA4Net.Indicators.Helpers;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{

    /**
     * The Class RandomWalkIndexHighIndicator.
     */
    public class RandomWalkIndexHighIndicator : CachedIndicator<decimal>
    {
        private readonly MaxPriceIndicator _maxPrice;
        private readonly MinPriceIndicator _minPrice;
        private readonly ATRIndicator _averageTrueRange;
        private readonly decimal _sqrtTimeFrame;
        private readonly int _timeFrame;

        /**
         * Constructor.
         *
         * @param series the series
         * @param timeFrame the time frame
         */
        public RandomWalkIndexHighIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _timeFrame = timeFrame;
            _maxPrice = new MaxPriceIndicator(series);
            _minPrice = new MinPriceIndicator(series);
            _averageTrueRange = new ATRIndicator(series, timeFrame);
            _sqrtTimeFrame = ((decimal)timeFrame).Sqrt();
        }

        protected override decimal Calculate(int index)
        {
            return _maxPrice.GetValue(index).Minus(_minPrice.GetValue(Math.Max(0, index - _timeFrame)))
                    .DividedBy(_averageTrueRange.GetValue(index).MultipliedBy(_sqrtTimeFrame));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, ATRIndicator: {_averageTrueRange}, MaxPriceIndicator: {_maxPrice.GetConfiguration()}, MinPriceindicator: {_minPrice.GetConfiguration()}";
        }
    }
}
