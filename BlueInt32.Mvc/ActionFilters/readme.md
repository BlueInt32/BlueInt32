Les filtres d'action peuvent être ajoutés à 3 niveaux : 

- au niveau de l'action : 

```cs
[LogExceptionFilter]  
public ActionResult Index()
{
	return View();
}
```

- au niveau du controleur :

```cs
[LogExceptionFilter]
public class HomeController : Controller {...}
```

- au niveau de l'application entière, en MVC4 dans App_Start/FilterConfig.cs

```cs
public static void RegisterGlobalFilters(GlobalFilterCollection filters)
{
	filters.Add(new HandleErrorAttribute());
	filters.Add(new LogExceptionFilter());
}
```