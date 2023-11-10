# Generate Swagger File(yaml) via Command-Line

1. Install `Swashbuckle.AspNetCore.Cli`

   ```shell
   dotnet new tool-manifest
   dotnet tool install --version 6.5.0 Swashbuckle.AspNetCore.Cli
   ```

   ```shell
   dotnet swagger tofile --help
   ```

   ```shell
   dotnet swagger tofile --output [output] [startupassembly] [swaggerdoc]
   ```

   - [output] is the relative path where the Swagger JSON will be output to
   - [startupassembly] is the relative path to your application's startup assembly
   - [swaggerdoc] is the name of the swagger document you want to retrieve, as configured in your startup class

2. Update `Swashbuckle.AspNetCore` Package in `api.csproj`

   - from

     ```xml
     <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
     ```

   - to

     ```xml
     <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
     ```

3. Build Project

   ```shell
   dotnet build
   ```

4. Generate Swagger(yaml)

   ```shell
   dotnet swagger tofile --output api.yaml ./api/bin/Debug/net6.0/api.dll v1
   ```

---

## References

- [https://www.nuget.org/packages/Swashbuckle.AspNetCore.Cl](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Cl)
- [https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcorecli](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcorecli)
