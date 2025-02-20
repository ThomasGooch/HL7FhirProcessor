namespace HL7ConsentProcessing.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}
