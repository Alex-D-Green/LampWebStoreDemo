using Newtonsoft.Json;


namespace LampStore.Infrastructure.WebApi.ApiModels
{
    /// <summary>
    /// Error information.
    /// </summary>
    public sealed class ErrorDetailsApi
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
