# C# Exmaple bot.

This is an example Discord bot using 
- Dicsord.net, Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.Logging
- Entityframework Core

The bot expects a token in one of the Iconfiguration soures
 - /config.json
 - Environment Variable
 - User Secrets

Personally I prefer user-secrets, either right click the project and find the option to open user secrets, or use the CLI tooling `dotnet user-secrets --help`

The bot sets up three slash commands
- /echo which takes in a string and echo that into chat.
- /usernote that takes a user mention/user id and returns the saved note for that user
- /set-user-note Takes a mention/userid and string, stores the string in a local databse for later retrival

