namespace BlogApp.Business.Constants;
public static class ServiceMessages
{
    public struct User
    {
        public const string Listed = "Members Listed";
        public const string Getted = "Member Getted";
        public const string UpdateSuccess = "Member Updated Successfully";
        public const string NotFound = "User Not Found";
    }

    public struct Article
    {
        public const string Listed = "Articles Listed";
        public const string NotFound = "Article Not Found";
        public const string Published = "Article Published Successfully";
    }

    public struct Comment
    {
        public const string Listed = "Comments Listed";
    }
}