## How to setup
1. Create migrations:
* `dotnet ef migrations add Initial --project=Persistence.Primary --startup-project=Presentation.API --context=XNewsDbContext`
* `dotnet ef migrations add Initial --project=Persistence.Logging --startup-project=Presentation.API --context=XNewsLoggingDbContext`
2. Follow [this](https://stackoverflow.com/a/43687656/11285108) instruction to avoid problems when running migrations.
3. Run migrations:
* `dotnet ef database update --project=Persistence.Primary --startup-project=Presentation.API --context=XNewsDbContext`
* `dotnet ef database update --project=Persistence.Logging --startup-project=Presentation.API --context=XNewsLoggingDbContext`

### Where are connection strings
Connection strings you can find in `appsettings.Persistence.json`.
