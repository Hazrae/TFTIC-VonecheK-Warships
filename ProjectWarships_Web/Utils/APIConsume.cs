using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Utils
{
    public class APIConsume : IAPIConsume
    {
        private Uri ui;
        public APIConsume(Uri uri)
        {
            ui = uri;
        }

        public void Post<T>(string action, T item)
        {
            HttpController http = new HttpController(ui);

            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage message = http.Client.PostAsync(action, content).Result)
            {
                message.EnsureSuccessStatusCode();
                if (!message.IsSuccessStatusCode) { throw new HttpRequestException(); }
            }
        }

        public T2 PostWithReturn<T,T2>(string action, T item)
        {
            HttpController http = new HttpController(ui);

            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage message = http.Client.PostAsync(action, content).Result)
            {
                message.EnsureSuccessStatusCode();
                if (!message.IsSuccessStatusCode) { throw new HttpRequestException(); }
                string json2 = message.Content.ReadAsStringAsync().Result;              
                return JsonConvert.DeserializeObject<T2>(json2);
            }
        }

        public T Get<T>(string action, int? id = null)
        {
            HttpController http = new HttpController(ui);

            HttpResponseMessage message = http.Client.GetAsync(action + id).Result;
            message.EnsureSuccessStatusCode();
            string json = message.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(json);
        }


        public void Delete(string action, int id)
        {
            HttpController http = new HttpController(ui);

            HttpResponseMessage message = http.Client.DeleteAsync(action + id).Result;
            message.EnsureSuccessStatusCode();
        }

        public void Put<T>(string action, T item)
        {
            HttpController http = new HttpController(ui);
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage message = http.Client.PutAsync(action, content).Result)
            {
                message.EnsureSuccessStatusCode();
                if (!message.IsSuccessStatusCode) { throw new HttpRequestException(); }
            }
        }

        public T2 PutWithReturn<T,T2>(string action, T item)
        {
            HttpController http = new HttpController(ui);
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage message = http.Client.PutAsync(action, content).Result)
            {
                message.EnsureSuccessStatusCode();
                if (!message.IsSuccessStatusCode) { throw new HttpRequestException(); }
                string json2 = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T2>(json2);
            }
        }
    }
}
