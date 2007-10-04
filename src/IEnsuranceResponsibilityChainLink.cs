namespace Ensurance
{
    public interface IEnsuranceResponsibilityChainLink : IEnsuranceHandler
    {
        IEnsuranceResponsibilityChainLink Successor { get; set; }
    }
}