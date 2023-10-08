﻿using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.EventArgs;

namespace LavaSharp.LavaManager;
using DisCatSharp.Enums;
using DisCatSharp.Lavalink;

[EventHandler]
public class LavaDiscordEvents : ApplicationCommandsModule
{
    [Event]
    public static async Task VoiceStateUpdated(DiscordClient client, VoiceStateUpdateEventArgs e)
    {
        // check if bot got disconnected
        if (e.User.Id == client.CurrentUser.Id && e.After.Channel is null)
        {
            LavalinkExtension lava = client.GetLavalink();
            LavalinkSession? node = lava?.ConnectedSessions.First().Value;
            LavalinkGuildPlayer? player = node?.GetGuildPlayer(e.Guild);
            if (player is null)
            {
                return;
            }
            Console.WriteLine("Bot got disconnected");
            await LavaQueue.DisconnectAndReset(player);
        }
    }
}