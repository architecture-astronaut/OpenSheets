namespace OpenSheets.Core.Hexagon
{
    public interface IServiceResolver
    {
        T Resolve<T>();
    }
}