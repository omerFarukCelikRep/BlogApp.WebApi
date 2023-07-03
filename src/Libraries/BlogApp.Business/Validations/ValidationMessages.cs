namespace BlogApp.Business.Validations;
public struct ValidationMessages
{
    public const string NotEmpty = "{0} boş bırakılamaz!"; 
    public const string Invalid = "Lütfen geçerli bir {0} giriniz!";
    public const string InvalidMaxLength = "{0} {1} karakterden fazla olamaz!";
    public const string InvalidMinLength = "{0} {1} karakterden az olamaz!";
    public const string NotMatch = "{0} eşleşmemektedir!";
}
