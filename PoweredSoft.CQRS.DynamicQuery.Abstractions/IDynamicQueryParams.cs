namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public interface IDynamicQueryParams<TParams>
        where TParams : class
    {
        TParams GetParams();
    }
}
