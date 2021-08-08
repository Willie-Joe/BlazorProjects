using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ASimpleBlogStarter.Shared.Post;
using Microsoft.AspNetCore.Components;

namespace ASimpleBlogStarter.Client.Pages.Posts
{
    public class EditBase : ComponentBase
    {
        [Inject] 
        public HttpClient Http { get; set; }

        [Parameter] 
        public int Id { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Update.Command Command { get; set; } = new Update.Command();
        public Get.Model Model { get; set; } = new Get.Model();

        protected override async Task OnInitializedAsync()
        {
            Model = await Http.GetFromJsonAsync<Get.Model>($"/api/Post/{Id}");

            Command = new Update.Command
            {
                Id = Model.Id,
                Body = Model.Body,
                Title = Model.Title
            };
        }

   

        protected async Task HandleValidSubmit()
        {
            await Http.PutAsJsonAsync("/api/Post", Command);
            NavigationManager.NavigateTo($"/{Model.Slug}");
        }

    }
}
