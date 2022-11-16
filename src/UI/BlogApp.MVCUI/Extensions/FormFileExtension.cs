namespace BlogApp.MVCUI.Extensions;

public static class FormFileExtension
{
    public static async Task<string> FileToString(this IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var fileAsByteArray = stream.ToArray();
        return Convert.ToBase64String(fileAsByteArray);
    }
}
