using Spectre.Console;
using Janice.Core.Services.Interfaces;

namespace Janice.CLI
{
    public class App
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public App(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        public void Run()
        {
            AnsiConsole.MarkupLine("[bold blue]Welcome to Janice CLI![/]");
            Login();
            // ... further CLI logic ...
        }

        private void Login()
        {
            while (true)
            {
                var username = AnsiConsole.Ask<string>("[green]Username:[/]");
                var password = AnsiConsole.Prompt(new TextPrompt<string>("[green]Password:[/]").Secret());
                var user = _loginService.LoginAsync(username, password).GetAwaiter().GetResult();
                if (user != null)
                {
                    AnsiConsole.MarkupLine($"[bold green]Login successful! Welcome, {user.UserName}.[/]");
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid credentials. Please try again.[/]");
                }
            }
        }
    }
}
