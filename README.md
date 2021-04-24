[![Codacy Badge](https://app.codacy.com/project/badge/Grade/fd78ba07e4bf4200b87658bbe4c3a3a3)](https://www.codacy.com/gh/BobMakhlin/XNews-backend/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=BobMakhlin/XNews-backend&amp;utm_campaign=Badge_Grade)

## How to setup
1. Set connection strings inside of `appsettings.Persistence.json` or set them using [user-secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#enable-secret-storage).
2. Create migrations:
* `dotnet ef migrations add Initial --project=Persistence.Primary --startup-project=Presentation.API --context=XNewsDbContext`
* `dotnet ef migrations add Initial --project=Persistence.Logging --startup-project=Presentation.API --context=XNewsLoggingDbContext`
3. Follow [this](https://stackoverflow.com/a/43687656/11285108) instruction to avoid problems when running migrations.
4. Run migrations:
* `dotnet ef database update --project=Persistence.Primary --startup-project=Presentation.API --context=XNewsDbContext`
* `dotnet ef database update --project=Persistence.Logging --startup-project=Presentation.API --context=XNewsLoggingDbContext`
