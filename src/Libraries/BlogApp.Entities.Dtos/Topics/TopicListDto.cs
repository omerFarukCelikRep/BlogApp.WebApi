﻿namespace BlogApp.Entities.Dtos.Topics;
public class TopicListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
}