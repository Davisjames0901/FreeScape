using FreeScape;
using FreeScape.Engine;
using FreeScape.Scenes;
using Microsoft.Extensions.DependencyInjection;

var collection = Bootstrapper.Bootstrap(new ServiceCollection());
using var provider = collection.BuildServiceProvider();
var game = provider.GetRequiredService<Game>();
game.Start<MainMenuScene>();