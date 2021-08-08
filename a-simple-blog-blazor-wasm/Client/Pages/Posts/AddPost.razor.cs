using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ASimpleBlogStarter.Shared.Post;
using Microsoft.AspNetCore.Components;

namespace ASimpleBlogStarter.Client.Pages.Posts
{
    public class AddPostBase : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        protected Add.Command Command { get; set; } = new Add.Command();

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync("api/post", Command);
            NavigationManager.NavigateTo($"/{Command.Slug}");
        }
    }
}
