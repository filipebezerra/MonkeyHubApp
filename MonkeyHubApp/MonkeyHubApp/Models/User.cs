using Newtonsoft.Json;

namespace MonkeyHubApp.Models
{
    /// <summary>
    /// Usuário autenticado
    /// </summary>
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
