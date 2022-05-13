﻿using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Entities.Dtos.Members;

namespace BlogApp.Business.Abstract;
public interface IMemberService
{
    Task<IDataResult<List<MemberDto>>> GetAllAsync();
}