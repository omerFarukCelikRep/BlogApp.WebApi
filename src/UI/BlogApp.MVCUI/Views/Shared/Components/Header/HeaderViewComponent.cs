using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.Header;

[ViewComponent(Name ="Header")]
public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}
