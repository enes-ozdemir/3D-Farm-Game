using Farm.Profile;

namespace Farm.Processor
{
    public interface IProcessor
    {
        bool PreProcess(ProductJob job, int jobIndex);

        bool CanProcess(ProductJob job, int jobIndex);

        void Process(ProductJob job, int jobIndex);

        void AfterProcess(ProductJob job, int jobIndex);

    }
}
