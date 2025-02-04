using System.Linq.Expressions;
using Common.DataAccess.Implementations;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.DataAccess.Interfaces
{
    /// <summary>
    /// Дженериковский репозиторий для работы с базой данных
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности из базы данных</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Получение сущности поп идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Экземпляр сущности</returns>
        public TEntity? GetById(object? id);

        /// <summary>
        /// Асинхронное получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Экземпляр сущности</returns>
        public Task<TEntity?> GetByIdAsync(object? id);

        /// <summary>
        /// Получения всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        public IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Получение сущностей, удовлетворяющих условия
        /// </summary>
        /// <param name="filter">Условия для фильтрации</param>
        /// <param name="orderBy">Условия для сортировки</param>
        /// <param name="includes">Включает подгрузку навигационных свойств</param>
        /// <param name="skip">Количество записей, которые надо пропустить</param>
        /// <param name="take">Количество записей, которые надо взять</param>
        /// <returns>Список сущностей</returns>
        internal IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null, int? skip = null, int? take = null);

        /// <summary>
        /// Получение числа сущностей, удовлетворяющих условию
        /// </summary>
        /// <param name="predicate">Условие</param>
        /// <returns>Число сущностей</returns>
        internal int GetCount(Expression<Func<TEntity, bool>>? predicate = null);

        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленная сущность</returns>
        internal TEntity Add(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        public void Update(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        public void Delete(TEntity entity);
    }
}
