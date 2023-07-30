using UKTakeHomeCalc.Core.TakeHomeSummaryItems;

namespace UKTakeHomeCalc.Core.CalculatorsHandlers
{
    public interface ICalculatorsHandler
    {
        public void SetNext(ICalculatorsHandler calculatorsHandler);
        public void Handle(ITakeHomeSummaryComposite takeHomeSummary);
    }
}
