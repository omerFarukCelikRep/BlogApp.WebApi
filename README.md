# BlogApp

---

# Technologies

    1. .Net 7
    2. EntityFrameworkCore
    3. Microsoft Identity
    4. Json Web Token
    5. Serilog
    6. Ms Sql Server
    7. FluentValidation
    8. AutoMapper
    9. Swagger



# Coding Standard

0. [Genel Standartlar](#0genel-standartlar)
1. [Dosyalar](#1dosyalar)
   1. [İsimlendirme](#11i̇simlendirme)
      1. [Db/VeriSet İsimlendirmesi](#111dbveriset-i̇simlendirmesi)
      2. [Db/Konfigurasyon İsimlendirmesi](#112konfigurasyon-i̇simlendirmesi)
      3. [Dto İsimlendirmesi](#113dto-i̇simlendirmesi)
      4. [View Model İsimlendirmesi](#114view-model-i̇simlendirmesi)
      5. [Repository İsimlendirmesi](#115repository-i̇simlendirmesi)
      6. [Servis İsimlendirmesi](#116servis-i̇simlendirmesi)
      7. [Interface İsimlendirmesi](#117interface-i̇simlendirmesi)
2. [Yorum Satırları](#2yorum-satırları)
   1. [Copyrights](#21-copyrights)
   2. [Method/Class/Prop vb. Özet (Summary)](#22-methodclassprop-vb-summary)
3. [Class ve Interface](#3class-ve-interface)
   1. [İsimlendirme](#31i̇simlendirme)
      1. [Interface İsimlendirme](#311interface-i̇simlendirme)
      2. [Db/VeriSet İsimlendirmesi](#312dbveriset-i̇simlendirmesi)
      3. [Db/Konfigurasyon İsimlendirmesi](#313konfigurasyon-i̇simlendirmesi)
      4. [Dto İsimlendirmesi](#314dto-i̇simlendirmesi)
      5. [View Model İsimlendirmesi](#315view-model-i̇simlendirmesi)
      6. [Repository İsimlendirmesi](#316repository-i̇simlendirmesi)
      7. [Servis İsimlendirmesi](#317servis-i̇simlendirmesi)
4. [Methodlar](#4methodlar)
   1. [İsimlendirme](#41i̇simlendirme)
      1. [Fiil (Verb)](#411fiil-verb)
      2. [Asenkron](#412asenkron)
      3. [Parametreler](#413parametreler)
      4. [Argüman (Parametre Gönderimi)](#414argüman-parametre-gönderimi)
5. [Değişkenler](#5değişkenler)
   1. [İsimlendirme](#51i̇simlendirme)
      1. [Tekil](#511tekil)
      2. [Lambda Expressions](#512lambda-expressions)
      3. [Çoğul](#513çoğul)

---

## 0.Genel Standartlar

    1.İsimlendirmeler İngilizce olmalı.
    2.Interface isimleri 'I' notasyonu ile isimlendirilmeli --> IInterface

## 1.Dosyalar

    Burada belirlenen standartlar C# dosyaları için geçerlidir.

### 1.1.İsimlendirme

    Dosya isimleri `.cs` uzantısına sahip dosyalar:
    1. İsimler 'PascalCase' olmalı. 

#### 1.1.1.Db/VeriSet İsimlendirmesi

    Veritabanı tablolarındaki yada veri setleri iç temsil eden dosyalar için geçerlidir.

##### Do ✅

```csharp
    Student.cs
```

##### Also, Do ✅

```csharp
    StudentQuestion.cs
```

##### Don't ❌

```csharp
    student.cs
```

##### Also, Don't ❌

```csharp
    studentQuestion.cs
```

##### Also, Don't ❌

```csharp
    Student_Question.cs
```

#### 1.1.2.Konfigurasyon İsimlendirmesi

    Veri tabanı tabloları ile entity sınıflarının konfigürasyon işlemlerini içeren sınıfları temsil eden dosyalar için geçerlidir.

##### Do ✅

```csharp
    StudentConfiguration.cs
```

##### Also, Do ✅

```csharp
    StudentQuestionConfiguration.cs
```

##### Don't ❌

```csharp
    Student_Question_Configuration.cs
```

##### Also, Don't ❌

```csharp
    studentQuestionConfiguration.cs
```

#### 1.1.3.Dto İsimlendirmesi

    Veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][işlem][Dto].cs

##### Do ✅

```csharp
    StudentCreateDto.cs  --> [Student][Create][Dto].cs
```

##### Also, Do ✅

```csharp
    StudentListDto.cs
```

##### Don't ❌

```csharp
    studentListDto.cs
```

##### Also, Don't ❌

```csharp
    Student_Create_Dto.cs
```

#### 1.1.4.View Model İsimlendirmesi

    View üzerinde veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][işlem][VM].cs

##### Do ✅

```csharp
    StudentCreateVM.cs  --> [Student][Create][VM].cs
```

##### Also, Do ✅

```csharp
    StudentListVM.cs
```

##### Don't ❌

```csharp
    studentListVM.cs
```

##### Also, Don't ❌

```csharp
    Student_Create_VM.cs
```

#### 1.1.5.Repository İsimlendirmesi

    Veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][Repository].cs

##### Do ✅

```csharp
    StudentRepository.cs
```

##### Also, Do ✅

```csharp
    StudentQuestionRepository.cs
```

##### Don't ❌

```csharp
    studentRepository.cs
```

##### Also, Don't ❌

```csharp
    Student_Repository.cs
```

#### 1.1.6.Servis İsimlendirmesi

    Uygulama içerisindeki servisleri yönetmek için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti / Servis][Service].cs

##### Do ✅

```csharp
    StudentService.cs
```

##### Also, Do ✅

```csharp
    MailService.cs
```

##### Also, Do ✅

```csharp
    AuthenticationService.cs
```

##### Don't ❌

```csharp
    studentService.cs
```

##### Also, Don't ❌

```csharp
    Student_Service.cs
```

#### 1.1.7.Interface İsimlendirmesi

    Uygulama içerisinde kullanılan  interface'leri temsil eden dosyalar için geçerlidir.

##### Do ✅

```csharp
    IStudentRepository.cs
```

##### Also, Do ✅

```csharp
    IMailService.cs
```

##### Also, Do ✅

```csharp
    IEntity.cs
```

##### Don't ❌

```csharp
    iStudentService.cs
```

##### Also, Don't ❌

```csharp
    StudentService.cs
```

##### Also, Don't ❌

```csharp
    IStudent_Service.cs
```

## 2.Yorum Satırları

    Yorumlar, programa etki etmeyen ancak kendimizin veya kodu inceleyen bir başkasının yapılan işlerin neden ve nasıl yapıldığını açıklamak için kullanılır.

### 2.1 Copyrights

##### Do ✅

```csharp
    // ---------------------------------------------------------------
    // Copyright (c) Coalition of the Good-Hearted Engineers
    // FREE TO USE TO CONNECT THE WORLD
    // ---------------------------------------------------------------
```

##### Don't ❌

```csharp
    //----------------------------------------------------------------
    // <copyright file="StudentService.cs" company="OpenSource">
    //      Copyright (C) Coalition of the Good-Hearted Engineers
    // </copyright>
    //----------------------------------------------------------------
```

##### Also, Don't ❌

```csharp
   /* 
    * ==============================================================
    * Copyright (c) Coalition of the Good-Hearted Engineers
    * FREE TO USE TO CONNECT THE WORLD
    * ==============================================================
    */
```

### 2.2.Method/Class/Prop vb. Özet (Summary)

    Method summary bir class, method, property, field vb. kod bloğunun yaptığı işi parametrelerin, sınıfların vb. elemanların neyi ifade ettiğini gösteren bilgi bloklarıdır.

    Visual studio'da method summary oluşturmak için method öncesinde '///'  yazıp method summary bloğu otomatik olarak oluşacaktır.

    ```csharp
    ///<summary>
    /// Buraya Metodun yaptığı ana iş gelecek. 
    ///</summary>
    ///<param name="Degisken1"> Değişken1 'i  neden istiyoruz. </param>
    ///<param name="Degisken2"> Değişken2 'yi neden istiyoruz. </param>
    ///<param name="Degisken3"> Değişken3 'ü  neden istiyoruz. </param>
    ///<param name="Degisken4"> Değişken4 'ü  neden istiyoruz. </param>
    ///<exception cref="System.OverflowException">
    /// Buraya eğer method bir exception barındırıyorsa onun koşullarını ekliyoruz
    ///</exception>
    ///<returns> Dönüş değerleri </returns>
    ```

## 3.Class ve Interface

    Burada belirlenen standartlar C# class ve interface dosyaları için geçerlidir.

### 3.1.İsimlendirme

#### 3.1.1.Interface İsimlendirme

##### Do ✅

```csharp
    IStudentRepository.cs
```

##### Also, Do ✅

```csharp
    IMailService.cs
```

##### Also, Do ✅

```csharp
    IEntity.cs
```

##### Don't ❌

```csharp
    iStudentService.cs
```

##### Also, Don't ❌

```csharp
    StudentService.cs
```

##### Also, Don't ❌

```csharp
    IStudent_Service.cs
```

#### 3.1.2.Db/VeriSet İsimlendirmesi

    Veritabanı tablolarındaki yada veri setleri iç temsil eden dosyalar için geçerlidir.

##### Do ✅

```csharp
    Student.cs
```

##### Also, Do ✅

```csharp
    StudentQuestion.cs
```

##### Don't ❌

```csharp
    student.cs
```

##### Also, Don't ❌

```csharp
    studentQuestion.cs
```

##### Also, Don't ❌

```csharp
    Student_Question.cs
```

#### 3.1.3.Konfigurasyon İsimlendirmesi

    Veri tabanı tabloları ile entity sınıflarının konfigürasyon işlemlerini içeren sınıfları temsil eden dosyalar için geçerlidir.

##### Do ✅

```csharp
    StudentConfiguration.cs
```

##### Also, Do ✅

```csharp
    StudentQuestionConfiguration.cs
```

##### Don't ❌

```csharp
    Student_Question_Configuration.cs
```

##### Also, Don't ❌

```csharp
    studentQuestionConfiguration.cs
```

#### 3.1.4.Dto İsimlendirmesi

    Veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][işlem][Dto].cs

##### Do ✅

```csharp
    StudentCreateDto.cs  --> [Student][Create][Dto].cs
```

##### Also, Do ✅

```csharp
    StudentListDto.cs
```

##### Don't ❌

```csharp
    studentListDto.cs
```

##### Also, Don't ❌

```csharp
    Student_Create_Dto.cs
```

#### 3.1.5.View Model İsimlendirmesi

    View üzerinde veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][işlem][VM].cs

##### Do ✅

```csharp
    StudentCreateVM.cs  --> [Student][Create][VM].cs
```

##### Also, Do ✅

```csharp
    StudentListVM.cs
```

##### Don't ❌

```csharp
    studentListVM.cs
```

##### Also, Don't ❌

```csharp
    Student_Create_VM.cs
```

#### 3.1.6.Repository İsimlendirmesi

    Veri transferi için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti][Repository].cs

##### Do ✅

```csharp
    StudentRepository.cs
```

##### Also, Do ✅

```csharp
    StudentQuestionRepository.cs
```

##### Don't ❌

```csharp
    studentRepository.cs
```

##### Also, Don't ❌

```csharp
    Student_Repository.cs
```

#### 3.1.7.Servis İsimlendirmesi

    Uygulama içerisindeki servisleri yönetmek için kullanılan sınıfları temsil eden dosyalar için geçerlidir.

        > [Veri Seti / Servis][Service].cs

##### Do ✅

```csharp
    StudentService.cs
```

##### Also, Do ✅

```csharp
    MailService.cs
```

##### Also, Do ✅

```csharp
    AuthenticationService.cs
```

##### Don't ❌

```csharp
    studentService.cs
```

##### Also, Don't ❌

```csharp
    Student_Service.cs
```

## 4.Methodlar

### 4.1.İsimlendirme

    Method isimleri:
    1. Methodun ne yaptığını özetleyecek şekilde olmalı.
    2. Net ve kısa olmalı
    3. Asenkron olan method isimleri 'Async' ifadesini barındırmalı

#### 4.1.1.Fiil (Verb)

    Method gerçekleştirdiği eylemi temsil eden fiili içermelidir.

##### Do ✅

```csharp
    public List<Student> GetAll()
    {
        ...
    }
```

##### Also, Do ✅

```csharp
    public Student Add()
    {
        ...
    }
```

##### Don't ❌

```csharp
    public List<Student> All()
    {
        ...
    }
```

##### Also, Don't ❌

```csharp
    public List<Student> getAll()
    {
        ...
    }
```

#### 4.1.2.Asenkron

    Asenkron methodlar isim sonuna ```Async``` ifadesini almalı ve ````Task``` yada ````ValueTask``` döndürmeli

##### Do ✅

```csharp
    public async Task<List<Student>> GetAllAsync()
    {
        ...
    }
```

##### Also, Do ✅

```csharp
    public Student Add()
    {
        ...
    }
```

##### Don't ❌

```csharp
    public async Task<List<Student>> GetAll()
    {
        ...
    }
```

#### 4.1.3.Parametreler

    Method parametreleri bir nesnenin hangi özelliğini ifade ettiğini atanacakları veya arama gibi herhangi bir eylem için kullanılacakları açık belirtmelidir.

##### Do ✅

```cs
    public async Task<Student> GetByNameAsync(string studentName)
    {
        ...
    }
```

##### Also, Do ✅

```cs
    public async Task<Student> GetByIdAsync(Guid studentId)
    {
        ...
    }
```

##### Also, Do ✅

```cs
    public async Task<Student> GetByClassroomIdAsync(Guid classroomId)
    {
        ...
    }
```

##### Don't ❌

```cs
    public async Task<Student> GetByStudentNameAsync(string text)
    {
        ...
    }
```

##### Also, Don't ❌

```csharp
public async Task<Student> GetByStudentNameAsync(string name)
{
    ...
}
```

#### 4.1.4.Argüman (Parametre Gönderimi)

    Bir methodu kullanırken, parametre isimleri, kısmen veya tamamen aktarılan değişkenlerle eşleşirse, parametre ismi kullanmanız gerekmez, aksi takdirde değişkenlerden önce parametre ismi belirtmeniz gerekir.

> Bir methodumuz olduğunu varsayalım:

```csharp
Student GetByNameAsync(string studentName);
```

##### Do ✅

```cs
string studentName = "Todd";
Student student = await GetStudentByNameAsync(studentName);
```

##### Also, Do ✅

```cs
    Student student = await GetByNameAsync(studentName: "Todd");
```

##### Don't ❌

```cs
Student student = await GetByNameAsync("Todd");
```

##### Also, Don't ❌

```cs
Student student = await GetByNameAsync(todd);
```

## 5.Değişkenler

### 5.1.İsimlendirme

    Değişken isimleri öz ve sahip olduğu veya potansiyel olarak tutacağı değeri temsil edecek şekilde isimlendirilmeli.

#### 5.1.1.Tekil

    Değişkenin tutacağı değer tekil bir değeri temsil edecek şekilde isimlendirilmeli.

##### Do ✅

```csharp
var student = new Student();
```

##### Also, Do ✅

```csharp
var hasStudent = await CheckNameAsync(studentName);
```

##### Don't ❌

```csharp
var studentModel = new Student();
```

##### Also, Don't ❌

```csharp
var studentObj = new Student();
```

#### 5.1.2.Lambda Expressions

##### Do ✅

```csharp
students.Where(student => student ... );
```

##### Don't ❌

```csharp
students.Where(s => s ... );
```

#### 5.1.3.Çoğul

    Değişkenin tutacağı değer çoğul değerleri temsil edecek şekilde isimlendirilmeli.

##### Do ✅

```csharp
var students = new List<Student>();
```

##### Don't ❌

```csharp
var studentList = new List<Student>();
```
