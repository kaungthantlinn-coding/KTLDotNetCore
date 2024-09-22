
using KTLDotNetCore.ConsoleApp;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();


DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("Test Title", "Test Author", "Test Content");
//dapperExample.Update(2, "Test Title", "Test Author", "Test Content");
dapperExample.Delete(3);
Console.ReadKey();