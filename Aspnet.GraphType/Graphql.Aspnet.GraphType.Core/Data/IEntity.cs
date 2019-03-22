namespace Graphql.Aspnet.GraphType.Core.Data
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}