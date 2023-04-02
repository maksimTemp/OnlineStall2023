using OrderAPI.Domain;

namespace OrderAPI.Services
{
    public interface IService <T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> UpdateAsync(T obj);
        Task<int> DeleteAsync(Guid id);
    }
}
