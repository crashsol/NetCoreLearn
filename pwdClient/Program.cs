using System;
using IdentityModel.Client;
using System.Net.Http;


namespace pwdClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 从元数据中发现客户端
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;

       
            //请求
            var tokenClient = new TokenClient(disco.TokenEndpoint, "pwdClient", "secret");
            //获取token
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("123", "123", "api").Result;//使用用户名密码


            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\r\n\r\n");


            //模拟请求API
            var http = new HttpClient();
            http.SetBearerToken(tokenResponse.AccessToken);  //设置请求 token
            var response = http.GetAsync("http://localhost:5001/api/values").Result;

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.Read();




            
        }
    }
}
