--project BookStore.DataAccess/BookStore.DataAccess.csproj --startup-project CRUD_dotNet/CRUD_dotNet.csproj

Создать миграцию
dotnet ef migrations add ИМЯ_МИГРАЦИИ --project BookStore.DataAccess/BookStore.DataAccess.csproj --startup-project CRUD_dotNet/CRUD_dotNet.csproj

Апдейт миграции
dotnet ef database update --project BookStore.DataAccess/BookStore.DataAccess.csproj --startup-project CRUD_dotNet/CRUD_dotNet.csproj
