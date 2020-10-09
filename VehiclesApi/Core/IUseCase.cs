namespace VehiclesApi.UseCases
{
    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface IUseCase<TRequest>
    {
        void Execute(TRequest request);
    }

    public class NoContent
    {
        protected NoContent()
        {

        }

        public static NoContent Return => new NoContent();
    }
}
