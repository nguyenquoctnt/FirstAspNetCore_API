namespace FirstAspNetCore_Model
{
    public interface IRepository<T, H>
        where T : IModel
        where H : IRequestHeaderModel
    {
        string Add(H header, T item);
        string Update(H header, T item);
        string Remove(H header, int id);
        string Remove(H header, string idList);
        string Remove(H header, T item);
    }
}
