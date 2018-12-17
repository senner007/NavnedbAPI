# Application - Search and filter danish names
Demonstrating fast DOM manipulation without blocking the UI

https://www.codetoshow.com

# API

https://www.codetoshow.com/api/navne/Ole

https://www.codetoshow.com/api/navne

https://www.codetoshow.com/api/navne?skip=100&take=100

https://www.codetoshow.com/api/navne?startsWith=a

https://www.codetoshow.com/api/navne?startsWith=a&gender=m

https://www.codetoshow.com/api/navne/1


### Install dependencies:
- dotnet restore

### To Run
- dotnet run

### To build to bin/Release/netcoreapp2.1/win10-x64/publish
- dotnet publish -c Release -r win10-x64

### Fix for .NET Core 2.2 Build
  https://stackoverflow.com/questions/53730020/deploying-a-plain-asp-net-core-2-2-web-app-in-azure-using-web-deploy-is-throwing/53730715#53730715

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

### Db:
- navne_mysql.sql

 ### Browser support:


### Reading CSV Files:

```
using System;
namespace ParseCSV
{
    public abstract class NameAbstract : IName
    {
        public string Name { get; set; }
        public virtual string Gender { get; set; }
    }
    public interface IName
    {
       string Name { get; set; }
       string Gender { get; set; }
    }
    public class BoyName : NameAbstract
    {
        public override string Gender { get; set; } = "M";
    }
    public class GirlName : NameAbstract
    {
        public override string Gender { get; set; } = "F";
    }
    public class UnisexName : NameAbstract
    {
        public override string Gender { get; set; } = "MF";
    }

}
```

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ParseCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var boynames = ReadCSVFile<BoyName>("d:\\drenge_navne.csv");
         
            foreach (var n in boynames)
            {
                Console.WriteLine(n.Name + " " +  n.Gender);
            }
        }
        private static List<T> ReadCSVFile<T>(string path) where T : IName, new()
        {    
            return File.ReadAllLines(path)
                .Where(line => line.Length > 1)
                .Select(line => new T { Name = line })
                .ToList();
        }
    }
}
```