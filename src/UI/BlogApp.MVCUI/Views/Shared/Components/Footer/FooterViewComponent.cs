using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.Footer;

[ViewComponent(Name = "Footer")]
public class FooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}
