using ProgramacionWeb_1057719_Project.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace ProgramacionWeb_1057719_Project.Functions
{
    public static class APIServiceRols
    {
        public static int timeout = 30;
        public static string baseurl = "https://localhost:7073/api/Rols";

        public static async Task<IEnumerable<RolModel>> GetRols()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<RolModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<RolModel> PostRols(RolModel object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PostAsync($"{baseurl}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<RolModel>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<RolModel> GetRol(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.GetAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<RolModel>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<RolModel> PutRol (RolModel object_to_serialize, int id)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PutAsync($"{baseurl}/{id}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)

            {
                return JsonConvert.DeserializeObject<RolModel>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<RolModel> DeleteRol(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.DeleteAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<RolModel>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

    }
}
