
namespace Core.Base;

[Serializable]
public partial class EntityBase : IEntity
{
  
}

[Serializable]
public partial class EntityBase<TKey> : EntityBase, IEntity<TKey>
{
    public TKey Id { get; set; }

}
