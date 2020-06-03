# SalesTaxesProblem
Resolution of problem of taxes calculation on items purchase

Pre requisites to change, build and execute this application:
  -.NET Core 2.1 SDK (download at https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.1.806-windows-x64-installer)
  
To run the application, launch the .bat file Execute, that will create the work directories on root and will push a sample file to be consumed by application to generate the receipt output message, that will be both shown on console and stored on a new file on an ouput path.

The application will consume the oldest file of the generated directory workdir/Input, so you can add other files after the first sample launch.

After getting content, file will be moved into folder workdir/Backup. 

If the content is not serializable, file will be moved into folder workdir/Error.

The generated output will be stored in a new file into folder workdir/Output. 


