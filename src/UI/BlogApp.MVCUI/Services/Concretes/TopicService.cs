using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Topics;
using BlogApp.MVCUI.Services.Interfaces;
using System.Net;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.MVCUI.Services.Concretes;

public class TopicService : ITopicService
{
    private readonly HttpClient _httpClient;

    public TopicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<List<TopicListVM>>> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<DataResult<List<TopicListVM>>>("/api/v1/Topics");

        return result;
    }

    public async Task<IResult> AddAsync(TopicAddVM topicAddVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Topics", topicAddVM);
        if (responseMessage is null)
        {
            return new ErrorResult("Giriş Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<DataResult<TopicAddVM>>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult($"{responseMessage.ReasonPhrase} - {response.Message}");
        }

        return new SuccessResult();
    }
}
