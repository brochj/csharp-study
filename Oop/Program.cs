using Oop.ContentContext;
using Oop.NotificationContext;
using Oop.SubscriptionContext;

var articles = new List<Article>();
articles.Add(new Article("Meu Título", "/meu-titulo"));
articles.Add(new Article("Artigo C#", "/artigo-csharp"));
articles.Add(new Article("Artigo .NET", "/artigo-dotnet"));

foreach (var item in articles)
{
    Console.WriteLine($"Id: {item.Id}");
    Console.WriteLine($"Title: {item.Title}");
    Console.WriteLine($"Url: {item.Url}");
    Console.WriteLine("--------------");
}

// ----------
var courses = new List<Course>();

var csharpCourse = new Course("Fundamentos C#", "fundamentos-csharp");
var oopCourse = new Course("Oop C#", "oop-csharp");

courses.Add(csharpCourse);
courses.Add(oopCourse);

oopCourse.AddNotification(new Notification("Course", "Message"));
// oopCourse.AddNotification(new Notification("Course", "Notification's message"));

// -----------
var careers = new List<Career>();
var careerDotnet = new Career("Especialista .NET", "pro-dotnet");

var careerItem1 = new CareerItem(1, "Comece por aqui", "", null);
var careerItem3 = new CareerItem(3, "Curso de C#", "", csharpCourse);
var careerItem2 = new CareerItem(2, "Comece por aqui", "", oopCourse);

careerDotnet.Items.Add(careerItem2);
careerDotnet.Items.Add(careerItem1);
careerDotnet.Items.Add(careerItem3);

careers.Add(careerDotnet);

foreach (var career in careers)
{
    Console.WriteLine(career.Title);
    foreach (var item in career.Items.OrderBy(x => x.Order))
    {
        Console.WriteLine($"{item.Order} - {item.Title}");
        Console.WriteLine($"Curso: {item.Course?.Title}");
        Console.WriteLine($"Nível: {item.Course?.Level}");

        foreach (var notification in item.Notifications)
        {
            Console.WriteLine($"Notificação: {notification.Property} - {notification.Message}");

        }
    }

}

var payPalSubscription = new PayPalSubscription();
var student = new Student();

student.CreateSubscription(payPalSubscription);