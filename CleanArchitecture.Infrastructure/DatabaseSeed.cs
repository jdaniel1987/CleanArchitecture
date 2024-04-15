﻿using CleanArchitecture.Infrastructure.Models;

namespace CleanArchitecture.Infrastructure;

public static class DatabaseSeed
{
    public static void SeedData(DatabaseContext context)
    {
        //Seeding Consoles
        context.GamesConsoles.Add(new GamesConsole
        {
            Id = 1,
            Name = "PlayStation 4",
            Manufacturer = "Sony"
        });
        context.GamesConsoles.Add(new GamesConsole
        {
            Id = 2,
            Name = "PlayStation 5",
            Manufacturer = "Sony"
        });
        context.GamesConsoles.Add(new GamesConsole
        {
            Id = 3,
            Name = "Xbox Series X",
            Manufacturer = "Microsoft",
        });
        context.GamesConsoles.Add(new GamesConsole
        {
            Id = 4,
            Name = "Nintendo Switch",
            Manufacturer = "Nintendo"
        });

        //Seeding Games
        context.Games.Add(new Game
        {
            Id = 1,
            Name = "Final Fantasy VII Remake",
            Publisher = "Square Enix",
            GamesConsoleId = 1
        });
        context.Games.Add(new Game
        {
            Id = 2,
            Name = "Final Fantasy VII Remake Intergrade",
            Publisher = "Square Enix",
            GamesConsoleId = 2
        });
        context.Games.Add(new Game
        {
            Id = 3,
            Name = "Horizon Forbidden West",
            Publisher = "Sony Interactive Entertainment",
            GamesConsoleId = 2
        });
        context.Games.Add(new Game
        {
            Id = 4,
            Name = "The Legend of Zelda: Tears of the Kingdom",
            Publisher = "Nintendo",
            GamesConsoleId = 4
        });
        context.Games.Add(new Game
        {
            Id = 5,
            Name = "Xenoblade Chronicles 3",
            Publisher = "Monolith Soft",
            GamesConsoleId = 4
        });
        context.Games.Add(new Game
        {
            Id = 6,
            Name = "Halo Infinite",
            Publisher = "Xbox Game Studios",
            GamesConsoleId = 3
        });
        context.Games.Add(new Game
        {
            Id = 7,
            Name = "Crisis Core Final Fantasy VII Reunion",
            Publisher = "Square Enix",
            GamesConsoleId = 4
        });

        context.SaveChanges();
    }
}
