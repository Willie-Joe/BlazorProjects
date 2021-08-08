using ASimpleBlogStarter.Shared.Post;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASimpleBlogStarter.Client.Pages.Posts
{
    public class PostBase : ComponentBase
    {
        [Inject] IHttpClientFactory HttpClientFactory { get; set; }

        [Parameter] public string Slug { get; set; }

        protected Search.Model Model { get; set; } = new Search.Model();

        protected override async Task OnInitializedAsync()
        {
            var http = HttpClientFactory.CreateClient("ASimpleBlogStarter.AnonymousAPI");
            Model = await http.GetFromJsonAsync<Search.Model>($"api/post/search?slug={Slug}");
        }
    }
}
