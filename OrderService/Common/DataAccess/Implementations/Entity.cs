using System.ComponentModel.DataAnnotations;
using Common.DataAccess.Interfaces;

namespace Common.DataAccess.Implementations
{
    public class Entity : IEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
