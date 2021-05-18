[![Codacy Badge](https://app.codacy.com/project/badge/Grade/fd78ba07e4bf4200b87658bbe4c3a3a3)](https://www.codacy.com/gh/BobMakhlin/XNews-backend/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=BobMakhlin/XNews-backend&amp;utm_campaign=Badge_Grade)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FBobMakhlin%2FXNews-backend.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FBobMakhlin%2FXNews-backend?ref=badge_shield)

## How to setup

1.  Set connection strings inside of `appsettings.Persistence.json` or set them using [user-secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0\&tabs=windows#enable-secret-storage).
2.  Run migrations:

*   `dotnet ef database update --project=Persistence.Primary --startup-project=Presentation.API --context=XNewsDbContext`
*   `dotnet ef database update --project=Persistence.Logging --startup-project=Presentation.API --context=XNewsLoggingDbContext`


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FBobMakhlin%2FXNews-backend.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FBobMakhlin%2FXNews-backend?ref=badge_large)