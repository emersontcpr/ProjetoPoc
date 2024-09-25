namespace ProjetoWeb.ConsultaApi
{
    public class ApiResponse<T>
    {
        public bool IsSuccessStatusCode { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
