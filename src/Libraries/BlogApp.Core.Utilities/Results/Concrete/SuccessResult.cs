﻿namespace BlogApp.Core.Utilities.Results.Concrete;
public record SuccessResult : Result
{
    public SuccessResult() : base(true) { }

    public SuccessResult(string message) : base(true, message) { }
}
