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
   2. [Method/Class/Prop vb. Summary](#22-methodclassprop-vb-summary)
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
5. [Variables](5.%20Variables.md)

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

### 2.2 Method/Class/Prop vb. Summary

    Method summary bir class, method, property, field vb. kod bloğunun yaptığı işi parametrelerin, sınıfların vb. elemanların neyi ifade ettiğini gösteren bilgi bloklarıdır.

    Visual studio'da method summary oluşturmak için method öncesinde '///'  yazıp method summary bloğu otomatik olarak oluşacaktır.

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

### 4.Methodlar

#### 4.1.İsimlendirme

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
public async ValueTask<List<Student>> GetAllAsync()
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
public async ValueTask<List<Student>> GetAll()
{
	...
}
```

#### 1.0.2 Input Parameters
Input parameters should be explicit about what property of an object they will be assigned to, or will be used for any action such as search.
##### Do
```cs
public async ValueTask<Student> GetStudentByNameAsync(string studentName)
{
	...
}
```
##### Don't
```cs
public async ValueTask<Student> GetStudentByNameAsync(string text)
{
	...
}
```
##### Also, Don't
```cs
public async ValueTask<Student> GetStudentByNameAsync(string name)
{
	...
}
```
<br />

#### 1.0.3 Action Parameters
If your method is performing an action with a particular parameter specify it.
##### Do
```cs
public async ValueTask<Student> GetStudentByIdAsync(Guid studentId)
{
	...
}
```
##### Don't
```cs
public async ValueTask<Student> GetStudentAsync(Guid studentId)
{
	...
}
```
<br />

#### 1.0.4 Passing Parameters
When utilizing a method, if the input parameters aliases match the passed in variables in part or in full, then you don't have to use the aliases, otherwise you must specify your values with aliases.

Assume you have a method:
```csharp
Student GetStudentByNameAsync(string studentName);
```

##### Do
```cs
string studentName = "Todd";
Student student = await GetStudentByNameAsync(studentName);
```
##### Also, Do
```cs
Student student = await GetStudentByNameAsync(studentName: "Todd");
```

##### Also, Do
```cs
Student student = await GetStudentByNameAsync(toddName);
```

##### Don't
```cs
Student student = await GetStudentByNameAsync("Todd");
```

##### Don't
```cs
Student student = await GetStudentByNameAsync(todd);
```

<br /><br />

### 1.1 Organization
In general encapsulate multiple lines of the same logic into their own method, and keep your method at level 0 of details at all times.

#### 1.1.0 One-Liners
Any method that contains only one line of code should use fat arrows
##### Do
```cs
public List<Student> GetStudents() => this.storageBroker.GetStudents();
```
##### Don't
```cs
public List<Student> Students()
{
	return this.storageBroker.GetStudents();
}
```

If a one-liner method exceeds the length of 120 characters then break after the fat arrow with an extra tab for the new line.

##### Do
```cs
public async ValueTask<List<Student>> GetAllWashingtonSchoolsStudentsAsync() => 
	await this.storageBroker.GetStudentsAsync();
```

##### Don't
```cs
public async ValueTask<List<Student>> GetAllWashingtonSchoolsStudentsAsync() => await this.storageBroker.GetStudentsAsync();
```
<br />

#### 1.1.1 Returns
For multi-liner methods, take a new line between the method logic and the final return line (if any).
##### Do
```cs
public List<Student> GetStudents(){
	StudentsClient studentsApiClient = InitializeStudentApiClient();
	return studentsApiClient.GetStudents();
}
```

##### Don't
```cs
public List<Student> GetStudents(){
	StudentsClient studentsApiClient = InitializeStudentApiClient();
	return studentsApiClient.GetStudents();
}
```
<br />

#### 1.1.2 Multiple Calls
With mutliple method calls, if both calls are less than 120 characters then they may stack unless the final call is a method return, otherwise separate with a new line.
##### Do
```cs
public List<Student> GetStudents(){
	StudentsClient studentsApiClient = InitializeStudentApiClient();
	List<Student> students = studentsApiClient.GetStudents();
	return students; 
}
```

##### Don't
```cs
public List<Student> GetStudents(){
	StudentsClient studentsApiClient = InitializeStudentApiClient();
	List<Student> students = studentsApiClient.GetStudents();
	return students; 
}
```
##### Also, Do

```cs
public List<Student> GetStudents(){
	StudentsClient washingtonSchoolsStudentsApiClient = 
		await InitializeWashingtonSchoolsStudentsApiClientAsync();
	List<Student> students = studentsApiClient.GetStudents();
	return students; 
}
```
##### Don't

```cs
public List<Student> GetStudents(){
	StudentsClient washingtonSchoolsStudentsApiClient = 
		await InitializeWashingtonSchoolsStudentsApiClientAsync();
	List<Student> students = studentsApiClient.GetStudents();
	return students; 
}
```
<br />

#### 1.1.3 Declaration
A method declaration should not be longer than 120 characters.
##### Do
```cs
public async ValueTask<List<Student>> GetAllRegisteredWashgintonSchoolsStudentsAsync(
	StudentsQuery studentsQuery)
{
	...
}
```

##### Don't
```cs
public async ValueTask<List<Student>> GetAllRegisteredWashgintonSchoolsStudentsAsync(StudentsQuery studentsQuery)
{
	...
}
```
<br />

#### 1.1.4 Multiple Parameters
If you are passing multiple parameters, and the length of the method call is over 120 characters, you must break by the parameters, with **one** parameter on each line.
##### Do
```cs
List<Student> redmondHighStudents = await QueryAllWashingtonStudentsByScoreAndSchoolAsync(
	MinimumScore: 130,
	SchoolName: "Redmond High");
```

##### Don't
```cs
List<Student> redmondHighStudents = await QueryAllWashingtonStudentsByScoreAndSchoolAsync(
	MinimumScore: 130,SchoolName: "Redmond High");
```

#### 1.1.5 Chaining (Uglification/Beautification)
Some methods offer extensions to call other methods. For instance, you can call a `Select()` method after a `Where()` method. And so on until a full query is completed.

We will follow a process of Uglification Beautification. We uglify our code to beautify our view of a chain methods. Here's some examples:

##### Do
```csharp
	students.Where(student => student.Name is "Elbek")
		.Select(student => student.Name)
			.ToList();
```

##### Don't
```csharp
	students
	.Where(student => student.Name is "Elbek")
	.Select(student => student.Name)
	.ToList();
```

The first approach enforces simplifying and cutting the chaining short as more calls continues to uglify the code like this:

```csharp
	students.SomeMethod(...)
		.SomeOtherMethod(...)
			.SomeOtherMethod(...)
				.SomeOtherMethod(...)
					.SomeOtherMethod(...);
```
The uglification process forces breaking down the chains to smaller lists then processing it. The second approach (no uglification approach) may require additional cognitive resources to distinguish between a new statement and an existing one as follows:

```csharp
	student
	.Where(student => student.Name is "Elbek")
	.Select(student => student.Name)
	.OrderBy(student => student.Name)
	.ToList();
	ProcessStudents(students);
```
