using Newtonsoft.Json;

namespace FirstAspNetCore_Model
{
    public abstract class Model : IModel
    {
        [JsonIgnore]
        public int? TotalRow { get; set; }

        public bool Validate(bool isNew = false)
        {
            return CheckValidate(isNew);
        }

        protected abstract bool CheckValidate(bool isNew = false);
    }
}
