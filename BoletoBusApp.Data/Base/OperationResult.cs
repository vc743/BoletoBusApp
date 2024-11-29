
namespace BoletoBusApp.Data.Base
{
    public class OperationResult<TData>
    {
        public OperationResult()
        {
            this.Success = true;
        }
        public bool Success { get; set; }
        public string? Message { get; set; }

        public TData? Result { get; set; }

    }
}
