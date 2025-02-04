using System.ComponentModel.DataAnnotations;
using Common.DataAccess.Interfaces;

namespace Common.DataAccess.Implementations
{
    /// <summary>
    /// Класс для сущностей
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        public virtual int Id { get; set; }
    }
}
