using System.Collections.Generic;

namespace FirstAspNetCore_Model
{
    public interface IReadOnlyRepository<T, H>
        where T : IModel
        where H : IRequestHeaderModel
    {
        IEnumerable<T> Find(H header, int offset, int limit, string orderBy, SortingType sortType, out int totalRow);
        IEnumerable<T> Find(H header, T condition, int offset, int limit, string orderBy, SortingType sortType, out int totalRow);
        IEnumerable<T> Find<T1>(H header, T1 condition, int offset, int limit, string orderBy, SortingType sortType, out int totalRow);
        T Find(H header, int id);
        T Find(H header, T condition);
        T Find<T1>(H header, T1 condition);
    }
}
