namespace OpenSheets.Contracts.Commands
{
    public class ValidationResult
    {
        public string Path { get; set; }
        public string Message { get; set; }
        public Level Level { get; set; }
    }

    public interface IValidationRule<T>
    {
        bool Validate(T obj, out ValidationResult result);
    }
}