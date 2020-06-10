
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext  -f

===== MVC =====

dotnet aspnet-codegenerator controller -name HomeworkController -actions -m Homework -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StudentHomeworkController -actions -m StudentHomework -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StudentCourseController -actions -m StudentCourse -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CoursesController -actions -m Course -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

===== API =====

dotnet aspnet-codegenerator controller -name HomeworkController -actions -m Homework -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name StudentHomeworkController -actions -m StudentHomework -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name StudentCourseController -actions -m StudentCourse -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CoursesController -actions -m Course -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f



dotnet ef database drop --project DAL.App.EF --startup-project WebApp --context AppDbContext
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp --context AppDbContext
dotnet ef database update --project DAL.App.EF --startup-project WebApp --context AppDbContext
