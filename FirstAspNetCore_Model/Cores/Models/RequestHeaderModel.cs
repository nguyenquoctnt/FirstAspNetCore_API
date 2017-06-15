namespace FirstAspNetCore_Model
{
    public class RequestHeaderModel : IRequestHeaderModel
    {

        public string Language { get; set; }

        public long Time { get; set; }

        public string UserAgent { get; set; }

        public int UserLoginId { get; set; }

        public int? UserTypeLoginId { get; set; }

        public string UserPasswordLogin { get; set; }
    }
}
