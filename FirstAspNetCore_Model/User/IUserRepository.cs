namespace FirstAspNetCore_Model
{
    public interface IUserRepository : IReadOnlyRepository<UserModel, RequestHeaderModel>, IRepository<UserModel, RequestHeaderModel>
    {
    }
}
