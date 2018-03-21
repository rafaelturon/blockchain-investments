using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blockchain.Investments.Api.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;
        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("callback")]
        [AllowAnonymous]
        public IActionResult CallBack(string state, string code, string authuser, string session_state, string prompt)
        {

            // build the request to validate the incoming code
            var clientID = "943502630298-rf4m7sn7dalce1iu7i0dsm2pdjpe7jou.apps.googleusercontent.com";                                   // from the Google API console, SHOULD BE IN A SAFE PLACE, NOT HERE!
            var clientSecret = "L40QOuL9znAebGHe6PvU1tqv";                           // from the Google API console, SHOULD BE IN A SAFE PLACE, NOT HERE!
            var redirectUri = "http://localhost:5000/api/identity/callback";   // the original url we sent must match what we original set as the callback
            var grantType = "authorization_code";

            // the url to send the POST request (from the google docs)
            var postUrl = "https://www.googleapis.com/oauth2/v4/token";

            // wrap parameters in a Form object
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", code),   // the code we got from the callback
                new KeyValuePair<string, string>("client_id", clientID),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("grant_type", grantType),
            });

            var resultContent = ProcessTokenRequest(postUrl, content);
/*
            // submit the request

            var client = new HttpClient();
            var result = client.PostAsync(postUrl, content);
            result.Wait();

            // get the result as a string
            var resultContent = result.Result.Content.ReadAsStringAsync();
            resultContent.Wait();

            // parse the result into an object
            var resultObject = new { access_token = "" };
            var json = JsonConvert.DeserializeAnonymousType(resultContent.Result, resultObject);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + json.access_token);
*/
            return new ObjectResult(code);
        }

        public static string ProcessTokenRequest(string postUrl, FormUrlEncodedContent content)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, postUrl);

            request.Content = content;
            var response = client.SendAsync(request).GetAwaiter().GetResult();

            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return result;
        }
    }
}
