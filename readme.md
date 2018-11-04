# Application
https://www.codetoshow.com

# API

https://www.codetoshow.com/api/navne

https://www.codetoshow.com/api/navne?startsWith=a

https://www.codetoshow.com/api/navne?startsWith=a&sex=m

https://www.codetoshow.com/api/navne/1


### Install dependencies:
- dotnet restore

### To Run
- dotnet run

### To build to bin/Release/netcoreapp2.1/win10-x64/publish
- dotnet publish -c Release -r win10-x64 /p:TrimUnusedDependencies=true

### Create an appsettings.json with a connectionstring to db:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=mysql.secretlocation.com;database=secret_database;user=secretuser;pwd=totallysecret;"
  },
  "AllowedHosts": "*"
}
```

 ### Browser support:


