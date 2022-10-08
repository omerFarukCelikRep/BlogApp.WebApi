using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Models.Topics;
using BlogApp.MVCUI.Services.Interfaces;
using System.Net;
using IResult = BlogApp.Core.Utilities.Results.Abstract.IResult;

namespace BlogApp.MVCUI.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly HttpClient _httpClient;
    public ArticleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IResult> AddAsync(ArticleAddVM articleAddVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Articles", articleAddVM);
        if (responseMessage is null)
        {
            return new ErrorResult("İşlem Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<DataResult<ArticleAddVM>>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult($"{responseMessage.ReasonPhrase} - {response.Message}");
        }

        return new SuccessResult();
    }

    public async Task<IDataResult<List<ArticleUnpublishedListVM>>> GetAllUnpublished()
    {
        var result = await _httpClient.GetFromJsonAsync<DataResult<List<ArticleUnpublishedListVM>>>("/api/v1/Articles/Unpublished");

        return result;
    }

    public async Task<IDataResult<ArticleUnpublishedDetailsVM>> GetUnpublishedById(Guid articleId)
    {
        var result = await _httpClient.GetFromJsonAsync<DataResult<ArticleUnpublishedDetailsVM>>("/api/v1/Articles/Unpublished/" + articleId.ToString());

        return result;
    }
}
