using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasketService.Client
{
    public partial class BasketApiEndpoint
    {
        private Lazy<Newtonsoft.Json.JsonSerializerSettings> settings;
        private string baseUrl = string.Empty;

        public BasketApiEndpoint(string endpointUrl)
        {
            baseUrl = endpointUrl;
        }

        public async Task<string> GetBasketAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            if (id >= 0)
            {
                throw new ArgumentNullException("id");
            }

            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(baseUrl).Append("/api/basket/{id}");
            urlBuilder.Replace("{id}", Uri.EscapeDataString(Convert.ToString(id, System.Globalization.CultureInfo.InvariantCulture)));

            var client = new HttpClient();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = new HttpMethod("GET");
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = System.Linq.Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        foreach (var item in response.Content.Headers)
                        {
                            headers[item.Key] = item.Value;
                        }

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "200")
                        {
                            var responseData = await response.Content.ReadAsStringAsync();
                            var result = default(string);
                            try
                            {
                                result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseData, settings.Value);
                                return result;
                            }
                            catch (Exception exception)
                            {
                                throw new Exception("Could not deserialise response body.", exception);
                            }
                        }
                        
                        return default(string);
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }

        public async Task<string> PostAsync(object value, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(baseUrl).Append("/api/basket");

            var client = new HttpClient();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(value));
                    content.Headers.ContentType.MediaType = "application/json";
                    request.Content = content;
                    request.Method = new HttpMethod("POST");

                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = System.Linq.Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        foreach (var item in response.Content.Headers)
                        {
                            headers[item.Key] = item.Value;
                        }

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "200")
                        {
                            var responseData = await response.Content.ReadAsStringAsync();
                            var result = default(string);
                            try
                            {
                                result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseData, settings.Value);
                                return result;
                            }
                            catch (Exception exception)
                            {
                                throw new Exception("Could not deserialise response body.", exception);
                            }
                        }

                        return default(string);
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }

        public async Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }


            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(baseUrl).Append("/api/Basket/{id}");
            urlBuilder.Replace("{id}", Uri.EscapeDataString(Convert.ToString(id, System.Globalization.CultureInfo.InvariantCulture)));

            var client = new HttpClient();
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("DELETE");

                    var url = urlBuilder.ToString();
                    request_.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                    var response = await client.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers = System.Linq.Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                        foreach (var item in response.Content.Headers)
                        {
                            headers[item.Key] = item.Value;
                        }

                        var status = ((int)response.StatusCode).ToString();
                        if (status == "204")
                        {
                            return;
                        }
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Dispose();
                        }

                    }
                }
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }

            }
        }

    }
}
