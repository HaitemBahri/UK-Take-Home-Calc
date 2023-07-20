using UKTakeHomeCalc.Core.Models;

namespace UKTakeHomeCalc.Core.TieredValueCalculators
{
    public class TieredValueResultsBuilder : ITieredValueResultsBuilder
    {
        private readonly List<TieredValueResult> _results = new List<TieredValueResult>();
        public ITieredValueResultsBuilder Add(TieredValueRule rule, MonetaryValue result)
        {
            _results.Add(new TieredValueResult(rule, result));

            return this;
        }

        public List<TieredValueResult> Build()
        {
            return _results;
        }
    }
}
