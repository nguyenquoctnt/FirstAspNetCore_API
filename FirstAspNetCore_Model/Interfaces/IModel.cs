namespace FirstAspNetCore_Model
{
    public interface IModel
    {
        bool Validate(bool isNew = false);
    }
}
