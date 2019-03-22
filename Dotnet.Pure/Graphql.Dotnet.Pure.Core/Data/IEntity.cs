namespace graphql.core.Data
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}