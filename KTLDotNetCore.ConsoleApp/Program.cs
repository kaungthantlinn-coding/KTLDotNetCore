
using KTLDotNetCore.ConsoleApp;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();


DapperExample dapperExample = new DapperExample();
dapperExample.Read();

Console.ReadKey();