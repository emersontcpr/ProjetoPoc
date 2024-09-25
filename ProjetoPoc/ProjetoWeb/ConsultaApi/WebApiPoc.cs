using System.Net.Http.Headers;
using System.Security.Policy;

namespace ProjetoWeb.ConsultaApi
{
    public class WebApiPoc
    {
        private readonly HttpClient _httpClient;

        public WebApiPoc(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void SetBearerToken(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string endpoint, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("accept", "*/*");

            request.Content = new StringContent(id.ToString());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            var apiResponse = new ApiResponse<TResponse>
            {
                StatusCode = (int)response.StatusCode,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ErrorMessage = await response.Content.ReadAsStringAsync()
            };

            if (apiResponse.ErrorMessage.Contains("{"))
            {
                // Leitura do conteúdo da resposta em caso de sucesso
                apiResponse.Data = await response.Content.ReadFromJsonAsync<TResponse>();
                apiResponse.ErrorMessage = string.Empty;
            }

            return apiResponse;


        }

        public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest requestObject)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, requestObject);

                var apiResponse = new ApiResponse<TResponse>
                {
                    StatusCode = (int)response.StatusCode,
                    IsSuccessStatusCode = response.IsSuccessStatusCode,
                    ErrorMessage = await response.Content.ReadAsStringAsync()
                };

                if (apiResponse.ErrorMessage.Contains("{"))
                {
                    // Leitura do conteúdo da resposta em caso de sucesso
                    apiResponse.Data = await response.Content.ReadFromJsonAsync<TResponse>();
                    apiResponse.ErrorMessage = string.Empty;
                }

                return apiResponse;

            }
            catch (Exception ex)
            {
                var apiResponseErro = new ApiResponse<TResponse>
                {
                    StatusCode = 500,
                    IsSuccessStatusCode =false,
                    ErrorMessage = ex.Message
                };
                return apiResponseErro;
            }

        }
        public async Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest requestObject)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, requestObject);
            var apiResponse = new ApiResponse<TResponse>
            {
                StatusCode = (int)response.StatusCode,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ErrorMessage = await response.Content.ReadAsStringAsync()
            };

            if (apiResponse.ErrorMessage.Contains("{"))
            {
                // Leitura do conteúdo da resposta em caso de sucesso
                apiResponse.Data = await response.Content.ReadFromJsonAsync<TResponse>();
                apiResponse.ErrorMessage = string.Empty;
            }

            return apiResponse;
        }

     
        public async Task<ApiResponse<TResponse>> DeleteAsync<TResponse>(string endpoint,int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            request.Headers.Add("accept", "*/*");

            request.Content = new StringContent(id.ToString());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            var apiResponse = new ApiResponse<TResponse>
            {
                StatusCode = (int)response.StatusCode,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ErrorMessage = await response.Content.ReadAsStringAsync()
            };

            if (apiResponse.ErrorMessage.Contains("{"))
            {
                // Leitura do conteúdo da resposta em caso de sucesso
                apiResponse.Data = await response.Content.ReadFromJsonAsync<TResponse>();
                apiResponse.ErrorMessage = string.Empty;
            }

            return apiResponse;
        }
    }
}
